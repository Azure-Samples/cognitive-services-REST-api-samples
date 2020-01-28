# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License.

import json
import requests
import os
from pprint import pprint

'''
This sample makes a call to the Face API with a URL image query
to detect faces and features in an image.
Face API: https://westus.dev.cognitive.microsoft.com/docs/services/563879b61984550e40cbbe8d/operations/563879b61984550f30395236
'''

# Add your Face subscription key and endpoint to your environment variables.
subscription_key = os.environ['FACE_SUBSCRIPTION_KEY']
endpoint = os.environ['FACE_ENDPOINT'] + '/face/v1.0/detect'

# Request headers.
headers = {
    'Content-Type': 'application/json',
    'Ocp-Apim-Subscription-Key': subscription_key,
}

# Request parameters.
params = {
    'returnFaceId': 'true',
    'returnFaceLandmarks': 'false',
    'returnFaceAttributes': 'age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise',
}

# The URL of a JPEG image of faces to analyze.
body = {'url': 'https://raw.githubusercontent.com/Azure-Samples/cognitive-services-sample-data-files/master/Face/images/test-image-person-group.jpg'}

try:
    # Call API. 
    response = requests.post(endpoint, headers=headers, params=params, json=body)
    response.raise_for_status()

    print("\nHeaders:\n")
    print(response.headers)

    print("\nJSON Response:\n")
    pprint(response.json())

except Exception as e:
    print('Error:')
    print(e)
