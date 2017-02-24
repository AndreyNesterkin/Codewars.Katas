using System;
using System.Linq;

namespace Codewars.Katas.ConstructingCar
{
    public class DrivingProcessor : IDrivingProcessor
    {
        private const int MaxSpeedLimit = 250;
        private const int MaxAcceptableAcceleration = 20;
        private const int MinAcceptableAcceleration = 5;
        private const int DefaultBreak = 10;
        private Tuple<int, int, double>[] _consumptionOnAccelerateTable = new Tuple<int, int, double>[]
           {
                new Tuple<int, int, double>(1, 60, 0.0020d),
                new Tuple<int, int, double>(61, 100, 0.0014d),
                new Tuple<int, int, double>(101, 140, 0.0020d),
                new Tuple<int, int, double>(141, 200, 0.0025d),
                new Tuple<int, int, double>(201, MaxSpeedLimit, 0.0030d)
           };

        private IEngine _engine;
        private int _maxAcceleration;

        public DrivingProcessor(IEngine engine, int maxAcceleration)
        {
            _engine = engine;

            _maxAcceleration = GetMaxAcceptableAcceleration(maxAcceleration);
        }

        private int GetMaxAcceptableAcceleration(int maxAcceleration)
        {
            if (maxAcceleration > MaxAcceptableAcceleration)
                return MaxAcceptableAcceleration;

            if (maxAcceleration < MinAcceptableAcceleration)
                return MinAcceptableAcceleration;

            return maxAcceleration;
        }

        public int ActualSpeed
        {
            get; private set;
        }

        public void IncreaseSpeedTo(int speed)
        {
            if (speed <= 0 || !_engine.IsRunning)
                return;

            Accelerate(speed);

            ConsumeFuelOnAccelerate(ActualSpeed);
        }

        private void Accelerate(int speed)
        {
            var acceleration = GetActualAcceleration(speed);

            ActualSpeed = LimitSpeed(ActualSpeed + acceleration);
        }

        private int GetActualAcceleration(int speed)
        {
            var requiredAcceleration = speed - ActualSpeed;

            return requiredAcceleration > _maxAcceleration ? _maxAcceleration : requiredAcceleration;
        }

        private void ConsumeFuelOnAccelerate(int actualSpeed)
        {
            var consumption = GetFuelConsumption(actualSpeed);

            _engine.Consume(consumption);
        }

        private double GetFuelConsumption(int speed)
        {
            return _consumptionOnAccelerateTable.First(t => speed >= t.Item1 && speed <= t.Item2).Item3;
        }

        public void ReduceSpeed(int speed)
        {
            if (ActualSpeed == 0)
                return;

            ActualSpeed -= GetActualBreak(speed);
        }

        private int GetActualBreak(int speed)
        {
            return speed > DefaultBreak ? DefaultBreak : speed;
        }

        private int LimitSpeed(int speed)
        {
            if (speed > MaxSpeedLimit)
                return MaxSpeedLimit;

            return speed;
        }
    }
}
