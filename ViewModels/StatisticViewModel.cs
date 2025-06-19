using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ReactiveUI;
using Warehouse.Models;

namespace Warehouse.ViewModels;

public delegate void RefresherDelegate();

public class StatisticViewModel : ViewModelBase
{
    public static bool IsDateInCurrentMonth(DateOnly date)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        return date.Year == today.Year && date.Month == today.Month;
    }

    public static bool IsDateInPreviousMonth(DateOnly date)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        return date.Year == today.Year && (date.Month + 1) == today.Month;
    }

    public class ItemCount
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }


    private int _itemsCount;

    public int ItemsCount
    {
        get => _itemsCount;
        set => this.RaiseAndSetIfChanged(ref _itemsCount, value);
    }

    private ObservableCollection<ItemCount> _itemCounts;

    public ObservableCollection<ItemCount> ItemCounts
    {
        get => _itemCounts;
        set => this.RaiseAndSetIfChanged(ref _itemCounts, value);
    }

    private ObservableCollection<ItemCount> _lowCountItems;

    public ObservableCollection<ItemCount> LowCountItems
    {
        get => _lowCountItems;
        set => this.RaiseAndSetIfChanged(ref _lowCountItems, value);
    }


    public void RefreshStatistic()
    {
        ItemsCount = DatabaseInterface.Items.Count(el => !el.IsSend || el.IsReverted);
        
        var result = DatabaseInterface.Items
            .Where(el=>el.IsSend && !el.IsReverted)
            .GroupBy(i => i.Name)
            .Select(g => new
            {
                Name = g.Key,
                Count = g.Count()
            }).OrderByDescending(el => el.Count).Take(3);
        var temp = new List<ItemCount>();
        foreach (var item in result)
        {
            temp.Add(new ItemCount
            {
                Name = item.Name,
                Count = item.Count
            });
        }

        ItemCounts = new ObservableCollection<ItemCount>(temp);


        var last = DatabaseInterface.Items
            .Where(el => IsDateInPreviousMonth(el.Record.DateEntrance) && el.IsSend)
            .GroupBy(el => el.Name)
            .Select(g => new
            {
                Name = g.Key,
                Count = g.Count()
            }).ToList();

        var thisMonth = DatabaseInterface.Items
            .Where(el => IsDateInCurrentMonth(el.Record.DateEntrance) && !el.IsSend)
            .GroupBy(el => el.Name)
            .Select(g => new
            {
                Name = g.Key,
                Count = g.Count()
            }).ToList();

        var thisMonthDict = thisMonth.ToDictionary(x => x.Name, x => x.Count);
        var newItems = new ObservableCollection<ItemCount>();

        foreach (var lastItem in last)
        {
            int currentCount = thisMonthDict.ContainsKey(lastItem.Name) ? thisMonthDict[lastItem.Name] : 0;

            if (currentCount < lastItem.Count)
            {
                newItems.Add(new ItemCount
                {
                    Name = lastItem.Name,
                    Count = lastItem.Count - currentCount
                });
            }
        }

        LowCountItems = newItems;
    }

    public StatisticViewModel()
    {
        RefreshStatistic();
    }
}