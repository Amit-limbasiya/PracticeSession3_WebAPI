using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Logging.Abstractions;
using PracticeSession3.Data;
using PracticeSession3.Models;

namespace PracticeSession3.Services
{
    public class UserService : IUserService
    {
        private readonly MyDBContext _context;

        public UserService(MyDBContext context)
        {
            _context = context;
        }

        public async Task<bool> AddUser(UserProfile user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user= await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserProfile?> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null) 
                return null;
            return user;
        }

        public async Task<List<UserProfile>?> GetUsers()
        {
            var users=await _context.Users.ToListAsync();
            return users;
        }

        public async Task<List<UserProfile>?> UpdateUser(int id, UserProfile newUser)
        {
            var user=await _context.Users.FindAsync(id);
            if(user==null)
                return null;
            user.name = newUser.name;
            user.email = newUser.email;
            user.age= newUser.age;
            user.PhoneNumber= newUser.PhoneNumber;
            await _context.SaveChangesAsync();
            return await _context.Users.ToListAsync();
        }

        public async Task<List<UserProfile>?> UpdateUserById(int id, JsonPatchDocument<UserProfile> newUser)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return null;
            try
            {
                newUser.ApplyTo(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            Console.WriteLine(user.age);
            await _context.SaveChangesAsync();
            return await _context.Users.ToListAsync();
        }

        public async Task<List<UserProfile>?> UpdateUserByName(string name, JsonPatchDocument<UserProfile> newUser)
        {
            var user = _context.Users.FirstOrDefault(u => u.name == name);
            if(user is null) return null;

            try
            {
                newUser.ApplyTo(user);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

            await _context.SaveChangesAsync();
            return await _context.Users.ToListAsync();
        }
    }
}
