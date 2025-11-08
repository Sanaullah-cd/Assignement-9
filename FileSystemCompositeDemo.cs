using System;
using System.Collections.Generic;
using System.Linq;

namespace FileSystemCompositeDemo
{
    // Абстрактный компонент
    public abstract class FileSystemComponent
    {
        protected string _name;
        protected FileSystemComponent(string name) => _name = name;
        public virtual void Display(string indent = "") => Console.WriteLine($"{indent}{_name}");
        public abstract long GetSize(); // в байтах (или условных единицах)
    }

    // Leaf — файл
    public class FileComponent : FileSystemComponent
    {
        private readonly long _size;
        public FileComponent(string name, long size) : base(name) { _size = size; }

        public override void Display(string indent = "")
        {
            Console.WriteLine($"{indent}- Файл: {_name} ({_size} bytes)");
        }

        public override long GetSize() => _size;
    }

    // Composite — папка (Directory)
    public class DirectoryComponent : FileSystemComponent
    {
        private readonly List<FileSystemComponent> _children = new List<FileSystemComponent>();

        public DirectoryComponent(string name) : base(name) { }

        public void Add(FileSystemComponent component)
        {
            if (component == null) throw new ArgumentNullException(nameof(component));
            if (_children.Contains(component))
            {
                Console.WriteLine($"Directory '{_name}': компонент '{component._name}' уже добавлен.");
                return;
            }
            _children.Add(component);
        }

        public void Remove(FileSystemComponent component)
        {
            if (component == null) throw new ArgumentNullException(nameof(component));
            if (!_children.Remove(component))
            {
                Console.WriteLine($"Directory '{_name}': компонент '{component._name}' не найден.");
            }
        }

        public override void Display(string indent = "")
        {
            Console.WriteLine($"{indent}+ Папка: {_name}");
            string childIndent = indent + "    ";
            foreach (var c in _children)
            {
                c.Display(childIndent);
            }
        }

        public override long GetSize()
        {
            return _children.Sum(c => c.GetSize());
        }

        public FileSystemComponent Find(string name)
        {
            return _children.FirstOrDefault(c => c._name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }

    // Клиент
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Демонстрация паттерна Composite: файловая система ===");

            // Создаём корневую папку
            var root = new DirectoryComponent("root");

            // Добавляем файлы и папки
            var file1 = new FileComponent("readme.txt", 1200);
            var file2 = new FileComponent("photo.jpg", 2_400_000);
            var docs = new DirectoryComponent("documents");
            var music = new DirectoryComponent("music");

            root.Add(file1);
            root.Add(file2);
            root.Add(docs);
            root.Add(music);

            // Внутри documents
            var doc1 = new FileComponent("report.docx", 45_000);
            var doc2 = new FileComponent("cv.pdf", 12_000);
            docs.Add(doc1);
            docs.Add(doc2);

            // Внутри music
            var song1 = new FileComponent("song1.mp3", 5_000_000);
            var album = new DirectoryComponent("album");
            music.Add(song1);
            music.Add(album);

            album.Add(new FileComponent("track1.mp3", 4_800_000));
            album.Add(new FileComponent("track2.mp3", 4_600_000));

            // Показать структуру и размеры
            root.Display();
            Console.WriteLine($"\nОбщий размер root: {root.GetSize()} bytes");

            Console.WriteLine("\nПытаемся добавить дубликат (file1) в root:");
            root.Add(file1); // демонстрация проверки

            Console.WriteLine("\nУдаляем cv.pdf из documents и показываем заново:");
            docs.Remove(doc2);
            root.Display();
            Console.WriteLine($"\nОбщий размер root после удаления: {root.GetSize()} bytes");
        }
    }
}
