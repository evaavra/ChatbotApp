using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChatbotMyAxelos.Models
{
    public class PDFFile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public List<SeperatedText> SeperatedTexts { get; set; }
    }
}
