using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Warehouse.Classes.User;

namespace Warehouse.Models;

public class RegistrationFormModel
{
    public bool CheckPasswords(string password1, string password2)
    {
        return password1.Equals(password2);
    }

    public bool AddUser(string username, string password)
    {
        try
        {
            List<User> users = UserRepository.GetAllUsers();
            using var context = new UserContext(new DbContextOptions<UserContext>());
            User newUser = new User
            {
                Name = username,
                Password = password
            };
            context.Users.Add(newUser);
            context.SaveChanges();
        }

        catch (Exception)
        {
            return false;
        }

        return true;
    }
}