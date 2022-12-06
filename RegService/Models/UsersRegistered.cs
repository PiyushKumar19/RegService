using System.ComponentModel.DataAnnotations;

namespace RegService.Models
{
    public class UsersRegistered
    {
        [Key]
        public int Id { get; set; }
        public int FileNo { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string ContactNo { get; set; }
        [Required]
        [EmailAddress]
        public string EmailId { get; set; }
        public string? Address { get; set; }
        public int? Pincode { get; set; }
        public string? State { get; set; }
        public string? District { get; set; }
        public string? AccountNo { get; set; }
        public string? IFSCcode { get; set; }
        public string? Branch { get; set; }
        public string? BankName { get; set; }
        public string? AadhaarNo { get; set; }
        public string? PanNo { get; set; }
    }
}
