using Codewars.Katas.ConstructingCar;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
}