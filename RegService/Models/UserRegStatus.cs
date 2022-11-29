namespace RegService.Models
{
    public class UserRegStatus
    {
        public int RegStatusId { get; set; }
        public int? Id { get; set; }
        public UsersRegModel? UserRegModel { get; set; }
        public bool? FileFilled { get; set; } = false;
    }
}
