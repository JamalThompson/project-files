using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace Last2TheParty
{
    class RssParse
    {

        // made <list> to place info within an array  //user 
        private Dictionary<string, string> EpisodeDescriptionDictionary = new Dictionary<string, string>();
        private Dictionary<string, string> EpisodeImageDictionary = new Dictionary<string, string>();
        //this rssURL feed contains title, description, and an iframe that pulls in another iframe that then
        //has a javascript function that does an ajax call to get the actual mp3.  That ajax call is the jsonEpisodes
        //string below.
        public string rssURL = "https://www.last2theparty.com/feed.xml";
        public string jsonEpisodes = "http://html5-player.libsyn.com/embed/list/id/518532/offset/0/size/";
        public string episodesParams = "/sorty_by_field/release_date/sort_by_direction/desc/category/";
        public string validURL = "http://ec2-54-202-108-54.us-west-2.compute.amazonaws.com/home/l2tp";
        
         
        public RssParse()
        {

        }

        /// <summary>
        /// Builds dictionary of title, description pairs for the episodes from the rssURL call.
        /// </summary>
        private void GetTitleDescription()
        {

            //part of xml
            XmlDocument rssXmlDoc = new XmlDocument();

            // Load the RSS file from the RSS URL
            rssXmlDoc.Load(rssURL);

            // Parse the Items in the RSS file
            XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");

            // Iterate through the items in the RSS file
            // loops through item 
            foreach (XmlNode rssNode in rssNodes)
            {
                // created epsoisode class for items from rss field to be placed in the function to be read by the looping function 
                //to grab what what places in the class or item.cs i.e the get and sets and group it together so that it can be used
                //all at once

                XmlNode rssSubNode = rssNode.SelectSingleNode("title");
                string title = rssSubNode != null ? rssSubNode.InnerText : "";

                rssSubNode = rssNode.SelectSingleNode("description");
                string description = rssSubNode != null ? rssSubNode.InnerText : "";

                int imgTagStartIndex = rssNode.InnerText.IndexOf("<img src=")+10;
                int imgTagEndIndex = rssNode.InnerText.IndexOf("/>", imgTagStartIndex);
                string imgSrc = rssNode.InnerText.Substring(imgTagStartIndex, imgTagEndIndex-imgTagStartIndex -1);

                //remove img tag from desc
                string replacing = "<img src=\"" + imgSrc + "\"/>";
                description = description.Replace(replacing,"");
               

                EpisodeDescriptionDictionary.Add(title.ToUpper(), description);
                EpisodeImageDictionary.Add(title.ToUpper(), imgSrc);

            }

        }

        /// <summary>
        /// Makes asyncronous call to internal url, then parses json into Episode objects and returns that list.
        /// </summary>
        public async Task<List<Episode>> GetEpisodes()
        {
            string json = await GetEpisodeJSON();
            var listEpisodes = JsonConvert.DeserializeObject<List<Episode>>(json);
            string description = "", imgSrc = "";

            GetTitleDescription();

            //add description
            foreach (Episode episode in listEpisodes)
            {
                //pair episodes with descriptions
                EpisodeDescriptionDictionary.TryGetValue(episode.item_title.ToUpper(), out description);
                episode.description = description;

                EpisodeImageDictionary.TryGetValue(episode.item_title.ToUpper(), out imgSrc);
                episode.thumbnail_url = imgSrc; 
            }

            return listEpisodes;
        }

        
        private async Task<String> GetEpisodeJSON(string numberOfEpisodes = "")
        {
            HttpResponseMessage responseMessage = null;
            string fifty = "50";//default number of episodes to retrieve.

            using(var client = new HttpClient())
            {
                //create a client based in webservice url 
                client.BaseAddress = new Uri(jsonEpisodes);

                client.DefaultRequestHeaders.Accept.Clear();
                //timeout to call the backend
                client.Timeout = TimeSpan.FromSeconds(10);
                //if request is not allowAnonymous

                //adding request header
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //to create a specific path for the call
                if (string.IsNullOrEmpty(numberOfEpisodes))
                {
                    responseMessage = await client.GetAsync(fifty+episodesParams);
                }
                else
                {
                    responseMessage = await client.GetAsync(numberOfEpisodes + episodesParams);
                }

                //check if the call succeed
                if (responseMessage.IsSuccessStatusCode)
                {
                    //get the response into string content & return it
                    var stringContent = await responseMessage.Content.ReadAsStringAsync();
                    return stringContent;

                }

                return null;
            }
           

        }


        public async Task<String> ValidateDownload()
        {
            HttpResponseMessage responseMessage = null;

            using (var client = new HttpClient())
            {
                //create a client based in webservice url 
                client.BaseAddress = new Uri(validURL);

                client.DefaultRequestHeaders.Accept.Clear();
                //timeout to call the backend
                client.Timeout = TimeSpan.FromSeconds(10);
                //if request is not allowAnonymous

                //adding request header
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //to create a specific path for the call

                responseMessage = await client.GetAsync("");
                
                //check if the call succeed
                if (responseMessage.IsSuccessStatusCode)
                {
                    //get the response into string content & return it
                    var stringContent = await responseMessage.Content.ReadAsStringAsync();
                    return stringContent;

                }

                return null;
            }


        }



    }
}