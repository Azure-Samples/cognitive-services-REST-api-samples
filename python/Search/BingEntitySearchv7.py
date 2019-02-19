import os, requests, json

subscriptionKey = 'put_your_key_here'
host = ''
path = ''
search_query = ''

def entitySearch():

    params = {
    }
	#the url to send the request
    constructed_url = host + path

    headers = {
        'Ocp-Apim-Subscription-Key': subscriptionKey
    }
    
    request = requests.get(constructed_url, headers=headers)
    response = request.json()

    print(json.dumps(response, sort_keys=True, indent=4, ensure_ascii=False, separators=(',', ': ')))

if __name__ = "__main__":
    entitySearch()






















# -*- coding: utf-8 -*-

import http.client, urllib.parse
import json

# **********************************************
# *** Update or verify the following values. ***
# **********************************************

# Replace the subscriptionKey string value with your valid subscription key.
subscriptionKey = 'ENTER KEY HERE'

host = 'api.cognitive.microsoft.com'
path = '/bing/v7.0/entities'

mkt = 'en-US'
query = 'italian restaurants near me'

params = '?mkt=' + mkt + '&q=' + urllib.parse.quote (query)

def get_suggestions ():
	headers = {'Ocp-Apim-Subscription-Key': subscriptionKey}
	conn = http.client.HTTPSConnection (host)
	conn.request ("GET", path + params, None, headers)
	response = conn.getresponse ()
	return response.read ()

result = get_suggestions ()
print (json.dumps(json.loads(result), indent=4))