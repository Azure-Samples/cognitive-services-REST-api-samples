# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License.

# -*- coding: utf-8 -*-

import http.client
import urllib.parse
import json
import os
from pprint import pprint

'''
This sample makes a call to the Bing Image Search API with a text query and returns relevant images with data.
Documentation: https: // docs.microsoft.com/en-us/azure/cognitive-services/bing-web-search/
'''

# Add your Bing Search V7 subscription key and endpoint to your environment variables.
subscriptionKey = os.environ['BING_SEARCH_V7_SUBSCRIPTION_KEY']
host = os.environ['BING_SEARCH_V7_ENDPOINT']
host = host.replace('https://', '')
path = "/bing/v7.0/images/search"

# Query to search for
query = "puppies"

# Construct a request
headers = {'Ocp-Apim-Subscription-Key': subscriptionKey}
conn = http.client.HTTPSConnection(host)
query_search = urllib.parse.quote(query)
conn.request("GET", path + "?q=" + query_search, headers=headers)

# Print response
response = conn.getresponse()
headers = [k + ": " + v for (k, v) in response.getheaders()
                if k.startswith("BingAPIs-") or k.startswith("X-MSEdge-")]

print('Searching images for: ', query)

print("\nRelevant HTTP Headers:\n")
print("\n".join(headers))
print("\nJSON Response:\n")
pprint(json.loads(response.read()))
