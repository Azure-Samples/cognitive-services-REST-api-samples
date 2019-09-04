

'use strict';

let https = require ('https');

// **********************************************
// *** Update or verify the following values. ***
// **********************************************

// Add your Bing Entity Search subscription key to your environment variables.
let subscriptionKey = process.env['BING_ENTITY_SEARCH_SUBSCRIPTION_KEY']
// Add your Bing Entity Search endpoint to your environment variables.
let host = process.env['BING_ENTITY_SEARCH_ENDPOINT']
let path = '/bing/v7.0/entities';

let mkt = 'en-US';
let q = 'italian restaurant near me';

let query = '?mkt=' + mkt + '&q=' + encodeURI(q);

let response_handler = function (response) {
    let body = '';
    response.on ('data', function (d) {
        body += d;
    });
    response.on ('end', function () {
        let json = JSON.stringify(JSON.parse(body), null, '  ');
        console.log (json);
    });
    response.on ('error', function (e) {
        console.log ('Error: ' + e.message);
    });
};

let Search = function () {
	let request_params = {
		method : 'GET',
		hostname : host,
		path : path + query,
		headers : {
			'Ocp-Apim-Subscription-Key' : subscriptionKey,
		}
	};

	let req = https.request (request_params, response_handler);
	req.end ();
}

Search ();
