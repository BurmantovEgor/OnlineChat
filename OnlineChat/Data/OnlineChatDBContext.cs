using Microsoft.EntityFrameworkCore;
using OnlineChat.Data.Models;

namespace OnlineChat.Data
{
    public class OnlineChatDBContext : DbContext
    {
        public DbSet<MessageEntity> messages { get; set; }

        public OnlineChatDBContext(DbContextOptions<OnlineChatDBContext> options)
            : base(options)
        {
        }
    }
}
