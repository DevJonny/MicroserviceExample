# Microservices Example

## Stage 1: HTTP communication

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

When we call `POST` we in turn call a `POST` endpoint on the EventConsumer. This in turn persists the `To-do` to the Datastore which is done via another API.

### Microservices.EventConsumer

For this stage this is just another API, which we want to replace for something that doesn't couple the two services together later on.

When the API calls the EventConsumer we invoke a `POST` endpoint that is responsible for persisting the To-do into the datastore.

### Microservices.Datastore

Our third service/API provides us with an in-memory datastore. We have an API over it to allow CRUD operations of To-Dos.

## Running Stage 1

To run up Stage 1 you need either use the `run.sh` or `run.ps1` files. This will invoke Docker Compose and start up all three containers on a shared network.

## Next Steps

For [Stage 2](https://github.com/DevJonny/MicroserviceExample/tree/stage2) we want to remove our coupled communication between the API and the EventConsumer. For this we will use messaging.