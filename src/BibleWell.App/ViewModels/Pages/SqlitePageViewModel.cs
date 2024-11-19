using System.Collections.ObjectModel;
using BibleWell.App.Data;
using BibleWell.App.Data.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BibleWell.App.ViewModels.Pages;
public sealed partial class SqlitePageViewModel : PageViewModelBase
{
    [ObservableProperty]
    private string _greeting = "Welcome to Avalonia with SQLite!";
    
    [ObservableProperty]
    private string _tableText = "Click the button to fetch from the Db.";
    
    public ObservableCollection<ExampleItem> ExampleItems { get; set; }
    private readonly BibleWellSqlite _dbContext;
    
    
    public SqlitePageViewModel()
    {
        _dbContext = new BibleWellSqlite();
        var fakeItems = new List<ExampleItem>
        {
            new() {
                Id = 2,
                Name = "Thing Two"
            },
            new() {
                Id = 3,
                Name = "Thing Three"
            },
            new() {
                Id = 1,
                Name = "Thing One"
            },
        };
        
        ExampleItems = new ObservableCollection<ExampleItem>(fakeItems);
    }
    
    [RelayCommand]
    private async Task GetItemsButtonClick()
    {
        TableText = "Here is a list of items from the database.";
        ExampleItems.Clear();
        
        var dbItems = await _dbContext.GetItemsAsync();

        foreach (var item in dbItems)
        {
            ExampleItems.Add(item);
        }
    }

    [RelayCommand]
    private async Task SaveNewItem()
    {
        // working here 
        await _dbContext.SaveItemAsync(new ExampleItem()
        {
            Name = "Thing four",
        });
    }
}

