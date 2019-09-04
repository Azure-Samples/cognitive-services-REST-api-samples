#Copyright (c) Microsoft Corporation. All rights reserved.
#Licensed under the MIT License.

import http.client, urllib.parse, json

text = 'Hollo, wrld!'

params = {'mkt': 'en-US', 'mode': 'proof', 'text': text}

# Add your Bing Spell Check subscription key to your environment variables.
key = os.environ['BING_SPELL_CHECK_SUBSCRIPTION_KEY']

# Add your Bing Spell Check endpoint to your environment variables.
host = os.environ['BING_SPELL_CHECK_ENDPOINT']
path = '/bing/v7.0/spellcheck'

headers = {'Ocp-Apim-Subscription-Key': key,
'Content-Type': 'application/x-www-form-urlencoded'}

# The headers in the following example 
# are optional but should be considered as required:
#
# X-MSEdge-ClientIP: 999.999.999.999  
# X-Search-Location: lat: +90.0000000000000;long: 00.0000000000000;re:100.000000000000
# X-MSEdge-ClientID: <Client ID from Previous Response Goes Here>

conn = http.client.HTTPSConnection(host)
params = urllib.parse.urlencode (params)
conn.request ("POST", path, params, headers)
response = conn.getresponse ()
print (response.read ())
