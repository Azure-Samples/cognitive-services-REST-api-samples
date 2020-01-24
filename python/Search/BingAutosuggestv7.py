#Copyright (c) Microsoft Corporation. All rights reserved.
#Licensed under the MIT License.

# -*- coding: utf-8 -*-

'''
This sample uses the Bing Autosuggest API to check the spelling of query words and then suggests corrections.
Bing Spell Check API: https://docs.microsoft.com/en-us/rest/api/cognitiveservices-bingsearch/bing-autosuggest-api-v7-reference57855119bca1df1c647bc358
'''

import http.client
import json
import os
import urllib.parse

# Add your Bing Autosuggest subscription key to your environment variables.
subscriptionKey = os.environ['BING_AUTOSUGGEST_SUBSCRIPTION_KEY']

# Add your Bing Autosuggest endpoint to your environment variables.
host = os.environ['BING_AUTOSUGGEST_ENDPOINT']
host = host.replace('https://', '')
path = '/bing/v7.0/Suggestions'

mkt = 'en-US'
query = 'sail'

params = '?mkt=' + mkt + '&q=' + query

def get_suggestions ():
    "Gets Autosuggest results for a query and returns the information."

    headers = {'Ocp-Apim-Subscription-Key': subscriptionKey}
    conn = http.client.HTTPSConnection(host)
    conn.request ("GET", path + params, None, headers)
    response = conn.getresponse ()
    return response.read ()

result = get_suggestions ()
print (json.dumps(json.loads(result), indent=4))
