#Copyright (c) Microsoft Corporation. All rights reserved.
#Licensed under the MIT License.

# -*- coding: utf-8 -*-

import http.client, urllib.parse, json

# **********************************************
# *** Update or verify the following values. ***
# **********************************************

# Add your Bing Search V7 subscription key to your environment variables.
subscriptionKey = os.environ['BING_SEARCH_V7_SUBSCRIPTION_KEY']

# Add your Bing Search V7 endpoint to your environment variables.
host = os.environ['BING_SEARCH_V7_ENDPOINT']
path = "/bing/v7.0/videos/search"

term = "kittens"

def BingVideoSearch(search):
    "Performs a Bing video search and returns the results."

    headers = {'Ocp-Apim-Subscription-Key': subscriptionKey}
    conn = http.client.HTTPSConnection(host)
    query = urllib.parse.quote(search)
    conn.request("GET", path + "?q=" + query, headers=headers)
    response = conn.getresponse()
    headers = [k + ": " + v for (k, v) in response.getheaders()
                   if k.startswith("BingAPIs-") or k.startswith("X-MSEdge-")]
    return headers, response.read().decode("utf8")

print('Searching videos for: ', term)

headers, result = BingVideoSearch(term)
print("\nRelevant HTTP Headers:\n")
print("\n".join(headers))
print("\nJSON Response:\n")
print(json.dumps(json.loads(result), indent=4))
