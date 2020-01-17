'use strict';

const fetch = require('node-fetch');
const fs = require('fs');
const util = require('util');
const delay = require('delay');

const readFile = util.promisify(fs.readFile);

// This sample uses the Recognize Text API call from Azure Computer Vision
// This call can recognize printed or handwritten text, just change the image and mode to specify.
// Computer Vision API - v2.0:
// https://westus.dev.cognitive.microsoft.com/docs/services/5adf991815e1060e6355ad44/operations/587f2c6a154055056008f200

let key = process.env['COMPUTER_VISION_SUBSCRIPTION_KEY'];
let baseUrl= process.env['COMPUTER_VISION_ENDPOINT']
let mode = 'Printed'; // Printed or Handwritten depending on what we want to detect

// Use a local file downloaded from here:
// https://github.com/Azure-Samples/cognitive-services-sample-data-files/tree/master/ComputerVision/Images
// Or add your own image to your project folder
let file = 'printed_text.jpg' // local

// Get the binary data of an image
async function readImageByPath(imagePath) {
    const imageBinary = await readFile(imagePath);
    const fileData = imageBinary.toString('hex');
    const result = [];
    for (let i = 0; i < fileData.length; i += 2) {
        result.push(parseInt(`${fileData[i]}${fileData[i + 1]}`, 16))
    }
    return Buffer.from(result);
}

async function checkAnalyzeImageJob(operationLocationUrl) {
    return new Promise(async (resolve, reject) => {
        try {
            const response = await fetch(operationLocationUrl, {
                method: 'GET',
                headers: {
                "Content-Type": "application/json",
                "Ocp-Apim-Subscription-Key": key
                }
            })

            const json = await response.json();
            return resolve(json);
        } catch (e) {
            return reject(e);
        }
    })
}

async function submitAnalyzeImageJob(imageBinary) {
    return new Promise(async (resolve, reject) => {
        const url = baseUrl + "/vision/v2.0/recognizeText" + "?" + `mode=${mode}`;

        try {
            const response = await fetch(url, {
                method: 'POST',
                headers: {
                    "Content-Type": "application/octet-stream",
                    "Ocp-Apim-Subscription-Key": key
                },
                body: imageBinary
            })

            if (response.headers.has('operation-location')) {
                return resolve(response.headers.get('operation-location'));
            } else {
                return reject(response.headers);
            }
        } catch (e) { 
            return reject(e);
        }
    });
}
  
async function analyzeImage(imageBinary) {
    return new Promise(async (resolve, reject) => {
        const jobLocation = await submitAnalyzeImageJob(imageBinary);
        let isDone = false;

        while (!isDone) {
            const result = await checkAnalyzeImageJob(jobLocation);
            if (result.status == 'NotStarted') {
                console.log('Recognition is not started yet');
                isDone = false;
                await delay(1000);
            } else if (result.status == 'Succeeded') {
                return resolve(result.recognitionResult);
            } else if (result.status == 'Running') {
                console.log('Recognition is running, awaiting result');
                isDone = false;
                await delay(1000);
            } else {
                return reject('Unknown status: ' + JSON.stringify(result));
            }
        }

        return reject('SHOULD NOT REACH');
    });
}

const start = async () => {
    const imageData = await readImageByPath(file);
    const result = await analyzeImage(imageData);
    console.log(result);
};

start();
