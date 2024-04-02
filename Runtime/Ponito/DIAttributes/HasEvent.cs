using System;

/// <summary>
///     Use this <see cref="Attribute"/> to mark a <see cref="class"/> has a certain event
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class HasEvent : Attribute
{
    public string Name { get; }

    public HasEvent(string name)
    {
        this.Name = name;
    }
}