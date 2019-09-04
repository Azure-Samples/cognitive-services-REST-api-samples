//Copyright (c) Microsoft Corporation. All rights reserved.
//Licensed under the MIT License.

'use strict';

let https = require ('https');

// **********************************************
// *** Update or verify the following values. ***
// **********************************************

// Add your Bing Autosuggest subscription key to your environment variables.
let subscriptionKey = process.env['BING_AUTOSUGGEST_SUBSCRIPTION_KEY']
// Add your Bing Autosuggest endpoint to your environment variables.
let host = process.env['BING_AUTOSUGGEST_endpoint']
let path = '/bing/v7.0/Suggestions';

let mkt = 'en-US';
let query = 'sail';

let params = '?mkt=' + mkt + '&q=' + query;

let response_handler = function (response) {
    let body = '';
    response.on ('data', function (d) {
        body += d;
    });
    response.on ('end', function () {
        let body_ = JSON.parse (body);
        let body__ = JSON.stringify (body_, null, '  ');
        console.log (body__);
    });
    response.on ('error', function (e) {
        console.log ('Error: ' + e.message);
    });
};

let get_suggestions = function () {
    let request_params = {
        method : 'GET',
        hostname : host,
        path : path + params,
        headers : {
            'Ocp-Apim-Subscription-Key' : subscriptionKey,
        }
    };

    let req = https.request (request_params, response_handler);
    req.end ();
}

get_suggestions ();
