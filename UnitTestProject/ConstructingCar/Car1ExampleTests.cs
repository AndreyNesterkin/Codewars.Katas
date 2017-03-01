using Codewars.Katas.ConstructingCar;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

[TestClass]
public class Car1ExampleTests
{
    [TestMethod]
    public void TestMotorStartAndStop()
    {
        var car = new Car();

        Assert.IsFalse(car.EngineIsRunning, "Engine could not be running.");

        car.EngineStart();

        Assert.IsTrue(car.EngineIsRunning, "Engine should be running.");

        car.EngineStop();

        Assert.IsFalse(car.EngineIsRunning, "Engine could not be running.");
    }

    [TestMethod]
    public void TestFuelConsumptionOnIdle()
    {
        var car = new Car(1);

        car.EngineStart();

        Enumerable.Range(0, 3000).ToList().ForEach(s => car.RunningIdle());

        Assert.AreEqual(0.10, car.fuelTankDisplay.FillLevel, "Wrong fuel tank fill level!");
    }

    [TestMethod]
    public void TestFuelTankDisplayIsComplete()
    {
        var car = new Car(60);

        Assert.IsTrue(car.fuelTankDisplay.IsComplete, "Fuel tank must be complete!");
    }

    [TestMethod]
    public void TestFuelTankDisplayIsOnReserve()
    {
        var car = new Car(4);

        Assert.IsTrue(car.fuelTankDisplay.IsOnReserve, "Fuel tank must be on reserve!");
    }

    [TestMethod]
    public void TestRefuel()
    {
        var car = new Car(5);

        car.Refuel(40);

        Assert.AreEqual(45, car.fuelTankDisplay.FillLevel, "Wrong fuel tank fill level!");
    }

    [TestMethod]
    public void DefaultFuelLevel()
    {
        var car = new Car();

        Assert.AreEqual(20, car.fuelTankDisplay.FillLevel);
    }

    [TestMethod]
    public void TestFuelLevelAllowedUpTo60()
    {
        var car = new Car();

        car.Refuel(65);

        Assert.AreEqual(60, car.fuelTankDisplay.FillLevel);

        car = new Car(65);

        Assert.AreEqual(60, car.fuelTankDisplay.FillLevel);
    }

    [TestMethod]
    public void TestEngineStopsCauseOfNoFuelExactly()
    {
        var car = new Car(0.0003);

        car.EngineStart();

        car.RunningIdle();

        Assert.IsFalse(car.EngineIsRunning);
    }

    [TestMethod]
    public void TestNoNegativeFuelLevelAllowed()
    {
        var car = new Car(-1);

        Assert.AreEqual(0, car.fuelTankDisplay.FillLevel);
    }

    [TestMethod]
    public void TestNoConsumptionWhenEngineNotRunning()
    {
        var car = new Car();

        var startLevel = car.fuelTankDisplay.FillLevel;

        Enumerable.Range(0, 3000).ToList().ForEach(s => car.RunningIdle());

        Assert.AreEqual(startLevel, car.fuelTankDisplay.FillLevel);
    }

    [TestMethod]
    public void TestMotorDoesntStartWithEmptyFuelTank()
    {
        var car = new Car(0.0d);

        car.EngineStart();

        Assert.IsFalse(car.EngineIsRunning);
    }

