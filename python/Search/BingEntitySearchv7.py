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