/**
 * Created by kai.you on 2015/10/14.
 */

var express = require('express');
var glob = require('glob');
var cookieParser = require('cookie-parser');
var bodyParser = require('body-parser');
var methodOverride = require('method-override');
var compression = require('compression');
var expressValidator = require('express-validator');
var timeout = require('connect-timeout');

var config = require("../config/development");

var app = express();

app.disable('x-powered-by');
app.disable('etag');
app.enable('trust proxy');

app.use(timeout('60s'));

app.use(compression());
app.use(bodyParser.json({limit: '5mb'}));
app.use(bodyParser.urlencoded({limit: '5mb'}));
app.use(expressValidator());
app.use(bodyParser.urlencoded({extended: true}));
app.use(cookieParser());
app.use(methodOverride());

app.use(function(req, res, next){
    res.header("Access-Control-Allow-Origin", "*");
    res.header("access-control-allow-methods", "GET, POST");
    res.header("Access-Control-Allow-Headers", "user-authorize");
    next();
});

var controllers = glob.sync(config.root + '/service/controllers/**/*.js');
controllers.forEach(function (controller) {
    require(controller)(app);
});

app.listen(config.servicePort, function () {
    console.log('Express server listening on port 4001');
});