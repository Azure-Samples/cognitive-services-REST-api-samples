// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

'use strict';

let request = require('request')

/**
 * This sample uses the Face API to detect faces from a remote image.
 *
 * Face API - V1 .0:
 * https: //westus.dev.cognitive.microsoft.com/docs/services/563879b61984550e40cbbe8d/operations/563879b61984550f30395236
 */

// Add your Azure Computer Vision subscription key and endpoint to your environment variables. 
let subscriptionKey = process.env['FACE_SUBSCRIPTION_KEY']
let endpoint = process.env['FACE_ENDPOINT'] + '/face/v1.0/detect'

// A remote image with several faces; you may replace with your own URL.
let url = 'https://raw.githubusercontent.com/Azure-Samples/cognitive-services-sample-data-files/master/ComputerVision/Images/faces.jpg'

// Request parameters.
const params = {
    'returnFaceId': 'true',
    'returnFaceLandmarks': 'false',
    'returnFaceAttributes': 'age,gender,headPose,smile,facialHair,glasses,' +
    'emotion,hair,makeup,occlusion,accessories,blur,exposure,noise'
}

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
