using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.SOLID.Valid
{
    // Liskov Substitution
    // - if a class is a subtype of another class, it should be able to be used wherever its parent class is used,
    // without the need to modify the code that uses the parent class.
    public class LiskovSub
    {
        private int Area(Rectangle r) => r.Width * r.Height;
        public void Run()
        {
            Rectangle rc = new Rectangle(2, 3);
            Console.WriteLine($"{rc} has area {Area(rc)}");

            Square sq = new Square();
            sq.Width = 4;
            Console.WriteLine($"{sq} has area {Area(sq)}");

            // Square should still behave as a Square even if referenced to a Rectangle
            Rectangle rsq = new Square();
            rsq.Width = 4;
            Console.WriteLine($"{rsq} has area {Area(rsq)}");
        }
    }

    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle()
        {

        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class Square : Rectangle
    {
        public override int Width
        {
            set
            {
                base.Width = base.Height = value;
            }
        }

        public override int Height
        {
            set
            {
                base.Width = base.Height = value;
            }
        }
    }
}
