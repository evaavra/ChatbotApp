using ChatbotMyAxelos.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatbotMyAxelos.DAL
{
    public class ChatBotContext : DbContext
    {
        public DbSet<PDFFile> PDFFiles { get; set; }
        public DbSet<SeperatedText> SeperatedTexts { get; set; }
        public ChatBotContext(DbContextOptions<ChatBotContext> options) : base(options) { }
    }
}
