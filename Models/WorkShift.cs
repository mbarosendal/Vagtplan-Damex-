using System;

namespace Damex_Vagtplan.Models
{
    // Enum til tidspunktet på dagen for vagten.
    public enum TimeSlot
    {
        Morning,
        Afternoon,
        Evening,
        WholeDay
    }

    // WorkShift-klassen repræsenterer en enkelt arbejdsvagt.
    public class WorkShift
    {
        public int Hours { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSlot TimeSlot { get; set; }

        public WorkShift(int hours, DayOfWeek day, TimeSlot timeSlot)
        {
            Hours = hours;
            Day = day;
            TimeSlot = timeSlot;
        }    
    }
}
