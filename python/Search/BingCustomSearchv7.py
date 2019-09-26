#Copyright (c) Microsoft Corporation. All rights reserved.
#Licensed under the MIT License.

# You may need the below as well
#pip install pipenv
#pipenv install requests

import json
import requests

# Add your Bing Custom Search subscription key to your environment variables.
subscriptionKey = os.environ['BING_CUSTOM_SEARCH_SUBSCRIPTION_KEY']
customConfigId = "YOUR-CUSTOM-CONFIG-ID"
searchTerm = "microsoft"

# Add your Bing Custom Search endpoint to your environment variables.
url = os.environ['BING_CUSTOM_SEARCH_ENDPOINT'] + "/bingcustomsearch/v7.0/search?q=' + searchTerm + '&customconfig=' + customConfigId
r = requests.get(url, headers={'Ocp-Apim-Subscription-Key': subscriptionKey})
print(r.text)
