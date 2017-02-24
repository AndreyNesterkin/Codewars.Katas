using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codewars.Katas.ConstructingCar
{
    public class DrivingInformationDisplay : IDrivingInformationDisplay
    {
        private IDrivingProcessor _drivingProcessor;

        public DrivingInformationDisplay(IDrivingProcessor drivingProcessor)
        {
            _drivingProcessor = drivingProcessor;
        }

        public int ActualSpeed
        {
            get
            {
                return _drivingProcessor.ActualSpeed;
            }
        }
    }
}
