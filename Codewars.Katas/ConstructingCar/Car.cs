using System;

namespace Codewars.Katas.ConstructingCar
{
    public class Car : ICar
    {
        public IFuelTankDisplay fuelTankDisplay;

        private IEngine _engine;

        private IFuelTank _fuelTank;

        public Car() : this(20.0d)
        {
            
        }

        public Car(double fuelLevel)
        {
            _fuelTank = new FuelTank(fuelLevel);

            _engine = new Engine(_fuelTank);

            fuelTankDisplay = new FuelTankDisplay(_fuelTank);
        }

        public bool EngineIsRunning
        {
            get
            {
                return _engine.IsRunning;
            }
        }

        public void EngineStart()
        {
            _engine.Start();
        }

        public void EngineStop()
        {
            _engine.Stop();
        }

        public void Refuel(double liters)
        {
            _fuelTank.Refuel(liters);
        }

        public void RunningIdle()
        {
            const double consumptionOnIdle = 0.0003;
            _engine.Consume(consumptionOnIdle);
        }
    }
}
