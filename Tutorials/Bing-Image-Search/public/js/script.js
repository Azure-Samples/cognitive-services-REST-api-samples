
// cookie names for data we store
// YOUR API KEY DOES NOT GO IN THIS CODE; don't paste it in.
API_KEY_COOKIE   = "bing-search-api-key";
CLIENT_ID_COOKIE = "bing-search-client-id";

// Bing Search API endpoint
BING_ENDPOINT = "https://api.cognitive.microsoft.com/bing/v7.0/images/search";

// Various browsers differ in their support for persistent storage by local
// HTML files (IE won't use localStorage, but Chrome won't use cookies). So
// use localStorage if we can, otherwise use cookies.

try {
    localStorage.getItem;   // try localStorage

    window.retrieveValue = function (name) {
        return localStorage.getItem(name) || "";
    }
    window.storeValue = function(name, value) {
        localStorage.setItem(name, value);
    }
} catch (e) {
    window.retrieveValue = function (name) {
        var cookies = document.cookie.split(";");
        for (var i = 0; i < cookies.length; i++) {
            var keyvalue = cookies[i].split("=");
            if (keyvalue[0].trim() === name) return keyvalue[1];
        }
        return "";
    }
    window.storeValue = function (name, value) {
        var expiry = new Date();
        expiry.setFullYear(expiry.getFullYear() + 1);
        document.cookie = name + "=" + value.trim() + "; expires=" + expiry.toUTCString();
    }
}

// get stored API subscription key, or prompt if it's not found
function getSubscriptionKey() {
    var key = retrieveValue(API_KEY_COOKIE);
    while (key.length !== 32) {
        key = prompt("Enter Bing Search API subscription key:", "").trim();
    }
    // always set the cookie in order to update the expiration date
    storeValue(API_KEY_COOKIE, key);
    return key;
}

// invalidate stored API subscription key so user will be prompted again
function invalidateSubscriptionKey() {
    storeValue(API_KEY_COOKIE, "");
}

