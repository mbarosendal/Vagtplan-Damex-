using Damex_Vagtplan.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Damex_Vagtplan
{
    /// <summary>
    /// Interaction logic for AvailabilityWindow.xaml
    /// </summary>
    public partial class AvailabilityWindow : Window
    {
        AvailabilityViewModel avm;
        public Employee selectedEmployee;
        EmployeeRepository repository;
        MainViewModel mainViewModel;

        public AvailabilityWindow(Employee MainWindowSelectedEmployee, MainViewModel mvm)
        {
            InitializeComponent();
            avm = new AvailabilityViewModel(selectedEmployee);
            DataContext = avm;
            this.selectedEmployee = MainWindowSelectedEmployee;
            this.mainViewModel = mvm;
            avm.SelectedEmployee = MainWindowSelectedEmployee;

            UpdateScheduleColors();
        }
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the clicked Button
            Button button = (Button)sender;

            // Get the day and time slot from the button's Tag property
            string[] tagParts = button.Tag.ToString().Split(',');
            DayOfWeek day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), tagParts[0].Trim());
            TimeSlot timeSlot = (TimeSlot)Enum.Parse(typeof(TimeSlot), tagParts[1].Trim());


            if (selectedEmployee != null)
            {
                // Check if the employee is available for the given day and time slot
                var availableShift = selectedEmployee.EmployeeAvailableShifts.FirstOrDefault(s => s.Day == day && s.TimeSlot == timeSlot);

                Console.WriteLine($"Editing {selectedEmployee.Initials}'s shift");

                if (availableShift != null)
                {
                    // Toggle the availability
                    availableShift.IsAvailable = !availableShift.IsAvailable;

                    // Update the button background based on the availability
                    button.Background = availableShift.IsAvailable ? Brushes.Green : Brushes.Red;
                }
                else
                {
                    // If the AvailableShift doesn't exist, add it
                    avm.EmpoyeeRepository.AddAvailableShift(selectedEmployee, day, timeSlot);
                    button.Background = Brushes.Green;
                }

                Console.WriteLine("Available Shift:");
                Console.WriteLine($"Day: {availableShift.Day}");
                Console.WriteLine($"Time Slot: {availableShift.TimeSlot}");
                Console.WriteLine($"Is Available: {availableShift.IsAvailable}");
            }
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            // Assuming selectedEmployee is accessible within this method

            // Iterate through each available shift and set IsAvailable to true
            foreach (var shift in selectedEmployee.EmployeeAvailableShifts)
            {
                shift.IsAvailable = true;
            }

            // Reset available hours (doesn't work!)
            selectedEmployee.Availability.AvailableHours = 40;

            UpdateScheduleColors();
        }

        private void UpdateScheduleColors()
        {
            foreach (UIElement element in MainGrid.Children)
            {
                if (element is Button button && button.Tag != null)
                {
                    string[] tagParts = button.Tag.ToString().Split(',');
                    DayOfWeek day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), tagParts[0].Trim());
                    TimeSlot timeSlot = (TimeSlot)Enum.Parse(typeof(TimeSlot), tagParts[1].Trim());

                    if (selectedEmployee != null)
                    {
                        // Check if the employee is available for the given day and time slot
                        var availableShift = selectedEmployee.EmployeeAvailableShifts.FirstOrDefault(s => s.Day == day && s.TimeSlot == timeSlot);

                        if (availableShift != null)
                        {
                            // Update the TextBox background based on the availability
                            button.Background = availableShift.IsAvailable ? Brushes.Green : Brushes.Red;
                        }
                        else
                        {
                            // If no specific shift is found, set a default color (e.g., Gray)
                            button.Background = Brushes.Gray;
                        }
                    }
                }
            }            
        }


    }
}


