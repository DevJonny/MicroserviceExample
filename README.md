# Microservices Example

## Stage 2: Messaging

Stage 1 consists of three parts:

1. API - this is our entry point to the system and what we interact with
2. EventConsumer - this is our service that we use to persist data
3. Datastore - This simulates a persistent data store

### Microservices.API

We have a two endpoints for the API:

- `POST /api/todo`
- `GET /api/todo/{id}`

We can create a new To-do by posting:

```json
{
    "id": "replace-with-a-guid-or-something-unique"
}
```

then we can retrieve the new to-do by calling the get API

```http
/api/todo/replace-with-a-guid-or-something-unique
```

When we call `POST` we add a message on to the `todo` RabbitMQ queue.

### Microservices.EventConsumer

For Stage 2 we've replaced the Web API for a console application that uses the RabbitMQ .NET Client to create a consumer to read messages from the `todo` queue.

When the EventConsumer receives a message we invoke a `POST` endpoint that is responsible for persisting the To-do into the datastore.

### Microservices.Datastore

Our third service/API provides us with an in-memory datastore. We have an API over it to allow CRUD operations of To-Dos.

## Next Steps

For [Stage 3](https://github.com/DevJonny/MicroserviceExample/tree/stage3) we to introduce [Brighter](https://github.com/BrighterCommand/Brighter) into the project.