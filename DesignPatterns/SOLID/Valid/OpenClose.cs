using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.SOLID.Valid
{
    // Open and Close Principle
    // - Parts of a system should be open for extension and close for modification
    public class OpenClose
    {
        public void Run()
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house };
            var bf = new BetterFilter();
            Console.WriteLine("Green products: ");
            foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine($" - {p.Name} is green");
            }

            Console.WriteLine("Large blue products: ");
            foreach (var p in bf.Filter(products,
                                        new AndSpecification<Product>(
                                            new ColorSpecification(Color.Blue),
                                            new SizeSpecification(Size.Large))
                                        ))
            {
                Console.WriteLine($" - {p.Name} is big and blue");
            }
        }
    }

    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large, Huge
    }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            if (name == null)
            {
                throw new ArgumentNullException(paramName: nameof(name));
            }

            Name = name;
            Color = color;
            Size = size;
        }
    }

    // Specification Pattern
    // - dictates whether or not a product satisfies a particular criteria
    public interface ISpecification<T>
    {
        bool IsSatisfied(T entity);
    }

    interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> specification);
    }

    #region Specifications
    public class ColorSpecification : ISpecification<Product>
    {
        private Color color;
        public ColorSpecification(Color color)
        {
            this.color = color;
        }

        public bool IsSatisfied(Product entity)
        {
            return entity.Color == color;
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private Size size;
        public SizeSpecification(Size size)
        {
            this.size = size;
        }

        public bool IsSatisfied(Product entity)
        {
            return entity.Size == size;
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        ISpecification<T> first, second;
        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            this.first = first ?? throw new ArgumentNullException(paramName: nameof(first)); ;
            this.second = second ?? throw new ArgumentNullException(paramName: nameof(second)); ;
        }

        public bool IsSatisfied(T entity)
        {
            return first.IsSatisfied(entity) && second.IsSatisfied(entity);
        }
    }
    #endregion

    // Implement filtering
    // should be closed for modification
    // if you want more functionality, you make new classes and implement ISpecification
    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> specification)
        {
            foreach (var item in items)
            {
                if (specification.IsSatisfied(item))
                {
                    yield return item;
                }
            }
        }
    }
}