// escape text for use in HTML
function escape(text) {
    return text.replace(/&/g, "&amp;").replace(/</g, "&lt;").
        replace(/'/g, "&apos;").replace(/"/g, "&quot;");
}

// get the host portion of a URL, strpping out search result formatting and www too
function getHost(url) {
    return url.replace(/<\/?b>/g, "").replace(/^https?:\/\//, "").split("/")[0].replace(/^www\./, "");
}

// format plain text for display as an HTML <pre> element
function preFormat(text) {
    text = "" + text;
    return "<pre>" + text.replace(/&/g, "&amp;").replace(/</g, "&lt;") + "</pre>"
}

// put HTML markup into a <div> and reveal it
function showDiv(id, html) {
    var content = document.getElementById("_" + id)
    if (content) content.innerHTML = html;
    var wrapper = document.getElementById(id);
    if (wrapper) wrapper.style.display = html.trim() ? "block" : "none";
}

// hides the specified <div>s
function hideDivs() {
    for (var i = 0; i < arguments.length; i++) {
        var element = document.getElementById(arguments[i])
        if (element) element.style.display = "none";
    }
}

// render functions for various types of search results
searchItemRenderers = { 
    images: function (item, index, count) {
        var height = 120;
        var width = Math.max(Math.round(height * item.thumbnail.width / item.thumbnail.height), 120);
        var html = [];
        if (index === 0) html.push("<p class='images'>");
        var title = escape(item.name) + "\n" + getHost(item.hostPageDisplayUrl);
        html.push("<p class='images' style='max-width: " + width + "px'>");
        html.push("<img src='"+ item.thumbnailUrl + "&h=" + height + "&w=" + width + 
            "' height=" + height + " width=" + width + "'>");
        html.push("<br>");
        html.push("<nobr><a href='" + item.contentUrl + "'>Image</a> - ");
        html.push("<a href='" + item.hostPageUrl + "'>Page</a></nobr><br>");
        html.push(title.replace("\n", " (").replace(/([a-z0-9])\.([a-z0-9])/g, "$1.<wbr>$2") + ")</p>");
        return html.join("");
    },
    relatedSearches: function(item) {
        var html = [];
        html.push("<p class='relatedSearches'>");
        html.push("<a href='#' onclick='return doRelatedSearch(&quot;" + 
            escape(item.text) + "&quot;)'>");
        html.push(item.displayText + "</a>");
        return html.join("");
    }
}


// render image search results
function renderImageResults(items) {
    var len = items.length;
    var html = [];
    if (!len) {
        showDiv("noresults", "No results.");
        hideDivs("paging1", "paging2");
        return "";
    }
    for (var i = 0; i < len; i++) {
        html.push(searchItemRenderers.images(items[i], i, len));
    }
    return html.join("\n\n");
}

// render related items
function renderRelatedItems(items) {
    var len = items.length;
    var html = [];
    for (var i = 0; i < len; i++) {
        html.push(searchItemRenderers.relatedSearches(items[i], i, len));
    }
    return html.join("\n\n");
}

// render the search results given the parsed JSON response
function renderSearchResults(results) {

    // add Prev / Next links with result count
    var pagingLinks = renderPagingLinks(results);
    showDiv("paging1", pagingLinks);
    showDiv("paging2", pagingLinks);
    
    showDiv("results", renderImageResults(results.value));
    if (results.relatedSearches)
        showDiv("sidebar", renderRelatedItems(results.relatedSearches));
}

function renderErrorMessage(message) {
    showDiv("error", preFormat(message));
    showDiv("noresults", "No results.");
}

// handle Bing search request results
function handleBingResponse() {
    hideDivs("noresults");

    var json = this.responseText.trim();
    var jsobj = {};

    // try to parse JSON results
    try {
        if (json.length) jsobj = JSON.parse(json);
    } catch(e) {
        renderErrorMessage("Invalid JSON response");
    }

    // show raw JSON and HTTP request
    showDiv("json", preFormat(JSON.stringify(jsobj, null, 2)));
    showDiv("http", preFormat("GET " + this.responseURL + "\n\nStatus: " + this.status + " " + 
        this.statusText + "\n" + this.getAllResponseHeaders()));

    // if HTTP response is 200 OK, try to render search results
    if (this.status === 200) {
        var clientid = this.getResponseHeader("X-MSEdge-ClientID");
        if (clientid) retrieveValue(CLIENT_ID_COOKIE, clientid);
        if (json.length) {
            if (jsobj._type === "Images") {
                if (jsobj.nextOffset) document.forms.bing.nextoffset.value = jsobj.nextOffset;
                renderSearchResults(jsobj);
            } else {
                renderErrorMessage("No search results in JSON response");
            }
        } else {
            renderErrorMessage("Empty response (are you sending too many requests too quickly?)");
        }
    }

    // Any other HTTP response is an error
    else {
        // 401 is unauthorized; force re-prompt for API key for next request
        if (this.status === 401) invalidateSubscriptionKey();

        // some error responses don't have a top-level errors object, so gin one up
        var errors = jsobj.errors || [jsobj];
        var errmsg = [];

        // display HTTP status code
        errmsg.push("HTTP Status " + this.status + " " + this.statusText + "\n");

        // add all fields from all error responses
        for (var i = 0; i < errors.length; i++) {
            if (i) errmsg.push("\n");
            for (var k in errors[i]) errmsg.push(k + ": " + errors[i][k]);
        }

        // also display Bing Trace ID if it isn't blocked by CORS
        var traceid = this.getResponseHeader("BingAPIs-TraceId");
        if (traceid) errmsg.push("\nTrace ID " + traceid);

        // and display the error message
        renderErrorMessage(errmsg.join("\n"));
    }
}

// perform a search given query, options string, and API key
function bingImageSearch(query, options, key) {

    // scroll to top of window
    window.scrollTo(0, 0);
    if (!query.trim().length) return false;     // empty query, do nothing

    showDiv("noresults", "Working. Please wait.");
    hideDivs("results", "related", "_json", "_http", "paging1", "paging2", "error");

    var request = new XMLHttpRequest();
    var queryurl = BING_ENDPOINT + "?q=" + encodeURIComponent(query) + "&" + options;

    // open the request
    try {
        request.open("GET", queryurl);
    } 
    catch (e) {
        renderErrorMessage("Bad request (invalid URL)\n" + queryurl);
        return false;
    }

    // add request headers
    request.setRequestHeader("Ocp-Apim-Subscription-Key", key);
    request.setRequestHeader("Accept", "application/json");
    var clientid = retrieveValue(CLIENT_ID_COOKIE);
    if (clientid) request.setRequestHeader("X-MSEdge-ClientID", clientid);
    
    // event handler for successful response
    request.addEventListener("load", handleBingResponse);
    
    // event handler for erorrs
    request.addEventListener("error", function() {
        renderErrorMessage("Error completing request");
    });

    // event handler for aborted request
    request.addEventListener("abort", function() {
        renderErrorMessage("Request aborted");
    });

    // send the request
    request.send();
    return false;
}

// build query options from the HTML form
function bingSearchOptions(form) {

    var options = [];
    options.push("mkt=" + form.where.value);
    options.push("SafeSearch=" + (form.safe.checked ? "strict" : "off"));
    if (form.when.value.length) options.push("freshness=" + form.when.value);
    var aspect = "all";
    for (var i = 0; i < form.aspect.length; i++) {
        if (form.aspect[i].checked) {
            aspect = form.aspect[i].value;
            break;
        }
    }
    options.push("aspect=" + aspect);
    if (form.color.value) options.push("color=" + form.color.value);
    options.push("count=" + form.count.value);
    options.push("offset=" + form.offset.value);
    return options.join("&");
}

// toggle display of a div (used by JSON/HTTP expandos)
function toggleDisplay(id) {

    var element = document.getElementById(id);
    if (element) {
        var display = element.style.display;
        if (display === "none") {
            element.style.display = "block";
            window.scrollBy(0, 200);
        } else {
            element.style.display = "none";
        }
    }
    return false;
}

// perform a related search (used by related search links)
function doRelatedSearch(query) {
    var bing = document.forms.bing;
    bing.query.value = query;
    return newBingImageSearch(bing);
}

// generate the HTML for paging links (prev/next)
function renderPagingLinks(results) {

    var html = [];
    var bing = document.forms.bing;
    var offset = parseInt(bing.offset.value, 10);
    var count = parseInt(bing.count.value, 10);
    html.push("<p class='paging'><i>Results " + (offset + 1) + " to " + (offset + count));
    html.push(" of about " + results.totalEstimatedMatches + ".</i> ");
    html.push("<a href='#' onclick='return doPrevSearchPage()'>Prev</a> | ");
    html.push("<a href='#' onclick='return doNextSearchPage()'>Next</a>");
    return html.join("");
}

// go to the next page (used by next page link)
function doNextSearchPage() {

    var bing = document.forms.bing;
    var query = bing.query.value;
    var offset = parseInt(bing.offset.value, 10);
    var stack = JSON.parse(bing.stack.value);
    stack.push(parseInt(bing.offset.value, 10));
    bing.stack.value = JSON.stringify(stack);
    bing.offset.value = bing.nextoffset.value;
    return bingImageSearch(query, bingSearchOptions(bing), getSubscriptionKey());
}

// go to the previous page (used by previous page link)
function doPrevSearchPage() {

    var bing = document.forms.bing;
    var query = bing.query.value;
    var stack = JSON.parse(bing.stack.value);
    if (stack.length) {
        var offset = stack.pop();
        var count = parseInt(bing.count.value, 10);
        bing.stack.value = JSON.stringify(stack);
        bing.offset.value = offset;
        return bingImageSearch(query, bingSearchOptions(bing), getSubscriptionKey());
    }
    alert("You're already at the beginning!");
    return false;
}

function newBingImageSearch(form) {
    form.offset.value = "0";
    form.stack.value = "[]";
    return bingImageSearch(form.query.value, bingSearchOptions(form), getSubscriptionKey());
}