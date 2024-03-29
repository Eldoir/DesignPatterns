﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns
{
    /// <summary>
    /// Use sharing to support large numbers of fine-grained objects efficiently.
    /// </summary>
    class Flyweight : IDesignPattern
    {
        public void DisplayExample()
        {
            var forest = new Forest(1000000);
            forest.Draw();
        }

        #region Implementation
        class TreeType
        {
            public string Name { get; }
            public string Color { get; }
            public object Texture { get; }

            public TreeType(string name, string color, object texture)
            {
                Name = name;
                Color = color;
                Texture = texture;
            }

            public void DrawAt(int x, int y)
            {
                // Disable if many many trees
                //Console.WriteLine($"Drawing tree at ({x},{y})");
            }
        }

        class TreeFactory
        {
            private static List<TreeType> treeTypes = new List<TreeType>();

            // Re-use an existing flyweight or create a new object.
            public static TreeType GetTreeType(string name, string color, object texture)
            {
                TreeType type = treeTypes.FirstOrDefault(t => t.Name == name && t.Color == color && t.Texture == texture);

                if (type is null)
                {
                    type = new TreeType(name, color, texture);
                    Console.WriteLine($"New TreeType: {type.Name}");
                    treeTypes.Add(type);
                }

                return type;
            }
        }

        class Tree
        {
            private int x;
            private int y;
            private TreeType type;

            public Tree(int x, int y, TreeType type)
            {
                this.x = x;
                this.y = y;
                this.type = type;
            }

            public void Draw()
            {
                type.DrawAt(x, y);
            }
        }

        class Forest
        {
            private readonly Tree[] trees;
            private int nbTrees;

            public Forest(int nbTrees)
            {
                trees = new Tree[nbTrees];
                this.nbTrees = nbTrees;

                GenerateTrees();
            }

            private void GenerateTrees()
            {
                var sqrt = (int)Math.Sqrt(nbTrees);

                for (int i = 0; i < sqrt; i++)
                {
                    for (int j = 0; j < sqrt; j++)
                    {
                        PlantTree(i * sqrt + j, i, j, "BasicTree", "Green", null);
                    }
                }
            }

            private void PlantTree(int idx, int x, int y, string name, string color, object texture)
            {
                TreeType type = TreeFactory.GetTreeType(name, color, texture);
                trees[idx] = new Tree(x, y, type);
            }

            public void Draw()
            {
                for (int i = 0; i < nbTrees; i++)
                {
                    trees[i].Draw();
                }

                Console.WriteLine($"Finished drawing {nbTrees} trees.");
            }
        }
        #endregion
    }
}