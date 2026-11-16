using System;

// Завдання 5. Prototype: додано клас "Трикутник" (Triangle).
namespace LabPatterns.Task5_Prototype
{
    // Prototype
    interface IFigure
    {
        IFigure Clone();
        void GetInfo();
    }

    // ConcretePrototype 1
    class Rectangle : IFigure
    {
        int width;
        int height;

        public Rectangle(int w, int h)
        {
            width = w;
            height = h;
        }

        public IFigure Clone()
        {
            return new Rectangle(this.width, this.height);
        }

        public void GetInfo()
        {
            Console.WriteLine("Прямокутник довжиною {0} і шириною {1}", height, width);
        }
    }

    // ConcretePrototype 2
    class Circle : IFigure
    {
        int radius;

        public Circle(int r)
        {
            radius = r;
        }

        public IFigure Clone()
        {
            return new Circle(this.radius);
        }

        public void GetInfo()
        {
            Console.WriteLine("Круг радіусом {0}", radius);
        }
    }

    // ConcretePrototype 3 (НОВИЙ)
    class Triangle : IFigure
    {
        int sideA;
        int sideB;
        int sideC;

        public Triangle(int a, int b, int c)
        {
            sideA = a;
            sideB = b;
            sideC = c;
        }

        public IFigure Clone()
        {
            return new Triangle(this.sideA, this.sideB, this.sideC);
        }

        public void GetInfo()
        {
            Console.WriteLine("Трикутник зі сторонами {0}, {1} і {2}", sideA, sideB, sideC);
        }
    }

    public static class Demo
    {
        public static void Run()
        {
            IFigure figure = new Rectangle(10, 20);
            IFigure clonedFigure = figure.Clone();
            figure.GetInfo();
            clonedFigure.GetInfo();

            figure = new Circle(15);
            clonedFigure = figure.Clone();
            figure.GetInfo();
            clonedFigure.GetInfo();

            figure = new Triangle(3, 4, 5);
            clonedFigure = figure.Clone();
            figure.GetInfo();
            clonedFigure.GetInfo();

            // Копія - це ІНШИЙ об'єкт у пам'яті, хоча дані в нього такі самі
            Console.WriteLine("Це той самий об'єкт? " + ReferenceEquals(figure, clonedFigure));
        }
    }
}
