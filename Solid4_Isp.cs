using System;
using System.Globalization;

// SOLID, завдання 4. Порушено принцип розділення інтерфейсу (ISP):
// "жирний" IItem змушує книжку реалізовувати SetColor/SetSize, які їй не потрібні.
// Виправлення: розбити на дрібні інтерфейси; кожен клас реалізує лише потрібні.
//
//   interface IItem { ApplyDiscount; ApplyPromocode; SetColor; SetSize; SetPrice }   <- було
//
namespace LabPatterns.Solid4_Isp
{
    interface IPriceable
    {
        void SetPrice(double price);
    }

    interface IDiscountable
    {
        void ApplyDiscount(string discount);     // наприклад "10" = 10 %
        void ApplyPromocode(string promocode);   // наприклад "SALE10"
    }

    interface IColorable
    {
        void SetColor(byte color);
    }

    interface ISizable
    {
        void SetSize(byte size);
    }

    // Спільна допоміжна логіка розрахунку знижки
    static class PriceHelper
    {
        public static double ApplyPercent(double price, string percentText)
        {
            double percent;
            if (!double.TryParse(percentText, NumberStyles.Float, CultureInfo.InvariantCulture, out percent)
                || percent < 0 || percent > 100)
            {
                throw new ArgumentException("Знижка має бути числом від 0 до 100: " + percentText);
            }
            return price * (100 - percent) / 100;
        }

        public static void CheckPrice(double price)
        {
            if (price < 0) throw new ArgumentException("Ціна не може бути від'ємною");
        }
    }

    // Книжка: ціна + знижки (без кольору та розміру)
    class Book : IPriceable, IDiscountable
    {
        public string Title { get; private set; }
        public double Price { get; private set; }

        public Book(string title, double price)
        {
            Title = title;
            SetPrice(price);
        }

        public void SetPrice(double price)
        {
            PriceHelper.CheckPrice(price);
            Price = price;
        }

        public void ApplyDiscount(string discount)
        {
            Price = PriceHelper.ApplyPercent(Price, discount);
        }

        public void ApplyPromocode(string promocode)
        {
            if (string.Equals(promocode, "SALE10", StringComparison.OrdinalIgnoreCase))
            {
                Price = PriceHelper.ApplyPercent(Price, "10");
            }
            else
            {
                Console.WriteLine("  Промокод '" + promocode + "' недійсний");
            }
        }

        public void Info()
        {
            Console.WriteLine("Книжка '{0}': ціна {1:F2}", Title, Price);
        }
    }

    // Верхній одяг: колір, розмір, ціна, знижки
    class OuterWear : IPriceable, IDiscountable, IColorable, ISizable
    {
        public string Name { get; private set; }
        public double Price { get; private set; }
        public byte Color { get; private set; }
        public byte Size { get; private set; }

        public OuterWear(string name, double price)
        {
            Name = name;
            SetPrice(price);
        }

        public void SetPrice(double price)
        {
            PriceHelper.CheckPrice(price);
            Price = price;
        }

        public void ApplyDiscount(string discount)
        {
            Price = PriceHelper.ApplyPercent(Price, discount);
        }

        public void ApplyPromocode(string promocode)
        {
            if (string.Equals(promocode, "SALE10", StringComparison.OrdinalIgnoreCase))
            {
                Price = PriceHelper.ApplyPercent(Price, "10");
            }
            else
            {
                Console.WriteLine("  Промокод '" + promocode + "' недійсний");
            }
        }

        public void SetColor(byte color) { Color = color; }
        public void SetSize(byte size) { Size = size; }

        public void Info()
        {
            Console.WriteLine("Верхній одяг '{0}': ціна {1:F2}, колір (код) {2}, розмір {3}",
                Name, Price, Color, Size);
        }
    }

    public static class Demo
    {
        // Метод залежить лише від того інтерфейсу, який йому справді потрібен
        static void SellWithDiscount(IDiscountable item, string discount)
        {
            item.ApplyDiscount(discount);
        }

        public static void Run()
        {
            Book book = new Book("Шаблони проєктування", 500);
            book.Info();
            SellWithDiscount(book, "20");
            book.ApplyPromocode("SALE10");
            book.Info();

            OuterWear jacket = new OuterWear("Куртка", 2000);
            jacket.SetColor(3);
            jacket.SetSize(48);
            jacket.Info();
            SellWithDiscount(jacket, "25");
            jacket.ApplyPromocode("WRONG");
            jacket.Info();
        }
    }
}
