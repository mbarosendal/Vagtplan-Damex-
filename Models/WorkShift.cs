using System;

namespace Damex_Vagtplan.Models
{
    public enum TimeSlot
    {
        Morning,
        Afternoon,
        Evening,
        WholeDay
    }

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

    // Properties vi ikke bruger:

    //private int workShiftId;

    ///// <summary>
    ///// Gets or sets the Work Shift ID.
    ///// </summary>
    //public int WorkShiftId
    //{
    //    get { return workShiftId; }
    //    set { workShiftId = value; }
    //}

    //private DateTime shiftStart;

    ///// <summary>
    ///// Gets or sets the start time of the Work Shift.
    ///// </summary>
    //public DateTime ShiftStart
    //{
    //    get { return shiftStart; }
    //    set { shiftStart = value; }
    //}

    //private DateTime shiftEnd;

    ///// <summary>
    ///// Gets or sets the end time of the Work Shift.
    ///// </summary>
    //public DateTime ShiftEnd
    //{
    //    get { return shiftEnd; }
    //    set { shiftEnd = value; }
    //}

    //private bool covered;

    ///// <summary>
    ///// Gets or sets a value indicating whether the Work Shift is covered.
    ///// </summary>
    //public bool Covered
    //{
    //    get { return covered; }
    //    set { covered = value; }
    //}

    //private int employeeId;

    ///// <summary>
    ///// Gets or sets the Employee ID associated with the Work Shift.
    ///// </summary>
    //public int EmployeeId
    //{
    //    get { return employeeId; }
    //    set { employeeId = value; }
    //}

    //private string comment;

    ///// <summary>
    ///// Gets or sets the comment for the Work Shift.
    ///// </summary>
    ///
    //public string Comment
    //{
    //    get { return comment; }
    //    set { comment = value; }
    //}
    //// Constructor
    //public WorkShift(int hours)
    //{
    //    Hours = hours;
    //}
}
}
