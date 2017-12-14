#Copyright (c) Microsoft Corporation. All rights reserved.
#Licensed under the MIT License.

import http.client, urllib.parse, json

text = 'Hollo, wrld!'

params = {'mkt': 'en-US', 'mode': 'proof', 'text': text}

# NOTE: Replace this example key with a valid subscription key.
key = 'enter key here'

host = 'api.cognitive.microsoft.com'
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
