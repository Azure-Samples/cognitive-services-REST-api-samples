#Copyright (c) Microsoft Corporation. All rights reserved.
#Licensed under the MIT License.

import requests
import json

api_key = "enter-your-key-here"
example_text = "Hollo, wrld"

endpoint = "https://api.cognitive.microsoft.com/bing/v7.0/SpellCheck"

data = {'text': example_text}

params = {
    'mkt':'en-us',
    'mode':'proof'
    }

headers = {
    'Ocp-Apim-Subscription-Key': api_key,
    'Content-Type': 'application/x-www-form-urlencoded'
    }

response = requests.post(endpoint, headers=headers, params=params, data=data)
json_response = response.json()
print(json.dumps(json_response, indent=4))