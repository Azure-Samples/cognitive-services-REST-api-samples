"""Bing Visual Search upload image example"""

# Download and install Python at https://www.python.org/
# Run the following in a command console window
# pip3 install requests

import requests, json

# Add your Bing Search V7 endpoint to your environment variables.
BASE_URI = os.environ['BING_SEARCH_V7_ENDPOINT'] + '/bing/v7.0/images/visualsearch'

# Add your Bing Search V7 subscription key to your environment variables.
SUBSCRIPTION_KEY = os.environ['BING_SEARCH_V7_SUBSCRIPTION_KEY']

imagePath = 'your-image-path'

HEADERS = {'Ocp-Apim-Subscription-Key': SUBSCRIPTION_KEY}

file = {'image' : ('myfile', open(imagePath, 'rb'))}

def main():
    
    try:
        response = requests.post(BASE_URI, headers=HEADERS, files=file)
        response.raise_for_status()
        print_json(response.json())

    except Exception as ex:
        raise ex


def print_json(obj):
    """Print the object as json"""
    print(json.dumps(obj, sort_keys=True, indent=2, separators=(',', ': ')))



# Main execution
if __name__ == '__main__':
    main()
