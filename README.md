# ToTheMoon

## Description

A small website to select your preferred coin and display the coin price details.

My goal was to address the requirements as best while keeping it somewhat simple but also including a few tricks.

For fault tolerance I included Polly for transient errors. I added Railway Oriented Programming for error handling for both user and expected errors. This makes the controllers look pretty sweet. To increase performance and scale out better, I cached the Cointree result with Memcache and I tried to tuck away the json parsing in a typed HttpClient. I've used Vue and it's event handling to give a smoother user experience and Bootstrap to not look terrible.

The project was built from the ground up with the default AspNet Web and Vue templates using Vscode on Linux.

Code: https://github.com/tamaw/ToTheMoon

Website: https://tothemoon.z26.web.core.windows.net/

## Run

To run from the code. From the root directory.

1. `cd frontend; npm serve`
2. `cd ../backend; dotnet run`
3. open browser to http://localhost:8080/

## Features

- User can select a preferred coin which is saved to their Session (15m)
- Upon selection, the prices update update from Cointree API
- You can press the refresh button to get the latest prices
- Upon hitting the refresh button you will see the difference from the last price

## Major Technologies

- Bootstrap 4.5
- Vue
  - axios
- AspNet Core 5
  - Sessions
  - Polly
  - Memcache

## Patterns

- Simple service layer pattern
- Typed HttpClient
- Caching cointree API responses (1s) as a API limiter
- Railway Oriented Programming for handling errors
- Inbuilt IoC (AspNet core)
- Models for request/response objects

## Hosting

- Vue Frontend - hosted on Azure Storage Static Websites
- AspNet Backend - Azure WebApp (Linux) for the API calls.

## Backlog (TODO)

- Unit Tests
- Include logging
- Error handling on the front end to process fault codes for the user
- Improve UI a bit more
