# WhiteboardService

## ASP.NET Core Web API + SignalR

A service that provides sync and data storage for the [WhiteboardClient](https://github.com/OfirNoy/WhiteboardClient)

Using SignalR to receive data from clients and pass the new data to other connected clients

Using [GitEngineDB](https://github.com/OfirNoy/GitEngineDB) to store the whiteboard state in a persistant storage (folders & files on disk)

The compilation products of [WhiteboardClient](https://github.com/OfirNoy/WhiteboardClient) application (dist folder) are placed in the [wwwroot](https://github.com/OfirNoy/WhiteboardService/tree/master/wwwroot) folder to be served by the WhiteboardService application

The main logic of the application is in [State.cs](https://github.com/OfirNoy/WhiteboardService/blob/master/Logic/State.cs)

The SignalR code is in [WhiteboardHub](https://github.com/OfirNoy/WhiteboardService/blob/master/Logic/WhiteboardHub.cs)