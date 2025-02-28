using CatAPI.ViewModels;

namespace CatAPI;

public partial class CatDetailPage : ContentPage
{
    public CatDetailPage() { }

    public CatDetailPage(CatDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}