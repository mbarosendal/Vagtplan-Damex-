using System;

namespace Damex_Vagtplan.Models
{
    // Representerer en tilgængelig vagt for en medarbejder på en bestemt dag og tidspunkt.
    public class AvailableShift
    {
        // Dagen i ugen, hvor vagten er tilgængelig.
        public DayOfWeek Day { get; set; }

        // Tidsintervallet for vagten (f.eks. morgen, eftermiddag, aften, hel dag).
        public TimeSlot TimeSlot { get; set; }

        // Angiver om medarbejderen er tilgængelig for denne vagt.
        public bool IsAvailable { get; set; }

        // Initialiserer en ny instans af AvailableShift med de angivne værdier.
        public AvailableShift(DayOfWeek day, TimeSlot timeSlot, bool isAvailable)
        {
            Day = day;
            TimeSlot = timeSlot;
            IsAvailable = isAvailable;
        }
    }
}
