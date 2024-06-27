using ChatApp_SingleR.Client.Models;
using ChatApp_SingleR.Repos;
using ChatModelsLibrary;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp_SingleR.chatHubs
{
    public class chatHub : Hub
    {
        private readonly ChatRepo _chatRepo;

        public chatHub(ChatRepo chatRepo)
        {
            _chatRepo = chatRepo;
        }

        public async Task SendMessage(Chat chat)
        {
            await _chatRepo.addChatAsync(chat);
            await Clients.All.SendAsync("ReciveMessage", chat);

        }

        public async Task AddAvailableUserAsync(AvailableUser availableUser)
        {
            availableUser.ConnectionId = Context.ConnectionId;
            await _chatRepo.AddAvailableUserAsync(availableUser);
            var users = await _chatRepo.GetAvailableUserAsync();
            await Clients.All.SendAsync("NotifyAllClients", users);
        }

    }
}
