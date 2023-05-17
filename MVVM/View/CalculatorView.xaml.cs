using Calculator.MVVM.ViewModels;

namespace Calculator.MVVM.View;

public partial class CalculatorView : ContentPage
{
    public CalculatorView()
    {
        InitializeComponent();
        BindingContext = new CalculatorViewModel();
    }
}