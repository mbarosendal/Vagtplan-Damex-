using System;
using System.Collections.Generic;

namespace Damex_Vagtplan.Models
{
    public class Schedule
    {
        public Dictionary<DayOfWeek, List<WorkShift>> WeeklySchedule { get; set; }

        public Schedule()
        {
            WeeklySchedule = new Dictionary<DayOfWeek, List<WorkShift>>();
            InitializeWeeklySchedule();
        }

        private void InitializeWeeklySchedule()
        {
            // Initialize shifts for the entire week
            AddShift(DayOfWeek.Sunday, new WorkShift(13, DayOfWeek.Sunday, TimeSlot.WholeDay));

            for (int i = 1; i <= 5; i++) // Monday to Friday
            {
                var day = (DayOfWeek)i;
                AddShift(day, new WorkShift(4, day, TimeSlot.Morning));
                AddShift(day, new WorkShift(4, day, TimeSlot.Morning));
                AddShift(day, new WorkShift(5, day, TimeSlot.Afternoon));
                AddShift(day, new WorkShift(4, day, TimeSlot.Evening));
            }

            AddShift(DayOfWeek.Saturday, new WorkShift(13, DayOfWeek.Saturday, TimeSlot.WholeDay));
        }

        private void AddShift(DayOfWeek day, WorkShift shift)
        {
            if (!WeeklySchedule.ContainsKey(day))
            {
                WeeklySchedule[day] = new List<WorkShift>();
            }

            WeeklySchedule[day].Add(shift);
        }


        public Dictionary<DayOfWeek, List<WorkShift>> GetSchedule()
        {
            return WeeklySchedule;
        }

        public DayOfWeek GetDayOfWeek(TimeSlot timeSlot)
        {
            int index = (int)timeSlot;
            if (index < 20)
            {
                return (DayOfWeek)(index / 4);
            }
            return (DayOfWeek)(index - 20 + 5); // Saturday or Sunday
        }

    }
}
