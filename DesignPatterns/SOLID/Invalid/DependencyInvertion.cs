﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.SOLID.Invalid
{
    // Dependency Inversion Principle
    // - High-level modules should not depend on low-level modules. Both should depend on abstractions.
    public class DependencyInvertion
    {
        public void Run()
        {
            var parent = new Person { Name = "John" };
            var child1 = new Person { Name = "Chris" };
            var child2 = new Person { Name = "Mary" };

            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            new Research(relationships);
        }
        
    }

    // High-level
    // This invalidates the Dependency Inversion principle
    // since the class depends on the property of the low level module which result in tight coulpling
    // classes should depend on abstraction instead
    public class Research
    {
        public Research(Relationships relationships)
        {
            var relations = relationships.Relations;
            foreach (var r in relations.Where(x => x.Item1.Name == "John" && x.Item2 == Relationship.Parent))
            {
                Console.WriteLine($"John has a child called {r.Item3.Name}");
            }
        }
    }

    public enum Relationship
    {
        Parent,
        Child,
        Sibling
    }

    public class Person
    {
        public string Name;
    }

    // low-level
    public class Relationships
    {
        private List<(Person, Relationship, Person)> relations = new List<(Person, Relationship, Person)> ();

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((parent, Relationship.Child, parent));
        }

        public List<(Person, Relationship, Person)> Relations => relations;
    }
}
