using ChatApp_SingleR.Authentication;
using ChatApp_SingleR.Client.DTOs;
using ChatApp_SingleR.Client.Models;
using ChatApp_SingleR.Data;
using ChatModelsLibrary;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChatApp_SingleR.Repos
{
    public class ChatRepo(AppDBContext appDBContext,UserManager<AppUser> userManager )
    {


        public async Task addChatAsync(Chat chat)
        {
            appDBContext.Chats.Add(chat);
            await appDBContext.SaveChangesAsync();
        }

        public async Task<List<Chat>> GetAllChatsAsync()
        {
            return await appDBContext.Chats.ToListAsync();
        }

        public async Task AddAvailableUserAsync(AvailableUser availableUser)
        {
            var getUser = await appDBContext.AvailableUsers.FirstOrDefaultAsync(
                a => a.UserId == availableUser.UserId
                );

            if (getUser != null)
                getUser.ConnectionId = availableUser.ConnectionId;
            else
                appDBContext.AvailableUsers.Add(availableUser);

            await appDBContext.SaveChangesAsync();
        }

        public async Task<List<AvailableUserDTO>> GetAvailableUserAsync()
        {
            var mlist= new List<AvailableUserDTO>();

            var users = await appDBContext.AvailableUsers.ToListAsync();
            foreach (var item in users)
            {
                mlist.Add(new AvailableUserDTO(
                    UserId: item.UserId,
                    ConnectionId: item.ConnectionId,
                    FullName: (await userManager.FindByIdAsync(item.UserId))!.FullName,
                    Email: (await userManager.FindByIdAsync(item.UserId))!.Email!
                    ));
            }

            return mlist;
        } 

    }
}
