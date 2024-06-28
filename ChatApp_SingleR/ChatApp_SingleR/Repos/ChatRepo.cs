using ChatApp_SingleR.Authentication;
using ChatApp_SingleR.Data;
using ChatModelsLibrary.DTOs;
using ChatModelsLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChatApp_SingleR.Repos
{
    public class ChatRepo(AppDBContext appDBContext,UserManager<AppUser> userManager )
    {


        public async Task<GroupChatDTO> addChatToGroupAsync(GroupChat chat)
        {
            var entity = appDBContext.GropChats.Add(chat).Entity;
            await appDBContext.SaveChangesAsync();
            return new GroupChatDTO()
            {
                SenderId = entity.SenderId,
                SenderName = (await userManager.FindByIdAsync(entity.SenderId!))!.FullName,
                DateTime = entity.DateTime,
                Id = entity.Id,
                Message = entity.Message,
            };
        }

        public async Task<List<GroupChatDTO>> GetGroupChatsAsync()
        {
            var list = new List<GroupChatDTO>();
            var chats = await appDBContext.GropChats.ToListAsync();
            foreach (var item in chats)
            {
                list.Add(new GroupChatDTO()
                {
                    Id = item.Id,
                    SenderId = item.SenderId,
                    DateTime = item.DateTime,
                    Message = item.Message,
                    SenderName = (await userManager.FindByIdAsync(item.SenderId!))!.FullName
                }); 
            }
            return list;
        }

        public async Task<List<AvailableUserDTO>> AddAvailableUser(AvailableUser availableUser)
        {
            var list = new List<AvailableUserDTO>();

            var getUser = await appDBContext.AvailableUsers
                .FirstOrDefaultAsync(a => a.UserId == availableUser.UserId);

            if (getUser != null)
                getUser.ConnectionId = availableUser.ConnectionId;
            
            else
                appDBContext.AvailableUsers.Add(availableUser);

            await appDBContext.SaveChangesAsync();

            var allUser = await appDBContext.AvailableUsers.ToListAsync();
            foreach (var item in allUser)
            {
                list.Add(new AvailableUserDTO()
                {
                    UserId = item.UserId,
                    FullName = (await userManager.FindByIdAsync(item.UserId!))!.FullName
                });
            }
            return list;
        }

        public async Task<List<AvailableUserDTO>> GetAvailableUserAsync()
        {
            var list= new List<AvailableUserDTO>();

            var users = await appDBContext.AvailableUsers.ToListAsync();
            foreach (var user in users)
            {
                list.Add(new AvailableUserDTO()
                {
                    UserId = user.UserId,
                    FullName = (await userManager.FindByIdAsync(user.UserId!))!.FullName
                });
            }

            return list;
        } 


        public async Task<List<AvailableUserDTO>> RemoveUserAsync(string userId)
        {
            var user = await appDBContext.AvailableUsers.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user != null)
            {
                appDBContext.AvailableUsers.Remove(user);
                await appDBContext.SaveChangesAsync();
            }


            var list = new List<AvailableUserDTO>();
            var users = await appDBContext.AvailableUsers.ToListAsync();
            foreach (var item in users)
            {
                list.Add(new AvailableUserDTO()
                {
                    UserId = item.UserId,
                    FullName = (await userManager.FindByIdAsync(item.UserId!))!.FullName
                });
            }
            return list;
        }


        public async Task AddIndividualChatAsync(IndividualChat individualChat)
        {
            appDBContext.IndividualChats.Add(individualChat);
            await appDBContext.SaveChangesAsync();
        }

        public async Task<List<IndividualChatDTO>> GetIndividualChatsAsync(RequestChatDTO requestChatDTO)
        {
            var chatList = new List<IndividualChatDTO>();
            var chats = await appDBContext.IndividualChats.Where(
                i => i.SenderId == requestChatDTO.SenderId && i.ReciverId == requestChatDTO.ReciverId 
                || i.SenderId == requestChatDTO.ReciverId && i.ReciverId == requestChatDTO.SenderId).ToListAsync();

            if (chats != null)
            {
                foreach (var item in chats)
                {
                    chatList.Add(new IndividualChatDTO()
                    {
                        SenderId = item.SenderId,
                        ReciverId = item.ReciverId,
                        SenderName = (await userManager.FindByIdAsync(item.SenderId!))!.FullName,
                        ReciverName = (await userManager.FindByIdAsync(item.ReciverId!))!.FullName,
                        Message = item.Message,
                        Date = item.date
                    });
                }
                return chatList;
            }
            else
                return null;

        }



    }
}
