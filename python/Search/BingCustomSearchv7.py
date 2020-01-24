# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License.

# You may need the below as well
# pip install pipenv
# pipenv install requests
# <importsAndVars>
import json
import os
from pprint import pprint
import requests 

'''
This sample uses the Bing Custom Search API to search for a query topic and get back user-controlled web page results.
Bing Custom Search API: https://docs.microsoft.com/en-us/rest/api/cognitiveservices-bingsearch/bing-custom-search-api-v7-reference 
'''

# Add your Bing Custom Search subscription key and endpoint to your environment variables.
# Your endpoint will have the form: https://<your-custom-subdomain>.cognitiveservices.azure.com
subscriptionKey = os.environ['BING_CUSTOM_SEARCH_SUBSCRIPTION_KEY']
endpoint = os.environ['BING_CUSTOM_SEARCH_ENDPOINT']
customConfigId = os.environ["BING_CUSTOM_CONFIG"]  # you can also use "1"
searchTerm = "microsoft"
# </importsAndVars>
# <url>
# Add your Bing Custom Search endpoint to your environment variables.
url = endpoint + "/bingcustomsearch/v7.0/search?q=" + searchTerm + "&customconfig=" + customConfigId
# </url>
# <request>
r = requests.get(url, headers={'Ocp-Apim-Subscription-Key': subscriptionKey})
pprint(json.loads(r.text))
# </request>
