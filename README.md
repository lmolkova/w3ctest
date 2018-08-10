# w3ctest

``` bash
curl -vX POST http://localhost:49624/api/forward -d @send.json --header "Content-Type: application/json"
```

``` json
{
    "url": "http://lmolkova-servicea.azurewebsites.net/api/forward",
    "arguments": {
        "url": "http://dynatrace-servicea.azurewebsites.net/api/forward",
        "arguments": {
            "url": "http://lmolkova-serviceb.azurewebsites.net/api/forward",
            "arguments": {}
        }
    }
}
```

More complex example

``` json
{
  "actions": [
    {
      "url": "http://localhost:49624/",
      "arguments": {
        "url": "http://localhost:49624/",
		"arguments": {
		  "url": "http://localhost:49624/",
		  "arguments": {
			"url": "http://localhost:49624/"
		  }
		}
      }
    },
    {
      "sleep": 10000
    },
    {
      "url": "http://localhost:49624/",
      "arguments": {
        "url": "http://localhost:49624/"
      }
    }
  ]
}
```
