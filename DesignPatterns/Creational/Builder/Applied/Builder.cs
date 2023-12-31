﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational.Applied
{
    public class Builder
    {
        public class HtmlElement
        {
            public string Name, Text;
            public List<HtmlElement> Elements = new List<HtmlElement>();
            private const int indentSize = 2;

            public HtmlElement(string name, string text)
            {
                Name = name;
                Text = text;
            }

            private string ToStringImpl(int indent)
            {
                var sb = new StringBuilder();
                var i = new string(' ', indentSize * indent);
                sb.Append($"{i}<{Name}>");

                if (!string.IsNullOrWhiteSpace(Text))
                {
                    sb.Append(new string (' ', indentSize * (indent + 1));
                    sb.AppendLine(Text);
                }

                foreach (var e in Elements)
                {
                    sb.Append(e.ToStringImpl(indent + 1));
                }
                sb.Append($"{i}<{Name}>");
                return sb.ToString();
            }

            public override string ToString()
            {
                return ToStringImpl(0);
            }
        }

        public class HtmlBuilder
        {
            private readonly string rootName;
            HtmlElement root = new HtmlElement();

            public HtmlBuilder(string rootName)
            {
                this.rootName = rootName;
                root.Name = rootName;
            }

            public void AddChild(string childName, string childText)
            {
                var e = new HtmlElement(childName, childText);
                root.Elements.Add(e);
            }

            public override string ToString()
            {
                return root.ToString();
            }

            public void Clear()
            {
                root = new HtmlElement { Name = rootName };
            }
        }

        public void Run()
        {
            var builder = new HtmlBuilder("ul");
            builder.AddChild("li", "hello");
            builder.AddChild("li", "world");
            Console.WriteLine(builder.ToString());
        }
    }
}
