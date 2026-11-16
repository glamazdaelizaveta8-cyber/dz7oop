using System;
using System.Collections.Generic;

// SOLID, завдання 1. Порушено принцип єдиної відповідальності (SRP):
// клас Order одночасно (1) тримає дані й рахує суму, (2) друкує/показує замовлення,
// (3) зберігає/завантажує себе з БД. У нього три причини для змін.
// Виправлення: розділити на три класи з однією відповідальністю.
namespace LabPatterns.Solid1_SrpOrder
{
    class Item
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public Item(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }

    // Відповідальність №1: предметна логіка замовлення
    class Order
    {
        private readonly List<Item> itemList = new List<Item>();

        public int Id { get; set; }

        public IReadOnlyList<Item> ItemList
        {
            get { return itemList; }
        }

        public decimal CalculateTotalSum()
        {
            decimal sum = 0;
            foreach (Item item in itemList)
            {
                sum += item.Price;
            }
            return sum;
        }

        public IReadOnlyList<Item> GetItems() { return itemList; }
        public int GetItemCount() { return itemList.Count; }

        public void AddItem(Item item)
        {
            if (item == null) throw new ArgumentNullException("item");
            itemList.Add(item);
        }

        public void DeleteItem(Item item)
        {
            itemList.Remove(item);
        }
    }

    // Відповідальність №2: подання замовлення (друк / показ)
    class OrderPrinter
    {
        public void Print(Order order)
        {
            Console.WriteLine("[Друк] Замовлення №" + order.Id);
            foreach (Item item in order.GetItems())
            {
                Console.WriteLine("  {0} - {1}", item.Name, item.Price);
            }
            Console.WriteLine("  Разом: " + order.CalculateTotalSum());
        }

        public void Show(Order order)
        {
            Console.WriteLine("[Екран] Замовлення №{0}: позицій - {1}, сума - {2}",
                order.Id, order.GetItemCount(), order.CalculateTotalSum());
        }
    }

    // Відповідальність №3: збереження. Абстракція + реалізація
    interface IOrderRepository
    {
        Order Load(int id);
        void Save(Order order);
        void Update(Order order);
        void Delete(Order order);
    }

    class InMemoryOrderRepository : IOrderRepository
    {
        private readonly Dictionary<int, Order> storage = new Dictionary<int, Order>();

        public Order Load(int id)
        {
            Order order;
            return storage.TryGetValue(id, out order) ? order : null;
        }

        public void Save(Order order)
        {
            storage[order.Id] = order;
            Console.WriteLine("[БД] Збережено замовлення №" + order.Id);
        }

        public void Update(Order order)
        {
            if (!storage.ContainsKey(order.Id))
                throw new InvalidOperationException("Замовлення №" + order.Id + " не знайдено");
            storage[order.Id] = order;
            Console.WriteLine("[БД] Оновлено замовлення №" + order.Id);
        }

        public void Delete(Order order)
        {
            storage.Remove(order.Id);
            Console.WriteLine("[БД] Видалено замовлення №" + order.Id);
        }
    }

    public static class Demo
    {
        public static void Run()
        {
            Order order = new Order();
            order.Id = 1;
            order.AddItem(new Item("Хліб", 25));
            order.AddItem(new Item("Молоко", 40));
            order.AddItem(new Item("Сир", 120));

            OrderPrinter printer = new OrderPrinter();
            printer.Print(order);
            printer.Show(order);

            IOrderRepository repository = new InMemoryOrderRepository();
            repository.Save(order);

            Order loaded = repository.Load(1);
            Console.WriteLine("Завантажено з БД, позицій: " + loaded.GetItemCount());

            repository.Delete(order);
            Console.WriteLine("Після видалення Load(1) == null? " + (repository.Load(1) == null));
        }
    }
}
