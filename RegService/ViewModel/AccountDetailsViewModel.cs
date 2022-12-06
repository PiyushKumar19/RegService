using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RegService.ViewModel
{
    [Keyless]
    public class AccountDetailsViewModel
    {
        //public int FileNo { get; set; }
        //public string? FirstName { get; set; }
        //public string? LastName { get; set; }
        //public string? ContactNo { get; set; }
        //public string EmailId { get; set; }
        //public string? Address { get; set; }
        //public int? Pincode { get; set; }
        //public string? State { get; set; }
        //public string? District { get; set; }
        public string? AccountNo { get; set; }
        public string? IFSCcode { get; set; }
        public string? Branch { get; set; }
        public string? BankName { get; set; }
        public string? AadhaarNo { get; set; }
        public string? PanNo { get; set; }
    }
}
