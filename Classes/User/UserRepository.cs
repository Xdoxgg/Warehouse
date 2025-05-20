using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Warehouse.Classes.User;

public static class UserRepository
{
    private static UserContext _context = new UserContext(new DbContextOptions<UserContext>());

    public static List<User> GetAllUsers()
    {
        return _context.Users.ToList();
    }
}