chrome.webRequest.onAuthRequired.addListener(function(details){
     console.log("chrome.webRequest.onAuthRequired event has fired");
     return {
             authCredentials: {username: "00092385060", password: "axl123!!suframa"}
         };
 },
 {urls:["<all_urls>"]},
 ['blocking']);