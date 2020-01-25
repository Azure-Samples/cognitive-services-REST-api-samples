# Download and install Python at https://www.python.org/ 
# Run the following in a command console window: pip3 install requests

import json 
import os
from pprint import pprint
import requests

'''
This sample uses the Bing Visual Search API with a local, query image and returns several web links
and data of the exact image and/or similar images.
'''

# Add your Bing Search V7 subscriptionKey and endpoint to your environment variables.
endpoint = os.environ['BING_SEARCH_V7_ENDPOINT'] + '/bing/v7.0/images/visualsearch'
subscription_key = os.environ['BING_SEARCH_V7_SUBSCRIPTION_KEY']

image_path = 'MY-IMAGE' # for example: my_image.jpg

# Construct the request
headers = {'Ocp-Apim-Subscription-Key': subscription_key}
file = {'image' : ('MY-IMAGE', open(image_path, 'rb'))} # MY-IMAGE is the name of the image file (no extention)
    
# Call the API
try:
    response = requests.post(endpoint, headers=headers, files=file)
    response.raise_for_status()

    print("\nHeaders:\n")
    print(response.headers)

    print("\nJSON Response:\n")
    pprint(response.json())
except Exception as ex:
    raise ex
