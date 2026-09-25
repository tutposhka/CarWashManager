namespace CarWashManager.Core
{
    public static class CarWashLogic
    {
        public static decimal GetPrice(VehicleType vehicleType, WashProgram washProgram)
        {
            return (vehicleType, washProgram) switch
            {
                (VehicleType.PassengerCar, WashProgram.Basic) => 10m,
                (VehicleType.PassengerCar, WashProgram.Standard) => 15m,
                (VehicleType.PassengerCar, WashProgram.Premium) => 20m,

                (VehicleType.SUV, WashProgram.Basic) => 12m,
                (VehicleType.SUV, WashProgram.Standard) => 18m,
                (VehicleType.SUV, WashProgram.Premium) => 24m,

                (VehicleType.Van, WashProgram.Basic) => 15m,
                (VehicleType.Van, WashProgram.Standard) => 22m,
                (VehicleType.Van, WashProgram.Premium) => 30m,

                _ => throw new ArgumentException()
            };
        }

        public static int GetDurationMinutes(VehicleType vehicleType, WashProgram washProgram)
        {
            return (vehicleType, washProgram) switch
            {
                (VehicleType.PassengerCar, WashProgram.Basic) => 15,
                (VehicleType.PassengerCar, WashProgram.Standard) => 25,
                (VehicleType.PassengerCar, WashProgram.Premium) => 35,

                (VehicleType.SUV, WashProgram.Basic) => 20,
                (VehicleType.SUV, WashProgram.Standard) => 30,
                (VehicleType.SUV, WashProgram.Premium) => 40,

                (VehicleType.Van, WashProgram.Basic) => 25,
                (VehicleType.Van, WashProgram.Standard) => 35,
                (VehicleType.Van, WashProgram.Premium) => 50,

                _ => throw new ArgumentException()
            };
        }

        public static void ValidateRegistrationNumber(string registrationNumber)
        {
            if (string.IsNullOrWhiteSpace(registrationNumber))
            {
                throw new ArgumentException();
            }

            if (registrationNumber.Length < 3 || registrationNumber.Length > 10)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public static decimal GetTotalPrice(CarWashOrder[] orders, int count)
        {
            decimal total = 0;

            for (int i = 0; i < count; i++)
            {
                total += orders[i].Price;
            }

            return total;
        }

        public static int GetActiveQueueDuration(CarWashOrder[] orders, int count)
        {
            int duration = 0;

            for (int i = 0; i < count; i++)
            {
                if (orders[i].Status != OrderStatus.Completed)
                {
                    duration += orders[i].DurationMinutes;
                }
            }

            return duration;
        }

        public static DateTime GetEstimatedFinishTime(CarWashOrder[] orders, int count)
        {
            int activeDuration = GetActiveQueueDuration(orders, count);

            return DateTime.Now.AddMinutes(activeDuration);
        }
    }
}