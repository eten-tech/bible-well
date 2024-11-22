using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace BibleWell.App.Data.Models;

public class ExampleItem: ObservableObject
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; init; }
    
    // this is the manual implementation of [ObservableProperty]
    // When also using the field as a sqlite definition we 
    // have to manually implement the observation logic.
    private string? _name;
    public string? Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }
    
    public string? Description { get; set; }
}

