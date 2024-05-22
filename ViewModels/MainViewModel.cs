using Damex_Vagtplan.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Damex_Vagtplan
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Employee selectedEmployee;
        private string output;
        private ObservableCollection<Employee> employees;
        private Dictionary<DayOfWeek, List<WorkShift>> _shifts;
        private Dictionary<WorkShift, Employee> _assignedShifts;
        // A list of the initials from AssignedShifts in the order they are assigned to shifts (0 = Monday Morning).
        private ObservableCollection<string> _assignedEmployeeInitials;

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set 
            { 
                selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }

        public string Output
        {
            get { return output; }
            set
            {
                if (!string.IsNullOrEmpty(output))
                {
                    // Append the new value with a line break
                    output += Environment.NewLine + value;
                }
                else
                {
                    output = value;
                }
                OnPropertyChanged(nameof(Output));
            }
        }

        public ObservableCollection<Employee> Employees
        {
            get { return employees; }
            set
            {
                employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }

        public Dictionary<DayOfWeek, List<WorkShift>> Shifts
        {
            get { return _shifts; }
            set
            {
                _shifts = value;
                OnPropertyChanged(nameof(Shifts));
            }
        }

        public Dictionary<WorkShift, Employee> AssignedShifts
        {
            get { return _assignedShifts; }
            set
            {
                _assignedShifts = value;
                OnPropertyChanged(nameof(AssignedShifts));
                OnPropertyChanged(nameof(AssignedEmployeeInitials)); // Notify that the collection of employees has changed
            }
        }
                
        public ObservableCollection<string> AssignedEmployeeInitials
        {
            get
            {
                if (AssignedShifts == null)
                    return new ObservableCollection<string>(); // Return an empty collection if AssignedShifts is null

                return new ObservableCollection<string>(AssignedShifts.Values.Select(employee => employee.Initials));
            }
            set { _assignedEmployeeInitials = value; }
        }

        public MainViewModel()
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            Schedule schedule = new Schedule();
            Employees = employeeRepository.GetEmployees();
            Shifts = schedule.GetSchedule();
            AssignedShifts = new Dictionary<WorkShift, Employee>();
        }

        public void AssignShifts()
        {
            // Clear previous assignments
            ResetAssignments();

            foreach (var shiftsByDay in Shifts)
            {
                // Iterate through shifts for each day
                foreach (var shift in shiftsByDay.Value)
                {
                    // Find available workers for the shift
                    var availableWorkers = Employees
                        .Where(worker =>
                            worker.EmployeeAvailableShifts.Any(availableShift =>
                                availableShift.Day == shift.Day &&
                                availableShift.TimeSlot == shift.TimeSlot &&
                                availableShift.IsAvailable) &&
                            !AssignedShifts.Any(kvp => kvp.Key.Day == shift.Day && kvp.Value == worker))
                        .OrderByDescending(worker => worker.Availability.AvailableHours - worker.Availability.WorkedHours)
                        .ToList();

                    if (availableWorkers.Any())
                    {
                        // Assign the shift to the worker with the most available hours
                        var workerWithMostAvailableHours = availableWorkers.First();

                        // Check if assigning the shift would exceed the worker's available hours
                        if (workerWithMostAvailableHours.Availability.WorkedHours + shift.Hours <= workerWithMostAvailableHours.Availability.AvailableHours)
                        {
                            // Assign the shift to the available worker
                            AssignedShifts[shift] = workerWithMostAvailableHours;

                            // Update the worker's worked hours
                            workerWithMostAvailableHours.Availability.WorkedHours += shift.Hours;
                        }
                        else
                        {
                            // Handle the case where assigning the shift would exceed the worker's available hours
                            // You can implement your desired logic here (e.g., log a warning, skip the shift, etc.)
                            Console.WriteLine($"Unable to assign shift: {shift.Day}, {shift.TimeSlot}. Exceeds available hours for all workers");
                        }
                    }
                    else
                    {
                        // Handle the case where no worker is available for the shift
                        // You can implement your desired logic here (e.g., log a warning, skip the shift, etc.)
                        Console.WriteLine($"Unable to assign shift: {shift.Day}, {shift.TimeSlot}");
                    }
                }
            }
            OnPropertyChanged(nameof(AssignedEmployeeInitials));
            OnPropertyChanged(nameof(SelectedEmployee));
        }

        public void ResetAssignments()
        {
            AssignedShifts.Clear();
            AssignedEmployeeInitials.Clear();

            foreach (var employee in Employees)
            {
                employee.Availability.WorkedHours = 0;
            }

            OnPropertyChanged(nameof(AssignedEmployeeInitials));
        }

        //public void ResetSchedule()
        //{
        //    AssignedShifts.Clear();
        //    OnPropertyChanged(nameof(AssignedShifts));
        //    OnPropertyChanged(nameof(AssignedEmployeeInitials));
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
