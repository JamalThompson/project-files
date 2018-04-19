var express = require('express');
var path = require('path');
var favicon = require('serve-favicon');
var logger = require('morgan');
var bodyParser = require('body-parser');
var cookieParser = require('cookie-parser');
var cors = require('cors');
var passport = require('passport');
var routesApi = require('./api/routes/index');
var webhook = require('./routes/webhook');
var products = require('./routes/products');
var search = require('./routes/searches');
var app = express();

require('./models/db');
require('./api/config/passport');


app.use(logger('dev'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({'extended':'false'}));
app.use(cookieParser());
app.use(cors());
app.use(express.static(path.join(__dirname, 'dist')));
app.use('/', express.static(path.join(__dirname, 'dist')));
app.use('/images', express.static(path.resolve(__dirname+'/public/images/')));
app.use(passport.initialize());

app.engine('html', require('ejs').renderFile);
app.set('view engine', 'html');

//routes
app.use('/webhook', webhook);
app.use('/products', products);
app.use('/search', search);


// [SH] Use the API routes when path starts with /api
app.use('/api', routesApi);
app.use(function(req, res){
  res.sendFile(path.resolve(__dirname+'/dist/index.html'));
});


// catch 404 and forward to error handler
app.use(function(req, res, next) {
  var err = new Error('Not Found');
  err.status = 404;
  next(err);
});

// error handler
app.use(function(err, req, res, next) {
  // set locals, only providing error in development
  //res.locals.message = err.message;
  //res.locals.error = req.app.get('env') === 'development' ? err : {};

  // render the error page
  res.status(err.status || 500);
  res.send(err.message);
});

app.use(function(req, res, next) {
//set headers to allow cross origin request.
  res.header("Access-Control-Allow-Origin", "*");
  res.header('Access-Control-Allow-Methods', 'PUT, GET, POST, DELETE, OPTIONS');
  res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
  next();
});

var mongoose = require('mongoose');
mongoose.Promise = require('bluebird');
mongoose.connect('mongodb://localhost/iamwireless',{ promiseLibrary: require('bluebird') })
  .then(() =>  console.log('connection succesful'))
  .catch((err) => console.error(err));



module.exports = app;
