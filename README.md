# MSQBot-API
[![License: AGPL 3.0](https://img.shields.io/badge/License-AGPL%203.0-brightgreen)](../master/LICENSE)

An API to acces and manage the MSQBot datas 

## Project notes
This project is a "training" project in .NetCore 6 and C# 9 and I'm always looking for new idea and improvement.

## Requirement 
This API is not really made to be available to anyone so if you want to run it you'll need a atabase with the following tables:

![image](https://user-images.githubusercontent.com/34239560/147969558-99025289-8097-4605-9917-2b1d873925be.png)


## Setup
To setup the api you need to have the following appsetting configuration
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "MSQBotDb": "server=YOUR_DATABASE_SERVER;database=DATABASE_NAME;user=DATABASE_USER;password=DATABASE_PASSWORD;"
  }
}
```

## MSQBot universe

I call the MSQBot universe all the application revolving around my discord bot. 

You can find the other application on my [GitHub](https://github.com/MaximeMohandi) feel free to take a look


## ROADMAP

- in memomry database test
- add interface on all services 
- add imageProcessor class to use in the image scrapper service 