using CatAPI.Models;
using CatAPI.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CatAPI.ViewModels;
public class CatsViewModel : BaseViewModel
{
    private readonly ICatApiService _catApiService;

    public ObservableCollection<Cat> Cats { get; } = new ObservableCollection<Cat>();

    public ICommand LoadCatsCommand => new Command(async () => await LoadCatsAsync());
    public ICommand RefreshCommand => new Command(async () => await RefreshCatsAsync());
    public ICommand SelectCatCommand => new Command<Cat>(async (cat) => await SelectCatAsync(cat));

    public CatsViewModel() { }

    public CatsViewModel(ICatApiService catApiService)
    {
        _catApiService = catApiService;
        Title = "Cats";
    }

    private async Task LoadCatsAsync()
    {
        if (IsBusy)
            return;

        IsBusy = true;

        try
        {
            Cats.Clear();
            var cats = await _catApiService.GetCatsAsync(15);
            foreach (var cat in cats)
            {
                Cats.Add(cat);
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task RefreshCatsAsync() => await LoadCatsAsync();

    private async Task SelectCatAsync(Cat cat)
    {
        if (cat == null) return;

        await Shell.Current.GoToAsync($"{nameof(CatDetailPage)}?id={cat.Id}");
    }
}
