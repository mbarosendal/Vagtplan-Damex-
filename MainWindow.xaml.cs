using Damex_Vagtplan.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Damex_Vagtplan
{
    public partial class MainWindow : Window
    {
        private MainViewModel mvm;

        public MainWindow()
        {
            InitializeComponent();
            mvm = new MainViewModel();
            DataContext = mvm;
        }

        private void UpdateScheduleColors()
        {
            foreach (UIElement element in MainGrid.Children)
            {
                if (element is TextBox textbox)
                {
                    switch (textbox.Text)
                    {
                        case "JB":
                            textbox.Background = mvm.Employees.FirstOrDefault(e => e.Initials == "JB")?.Color;
                            break;
                        case "JV":
                            textbox.Background = mvm.Employees.FirstOrDefault(e => e.Initials == "JV")?.Color;
                            break;
                        case "LA":
                            textbox.Background = mvm.Employees.FirstOrDefault(e => e.Initials == "LA")?.Color;
                            break;
                        case "RR":
                            textbox.Background = mvm.Employees.FirstOrDefault(e => e.Initials == "RR")?.Color;
                            break;
                        default:
                            textbox.Background = null;
                            break;
                    }
                }
            }
        }

        // Sends the same instance selected in the ComboBox into the AvailabilityWindow's constructor.
        private void OpenAvailabilityWindow(object sender, RoutedEventArgs e)
        {
            //mvm.EmployeeRepository.LoadEmployees();
            mvm.SelectedEmployee = (Employee)cbxEmployees.SelectedItem;
            Employee SelectedEmployee = mvm.SelectedEmployee;
            AvailabilityWindow availabilityWindow = new AvailabilityWindow(SelectedEmployee, mvm);
            availabilityWindow.ShowDialog();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            mvm.AssignShifts();
            UpdateScheduleColors();
            CountOccurrences();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            //mvm.ResetSchedule(); (not working!)
            UpdateScheduleColors();
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxEmployees.SelectedItem != null)
            {
                btnAvailability.IsEnabled = true;
            }
            else
            {
                btnAvailability.IsEnabled = false;
            }
        }

        private void TxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CountOccurrences();
        }

        private void CountOccurrences()
        {
            int countJV = 0;
            int countLA = 0;
            int countRR = 0;
            int countJB = 0;

            var textBoxes = new TextBox[]
            {
                TxtBox1a, TxtBox1b, TxtBox2a, TxtBox2b, TxtBox3a, TxtBox3b, TxtBox4a, TxtBox4b, TxtBox5a, TxtBox5b,
                TxtBoxAfn1, TxtBoxAfn2, TxtBoxAfn3, TxtBoxAfn4, TxtBoxAfn5,
                TxtBoxEve1, TxtBoxEve2, TxtBoxEve3, TxtBoxEve4, TxtBoxEve5,
                TxtBoxWnd1, TxtBoxWnd2
            };

            foreach (var textBox in textBoxes)
            {
                switch (textBox.Text)
                {
                    case "JV":
                        countJV++;
                        break;
                    case "LA":
                        countLA++;
                        break;
                    case "RR":
                        countRR++;
                        break;
                    case "JB":
                        countJB++;
                        break;
                }
            }

            CountJV.Text = $"JV: {countJV}";
            CountLA.Text = $"LA: {countLA}";
            CountRR.Text = $"RR: {countRR}";
            CountJB.Text = $"JB: {countJB}";
        }

    }
}
