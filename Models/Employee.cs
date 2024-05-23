using System.Collections.Generic;
using System.Windows.Media;

namespace Damex_Vagtplan.Models
{
    // Representerer en medarbejder i virksomheden.
    public class Employee
    {
        public string Initials { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        // Farvekoden til visning af medarbejderen i brugergrænsefladen.
        public SolidColorBrush Color { get; set; }
        // Medarbejderens tilgængelighed, herunder deres arbejdede timer, maks timer, og vagter, de kan tage.
        public Availability Availability { get; set; }
        // En liste over tilgængelige vagter for medarbejderen.
        public List<AvailableShift> EmployeeAvailableShifts { get; set; }

        public Employee(
            string initials,
            string name,
            string phone,
            string email,
            SolidColorBrush color,
            Availability availability,
            List<AvailableShift> employeeAvailableShifts = null)
        {
            Initials = initials;
            Name = name;
            Phone = phone;
            Email = email;
            Color = color;
            Availability = availability;
            EmployeeAvailableShifts = employeeAvailableShifts ?? new List<AvailableShift>();
        }
    }
}
