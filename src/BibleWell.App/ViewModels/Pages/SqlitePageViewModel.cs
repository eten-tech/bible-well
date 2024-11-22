using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using BibleWell.App.Data;
using BibleWell.App.Data.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BibleWell.App.ViewModels.Pages;

public sealed partial class SqlitePageViewModel : PageViewModelBase
{
    private readonly BibleWellSqlite _dbContext;

    [ObservableProperty]
    private string _greeting = "Welcome to Avalonia with SQLite!";

    [ObservableProperty]
    private string _tableText = "Click the button to fetch from the Db.";

    public ObservableCollection<ExampleItem> ExampleItems { get; set; }

    [ObservableProperty]
    private bool _isDataGridEnabled;
    
    public SqlitePageViewModel()
    {
        IsDataGridEnabled = false;
        _dbContext = new BibleWellSqlite();
        ExampleItems = new ObservableCollection<ExampleItem>([]);
        ExampleItems.CollectionChanged += ExampleItems_CollectionChanged;
    }

    [RelayCommand]
    private async Task GetItemsButtonClick()
    {
        ExampleItems.Clear();
        var dbItems = await _dbContext.GetItemsAsync();

        foreach (var item in dbItems)
        {
            ExampleItems.Add(item);
        }

        IsDataGridEnabled = true;
        TableText = "Items fetched. Click again to refresh the list from the DB.";
    }

    [ObservableProperty]
    private string _newItemText = "";

    [RelayCommand]
    private async Task CreateItem()
    {
        await _dbContext.SaveItemAsync(new ExampleItem()
        {
            Name = NewItemText,
        });

        NewItemText = string.Empty;

        await GetItemsButtonClick();
    }

    [RelayCommand]
    private Task WriteCollectionToLog()
    {
        foreach (var item in ExampleItems)
        {
            Debug.WriteLine(item.Name);
        }

        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task DeleteItem(int itemId)
    {
        var item = ExampleItems.FirstOrDefault(i => i.Id == itemId);

        if (item is null)
        {
            return;
        }

        await _dbContext.DeleteItemAsync(item);
        await GetItemsButtonClick();
    }

    private void ExampleItems_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (ExampleItem item in e.NewItems)
            {
                item.PropertyChanged += Item_PropertyChanged;
            }
        }

        if (e.OldItems != null)
        {
            foreach (ExampleItem item in e.OldItems)
            {
                item.PropertyChanged -= Item_PropertyChanged;
            }
        }
    }

    private async void Item_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is null)
        {
            return;
        }
        
        var item = (ExampleItem)sender;
        await _dbContext.SaveItemAsync(item);
    }
}

