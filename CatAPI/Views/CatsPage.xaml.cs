using CatAPI.ViewModels;

namespace CatAPI;

partial class CatsPage : ContentPage
{
    private CatsViewModel _viewModel;

    public CatsPage(CatsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadCatsCommand.Execute(null);
    }
}
