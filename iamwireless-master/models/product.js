var mongoose = require('mongoose');

var ProductSchema = new mongoose.Schema({

  category: String,
  carrier: String,
  manufacturer: String,
  model: String,
  description: String,
  price: Number,
  image_path: String

});

module.exports = mongoose.model('Product', ProductSchema);
