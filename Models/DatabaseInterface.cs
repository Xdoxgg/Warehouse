using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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

    #region DataProcessing

    public static async Task<bool> DeleteItemAsync(Item item)
    {
        if (!_dbExists)
        {
            Initialize();
        }

        var t = Items.FirstOrDefault(x => x.Id == item.Id);
        if (t != null)
        {
            Items.Remove(t);
            try
            {
                await _context.SaveChangesAsync(); // Используем асинхронный метод
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Ошибка при удалении: {ex.Message}");
                return false;
            }
        }
        else
        {
            return false;
        }

        return true;
    }

    public static bool DeleteRecord(Record item)
    {
        if (!_dbExists)
        {
            Initialize();
        }

        var t = Records.FirstOrDefault(x => x.Id == item.Id);
        if (t != null)
        {
            Records.Remove(t);
            _context.SaveChanges();
        }
        else
        {
            return false;
        }

        return true;
    }

    public static bool DeleteItemType(ItemType item)
    {
        if (!_dbExists)
        {
            Initialize();
        }

        var t = ItemTypes.FirstOrDefault(x => x.Id == item.Id);
        if (t != null)
        {
            ItemTypes.Remove(t);
            _context.SaveChanges();
        }
        else
        {
            return false;
        }

        return true;
    }

    public static bool DeleteCreators(Creator item)
    {
        if (!_dbExists)
        {
            Initialize();
        }

        var t = Creators.FirstOrDefault(x => x.Id == item.Id);
        if (t != null)
        {
            Creators.Remove(t);
            _context.SaveChanges();
        }
        else
        {
            return false;
        }

        return true;
    }

    public static void AddUser(string username, string password, bool type)
    {
        if (!_dbExists)
        {
            Initialize();
        }

        UserClass user = new UserClass
        {
            Name = username,
            Password = EncryptionProcess.Encrypt(password),
            Type = type
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

        return _context.Users.Any(el => el.Name == username && el.Password == EncryptionProcess.Encrypt(password));
    }

    public static bool UserType(string username, string password)
    {
        if (!_dbExists)
        {
            Initialize();
        }

        return _context.Users.First(el => el.Name == username && el.Password == EncryptionProcess.Encrypt(password))
            .Type;
    }

    public static void SaveOrUpdateItems(ObservableCollection<object> items)
    {
        if (!_dbExists)
        {
            Initialize();
        }

        foreach (var item in items.OfType<Item>())
        {
            var existingItem = _context.Items.Include(i => i.ItemType).Include(i => i.Record)
                .FirstOrDefault(i => i.Id == item.Id);

            if (existingItem != null)
            {
                existingItem.Name = item.Name;
                existingItem.Price = item.Price;
                existingItem.Description = item.Description;
                existingItem.ToDate = item.ToDate;
                existingItem.RecordId = item.RecordId;
                existingItem.ItemTypeId = item.ItemTypeId;
                existingItem.IsSend = item.IsSend;
                existingItem.IsReverted = item.IsReverted;
                existingItem.CreatorId = item.CreatorId;
                if (existingItem.ItemType?.Id != item.ItemType?.Id)
                {
                    existingItem.ItemType = _context.ItemTypes.Find(item.ItemType.Id);
                }

                if (existingItem.Record?.Id != item.Record?.Id)
                {
                    existingItem.Record = _context.Records.Find(item.Record.Id);
                }

                if (existingItem.Creator?.Id != item.Creator?.Id)
                {
                    existingItem.Creator = _context.Creators.Find(item.Creator.Id);
                }
            }
            else
            {
                Item addedItem = new Item()
                {
                    Name = item.Name,
                    Price = item.Price,
                    Description = item.Description,
                    ToDate = item.ToDate,
                    RecordId = item.RecordId,
                    ItemTypeId = item.ItemTypeId,
                    ItemType = _context.ItemTypes.Find(item.ItemType.Id),
                    Record = _context.Records.Find(item.Record.Id),
                    CreatorId = item.Creator.Id,
                    Creator = _context.Creators.Find(item.Creator.Id)
                };
                _context.Items.Add(addedItem);
            }
        }

        _context.SaveChanges();
    }

    public static void SaveOrUpdateRecords(ObservableCollection<object> items)
    {
        if (!_dbExists)
        {
            Initialize();
        }

        foreach (var item in items.OfType<Record>())
        {
            var existingItem = _context.Records.FirstOrDefault(i => i.Id == item.Id);

            if (existingItem != null)
            {
                existingItem.DateEntrance = item.DateEntrance;
            }
            else
            {
                Record addedItem = new Record()
                {
                    DateEntrance = item.DateEntrance,
                };
                _context.Records.Add(addedItem);
            }
        }

        _context.SaveChanges();
    }

    public static void SaveOrUpdateItemTypes(ObservableCollection<object> items)
    {
        if (!_dbExists)
        {
            Initialize();
        }

        foreach (var item in items.OfType<ItemType>())
        {
            var existingItem = _context.ItemTypes.FirstOrDefault(i => i.Id == item.Id);

            if (existingItem != null)
            {
                existingItem.Name = item.Name;
                existingItem.Description = item.Description;
            }
            else
            {
                ItemType addedItem = new ItemType()
                {
                    Name = item.Name,
                    Description = item.Description,
                };
                _context.ItemTypes.Add(addedItem);
            }
        }

        _context.SaveChanges();
    }

    public static void SaveOrUpdateCreators(ObservableCollection<object> items)
    {
        if (!_dbExists)
        {
            Initialize();
        }

        foreach (var item in items.OfType<Creator>())
        {
            var existingItem = _context.Creators.FirstOrDefault(i => i.Id == item.Id);

            if (existingItem != null)
            {
                existingItem.Name = item.Name;
            }
            else
            {
                Creator addedItem = new Creator()
                {
                    Name = item.Name
                };
                _context.Creators.Add(addedItem);
            }
        }

        _context.SaveChanges();
    }

    #endregion


    public static List<Item> Items
    {
        get { return _context.Items.Include(i => i.ItemType).Include(i => i.Record).Include(c => c.Creator).ToList(); }
    }

    public static DbSet<Record> Records => _context.Records;

    public static DbSet<ItemType> ItemTypes => _context.ItemTypes;

    public static DbSet<Creator> Creators => _context.Creators;
}