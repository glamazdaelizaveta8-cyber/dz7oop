using System;

// Завдання 4. Decorator: клас "Ялинка", який можна декорувати
// прикрасами (поля) та/або гірляндою (метод Shine - ялинка світиться).
namespace LabPatterns.Task4_Decorator
{
    // Component
    abstract class ChristmasTree
    {
        public abstract string GetDescription();
        public abstract int GetOrnamentCount();
        public abstract void Shine();
    }

    // ConcreteComponent - "чиста" ялинка без прикрас і без гірлянди
    class FirTree : ChristmasTree
    {
        public override string GetDescription() { return "Ялинка"; }
        public override int GetOrnamentCount() { return 0; }
        public override void Shine()
        {
            Console.WriteLine("  Ялинка не світиться (гірлянди немає).");
        }
    }

    // Decorator - тримає посилання на обгорнутий об'єкт і за замовчуванням делегує йому все
    abstract class TreeDecorator : ChristmasTree
    {
        protected ChristmasTree tree;

        protected TreeDecorator(ChristmasTree tree)
        {
            this.tree = tree;
        }

        public override string GetDescription() { return tree.GetDescription(); }
        public override int GetOrnamentCount() { return tree.GetOrnamentCount(); }
        public override void Shine() { tree.Shine(); }
    }

    // ConcreteDecoratorA - прикраси (додає ПОЛЯ: назва прикраси і кількість)
    class OrnamentDecorator : TreeDecorator
    {
        private readonly string ornamentName;
        private readonly int count;

        public OrnamentDecorator(ChristmasTree tree, string ornamentName, int count)
            : base(tree)
        {
            this.ornamentName = ornamentName;
            this.count = count;
        }

        public override string GetDescription()
        {
            return tree.GetDescription() + string.Format(" + {0} (x{1})", ornamentName, count);
        }

        public override int GetOrnamentCount()
        {
            return tree.GetOrnamentCount() + count;
        }
    }

    // ConcreteDecoratorB - гірлянда (додає МЕТОД: ялинка може світитися)
    class GarlandDecorator : TreeDecorator
    {
        private readonly string color;

        public GarlandDecorator(ChristmasTree tree, string color)
            : base(tree)
        {
            this.color = color;
        }

        public override string GetDescription()
        {
            return tree.GetDescription() + string.Format(" + гірлянда ({0})", color);
        }

        public override void Shine()
        {
            Console.WriteLine("  Гірлянда ({0}) увімкнена - ялинка світиться!", color);
        }
    }

    public static class Demo
    {
        static void Show(string title, ChristmasTree tree)
        {
            Console.WriteLine(title);
            Console.WriteLine("  Опис: " + tree.GetDescription());
            Console.WriteLine("  Кількість прикрас: " + tree.GetOrnamentCount());
            tree.Shine();
        }

        public static void Run()
        {
            // 1) без декорування
            ChristmasTree plain = new FirTree();
            Show("1) Проста ялинка:", plain);

            // 2) тільки прикраси
            ChristmasTree withOrnaments = new OrnamentDecorator(new FirTree(), "червоні кульки", 12);
            Show("2) Ялинка з прикрасами:", withOrnaments);

            // 3) тільки гірлянда
            ChristmasTree withGarland = new GarlandDecorator(new FirTree(), "жовта");
            Show("3) Ялинка з гірляндою:", withGarland);

            // 4) комбінація декораторів (порядок обгортання довільний)
            ChristmasTree full = new GarlandDecorator(
                new OrnamentDecorator(
                    new OrnamentDecorator(new FirTree(), "кульки", 10),
                    "зірка", 1),
                "різнокольорова");
            Show("4) Повністю прикрашена ялинка:", full);
        }
    }
}
