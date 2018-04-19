var express = require('express');
var router = express.Router();
var mongoose = require('mongoose');
var Search = require('../models/search.js');


/* GET ALL search */
router.get('/', function(req, res, next) {
  Search.find(function (err, search) {

    if (err){
      console.log(err);
      return next(err);
    }
    res.json(search);
  });
});


router.post('/', function(req, res, next) {
  Search.create(req.body, function (err, post) {
    if (err){
      console.log(err);
      return next(err);
    }
    res.json(post);
  });
});

/* UPDATE search */
router.put('/:id', function(req, res, next) {
  Search.findByIdAndUpdate(req.params.id, req.body, function (err, post) {
    if (err){
      console.log(err);
      return next(err);
    }
    res.json(post);
  });
});


module.exports = router;
