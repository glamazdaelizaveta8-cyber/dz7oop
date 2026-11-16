using System;

// Завдання 2. Builder: додано клас MargaritaPizzaBuilder.
namespace LabPatterns.Task2_Builder
{
    // Product
    class Pizza
    {
        string dough;
        string sauce;
        string topping;

        public Pizza() { }
        public void SetDough(string d) { dough = d; }
        public void SetSauce(string s) { sauce = s; }
        public void SetTopping(string t) { topping = t; }

        public void Info()
        {
            Console.WriteLine("Dough: {0}\nSauce: {1}\nTopping: {2}", dough, sauce, topping);
        }
    }

    // Abstract Builder
    abstract class PizzaBuilder
    {
        protected Pizza pizza;

        public PizzaBuilder() { }
        public Pizza GetPizza() { return pizza; }
        public void CreateNewPizza() { pizza = new Pizza(); }

        public abstract void BuildDough();
        public abstract void BuildSauce();
        public abstract void BuildTopping();
    }

    // Concrete Builder 1
    class HawaiianPizzaBuilder : PizzaBuilder
    {
        public override void BuildDough() { pizza.SetDough("cross"); }
        public override void BuildSauce() { pizza.SetSauce("mild"); }
        public override void BuildTopping() { pizza.SetTopping("ham+pineapple"); }
    }

    // Concrete Builder 2
    class SpicyPizzaBuilder : PizzaBuilder
    {
        public override void BuildDough() { pizza.SetDough("pan baked"); }
        public override void BuildSauce() { pizza.SetSauce("hot"); }
        public override void BuildTopping() { pizza.SetTopping("pepperoni+salami"); }
    }

    // Concrete Builder 3 (НОВИЙ)
    class MargaritaPizzaBuilder : PizzaBuilder
    {
        public override void BuildDough() { pizza.SetDough("thin"); }
        public override void BuildSauce() { pizza.SetSauce("tomato"); }
        public override void BuildTopping() { pizza.SetTopping("mozzarella+basil"); }
    }

    // Director
    class Waiter
    {
        private PizzaBuilder pizzaBuilder;

        public void SetPizzaBuilder(PizzaBuilder pb) { pizzaBuilder = pb; }
        public Pizza GetPizza() { return pizzaBuilder.GetPizza(); }

        public void ConstructPizza()
        {
            pizzaBuilder.CreateNewPizza();
            pizzaBuilder.BuildDough();
            pizzaBuilder.BuildSauce();
            pizzaBuilder.BuildTopping();
        }
    }

    public static class Demo
    {
        public static void Run()
        {
            Waiter waiter = new Waiter();

            PizzaBuilder hawaiianPizzaBuilder = new HawaiianPizzaBuilder();
            PizzaBuilder spicyPizzaBuilder = new SpicyPizzaBuilder();
            PizzaBuilder margaritaPizzaBuilder = new MargaritaPizzaBuilder();

            PizzaBuilder[] builders = { hawaiianPizzaBuilder, spicyPizzaBuilder, margaritaPizzaBuilder };

            foreach (PizzaBuilder builder in builders)
            {
                waiter.SetPizzaBuilder(builder);
                waiter.ConstructPizza();

                Pizza pizza = waiter.GetPizza();
                Console.WriteLine("--- {0} ---", builder.GetType().Name);
                pizza.Info();
            }
        }
    }
}
