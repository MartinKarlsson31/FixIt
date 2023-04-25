using FixIt.Models;
using Microsoft.AspNetCore.Identity;

namespace FixIt.Services
{
    public interface IFixItService
    {
        Task<ToDoItem[]> GetIncompleteAsync(IdentityUser user);
        Task<bool> AddItemAsync(ToDoItem newItem, IdentityUser user);
        Task<bool> MarkDoneAsync(Guid id, IdentityUser user);
    }
}
