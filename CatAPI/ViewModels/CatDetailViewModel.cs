using CatAPI.Models;
using CatAPI.Services;

namespace CatAPI.ViewModels;

[QueryProperty(nameof(CatId), "id")]
public class CatDetailViewModel : BaseViewModel
{
    private readonly ICatApiService _catApiService;
    private string _catId;
    private Cat _cat;

    public string CatId
    {
        get => _catId;
        set
        {
            _catId = value;
            LoadCatAsync(_catId);
        }
    }

    public Cat Cat
    {
        get => _cat;
        set => SetProperty(ref _cat, value);
    }

    public CatDetailViewModel(ICatApiService catApiService)
    {
        _catApiService = catApiService;
        Title = "Cat Details";
    }

    private async Task LoadCatAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
            return;

        IsBusy = true;

        try
        {
            var cat = await _catApiService.GetCatByIdAsync(id);
            Cat = cat;
        }
        catch (Exception)
        {
            
        }
        finally
        {
            IsBusy = false;
        }
    }
}
