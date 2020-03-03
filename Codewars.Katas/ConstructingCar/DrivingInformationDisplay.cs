namespace Codewars.Katas.ConstructingCar
{
    public class DrivingInformationDisplay : IDrivingInformationDisplay
    {
        private readonly IDrivingProcessor _drivingProcessor;

        public DrivingInformationDisplay(IDrivingProcessor drivingProcessor)
        {
            _drivingProcessor = drivingProcessor;
        }

        public int ActualSpeed => _drivingProcessor.ActualSpeed;
    }
}
