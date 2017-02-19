namespace Codewars.Katas.ConstructingCar
{
    public class Engine : IEngine
    {
        IFuelTank _fuelTank;

        public Engine(IFuelTank fuelTank)
        {
            _fuelTank = fuelTank;
        }

        public bool IsRunning
        {
            get; private set;
        }

        public void Consume(double liters)
        {
            if (!IsRunning)
                return;

            if (_fuelTank.FillLevel > liters)
                _fuelTank.Consume(liters);
            else
                IsRunning = false;
        }

        public void Start()
        {
            if (_fuelTank.FillLevel > 0.0d)
                IsRunning = true;
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }
}
