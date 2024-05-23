using Damex_Vagtplan.Models;
using System.ComponentModel;

namespace Damex_Vagtplan
{
    // AvailabilityViewModel klassen fungerer som en ViewModel til visning tilgængeligheds-data for en arbejder.
    // Arbejderen er specifikt den arbejder, der er valgt i brugergrænsefladen.
    public class AvailabilityViewModel : INotifyPropertyChanged
    {
        private Employee selectedEmployee;
        private EmployeeRepository emloyeeRepository;

        public EmployeeRepository EmpoyeeRepository
        {
            get { return emloyeeRepository; }
            set 
            { 
                emloyeeRepository = value;
                OnPropertyChanged(nameof(EmpoyeeRepository));
            }
        }

        // Den valgte medarbejder i brugergrænsefladen.
        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                if (selectedEmployee != value)
                {
                    selectedEmployee = value;
                    OnPropertyChanged(nameof(SelectedEmployee));
                    OnPropertyChanged(nameof(WorkedHours));
                    OnPropertyChanged(nameof(AvailableHours));
                }
            }
        }

        // De arbejdstimer, den valgte medarbejder har arbejdet i en uge.
        public int WorkedHours
        {
            get { return selectedEmployee?.Availability?.WorkedHours ?? 0; }
            set
            {
                if (selectedEmployee != null && selectedEmployee.Availability.WorkedHours != value)
                {
                    selectedEmployee.Availability.WorkedHours = value;
                    OnPropertyChanged(nameof(WorkedHours));
                }
            }
        }

        // De timer, den valgte medarbejder har til rådighed i en uge.
        public int AvailableHours
        {
            get { return selectedEmployee?.Availability?.AvailableHours ?? 0; }
            set
            {
                if (selectedEmployee != null && selectedEmployee.Availability.AvailableHours != value)
                {
                    selectedEmployee.Availability.AvailableHours = value;
                    OnPropertyChanged(nameof(AvailableHours));
                }
            }
        }

        public AvailabilityViewModel(Employee selectedEmployee)
        {
            this.selectedEmployee = selectedEmployee;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
