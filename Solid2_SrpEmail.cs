using System;

// SOLID, завдання 2. Порушено SRP: EmailSender і відправляє листи, і вирішує,
// як вести лог (Console). Змінимо спосіб логування - доведеться змінювати EmailSender.
// Виправлення: виокремити логування в ILogger; EmailSender отримує його ззовні
// через конструктор (заодно виконується DIP: залежність від абстракції).
namespace LabPatterns.Solid2_SrpEmail
{
    class Email
    {
        public string Theme { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }

    // Відповідальність: ведення логу
    interface ILogger
    {
        void Log(string message);
    }

    class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    // Другий спосіб логування - EmailSender про це не знає і не змінюється
    class TimestampLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + message);
        }
    }

    // Відповідальність: лише відправка листів
    class EmailSender
    {
        private readonly ILogger logger;

        public EmailSender(ILogger logger)
        {
            if (logger == null) throw new ArgumentNullException("logger");
            this.logger = logger;
        }

        public void Send(Email email)
        {
            // ... sending ...
            logger.Log("Email from '" + email.From + "' to '" + email.To + "' was sent");
        }
    }

    public static class Demo
    {
        public static void Run()
        {
            Email e1 = new Email() { From = "Me", To = "Vasya", Theme = "Who are you?" };
            Email e2 = new Email() { From = "Vasya", To = "Me", Theme = "vacuum cleaners!" };
            Email e3 = new Email() { From = "Kolya", To = "Vasya", Theme = "No! Thanks!" };
            Email e4 = new Email() { From = "Vasya", To = "Me", Theme = "washing machines!" };
            Email e5 = new Email() { From = "Me", To = "Vasya", Theme = "Yes" };
            Email e6 = new Email() { From = "Vasya", To = "Petya", Theme = "+2" };

            Console.WriteLine("Лог у консоль:");
            EmailSender es = new EmailSender(new ConsoleLogger());
            es.Send(e1);
            es.Send(e2);
            es.Send(e3);

            Console.WriteLine("Той самий EmailSender з іншим логером:");
            EmailSender es2 = new EmailSender(new TimestampLogger());
            es2.Send(e4);
            es2.Send(e5);
            es2.Send(e6);
        }
    }
}
