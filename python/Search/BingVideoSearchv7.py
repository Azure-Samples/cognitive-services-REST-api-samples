# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License.

# -*- coding: utf-8 -*-

import json
import os
from pprint import pprint
import requests

'''
This sample makes a call to the Bing Video Search API with a topic query and returns relevant video with data.
Documentation: https: // docs.microsoft.com/en-us/azure/cognitive-services/bing-web-search/
'''

# Add your Bing Search V7 subscription key and endpoint to your environment variables.
subscriptionKey = os.environ['BING_SEARCH_V7_SUBSCRIPTION_KEY']
host = os.environ['BING_SEARCH_V7_ENDPOINT']
path = "/bing/v7.0/videos/search"

# Search query
query = "kittens"

# Construct a request
headers = {
    'Content-Type': 'application/json',
    'Ocp-Apim-Subscription-Key': subscriptionKey
    }
params = { "q": query }

# Call the API
response = requests.get(host + path, headers=headers, params=params)
response.raise_for_status()

# Print results
print("\nHeaders:\n")
print(response.headers)

print("\nJSON Response:\n")
pprint(response.json())
