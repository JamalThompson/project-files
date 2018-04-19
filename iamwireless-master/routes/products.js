var express = require('express');
var router = express.Router();
var mongoose = require('mongoose');
var Product = require('../models/product.js');

//File Upload code with multer
var multer = require('multer');
var storage = multer.diskStorage({
  destination: function (req, file, cb) {
    cb(null, '../public/images/')
  },
  filename: function (req, file, cb) {
    cb(null, file.originalname)
  }
});

var upload = multer({ storage: storage });


/* GET ALL ProductS */
router.get('/', function(req, res, next) {
  Product.find(function (err, products) {

    if (err){
      console.log(err);
      return next(err);
    }
    res.json(products);
  });
});

/*router.get('/manufacturer', function(req, res, next) {
  Product.find().distinct('manufacturer',function(err, products) {

    if (err){
      console.log(err);
      return next(err);
    }
    res.json(products);
  });
});
*/

router.get('/manufacturer/:name', function(req, res, next) {
  Product.find({manufacturer:req.params.name}).distinct('model',function(err, products) {

    if (err){
      console.log(err);
      return next(err);
    }
    res.json(products);
  });
});

router.get('/category', function(req, res, next) {
  Product.find().distinct('category',function(err, products) {

    if (err){
      console.log(err);
      return next(err);
    }
    res.json(products);
  });
});
router.get('/manufacturer/carrier/:name', function(req, res, next) {
  Product.find({carrier:req.params.name}).distinct('carrier',function(err, products) {

    if (err){
      console.log(err);
      return next(err);
    }
    res.json(products);
  });
});

/* GET SINGLE Product BY ID */
router.get('/:id', function(req, res, next) {
  Product.findById(req.params.id, function (err, post) {
    if (err){
      console.log(err);
      return next(err);
    }
    res.json(post);
  });
});

/* SAVE Product */
router.post('/', upload.single('imageUpload'),function(req, res, next) {

  //only change image_path field if a new image was uploaded
  if( req.file != undefined ){
    req.body.image_path =  req.file.originalname;
  }

  Product.create(req.body, function (err, post) {
    if (err){
      console.log(err);
      return next(err);
    }
    res.json(post);
  });
});

/* UPDATE Product */
router.put('/:id', function(req, res, next) {
  Product.findByIdAndUpdate(req.params.id, req.body, function (err, post) {
    if (err){
      console.log(err);
      return next(err);
    }
    res.json(post);
  });
});

/* DELETE Product */
router.delete('/:id', function(req, res, next) {
  Product.findByIdAndRemove(req.params.id, req.body, function (err, post) {
    if (err){
      console.log(err);
      return next(err);
    }
    res.json(post);
  });
});

module.exports = router;
