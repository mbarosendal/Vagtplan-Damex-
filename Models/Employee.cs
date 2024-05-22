using System.Collections.Generic;
using System.Windows.Media;

namespace Damex_Vagtplan.Models
{
    public class Employee
    {
        public string Initials { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public SolidColorBrush Color { get; set; }
        public Availability Availability { get; set; }
        public List<AvailableShift> EmployeeAvailableShifts { get; set; }

        public Employee(
            string initials,
            string name,
            string phone,
            string email,
            SolidColorBrush color,
            Availability availability,
            List<AvailableShift> employeeAvailableShifts = null) // Provide a default value for availableShifts
        {
            Initials = initials;
            Name = name;
            Phone = phone;
            Email = email;
            Color = color;
            Availability = availability;
            EmployeeAvailableShifts = employeeAvailableShifts ?? new List<AvailableShift>(); // Initialize AvailableShifts with an empty list if null
        }
    }
}
