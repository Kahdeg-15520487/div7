using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AsciiTreeDiagram
{
    class asciitreestuff
    {
        // Constants for drawing lines and spaces
        private const string _cross = " ├─";
        private const string _corner = " └─";
        private const string _vertical = " │ ";
        private const string _space = "   ";

        static void draw()
        {
            // Get the list of nodes
            List<Node> topLevelNodes = Create("", 8).ToList();

            foreach (var node in topLevelNodes)
            {
                PrintNode(node, indent: "");
            }

            if (Debugger.IsAttached)
            {
                Console.WriteLine("Press any key to exit...");
                Console.Read();
            }
        }

        static void PrintNode(Node node, string indent)
        {
            Console.WriteLine(node.Name);

            // Loop through the children recursively, passing in the
            // indent, and the isLast parameter
            var numberOfChildren = node.Children.Count;
            for (var i = 0; i < numberOfChildren; i++)
            {
                var child = node.Children[i];
                var isLast = (i == (numberOfChildren - 1));
                PrintChildNode(child, indent, isLast);
            }
        }

        static void PrintChildNode(Node node, string indent, bool isLast)
        {
            // Print the provided pipes/spaces indent
            Console.Write(indent);

            // Depending if this node is a last child, print the
            // corner or cross, and calculate the indent that will
            // be passed to its children
            if (isLast)
            {
                Console.Write(_corner);
                indent += _space;
            }
            else
            {
                Console.Write(_cross);
                indent += _vertical;
            }

            PrintNode(node, indent);
        }

        static IEnumerable<Node> Create(string lvl, int limit = 8)
        {
            if (lvl.Length < limit)
            {
                yield return new Node(lvl, Create(lvl + '0', limit).ToList());
                yield return new Node(lvl, Create(lvl + '1', limit).ToList());
            }
            else
            {
                yield return new Node(lvl, new List<Node>());
            }
        }
    }
}

namespace AsciiTreeDiagram
{
    class Node
    {
        public Node(string name, List<Node> children)
        {
            Name = name;
            Children = children;
        }

        public string Name { get; set; }

        public List<Node> Children { get; private set; }
    }
}