    [TestMethod]
    public void TestStartSpeed()
    {
        var car = new Car();

        car.EngineStart();

        Assert.AreEqual(0, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
    }

    [TestMethod]
    public void TestFreeWheelSpeed()
    {
        var car = new Car();

        car.EngineStart();

        Enumerable.Range(0, 10).ToList().ForEach(s => car.Accelerate(100));

        Assert.AreEqual(100, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");

        car.FreeWheel();
        car.FreeWheel();
        car.FreeWheel();

        Assert.AreEqual(97, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
    }

    [TestMethod]
    public void TestAccelerateBy10()
    {
        var car = new Car();

        car.EngineStart();

        Enumerable.Range(0, 10).ToList().ForEach(s => car.Accelerate(100));

        car.Accelerate(160);
        Assert.AreEqual(110, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
        car.Accelerate(160);
        Assert.AreEqual(120, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
        car.Accelerate(160);
        Assert.AreEqual(130, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
        car.Accelerate(160);
        Assert.AreEqual(140, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
        car.Accelerate(145);
        Assert.AreEqual(145, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
    }

    [TestMethod]
    public void TestBraking()
    {
        var car = new Car();

        car.EngineStart();

        Enumerable.Range(0, 10).ToList().ForEach(s => car.Accelerate(100));

        car.BrakeBy(20);

        Assert.AreEqual(90, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");

        car.BrakeBy(10);

        Assert.AreEqual(80, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
    }

    [TestMethod]
    public void TestConsumptionSpeedUpTo30()
    {
        var car = new Car(1, 20);

        car.EngineStart();

        car.Accelerate(30);
        car.Accelerate(30);
        car.Accelerate(30);
        car.Accelerate(30);
        car.Accelerate(30);
        car.Accelerate(30);
        car.Accelerate(30);
        car.Accelerate(30);
        car.Accelerate(30);
        car.Accelerate(30);

        Assert.AreEqual(0.98, car.fuelTankDisplay.FillLevel, "Wrong fuel tank fill level!");
    }

    [TestMethod]
    public void TestCustomAccelerationGreaterThan20()
    {
        const int startFuelLevel = 20;
        const int maxAcceleration = 100;

        var car = new Car(startFuelLevel, maxAcceleration);

        car.EngineStart();

        car.Accelerate(100);

        Assert.AreEqual(20, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
    }

    [TestMethod]
    public void TestCustomAccelerationLesserThan5()
    {
        const int startFuelLevel = 20;
        const int maxAcceleration = 1;

        var car = new Car(startFuelLevel, maxAcceleration);

        car.EngineStart();

        car.Accelerate(100);

        Assert.AreEqual(5, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
    }

    [TestMethod]
    public void NegativeSpeedAccelerateShouldNotDrive()
    {
        var car = new Car();

        car.EngineStart();

        car.Accelerate(-10);

        Assert.AreEqual(0, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
    }

    [TestMethod]
    public void ZeroSpeedAccelerateShouldNotDrive()
    {
        var car = new Car();

        car.EngineStart();

        car.Accelerate(0);

        Assert.AreEqual(0, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
    }

    [TestMethod]
    public void UpperMaxSpeedAccelerateShouldNotAccelerate()
    {
        var car = new Car();

        car.EngineStart();

        Enumerable.Range(0, 26).ToList().ForEach(s => car.Accelerate(260));

        Assert.AreEqual(250, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
    }

    [TestMethod]
    public void TestNoAccelerationWhenEngineNotRunning()
    {
        var car = new Car();

        car.Accelerate(100);

        Assert.AreEqual(0, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
    }

    [TestMethod]
    public void TestFreeWheelNoSpeedReduceWhenNoMoving()
    {
        var car = new Car();

        car.EngineStart();

        car.FreeWheel();

        Assert.AreEqual(0, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
    }

    [TestMethod]
    public void TestBrakeOnlyOver0()
    {
        var car = new Car();

        car.EngineStart();

        car.BrakeBy(10);

        Assert.AreEqual(0, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
    }

    [TestMethod]
    public void TestConsumptionAsRunIdleWhenFreeWheelingAt0()
    {
        var car = new Car(1);

        car.EngineStart();

        Enumerable.Range(0, 1000).ToList().ForEach(s => car.FreeWheel());

        Assert.AreEqual(0.7, car.fuelTankDisplay.FillLevel, "Wrong fuel level!");
    }

    [TestMethod]
    public void TestAccelerateLowerThanActualSpeed()
    {
        var car = new Car();

        car.EngineStart();

        Enumerable.Range(0, 10).ToList().ForEach(s => car.Accelerate(100));

        car.Accelerate(30);

        Assert.AreEqual(99, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
    }

    [TestMethod]
    public void TestRandom()
    {
        var car = new Car(20, 10);

        car.EngineStart();

        Enumerable.Range(0, 10).ToList().ForEach(s => car.Accelerate(250));

        car.BrakeBy(7);

        Enumerable.Range(0, 15).ToList().ForEach(s => car.FreeWheel());

        car.Accelerate(87);

        Assert.AreEqual(87, car.drivingInformationDisplay.ActualSpeed, "Wrong actual speed!");
    }
}