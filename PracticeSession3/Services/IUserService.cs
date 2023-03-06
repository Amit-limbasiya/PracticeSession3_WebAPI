using Azure;
using Microsoft.AspNetCore.JsonPatch;
using PracticeSession3.Models;

namespace PracticeSession3.Services
{
    public interface IUserService
    {
        Task<bool> AddUser(UserProfile user);
        Task<List<UserProfile>?> GetUsers();
        Task<UserProfile?> GetUser(int id);
        Task<List<UserProfile>?> UpdateUser(int id,UserProfile newUser);
        Task<List<UserProfile>?> UpdateUserById(int id, JsonPatchDocument<UserProfile> newUser);
        Task<List<UserProfile>?> UpdateUserByName(string name, JsonPatchDocument<UserProfile> newUser);
        Task<bool> DeleteUser(int id);
    }
}
