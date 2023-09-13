using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Collections.Generic;
using ChatbotMyAxelos.Models;

namespace ChatbotMyAxelos.Services
{
    public interface ITextService
    {
        //IEnumerable<SeperatedText> GetSeperatedTexts();
        SeperatedText GetSeperatedTextById(int id);

        void CreateSeperatedText(SeperatedText text);
        void DeleteSeperatedText(int id);
    }
}
