using Damex_Vagtplan.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Damex_Vagtplan
{
    // MainViewModel klassen fungerer som en ViewModel til MainWindow (hovedvinduet) i systemet.
    public class MainViewModel : INotifyPropertyChanged
    {
        private Employee selectedEmployee;
        // En Property, der kan bindes til, for at vise output til brugeren, den bruges dog ikke lige nu.
        private string output;
        private ObservableCollection<Employee> employees;
        // En kollektion til alle vagter, arrangeret efter dag i ugen.
        private Dictionary<DayOfWeek, List<WorkShift>> _shifts;
        // En kollektion til alle vagter og hvilken arbejder, der har fået vagten.
        // AssignShift()-metoden gemmer resultatet af dens udregninger her, altså hvilken arbejder, der får vagten.
        private Dictionary<WorkShift, Employee> _assignedShifts;
        // En observable-kollektion, der henter initialerne fra Dictionary'en assignedShifts, så de nemmere kan vises i brugergrænsefladen.
        // Indexeringen tilsvarer vagterne, f.eks. tilsvarer index 0 i kollektionen Søndags-WholeDay-vagten, index 1 tilsvarer Mandags-Morning-Vagt osv.
        // Textbox'ene i vores UI er bundet til indexerne i den her kollektion. 
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
                OnPropertyChanged(nameof(AssignedEmployeeInitials));
            }
        }
                
        public ObservableCollection<string> AssignedEmployeeInitials
        {
            get
            {
                if (AssignedShifts == null)
                    return new ObservableCollection<string>();

                return new ObservableCollection<string>(AssignedShifts.Values.Select(employee => employee.Initials));
            }
            set { _assignedEmployeeInitials = value; }
        }

        public MainViewModel()
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            Schedule schedule = new Schedule();
            // Employees og Shifts instantieres med Get-metoder, så de får instanset med data fra Model-klasserne.
            Employees = employeeRepository.GetEmployees();
            Shifts = schedule.GetSchedule();
            AssignedShifts = new Dictionary<WorkShift, Employee>();
        }

        // Metode til at tildele vagter til medarbejdere.
        public void AssignShifts()
        {
            // Nulstil tidligere tildelinger af vagter.
            ResetAssignments();

            // Iterer gennem alle vagter.
            foreach (var shiftsByDay in Shifts)
            {
                // Iterer så gennem vagter for hver dag.
                foreach (var shift in shiftsByDay.Value)
                {
                    // Find medarbejdere, der kan tage vagten, ud fra deres Availability.
                    var availableWorkers = Employees
                        .Where(worker =>
                            worker.EmployeeAvailableShifts.Any(availableShift =>
                                availableShift.Day == shift.Day &&
                                availableShift.TimeSlot == shift.TimeSlot &&
                                availableShift.IsAvailable) &&
                            !AssignedShifts.Any(kvp => kvp.Key.Day == shift.Day && kvp.Value == worker))
                        // Sorter så listen, så arbejderen med færrest timer i ugen kigges på først i næste iteration (giver bedre fordeling af vagter).
                        .OrderByDescending(worker => worker.Availability.AvailableHours - worker.Availability.WorkedHours)
                        .ToList();

                    // Hvis vi fandt en arbejder, der kan tage vagten, får de den.
                    if (availableWorkers.Any())
                    {
                        // Blandt de arbejdere, der kan tage vagten, hvem har så arbejdet mindst i ugen?
                        var workerWithMostAvailableHours = availableWorkers.First();

                        // Hvis de kan tage vagten uden at gå over deres maks ugentlige timer, får de vagten.
                        if (workerWithMostAvailableHours.Availability.WorkedHours + shift.Hours <= workerWithMostAvailableHours.Availability.AvailableHours)
                        {
                            AssignedShifts[shift] = workerWithMostAvailableHours;

                            // Opdater hvor mange timer, arbejderen har arbejdet denne uge.
                            workerWithMostAvailableHours.Availability.WorkedHours += shift.Hours;
                        }
                        else
                        {
                            // Noter hvis en arbejder kunne tage vagten, men den ville få dem over deres ugentlige timer.
                            Console.WriteLine($"Unable to assign shift: {shift.Day}, {shift.TimeSlot}. Exceeds available hours for all workers");
                        }
                    }
                    else
                    {
                        // Noter hvis en vagt ikke kunne tages.
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
