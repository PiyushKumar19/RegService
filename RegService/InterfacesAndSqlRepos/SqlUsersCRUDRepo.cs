using Microsoft.AspNetCore.Mvc;
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

        public UsersRegModel Delete(int id)
        {
            var user = context.Users.Find(id);
            if (user != null)
                context.Users.Remove(user);
                context.SaveChanges();
            return user;
        }

        public UsersRegModel GetById(int id)
        {
            return context.Users.Find(id);
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

        public bool FindByFileNoAndContactNo(int fileno, string contactno)
        {
            var f = context.Users.Where(c => c.FileNo.Equals(fileno)).FirstOrDefault();
            var c = context.Users.Where(c => c.ContactNo.Equals(contactno)).FirstOrDefault();
            if (f == null && c == null)
            {
                return false;
            }
            return true;
        }

        public UsersRegModel FindByFileNo(int fileno)
        {
            var f = context.Users.Where(c => c.FileNo.Equals(fileno)).FirstOrDefault();
            if (f == null)
            {
                return null;
            }
            return f;
        }
    }
}
