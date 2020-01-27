import json
import os
from pprint import pprint
import requests
import urllib.parse

'''
This sample uses the Bing Entity Search v7 to search for restaurants and return details about it.
Bing Entity Search API: https://westus2.dev.cognitive.microsoft.com/docs/services/7a3fb374be374859a823b79fd938cc65/operations/52069701a465405ab3286f82
'''

# Add your Bing Entity Search subscription key and endpoint to your environment variables.
subscription_key = os.environ['BING_ENTITY_SEARCH_SUBSCRIPTION_KEY']
endpoint = os.environ['BING_ENTITY_SEARCH_ENDPOINT'] + '/bing/v7.0/entities'

# Entity you want to find
query = 'italian restaurants near me'

# Construct the request
mkt = 'en-US'
params = 'mkt=' + mkt + '&q=' + urllib.parse.quote(query)
headers = {'Ocp-Apim-Subscription-Key': subscription_key}

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
pprint(json.loads(response.read())) 
