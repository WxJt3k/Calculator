using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        public string _result;

        public ICommand ClearCommand => new Command(() => Result = "0");
        public ICommand DeleteCharacterCommand => new Command(() => Result = Result.Substring(0, Result.Length - 1));
        public ICommand PercentageCommand => new Command(() => Result = (Double.Parse(Result) * 0.01).ToString());
        public ICommand NumberSelectionCommand { get; set; }
        public ICommand OperatorSelectionCommand { get; set; }
        public ICommand CalculateCommand { get; set; }

        public CalculatorViewModel()
        {
            NumberSelectionCommand = new Command<string>(OnNumberSelection);
            OperatorSelectionCommand = new Command<string>(OnOperatorSelection);
            CalculateCommand = new Command(OnCalculate);

            Result = "0";
        }

        public void OnNumberSelection(string number)
        {
            if (Result == "0")
                Result = number;
            else
                Result += number;
        }

        public void OnOperatorSelection(string operation)
        {
            Result += operation;
        }

        public void OnCalculate()
        {
            try
            {
                var dataTable = new System.Data.DataTable();
                var result = dataTable.Compute(Result, "");
                Result = result.ToString();
            }
            catch (Exception ex)
            {
                Result = "Se presentó un error en la calculadora: " + ex.Message;
            }
        }

        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                changedButtonsCalculator(nameof(Result));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void changedButtonsCalculator(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
