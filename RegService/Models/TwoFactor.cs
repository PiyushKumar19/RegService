using System.ComponentModel.DataAnnotations;

namespace RegService.Models
{
    public class TwoFactor
    {
        [Required]
        public string TwoFactorCode { get; set; }
        public string? GeneratedCode { get; set; }
    }
}
