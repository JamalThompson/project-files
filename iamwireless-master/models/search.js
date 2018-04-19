var mongoose = require('mongoose');

var SearchSchema = new mongoose.Schema({

  url: String,
  keywords: String,
  description: String

});

module.exports = mongoose.model('Search', SearchSchema);
