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

    }
}
