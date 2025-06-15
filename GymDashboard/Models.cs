namespace GymDashboard
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UsageEntry
    {
        public string EquipmentName { get; set; }
        public int DurationMinutes { get; set; }
        public string DateUsed { get; set; }
    }
}
