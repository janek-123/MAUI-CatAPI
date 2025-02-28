using CatAPI.ViewModels;

namespace CatAPI;

public partial class CatDetailPage : ContentPage
{
    //public CatDetailPage()
    //{
    //    InitializeComponent();
    //}

    public CatDetailPage(CatDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}