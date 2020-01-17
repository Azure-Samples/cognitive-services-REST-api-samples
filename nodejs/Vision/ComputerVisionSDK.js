// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

'use strict';

let https = require('https');

// This sample recognizes printed text using the OCR method.
// Add a valid Azure Computer Vision endpoint to your environment variables.
// Example: https://northeurope.api.cognitive.microsoft.com/ 
let endpoint = process.env['COMPUTER_VISION_ENDPOINT']

// Add a valid Azure Computer Vision subscription key to your environment variables. 
// Also note v5 and v7 require separate subscription keys. 
let key = process.env['COMPUTER_VISION_SUBSCRIPTION_KEY'];

// Use this url image with text, or replace with your own 
let url = 'https://raw.githubusercontent.com/Azure-Samples/cognitive-services-sample-data-files/master/ComputerVision/Images/printed_text.jpg';
let detectOrientation = false;

const ComputerVisionClient = require('azure-cognitiveservices-computervision');
const CognitiveServicesCredentials = require('ms-rest-azure').CognitiveServicesCredentials;

let credentials = new CognitiveServicesCredentials(key);
let client = new ComputerVisionClient(credentials, endpoint);

client.recognizePrintedText(detectOrientation, url)
    .then(ocrResult => {
        printOcrResult(ocrResult);
    });

function printOcrResult(ocrResult) {
    console.log('OcrResult')
    console.log(`language: ${ocrResult.language}`)
    console.log(`textAngle: ${ocrResult.textAngle}`)
    console.log(`orientation: ${ocrResult.orientation}`)

    ocrResult.regions.forEach((r, ri) => {
        console.log(`\tRegion ${ri} - boundingBox: ${r.boundingBox}`)

        r.lines.forEach((l, li) => {
            console.log(`\t\tLine ${li} - boundingBox: ${l.boundingBox}`)

            l.words.forEach((w, wi) => {
                console.log(`\t\t\tWord ${wi} - boundingBox: ${w.boundingBox}`)
                console.log(`\t\t\ttext: ${w.text}`)
            })

        });

    });
}
