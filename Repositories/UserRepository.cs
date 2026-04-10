using JobTracker.API.Data;
using JobTracker.API.Entities;
using JobTracker.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.API.Repositories;

public class UserRepository(AppDbContext db) : IUserRepository
{
   public async Task<User?> GetByEmailAsync(string email)
        => await db.Users.FirstOrDefaultAsync(u => u.Email == email.ToLower());

   public async Task<User?> GetByIdAsync(int id)
     => await db.Users.FindAsync(id);

   public async Task<User> CreateAsync(User user)
   {
     user.Email = user.Email.ToLower();
     db.Users.Add(user);
     await db.SaveChangesAsync();
     return user;
   }
   
    public async Task<bool> ExistsAsync(string email)
        => await db.Users.AnyAsync(u => u.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
}