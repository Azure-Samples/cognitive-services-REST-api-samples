require 'net/https'
require 'uri'
require 'json'

# Replace with a valid subscription key from your Azure account.
accessKey = "enter key here"
uri  = "https://api.cognitive.microsoft.com"
path = "/bing/v7.0/search"

term = "Microsoft Cognitive Services"

# Validate the subscription key.
if accessKey.length != 32 then
    puts "Invalid Bing Search API subscription key!"
    puts "Please paste yours into the source code."
    abort
end
# Construct the endpoint uri.
uri = URI(uri + path + "?q=" + URI.escape(term))

puts "Searching the Web for: " + term

# Create the request.
request = Net::HTTP::Get.new(uri)
request['Ocp-Apim-Subscription-Key'] = accessKey

# Get the response.
response = Net::HTTP.start(uri.host, uri.port, :use_ssl => uri.scheme == 'https') do |http|
    http.request(request)
end

puts "\nRelevant Headers:\n\n"
response.each_header do |key, value|
    # Header names are lower-cased.
    if key.start_with?("bingapis-") or key.start_with?("x-msedge-") then
        puts key + ": " + value
    end
end

# Print the response.
puts "\nJSON Response:\n\n"
puts JSON::pretty_generate(JSON(response.body))
