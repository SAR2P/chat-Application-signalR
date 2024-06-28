using ChatApp_SingleR.Repos;
using ChatModelsLibrary.DTOs;
using ChatModelsLibrary.Models;
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

        public async Task SendMessageToGroup(GroupChat chat)
        {
            var saveChatDTO = await _chatRepo.addChatToGroupAsync(chat);
            await Clients.All.SendAsync("ReciveGroupMessages", saveChatDTO);

        }

        public async Task AddAvailableUser(AvailableUser availableUser)
        {
            availableUser.ConnectionId = Context.ConnectionId;
            var availableUsers = await _chatRepo.AddAvailableUser(availableUser);
            await Clients.All.SendAsync("NotifyAllClients", availableUser);
        }

        public async Task RemoveUser(string userId)
        {
            var availableUSers = await _chatRepo.RemoveUserAsync(userId);
            await Clients.All.SendAsync("NotifyAllClients", availableUSers);
        }


        public async Task AddIndividualChat(IndividualChat individualChat)
        {
            await _chatRepo.AddIndividualChatAsync(individualChat);
            var requestDTO = new RequestChatDTO()
            {
                ReciverId = individualChat.ReciverId,
                SenderId = individualChat.SenderId,
            };
            var getChats = await _chatRepo.GetIndividualChatsAsync(requestDTO);
            var prepareIndividualChat = new IndividualChatDTO()
            {
                SenderId = individualChat.SenderId,
                ReciverId = individualChat.ReciverId,
                Message = individualChat.Message,
                Date = individualChat.date,
                ReciverName = getChats.Where(c => c.ReciverId == individualChat.ReciverId).FirstOrDefault()!.ReciverName,
                SenderName = getChats.Where(c => c.SenderId == individualChat.SenderId).FirstOrDefault()!.SenderName
            };
            await Clients.Users(individualChat.ReciverId!).SendAsync("ReceiveIndividualMessage", prepareIndividualChat);
        }


    }
}
