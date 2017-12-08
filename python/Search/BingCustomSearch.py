#Copyright (c) Microsoft Corporation. All rights reserved.
#Licensed under the MIT License.

# You may need the below as well
#pip install pipenv
#pipenv install requests

import json
import requests

subscriptionKey = "YOUR-SUBSCRIPTION-KEY"
customConfigId = "YOUR-CUSTOM-CONFIG-ID"
searchTerm = "microsoft"

url = 'https://api.cognitive.microsoft.com/bingcustomsearch/v7.0/search?q=' + searchTerm + '&customconfig=' + customConfigId
r = requests.get(url, headers={'Ocp-Apim-Subscription-Key': subscriptionKey})
print(r.text)
