using System.ComponentModel.DataAnnotations;

namespace RegService.Models
{
    public class SendMailDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
        [EmailAddress]
        [Required]
        public string SenderEmail { get; set; }
    }
}
