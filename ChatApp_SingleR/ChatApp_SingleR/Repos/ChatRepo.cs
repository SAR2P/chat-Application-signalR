using ChatApp_SingleR.Data;
using ChatModelsLibrary;
using Microsoft.EntityFrameworkCore;

namespace ChatApp_SingleR.Repos
{
    public class ChatRepo(AppDBContext appDBContext)
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
    }
}
