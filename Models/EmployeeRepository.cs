using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace Damex_Vagtplan.Models
{
    public class EmployeeRepository
    {
        private ObservableCollection<Employee> Employees;

        public EmployeeRepository()
        {
            Employees = new ObservableCollection<Employee>
            {
                new Employee(
                    initials: "JB",
                    name: "John Brown",
                    phone: "123-456-7890",
                    email: "john.brown@example.com",
                    color: new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF5733")),
                    availability: new Availability(),
                    employeeAvailableShifts: InitializeAvailableShifts() // Initialize available shifts for JB
                    ),

                new Employee(
                    initials: "JV",
                    name: "Jane Vance",
                    phone: "234-567-8901",
                    email: "jane.vance@example.com",
                    color: new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF33FF57")),
                    availability: new Availability(),
                    employeeAvailableShifts: InitializeAvailableShifts() // Initialize available shifts for JB
                    ),

                new Employee(
                    initials: "LA",
                    name: "Lisa Adams",
                    phone: "345-678-9012",
                    email: "lisa.adams@example.com",
                    color: new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3357FF")),
                    availability: new Availability(),
                    employeeAvailableShifts: InitializeAvailableShifts() // Initialize available shifts for JB
                    ),

                new Employee(
                    initials: "RR",
                    name: "Robert Roe",
                    phone: "456-789-0123",
                    email: "robert.roe@example.com",
                    color: new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF33A1")),
                    availability: new Availability(),
                    employeeAvailableShifts: InitializeAvailableShifts() // Initialize available shifts for JB
                    ),
            };
        }

        private List<AvailableShift> InitializeAvailableShifts()
        {
            var availableShifts = new List<AvailableShift>();

            // Add all shifts as available for the employee
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




        /// Kode vi ikke har UI til herunder.

        //public void AssignWorkShift(int employeeId, WorkShift workShift)
        //{
        //    var employee = employees.FirstOrDefault(e => e.EmployeeId == employeeId);
        //    if (employee != null)
        //    {
        //        // Assign the work shift to the employee
        //        // This assumes that the Employee class has a property to hold assigned work shifts
        //        employee.WorkShifts.Add(workShift);
        //    }
        //}

        //public void AddEmployee(Employee employee)
        //{
        //    employees.Add(employee);
        //}

        //public void RemoveEmployee(int employeeId)
        //{
        //    var employee = employees.FirstOrDefault(e => e.EmployeeId == employeeId);
        //    if (employee != null)
        //    {
        //        employees.Remove(employee);
        //    }
        //}

        //public Employee GetEmployee(int employeeId)
        //{
        //    return employees.FirstOrDefault(e => e.EmployeeId == employeeId);
        //}

        //public void UpdateEmployee(Employee updatedEmployee)
        //{
        //    var employee = employees.FirstOrDefault(e => e.EmployeeId == updatedEmployee.EmployeeId);
        //    if (employee != null)
        //    {
        //        // Update properties of the employee
        //        employee.Initials = updatedEmployee.Initials;
        //        employee.Name = updatedEmployee.Name;
        //        employee.Phone = updatedEmployee.Phone;
        //        employee.Email = updatedEmployee.Email;
        //    }
        //}

    }
}