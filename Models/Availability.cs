using System.Collections.ObjectModel;

namespace Damex_Vagtplan.Models
{
    // Holder styr på en medarbejders tilgængelighedsdata, arbejdstimer og tilgængelige vagter.
    public class Availability
    {
        // Antal timer, medarbejderen har arbejdet i løbet af ugen.
        public int WorkedHours { get; set; }

        // Det maksimale antal timer, medarbejderen er tilgængelig til at arbejde i løbet af ugen.
        public int AvailableHours { get; set; } = 40;
    }
}
