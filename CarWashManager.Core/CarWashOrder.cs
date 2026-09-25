namespace CarWashManager.Core
{
    public struct CarWashOrder
    {
        public string RegistrationNumber { get; set; }
        public VehicleType VehicleType { get; set; }
        public WashProgram WashProgram { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Price { get; set; }
        public int DurationMinutes { get; set; }
    }
}