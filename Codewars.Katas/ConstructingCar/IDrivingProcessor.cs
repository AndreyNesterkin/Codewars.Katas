namespace Codewars.Katas.ConstructingCar
{
    public interface IDrivingProcessor
    {
        int ActualSpeed { get; }

        void IncreaseSpeedTo(int speed);

        void ReduceSpeed(int speed);
    }
}
