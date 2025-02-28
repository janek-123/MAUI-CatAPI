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
        if (_viewModel.Cats.Count == 0)
            _viewModel.LoadCatsCommand.Execute(null);
    }
}
