using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace Damex_Vagtplan.Models
{
    // Representerer et repository til håndtering af medarbejderdata.
    public class EmployeeRepository
    {
        // En kollektion til medarbejderne.
        private ObservableCollection<Employee> Employees;

        public EmployeeRepository()
        {
            // Tilføj de initialiserede medarbejdere til kollektionen.
            // Hver medarbejder får tildelt en standard tilgængelighed (Availability) og en liste med deres tilgængelige vagter.
            // Hver medarbejder kan som udgangspunkt tage alle vagter og arbejde op til 40 timer.
            Employees = new ObservableCollection<Employee>
            {
                new Employee(
                    initials: "JB",
                    name: "John Brown",
                    phone: "123-456-7890",
                    email: "john.brown@example.com",
                    color: new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF5733")),
                    availability: new Availability(),
                    employeeAvailableShifts: InitializeAvailableShifts()
                    ),

                new Employee(
                    initials: "JV",
                    name: "Jane Vance",
                    phone: "234-567-8901",
                    email: "jane.vance@example.com",
                    color: new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF33FF57")),
                    availability: new Availability(),
                    employeeAvailableShifts: InitializeAvailableShifts()
                    ),

                new Employee(
                    initials: "LA",
                    name: "Lisa Adams",
                    phone: "345-678-9012",
                    email: "lisa.adams@example.com",
                    color: new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3357FF")),
                    availability: new Availability(),
                    employeeAvailableShifts: InitializeAvailableShifts()
                    ),

                new Employee(
                    initials: "RR",
                    name: "Robert Roe",
                    phone: "456-789-0123",
                    email: "robert.roe@example.com",
                    color: new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF33A1")),
                    availability: new Availability(),
                    employeeAvailableShifts: InitializeAvailableShifts()
                    ),
            };
        }

        // Hver AvailableShift-vagt i employeeAvailableShifts bliver initialiseret som tilgængelig (true) for medarbejderne.
        private List<AvailableShift> InitializeAvailableShifts()
        {
            var availableShifts = new List<AvailableShift>();

            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                foreach (TimeSlot timeSlot in Enum.GetValues(typeof(TimeSlot)))
                {
                    availableShifts.Add(new AvailableShift(day, timeSlot, true));
                }
            }

            return availableShifts;
        }

        public ObservableCollection<Employee> GetEmployees()
        {
            return Employees;
        }

        // Tilføjer en ny tilgængelig vagt til en bestemt medarbejder på en given dag og tid.
        public void AddAvailableShift(Employee selectedEmployee, DayOfWeek day, TimeSlot timeSlot)
        {
            var availableShift = selectedEmployee.EmployeeAvailableShifts.FirstOrDefault(s => s.Day == day && s.TimeSlot == timeSlot);
            if (availableShift != null)
            {
                availableShift.IsAvailable = true;
            }
            else
            {
                selectedEmployee.EmployeeAvailableShifts.Add(new AvailableShift(day, timeSlot, true));
            }
        }

        // Fjerner en tilgængelig vagt fra en bestemt medarbejder på en given dag og tidspunkt.
        public void RemoveAvailableShift(Employee selectedEmployee, DayOfWeek day, TimeSlot timeSlot)
        {
            var availableShift = selectedEmployee.EmployeeAvailableShifts.FirstOrDefault(s => s.Day == day && s.TimeSlot == timeSlot);
            if (availableShift != null)
            {
                availableShift.IsAvailable = false;
            }
        }

        public Employee GetEmployeeByInitials(string initials)
        {
            foreach (var employee in Employees)
            {
                if (employee.Initials == initials)
                {
                    return employee;
                }
            }
            return null;
        }
    }
}