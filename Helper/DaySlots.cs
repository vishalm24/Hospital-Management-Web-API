namespace Hospital_Management.Helper
{
    public class DaySlots
    {
        public int Slot { get; set; }
        public TimeOnly SlotTime { get; set; }
        public bool IsBooked { get; set; } = false;
    }
    public static class DaySlotsData
    {
        public static List<DaySlots> GetTimeSlots()
        {
            var slots = new List<DaySlots>();
            TimeOnly startTime = new TimeOnly(10, 0);
            TimeOnly endTime = new TimeOnly(18, 0);
            int slotNumber = 1;
            while (startTime < endTime)
            {
                slots.Add(new DaySlots
                {
                    Slot = slotNumber,
                    SlotTime = startTime
                });
                startTime = startTime.AddMinutes(30);
                slotNumber++;
            }
            return slots;
        }
    }
}
