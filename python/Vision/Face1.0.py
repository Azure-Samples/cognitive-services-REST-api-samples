# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License.

########### Python 3.6 #############
import http.client, urllib.request, urllib.parse, urllib.error, base64, requests, json

###############################################
#### Update or verify the following values. ###
###############################################

# Replace the subscription_key string value with your valid subscription key.
subscription_key = 'Enter key here'

# Replace or verify the region.
#
# You must use the same region in your REST API call as you used to obtain your subscription keys.
# For example, if you obtained your subscription keys from the westus region, replace 
# "westcentralus" in the URI below with "westus".
#
# NOTE: Free trial subscription keys are generated in the westcentralus region, so if you are using
# a free trial subscription key, you should not need to change this region.
uri_base = 'https://westcentralus.api.cognitive.microsoft.com'

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

# Body. The URL of a JPEG image to analyze.
body = {'url': 'https://upload.wikimedia.org/wikipedia/commons/c/c3/RH_Louise_Lillian_Gish.jpg'}

try:
    # Execute the REST API call and get the response.
    response = requests.request('POST', uri_base + '/face/v1.0/detect', json=body, data=None, headers=headers, params=params)

    print ('Response:')
    parsed = json.loads(response.text)
    print (json.dumps(parsed, sort_keys=True, indent=2))

except Exception as e:
    print('Error:')
    print(e)

####################################
