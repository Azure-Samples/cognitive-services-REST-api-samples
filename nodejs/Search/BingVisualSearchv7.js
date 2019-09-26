var request = require('request');
var FormData = require('form-data');
var fs = require('fs');

// Add your Bing Search V7 endpoint to your environment variables.
var baseUri = process.env['BING_SEARCH_V7_ENDPOINT'] + '/bing/v7.0/images/visualsearch';
// Add your Bing Search V7 subscription key to your environment variables.
var subscriptionKey = process.env['BING_SEARCH_V7_SUBSCRIPTION_KEY']
var imagePath = "path-to-your-image";

var form = new FormData();
form.append("image", fs.createReadStream(imagePath));

form.getLength(function(err, length){
  if (err) {
    return requestCallback(err);
  }

  var r = request.post(baseUri, requestCallback);
  r._form = form; 
  r.setHeader('Ocp-Apim-Subscription-Key', subscriptionKey);
});

function requestCallback(err, res, body) {
    console.log(JSON.stringify(JSON.parse(body), null, '  '))
}
