using System;

// SOLID, завдання 3. Порушено принцип підстановки Лісков (LSP).
// Square успадковано від Rectangle, але змінює поведінку сеттерів (Width і Height
// пов'язані). Код, що працює з Rectangle, ламається при підстановці Square:
// очікували 5 * 10 = 50, а отримали 100.
// Виправлення: Square НЕ є підтипом Rectangle у сенсі поведінки.
// Робимо спільну абстракцію Shape; Rectangle і Square - її незалежні нащадки.
namespace LabPatterns.Solid3_Lsp
{
    // ---------- ПОГАНИЙ варіант (із завдання) ----------
    class BadRectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public int GetRectangleArea()
        {
            return Width * Height;
        }
    }

    class BadSquare : BadRectangle
    {
        public override int Width
        {
            get { return base.Width; }
            set
            {
                base.Height = value;
                base.Width = value;
            }
        }

        public override int Height
        {
            get { return base.Height; }
            set
            {
                base.Height = value;
                base.Width = value;
            }
        }
    }

    // ---------- ВИПРАВЛЕНИЙ варіант ----------
    abstract class Shape
    {
        public abstract int GetArea();
    }

    class Rectangle : Shape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override int GetArea()
        {
            return Width * Height;
        }
    }

    class Square : Shape
    {
        public int Side { get; set; }

        public Square(int side)
        {
            Side = side;
        }

        public override int GetArea()
        {
            return Side * Side;
        }
    }

    public static class Demo
    {
        public static void Run()
        {
            // Поганий варіант
            BadRectangle rect = new BadSquare();
            rect.Width = 5;
            rect.Height = 10;
            Console.WriteLine("Поганий варіант (очікували 50): " + rect.GetRectangleArea());

            // Виправлений варіант: будь-який Shape можна підставити без сюрпризів
            Shape[] shapes = { new Rectangle(5, 10), new Square(5) };
            foreach (Shape shape in shapes)
            {
                Console.WriteLine("{0}: площа = {1}", shape.GetType().Name, shape.GetArea());
            }
        }
    }
}
