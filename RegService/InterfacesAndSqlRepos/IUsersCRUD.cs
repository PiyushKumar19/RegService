using RegService.Models;

namespace RegService.InterfacesAndSqlRepos
{
    public interface IUsersCRUD
    {
        public Task<UsersRegModel> Get(int id);
        public IEnumerable<UsersRegModel> GetAll();
        public UsersRegModel Create(UsersRegModel model);
        public UsersRegModel Update(UsersRegModel upmodel);
        public Task<UsersRegModel> Delete(int id);
    }
}
