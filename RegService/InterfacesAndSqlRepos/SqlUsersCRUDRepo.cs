using RegService.AppDbContext;
using RegService.Models;

namespace RegService.InterfacesAndSqlRepos
{
    public class SqlUsersCRUDRepo : IUsersCRUD
    {
        private readonly DatabaseContext context;

        public SqlUsersCRUDRepo(DatabaseContext _context)
        {
            context = _context;
        }

        public UsersRegModel Create(UsersRegModel model)
        {
            context.Users.Add(model);
            context.SaveChanges();
            return model;
        }

        public async Task<UsersRegModel> Delete(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user != null)
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            return user;
        }

        public async Task<UsersRegModel> Get(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public IEnumerable<UsersRegModel> GetAll()
        {
            return context.Users;
        }

        public UsersRegModel Update(UsersRegModel upmodel)
        {
            var up = context.Users.Attach(upmodel);
            up.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return upmodel;
        }
    }
}
