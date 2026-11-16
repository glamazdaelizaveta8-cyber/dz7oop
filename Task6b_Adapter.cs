using System;

// Завдання 6 (шаблон 2). Adapter: Водій вміє їздити лише на ITransport.
// Є клас Camel (верблюд) з адаптером; додано клас Horse та HorseToTransportAdapter.
namespace LabPatterns.Task6b_Adapter
{
    // Target - інтерфейс, який очікує клієнт
    interface ITransport
    {
        void Drive();
    }

    // звичайний сумісний клас
    class Car : ITransport
    {
        public void Drive()
        {
            Console.WriteLine("  Автомобіль їде по дорозі");
        }
    }

    // Client
    class Driver
    {
        public void Travel(ITransport transport)
        {
            transport.Drive();
        }
    }

    // Adaptee 1 - має ІНШИЙ інтерфейс (Move замість Drive)
    class Camel
    {
        public void Move()
        {
            Console.WriteLine("  Верблюд іде по пустелі");
        }
    }

    // Adaptee 2 (НОВИЙ) - ще один інтерфейс (Gallop)
    class Horse
    {
        public void Gallop()
        {
            Console.WriteLine("  Кінь скаче галопом");
        }
    }

    // Adapter 1
    class CamelToTransportAdapter : ITransport
    {
        private readonly Camel camel;

        public CamelToTransportAdapter(Camel camel)
        {
            this.camel = camel;
        }

        public void Drive()
        {
            camel.Move();
        }
    }

    // Adapter 2 (НОВИЙ)
    class HorseToTransportAdapter : ITransport
    {
        private readonly Horse horse;

        public HorseToTransportAdapter(Horse horse)
        {
            this.horse = horse;
        }

        public void Drive()
        {
            horse.Gallop();
        }
    }

    public static class Demo
    {
        public static void Run()
        {
            Driver driver = new Driver();

            Console.WriteLine("Їдемо на автомобілі:");
            driver.Travel(new Car());

            Console.WriteLine("Їдемо на верблюді (через адаптер):");
            driver.Travel(new CamelToTransportAdapter(new Camel()));

            Console.WriteLine("Їдемо на коні (через адаптер):");
            driver.Travel(new HorseToTransportAdapter(new Horse()));
        }
    }
}
