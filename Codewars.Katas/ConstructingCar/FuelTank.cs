namespace Codewars.Katas.ConstructingCar
{
    public class FuelTank : IFuelTank
    {
        private double _maxFuelTankSize = 60.0d;
        private double _reserveLevel = 5.0d;

        public FuelTank(double fuelLevel)
        {
            Refuel(fuelLevel);
        }

        public double FillLevel
        {
            get; private set;
        }

        public bool IsComplete => FillLevel == _maxFuelTankSize;

        public bool IsOnReserve => FillLevel < _reserveLevel;

        public void Consume(double liters)
        {
            FillLevel -= liters;
        }

        public void Refuel(double liters)
        {
            var fuelLevel = FillLevel + liters;

            if (fuelLevel > _maxFuelTankSize)
                fuelLevel = _maxFuelTankSize;

            if (fuelLevel < 0.0d)
                fuelLevel = 0.0d;

            FillLevel = fuelLevel;
        }
    }
}
