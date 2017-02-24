using System;
using System.Linq;

namespace Codewars.Katas.ConstructingCar
{
    public class Car : ICar
    {
        private const int DefaultAcceleration = 10;
        private const int MaxAcceptableAcceleration = 20;
        private const int MinAcceptableAcceleration = 5;
        private const int DefaultBreak = 10;
        private const int FreeWheelSlowingDown = 1;
        private const int MaxSpeedLimit = 250;

        private Tuple<int, int, double>[] _consumptionOnAccelerateTable = new Tuple<int, int, double>[]
        {
            new Tuple<int, int, double>(1, 60, 0.0020d),
            new Tuple<int, int, double>(61, 100, 0.0014d),
            new Tuple<int, int, double>(101, 140, 0.0020d),
            new Tuple<int, int, double>(141, 200, 0.0025d),
            new Tuple<int, int, double>(201, MaxSpeedLimit, 0.0030d)
        };

        private const double DefaultFuelLevel = 20.0d;
        private const double ConsumptionOnIdle = 0.0003d;

        private int _maxAcceleration;

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

            _maxAcceleration = GetMaxAcceptableAcceleration(maxAcceleration);

            _drivingProcessor = new DrivingProcessor();

            drivingInformationDisplay = new DrivingInformationDisplay(_drivingProcessor);
        }

        private int GetMaxAcceptableAcceleration(int maxAcceleration)
        {
            if (maxAcceleration > MaxAcceptableAcceleration)
                return MaxAcceptableAcceleration;

            if (maxAcceleration < MinAcceptableAcceleration)
                return MinAcceptableAcceleration;

            return maxAcceleration;
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
            if (_drivingProcessor.ActualSpeed == 0)
                return;

            var brake = GetActualBreak(speed);

            _drivingProcessor.ReduceSpeed(brake);
        }

        private int GetActualBreak(int speed)
        {
            return speed > DefaultBreak ? DefaultBreak : speed;
        }

        public void Accelerate(int speed)
        {
            if (speed <= 0 || !_engine.IsRunning)
                return;

            speed = LimitSpeed(speed);

            if (speed >= _drivingProcessor.ActualSpeed)
                AccelerateTo(speed);
            else
                FreeWheel();
        }

        private void AccelerateTo(int speed)
        {
            IncreaseSpeedTo(speed);

            ConsumeFuelOnAccelerate();
        }

        private int LimitSpeed(int speed)
        {
            if (speed > MaxSpeedLimit)
                return MaxSpeedLimit;

            return speed;
        }

        private void IncreaseSpeedTo(int speed)
        {
            var acceleration = GetActualAcceleration(speed);

            _drivingProcessor.IncreaseSpeedTo(acceleration);
        }

        private int GetActualAcceleration(int speed)
        {
            var requiredAcceleration = speed - _drivingProcessor.ActualSpeed;

            return requiredAcceleration > _maxAcceleration ? _maxAcceleration : requiredAcceleration;
        }

        private void ConsumeFuelOnAccelerate()
        {
            var consumption = GetConsumptionOnAccelerate();

            _engine.Consume(consumption);
        }

        private double GetConsumptionOnAccelerate()
        {
            var speed = _drivingProcessor.ActualSpeed;

            return _consumptionOnAccelerateTable.First(t => speed >= t.Item1 && speed <= t.Item2).Item3;
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
