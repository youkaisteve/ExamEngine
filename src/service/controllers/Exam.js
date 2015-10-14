var express = require('express');
var router = express.Router();

module.exports = function (app) {
    app.use("/exam", router);
};

router.get("/", function (req, res, next) {
    res.send("Welcome to exam!");
});