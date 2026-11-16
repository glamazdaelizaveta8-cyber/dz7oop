using System;

// Завдання 1. Abstract Factory: додано конкретну фабрику Mercedes
// (клас Mercedes, клас MercedesEngine, клас MercedesFactory).
namespace LabPatterns.Task1_AbstractFactory
{
    // AbstractProductA
    abstract class Car
    {
        public abstract void Info();
    }

    // ConcreteProductA1
    class Ford : Car
    {
        public override void Info()
        {
            Console.WriteLine("Ford");
        }
    }

    // ConcreteProductA2
    class Toyota : Car
    {
        public override void Info()
        {
            Console.WriteLine("Toyota");
        }
    }

    // ConcreteProductA3 (НОВИЙ)
    class Mercedes : Car
    {
        public override void Info()
        {
            Console.WriteLine("Mercedes");
        }
    }

    // AbstractProductB
    abstract class Engine
    {
        public abstract void GetPower();
    }

    // ConcreteProductB1
    class FordEngine : Engine
    {
        public override void GetPower()
        {
            Console.WriteLine("Ford Engine 4.4");
        }
    }

    // ConcreteProductB2
    class ToyotaEngine : Engine
    {
        public override void GetPower()
        {
            Console.WriteLine("Toyota Engine 3.2");
        }
    }

    // ConcreteProductB3 (НОВИЙ)
    class MercedesEngine : Engine
    {
        public override void GetPower()
        {
            Console.WriteLine("Mercedes Engine 4.0");
        }
    }

    // AbstractFactory
    interface ICarFactory
    {
        Car CreateCar();
        Engine CreateEngine();
    }

    // ConcreteFactory1
    class FordFactory : ICarFactory
    {
        public Car CreateCar() { return new Ford(); }
        public Engine CreateEngine() { return new FordEngine(); }
    }

    // ConcreteFactory2
    class ToyotaFactory : ICarFactory
    {
        public Car CreateCar() { return new Toyota(); }
        public Engine CreateEngine() { return new ToyotaEngine(); }
    }

    // ConcreteFactory3 (НОВА)
    class MercedesFactory : ICarFactory
    {
        public Car CreateCar() { return new Mercedes(); }
        public Engine CreateEngine() { return new MercedesEngine(); }
    }

    public static class Demo
    {
        // Клієнтський код працює лише з абстракціями (ICarFactory, Car, Engine)
        // і нічого не знає про конкретні марки.
        static void Assemble(ICarFactory carFactory)
        {
            Car myCar = carFactory.CreateCar();
            myCar.Info();
            Engine myEngine = carFactory.CreateEngine();
            myEngine.GetPower();
        }

        public static void Run()
        {
            Assemble(new ToyotaFactory());
            Assemble(new FordFactory());
            Assemble(new MercedesFactory());
        }
    }
}
