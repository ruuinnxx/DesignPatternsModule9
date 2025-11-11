﻿var root = new MyDirectory("Root");

var file1 = new MyFile("File1.txt", 1200);
var file2 = new MyFile("File2.txt", 3400);

var subDir = new MyDirectory("SubDirectory");
var subFile1 = new MyFile("SubFile1.txt", 800);
var subFile2 = new MyFile("SubFile2.txt", 1500);

subDir.Add(subFile1);
subDir.Add(subFile2);

root.Add(file1);
root.Add(file2);
root.Add(subDir);

Console.WriteLine("Структура файловой системы:\n");
root.Display(1);

Console.WriteLine($"\nОбщий размер папки Root: {root.GetSize()} байт");


public abstract class FileSystemComponent(string name)
{
    public string Name { get; set; } = name;

    public abstract void Display(int depth);
    public abstract int GetSize();

    public virtual void Add(FileSystemComponent component) => 
        throw new NotImplementedException();

    public virtual void Remove(FileSystemComponent component) => 
        throw new NotImplementedException();
}


public class MyFile(string name, int size) : FileSystemComponent(name)
{
    public override void Display(int depth) => 
        Console.WriteLine($"{new string('-', depth)} File: {Name}, Size: {size} байт");

    public override int GetSize() => size;
}


public class MyDirectory(string name) : FileSystemComponent(name)
{
    private readonly List<FileSystemComponent> _children = new();

    public override void Display(int depth)
    {
        Console.WriteLine($"{new string('-', depth)} Directory: {Name}");
        foreach (var component in _children)
            component.Display(depth + 2);
    }

    public override int GetSize() => _children.Sum(c => c.GetSize());

    public override void Add(FileSystemComponent component)
    {
        if (_children.Contains(component))
        {
            Console.WriteLine($"Компонент '{component.Name}' уже существует в '{Name}'!");
            return;
        }

        _children.Add(component);
        Console.WriteLine($"Добавлен компонент '{component.Name}' в папку '{Name}'");
    }

    public override void Remove(FileSystemComponent component)
    {
        if (!_children.Contains(component))
        {
            Console.WriteLine($"Компонент '{component.Name}' не найден в '{Name}'!");
            return;
        }

        _children.Remove(component);
        Console.WriteLine($"Удалён компонент '{component.Name}' из '{Name}'");
    }
}
