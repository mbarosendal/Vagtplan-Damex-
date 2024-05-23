using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;

namespace Damex_Vagtplan.Models
{
    // Schedule-klassen repræsenterer en uges vagtplan.
    public class Schedule
    {
        // En dictionary der 'mapper' hver dag i ugen til en liste af vagter for den pågældende dag.
        // F.eks. har Monday vagterne: Morning, AFternooon og Evening, men Sunday har kun vagten: WholeDay.
        public Dictionary<DayOfWeek, List<WorkShift>> WeeklySchedule { get; set; }

        public Schedule()
        {
            WeeklySchedule = new Dictionary<DayOfWeek, List<WorkShift>>();
            // Initialiser vagterne for hele ugen.
            InitializeWeeklySchedule();
        }

        // Metode der initialiserer vagterne for hele ugen med antal timer de kræver og dagen, de er på.
        private void InitializeWeeklySchedule()
        {
            // Søndag.
            AddShift(DayOfWeek.Sunday, new WorkShift(13, DayOfWeek.Sunday, TimeSlot.WholeDay));

            // Mandag til fredag.
            for (int i = 1; i <= 5; i++)
            {
                // day bruges til at sikre, at vagten tilføjes til den rigtige dag i WeeklySchedule-dictionary'en.
                var day = (DayOfWeek)i;
                AddShift(day, new WorkShift(4, day, TimeSlot.Morning));
                AddShift(day, new WorkShift(4, day, TimeSlot.Morning));
                AddShift(day, new WorkShift(5, day, TimeSlot.Afternoon));
                AddShift(day, new WorkShift(4, day, TimeSlot.Evening));
            }

            // Lørdag.
            AddShift(DayOfWeek.Saturday, new WorkShift(13, DayOfWeek.Saturday, TimeSlot.WholeDay));
        }

        // En hjælpemetode, der bruges af InitializeWeeklySchedule til at tilføje en enkelt vagt til den ugentlige vagtplan.
        // Den sikrer, at vagterne tilføjes til den korrekte dag i ugen i WeeklySchedule-dictionary'en.
        // Hvis den pågældende dag ikke allerede er repræsenteret i WeeklySchedule, oprettes der en ny liste af vagter for den pågældende dag for at undgå fejl.
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
    }
}
