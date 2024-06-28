using ChatModelsLibrary.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp_SingleR.Client.chatServices
{
    public class MyHubConnectionService
    {

        private readonly HubConnection _hubConnection;

        public bool IsConnected { get; set; }

        public MyHubConnectionService(NavigationManager navigationManager)
        {
            //initialize the hubConnection
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(navigationManager.ToAbsoluteUri("/chatHub"))
                .Build();

            //start the connection
            _hubConnection.StartAsync();
            getConnectionState();

        }
    
       
        public HubConnection GetHubConnection()
        {
            return _hubConnection;
        }

        public bool getConnectionState()
        {
            var hubconnection = GetHubConnection();
            IsConnected = hubconnection != null;//this is like if statment, if it was`nt null it return true
            return IsConnected;
        }


    }
}
