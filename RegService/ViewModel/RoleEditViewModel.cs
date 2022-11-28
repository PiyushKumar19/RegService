using System.ComponentModel.DataAnnotations;

namespace RegService.ViewModel
{
    public class RoleEditViewModel
    {
        public RoleEditViewModel()
        {
            Users = new List<string>();
        }
        public string Id { get; set; }
        [Required(ErrorMessage = "Role Name Is Required")]
        public string RoleName { get; set; }
        public List<string> Users { get; set; }
    }
}
