﻿namespace Codewars.Katas.ConstructingCar
{
    public class Car : ICar
    {
        private const int DefaultAcceleration = 10;
        private const int FreeWheelSlowingDown = 1;

        private const double DefaultFuelLevel = 20.0d;
        private const double ConsumptionOnIdle = 0.0003d;

        private IEngine _engine;

        private IFuelTank _fuelTank;

        private IDrivingProcessor _drivingProcessor;

        public IFuelTankDisplay fuelTankDisplay;

        public IDrivingInformationDisplay drivingInformationDisplay;
        

        public Car() : this(DefaultFuelLevel, DefaultAcceleration)
        {
        }

        public Car(double fuelLevel) : this(fuelLevel, DefaultAcceleration)
        {
        }

        public Car(double fuelLevel, int maxAcceleration)
        {
            _fuelTank = new FuelTank(fuelLevel);

            _engine = new Engine(_fuelTank);

            fuelTankDisplay = new FuelTankDisplay(_fuelTank);

            _drivingProcessor = new DrivingProcessor(_engine, maxAcceleration);

            drivingInformationDisplay = new DrivingInformationDisplay(_drivingProcessor);
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
            _engine.Consume(ConsumptionOnIdle);
        }

        public void BrakeBy(int speed)
        {
            _drivingProcessor.ReduceSpeed(speed);
        }

        public void Accelerate(int speed)
        {
            if (speed >= _drivingProcessor.ActualSpeed)
                _drivingProcessor.IncreaseSpeedTo(speed);
            else
                FreeWheel();
        }

        public void FreeWheel()
        {
            if (_drivingProcessor.ActualSpeed == 0)
                RunningIdle();
            else
                _drivingProcessor.ReduceSpeed(FreeWheelSlowingDown);
        }
    }
}
