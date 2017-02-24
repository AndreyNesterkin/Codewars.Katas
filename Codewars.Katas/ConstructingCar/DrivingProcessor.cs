namespace Codewars.Katas.ConstructingCar
{
    public class DrivingProcessor : IDrivingProcessor
    {
        public int ActualSpeed
        {
            get; private set;
        }

        public void IncreaseSpeedTo(int speed)
        {
            ActualSpeed += speed;
        }

        public void ReduceSpeed(int speed)
        {
            ActualSpeed -= speed;
        }
    }
}
