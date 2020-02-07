// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

'use strict';

let request = require('request')

/**
 * This sample uses the Computer Vision API to detect printed text in an image,
 * then returns the text and its properties, such as language, text angle, orientation.
 * Bounding boxes are calculated around portions of the text.
 *
 * Computer Vision API - v2 .1:
 * https: //westus.dev.cognitive.microsoft.com/docs/services/5cd27ec07268f6c679a3e641/operations/56f91f2e778daf14a499f20d
 */

// Add your Azure Computer Vision subscription key and endpoint to your environment variables. 
let subscriptionKey = process.env['COMPUTER_VISION_SUBSCRIPTION_KEY']
let endpoint = process.env['COMPUTER_VISION_ENDPOINT'] + '/vision/v2.1/ocr'

// An image with printed text; or replace with your own.
let url = 'https://raw.githubusercontent.com/Azure-Samples/cognitive-services-sample-data-files/master/ComputerVision/Images/printed_text.jpg'

// Request parameters.
const params = {
    'language': 'unk', // auto-detects language
    'detectOrientation': 'true',
};

// Options
let request_params = {
    method: 'POST',
    uri: endpoint,
    qs: params,
    body: '{"url": ' + '"' + url + '"}',
    headers: {
        'Content-Type': 'application/json',
        'Ocp-Apim-Subscription-Key': subscriptionKey
    }
}

// Make request
request(request_params, function (error, response, body) {
    console.error('error:', error)
    console.log('statusCode:', response && response.statusCode)
    console.log('original image: ' + url.substring(url.lastIndexOf('/') + 1))
    console.log()
    console.log(JSON.stringify(JSON.parse(body), null, 2))
})
