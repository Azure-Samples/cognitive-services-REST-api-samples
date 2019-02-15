require 'net/https'
require 'uri'
require 'json'

# **********************************************
# *** Update or verify the following values. ***
# **********************************************

# Replace the subscriptionKey string value with your valid subscription key.
subscriptionKey = 'enter key here'

host = 'https://api.cognitive.microsoft.com'
path = '/bing/v7.0/Suggestions'

mkt = 'en-US'
query = 'sail'

params = '?mkt=' + mkt + '&q=' + query
uri = URI (host + path + params)

request = Net::HTTP::Get.new(uri)
request['Ocp-Apim-Subscription-Key'] = subscriptionKey

response = Net::HTTP.start(uri.host, uri.port, :use_ssl => uri.scheme == 'https') do |http|
    http.request (request)
end

puts JSON::pretty_generate (JSON (response.body))