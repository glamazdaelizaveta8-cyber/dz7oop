using System;
using System.Text;

namespace LabPatterns
{
    static class Program
    {
        static void Header(string title)
        {
            Console.WriteLine();
            Console.WriteLine("=== " + title + " ===");
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Header("Завдання 1. Abstract Factory (+ Mercedes)");
            Task1_AbstractFactory.Demo.Run();

            Header("Завдання 2. Builder (+ MargaritaPizzaBuilder)");
            Task2_Builder.Demo.Run();

            Header("Завдання 3. Mediator (+ ConcreteColleague3)");
            Task3_Mediator.Demo.Run();

            Header("Завдання 4. Decorator (Ялинка)");
            Task4_Decorator.Demo.Run();

            Header("Завдання 5. Prototype (+ Triangle)");
            Task5_Prototype.Demo.Run();

            Header("Завдання 6а. Factory Method (+ PdfCreator)");
            Task6a_FactoryMethod.Demo.Run();

            Header("Завдання 6б. Adapter (+ Horse)");
            Task6b_Adapter.Demo.Run();

            Header("SOLID 1. SRP - замовлення (Order)");
            Solid1_SrpOrder.Demo.Run();

            Header("SOLID 2. SRP - EmailSender та логування");
            Solid2_SrpEmail.Demo.Run();

            Header("SOLID 3. LSP - Rectangle / Square");
            Solid3_Lsp.Demo.Run();

            Header("SOLID 4. ISP - IItem, Book, OuterWear");
            Solid4_Isp.Demo.Run();

            Console.WriteLine();
            Console.WriteLine("Готово. Натисніть Enter для виходу...");
            Console.ReadLine();
        }
    }
}
