# Microservices Example

The example has various stages with added complexity at each stages

## Stage 1: HTTP communication

[Link to Stage 1](http://github.com/DevJonny/MicroserviceExample/tree/stage1)

This first stage we have two Web API projects. One is the "front-end" API the second is a "backend" system. They are tightly coupled by a syncronous HTTP calls but it keeps things simple.

## Stage 2: Messaging

[Link to Stage 2](https://github.com/DevJonny/MicroserviceExample/tree/stage2)

In the second stage we want to remove the dependency on having the `EventConsumer` service running when we call the `POST` endpoint of the API.

This stage is not yet complete.