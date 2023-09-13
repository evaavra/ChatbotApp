using ChatbotMyAxelos.DAL;
using ChatbotMyAxelos.Models;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;

namespace ChatbotMyAxelos.Services
{
    public class TextService : ITextService
    {
        private readonly ChatBotContext _context;

        public TextService(ChatBotContext context)
        {
            _context = context;
        }

        public void CreateSeperatedText(SeperatedText text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            _context.SeperatedTexts.Add(text);
            _context.SaveChanges();
        }

        public void DeleteSeperatedText(int id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException();
            }
            var text = GetSeperatedTextById(id);
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            _context.SaveChanges();
        }

        public SeperatedText GetSeperatedTextById(int id)
        {
            if(id == 0)
            {
                throw new ArgumentNullException();
            }
            return _context.SeperatedTexts.FirstOrDefault(s => s.Id == id);
        }

    }
}
