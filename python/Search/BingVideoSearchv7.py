#Copyright (c) Microsoft Corporation. All rights reserved.
#Licensed under the MIT License.

# -*- coding: utf-8 -*-

import http.client
import json
import os
import urllib.parse

'''
This sample makes a call to the Bing Video Search API with a topic query and returns relevant video links with data.
Documentation: https: // docs.microsoft.com/en-us/azure/cognitive-services/bing-web-search/
'''

# Add your Bing Search V7 subscription key and endpoint to your environment variables.
subscriptionKey = os.environ['BING_SEARCH_V7_SUBSCRIPTION_KEY']
host = os.environ['BING_SEARCH_V7_ENDPOINT']
host = host.replace('https://', '')
path = "/bing/v7.0/videos/search"

# Search query
term = "kittens"

# Construct a request
headers = {'Ocp-Apim-Subscription-Key': subscriptionKey}
conn = http.client.HTTPSConnection(host)
query = urllib.parse.quote(term)

# Call the API
print('Searching videos for: ', term)
conn.request("GET", path + "?q=" + query, headers=headers)
response = conn.getresponse()
headers = [k + ": " + v for (k, v) in response.getheaders()
           if k.startswith("BingAPIs-") or k.startswith("X-MSEdge-")]

# Print results
print("\nRelevant HTTP Headers:\n")
print("\n".join(headers))
print("\nJSON Response:\n")
print(json.dumps(json.loads(response.read()), indent=4))
