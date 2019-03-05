using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using RestSharp;

namespace RestSharpTestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DateTime _time;
        private DataModel _dataModel;
        private double _hours, _minutes, _seconds;

        public MainWindow()
        {
            InitializeComponent();
            _dataModel = new DataModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _dataModel.SendRequest(_time);
            _dataModel.ProccessResponce();
            UpdateDataGrid();
        }

        private void _hours_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                ParseTimeValues(textBox);
            }
        }

        private void UpdateDataGrid()
        {
            _grid.ItemsSource = _dataModel.Data.AsDataView();
        }



        private void DatePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (sender is DatePicker pickerTime && pickerTime.SelectedDate != null)
            {
                _time = pickerTime.SelectedDate.Value;
                UpdateTimeStampValue();
            }
        }

        private void UpdateTimeStampValue()
        {
            if (_datePicker.SelectedDate == null)
                _time = DateTime.MinValue;
            else
                _time = _datePicker.SelectedDate.Value;

            _time = _time.AddHours(_hours).AddMinutes(_minutes).AddSeconds(_seconds);
            timestampValue.Content = _time.ToBinary().ToString();
        }

        private void ParseTimeValues(TextBox textBox)
        {
            bool isTimeValid = true;
            string text = textBox.Text;

            if (double.TryParse(text, out var result))
            {
                if (textBox == _hours_tb && result < 24)
                {
                    _hours = result;
                }
                else if (textBox == _minutes_tb && result < 60)
                {
                    _minutes = result;
                }
                else if (textBox == _seconds_tb && result < 60)
                {
                    _seconds = result;
                }
                else
                {
                    isTimeValid = false;
                    textBox.Text = "00";
                }
            }
            else
            {
                isTimeValid = false;
                textBox.Text = "00";
            }

            if (isTimeValid)
                UpdateTimeStampValue();
            else
                MessageBox.Show("Wrong time format", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}
