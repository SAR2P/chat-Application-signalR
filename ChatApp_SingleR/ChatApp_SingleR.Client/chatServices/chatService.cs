using ChatModelsLibrary;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp_SingleR.Client.chatServices
{
    public class chatService
    {

        public Action? InvokeChatDisplay { get; set; }
        public List<Chat> Chats { get; set; } = [];
        private readonly HubConnection _hubConnection;
        public bool IsConnected { get; set; }

        public chatService(NavigationManager navigationManager)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(navigationManager.ToAbsoluteUri("/chathub"))
                .Build();
        }
        public void ReciveMessage()
        {
            _hubConnection.On<Chat>("ReciveMessage", (chat) =>
            {
                Chats.Add(chat);
                InvokeChatDisplay?.Invoke();
            });
        }

        public async Task StartConnectionString()
        {
            await _hubConnection.StartAsync();
            IsConnected = _hubConnection!.State == HubConnectionState.Connected;
        }

        public Task SendChat(Chat chat)
            => _hubConnection!.SendAsync("SendMessage", chat);


    }
}
