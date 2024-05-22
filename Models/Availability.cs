using System.Collections.ObjectModel;

namespace Damex_Vagtplan.Models
{
    public class Availability
    {
        public int WorkedHours { get; set; }
        public int AvailableHours { get; set; } = 40;
        public ObservableCollection<AvailableShift> AvailableShifts { get; set; }

        //public bool ExceedsMaxWeeklyHours(int shiftHours)
        //{
        //    return WorkedHours + shiftHours > AvailableHours;
        //}

        //// Method to add an available shift
        //public void AddAvailableShift(AvailableShift shift)
        //{
        //    AvailableShifts.Add(shift);
        //}

        //// Method to remove an available shift
        //public void RemoveAvailableShift(AvailableShift shift)
        //{
        //    AvailableShifts.Remove(shift);
        //}    



    // Properties vi ikke bruger?

    //private int maxNumberofShifts = 6;

    //public int MaxNumberOfShifts
    //{
    //    get { return maxNumberofShifts; }
    //    set { maxNumberofShifts = value; }
    //}

    //private bool available;
    //public bool Available
    //{
    //    get { return available; }
    //    set { available = value; }
    //}

    //private DateTime whenAvailableAgain;
    //public DateTime WhenAvailableAgain
    //{
    //    get { return whenAvailableAgain; }
    //    set { whenAvailableAgain = value; }
    //}

    //private int preferredDays;
    //public int PreferredDays
    //{
    //	get { return preferredDays; }
    //	set { preferredDays = value; }
    //}

    //private int preferredShifts;
    //public int PreferredShifts
    //{
    //	get { return preferredShifts; }
    //	set { preferredShifts = value; }
    //}

}
}
