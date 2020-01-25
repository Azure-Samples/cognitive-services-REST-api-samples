#Copyright (c) Microsoft Corporation. All rights reserved.
#Licensed under the MIT License.

# -*- coding: utf-8 -*-

import json
import os
import requests
from pprint import pprint

'''
This sample uses the Bing Autosuggest API to check the spelling of query words and then suggests corrections.
Bing Spell Check API: https://docs.microsoft.com/en-us/rest/api/cognitiveservices-bingsearch/bing-autosuggest-api-v7-reference57855119bca1df1c647bc358
'''

# Add your Bing Autosuggest subscription key and endpoint to your environment variables.
subscription_key = os.environ['BING_AUTOSUGGEST_SUBSCRIPTION_KEY']
endpoint = os.environ['BING_AUTOSUGGEST_ENDPOINT'] + '/bing/v7.0/Suggestions/'

# Construct the request
mkt = 'en-US'
query = 'sail'
params = { 'q': query, 'mkt': mkt }
headers = { 'Ocp-Apim-Subscription-Key': subscription_key }

# Call the API
try:
    response = requests.get(endpoint, headers=headers, params=params)
    response.raise_for_status()

    print("\nHeaders:\n")
    print(response.headers)

    print("\nJSON Response:\n")
    pprint(response.json())
except Exception as ex:
    raise ex
