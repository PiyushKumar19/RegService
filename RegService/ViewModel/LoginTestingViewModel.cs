using Microsoft.EntityFrameworkCore;

namespace RegService.ViewModel
{
    [Keyless]
    public class LoginTestingViewModel
    {
        public int FileNo { get; set; }
        public string ContactNo { get; set; }
    }
}
