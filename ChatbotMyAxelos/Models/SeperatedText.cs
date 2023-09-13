using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChatbotMyAxelos.Models
{
    public class SeperatedText
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(1050)]
        public string Text { get; set; }
        [ForeignKey("PDFFile")]
        public int PDFFileId { get; set; }

        public PDFFile PDFFile { get; set; }
    }
}
