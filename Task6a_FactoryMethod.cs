using System;

// Завдання 6 (шаблон 1). Factory Method: у прикладі з документами
// додано клас PdfDocument та його творець PdfCreator.
namespace LabPatterns.Task6a_FactoryMethod
{
    // Product
    abstract class Document
    {
        public abstract string Name { get; }
        public abstract void Open();
        public abstract void Save();
    }

    // ConcreteProduct 1
    class WordDocument : Document
    {
        public override string Name { get { return "Word-документ"; } }
        public override void Open() { Console.WriteLine("  Відкриваємо документ у Word (.docx)"); }
        public override void Save() { Console.WriteLine("  Зберігаємо у форматі .docx"); }
    }

    // ConcreteProduct 2
    class TextDocument : Document
    {
        public override string Name { get { return "Текстовий документ"; } }
        public override void Open() { Console.WriteLine("  Відкриваємо документ у Блокноті (.txt)"); }
        public override void Save() { Console.WriteLine("  Зберігаємо у форматі .txt"); }
    }

    // ConcreteProduct 3 (НОВИЙ)
    class PdfDocument : Document
    {
        public override string Name { get { return "PDF-документ"; } }
        public override void Open() { Console.WriteLine("  Відкриваємо документ у PDF-переглядачі (.pdf)"); }
        public override void Save() { Console.WriteLine("  Зберігаємо у форматі .pdf"); }
    }

    // Creator
    abstract class DocumentCreator
    {
        // Фабричний метод: підкласи самі вирішують, який саме Document створити
        public abstract Document FactoryMethod();

        // Загальна логіка, що не залежить від конкретного типу документа
        public void NewDocument()
        {
            Document doc = FactoryMethod();
            Console.WriteLine("Створено: " + doc.Name);
            doc.Open();
            doc.Save();
        }
    }

    // ConcreteCreator 1
    class WordCreator : DocumentCreator
    {
        public override Document FactoryMethod() { return new WordDocument(); }
    }

    // ConcreteCreator 2
    class TextCreator : DocumentCreator
    {
        public override Document FactoryMethod() { return new TextDocument(); }
    }

    // ConcreteCreator 3 (НОВИЙ)
    class PdfCreator : DocumentCreator
    {
        public override Document FactoryMethod() { return new PdfDocument(); }
    }

    public static class Demo
    {
        public static void Run()
        {
            DocumentCreator[] creators = { new WordCreator(), new TextCreator(), new PdfCreator() };

            foreach (DocumentCreator creator in creators)
            {
                creator.NewDocument();
            }
        }
    }
}
