using System;

namespace CSharpLearning.Examples.CommonPitfalls
{
    // Parent class with reference to child
    public class Parent
    {
        public string Name { get; }
        public Child Child { get; private set; }

        public Parent(string name)
        {
            Name = name;
        }

        public void SetChild(Child child)
        {
            Child = child;
        }
    }

    // Child class with reference to parent
    public class Child
    {
        public string Name { get; }
        public Parent Parent { get; private set; }

        public Child(string name)
        {
            Name = name;
        }

        public void SetParent(Parent parent)
        {
            Parent = parent;
        }
    }
}