using System;

namespace Damex_Vagtplan.Models
{
    public class AvailableShift
    {
        public DayOfWeek Day { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public bool IsAvailable { get; set; }

        public AvailableShift(DayOfWeek day, TimeSlot timeSlot, bool isAvailable)
        {
            Day = day;
            TimeSlot = timeSlot;
            IsAvailable = isAvailable;
        }
    }
}
