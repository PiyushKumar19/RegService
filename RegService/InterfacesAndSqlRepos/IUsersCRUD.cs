using RegService.Models;

namespace RegService.InterfacesAndSqlRepos
{
    public interface IUsersCRUD
    {
        public UsersRegModel GetById(int id);
        public IEnumerable<UsersRegModel> GetAll();
        public UsersRegModel Create(UsersRegModel model);
        public UsersRegModel Update(UsersRegModel upmodel);
        public UsersRegModel Delete(int id);
        public bool FindByFileNoAndContactNo(int fileno, string contactno);
        public UsersRegModel FindByFileNo(int fileno);
        //public UsersRegistered FindByFileNo(UsersRegModel model);
    }
}
