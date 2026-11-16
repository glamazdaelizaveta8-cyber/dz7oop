using System;

// Завдання 3. Mediator: додано ConcreteColleague3, який надсилає повідомлення "Hello!".
namespace LabPatterns.Task3_Mediator
{
    // Mediator
    abstract class Mediator
    {
        public abstract void Send(string message, Colleague colleague);
    }

    // ConcreteMediator
    class ConcreteMediator : Mediator
    {
        private ConcreteColleague1 colleague1;
        private ConcreteColleague2 colleague2;
        private ConcreteColleague3 colleague3;

        public ConcreteColleague1 Colleague1
        {
            set { colleague1 = value; }
        }

        public ConcreteColleague2 Colleague2
        {
            set { colleague2 = value; }
        }

        // НОВА властивість
        public ConcreteColleague3 Colleague3
        {
            set { colleague3 = value; }
        }

        // Повідомлення отримують ВСІ колеги, крім відправника.
        public override void Send(string message, Colleague colleague)
        {
            if (ReferenceEquals(colleague, colleague1))
            {
                colleague2.Notify(message);
                colleague3.Notify(message);
            }
            else if (ReferenceEquals(colleague, colleague2))
            {
                colleague1.Notify(message);
                colleague3.Notify(message);
            }
            else
            {
                colleague1.Notify(message);
                colleague2.Notify(message);
            }
        }
    }

    // Colleague
    abstract class Colleague
    {
        protected Mediator mediator;

        public Colleague(Mediator mediator)
        {
            this.mediator = mediator;
        }

        public void Send(string message)
        {
            mediator.Send(message, this);
        }

        public abstract void Notify(string message);
    }

    // ConcreteColleague1
    class ConcreteColleague1 : Colleague
    {
        public ConcreteColleague1(Mediator mediator) : base(mediator) { }

        public override void Notify(string message)
        {
            Console.WriteLine("Colleague1 gets message: " + message);
        }
    }

    // ConcreteColleague2
    class ConcreteColleague2 : Colleague
    {
        public ConcreteColleague2(Mediator mediator) : base(mediator) { }

        public override void Notify(string message)
        {
            Console.WriteLine("Colleague2 gets message: " + message);
        }
    }

    // ConcreteColleague3 (НОВИЙ)
    class ConcreteColleague3 : Colleague
    {
        public ConcreteColleague3(Mediator mediator) : base(mediator) { }

        public override void Notify(string message)
        {
            Console.WriteLine("Colleague3 gets message: " + message);
        }
    }

    public static class Demo
    {
        public static void Run()
        {
            ConcreteMediator m = new ConcreteMediator();
            ConcreteColleague1 c1 = new ConcreteColleague1(m);
            ConcreteColleague2 c2 = new ConcreteColleague2(m);
            ConcreteColleague3 c3 = new ConcreteColleague3(m);

            m.Colleague1 = c1;
            m.Colleague2 = c2;
            m.Colleague3 = c3;

            m.Send("How are you?", c1);
            m.Send("Fine, thanks", c2);
            c3.Send("Hello!");   // повідомлення від третього колеги
        }
    }
}
