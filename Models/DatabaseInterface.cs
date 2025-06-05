using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Warehouse.Models;

public static class DatabaseInterface
{
    private static DatabaseContext _context;
    private static bool _dbExists = false;

    private static void Initialize()
    {
        _context = new DatabaseContext();
    }

    #region UserProcessing

    
    public static void AddUser(string username, string password)
    {
        if (!_dbExists)
        {
            Initialize();
        }

        UserClass user = new UserClass
        {
            Name = username,
            Password = EncryptionProcess.Encrypt(password)
        };

        if (!_context.Users.Any(el => el.Name == user.Name && el.Password == user.Password))
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        else
        {
            throw new ArgumentException("Username already exists");
        }
        
        
    }

    public static bool ExistUser(string username, string password)
    {
        if (!_dbExists)
        {
            Initialize();
        }
        return _context.Users.Any(el=>el.Name==username && el.Password==EncryptionProcess.Encrypt(password));
    }
    
    #endregion



    public static List<Item> Items
    {
        get { return _context.Items.Include(i=>i.ItemType).Include(i=>i.Record).ToList(); }
    }

    public static DbSet<Record> Records
    {
        get { return _context.Records; }
    }
}