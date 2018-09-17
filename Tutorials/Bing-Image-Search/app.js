const http = require('http');
const express = require("express");
const app = express();
var port = 8000;

app.use(express.static("public"));

app.listen(port, function () {
    console.log('Starting the Node.js server for this sample. Navigate to http://localhost:'+port+'/ to view the webpage.');
});