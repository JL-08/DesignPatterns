using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.SOLID.Invalid
{
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

            // Current implementation invalidates the principle since setting the Square Width and Height both set the same value,
            // effectively making the Square a special case of a rectangle where the width and height are always equal.
            // this contradicts the principle that a Square is a type of Rectangle and should inherit behavior without altering its contract.
            Rectangle rsq = new Square();
            rsq.Width = 4;
            Console.WriteLine($"{rsq} has area {Area(rsq)}");
        }
    }

    public class Rectangle
    {
        public int Width { get; set; }
        public int Height { get; set; }

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
        public new int Width
        {
            set 
            {
                base.Width = base.Height = value;
            }
        }

        public new int Height
        {
            set
            {
                base.Width = base.Height = value;
            }
        }
    }
}
