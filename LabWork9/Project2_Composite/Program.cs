﻿var root = new MyDirectory("Root");
var file1 = new MyFile("File1.txt");
var file2 = new MyFile("File2.txt");

var subDir = new MyDirectory("SubDirectory");
var subFile1 = new MyFile("SubFile1.txt");

root.Add(file1);
root.Add(file2);
subDir.Add(subFile1);
root.Add(subDir);

root.Display(1);
public abstract class Component(string name)
{
    public string Name { get; set; } = name;

    public abstract void Display(int depth);

    public virtual void Add (Component component) =>
        throw new NotImplementedException();

    public virtual void Remove(Component component) =>
        throw new NotImplementedException();

    public virtual Component GetChild(int index) =>
        throw new NotImplementedException();
}

public class MyFile(string name) : Component(name)
{
    
    public override void Display(int depth) =>
        Console.WriteLine($"{new string('-', depth)} File: {name}" );
}

public class MyDirectory(string name) : Component(name)
{
    private readonly List<Component> _children = new();
    public override void Display(int depth)
    {
        Console.WriteLine($"{new string('-', depth)} File: {name}" );
        foreach (var component in _children)
            component.Display(depth + 2);
    }

    public override void Add(Component component) =>
        _children.Add(component);

    public override void Remove(Component component) =>
        _children.Remove(component);

    public override Component GetChild(int index) =>
        _children[index];
}

