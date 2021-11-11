using System;
using System.Collections.Generic;

namespace DesignPatterns
{
    /// <summary>
    /// Avoid unneccessary work by deferring it until the result is needed.
    /// </summary>
    class DirtyFlag : IDesignPattern
    {
        public void DisplayExample()
        {
            var graph = new GraphNode(null);

            var boatObj = new GraphNode(new Mesh("Boat"));
            var crowNestObj = new GraphNode(new Mesh("CrowNest"));
            var pirateObj = new GraphNode(new Mesh("Pirate"));
            var parrotObj = new GraphNode(new Mesh("Parrot"));

            graph.AddChild(boatObj);
            boatObj.AddChild(crowNestObj);
            crowNestObj.AddChild(pirateObj);
            pirateObj.AddChild(parrotObj);

            boatObj.SetTransform(new Transform(new Vector3(0, 0, 0)));
            crowNestObj.SetTransform(new Transform(new Vector3(0, 1, 0)));
            pirateObj.SetTransform(new Transform(new Vector3(0, 0.5f, 0)));
            parrotObj.SetTransform(new Transform(new Vector3(0, 0.2f, 0)));

            graph.Render(Transform.Origin, true); // Set dirty the first time

            Console.WriteLine("\nThe boat is moving.\n");
            boatObj.Translate(new Vector3(0, 0, 1));

            graph.Render(Transform.Origin, false);

            Console.WriteLine("\nThe pirate fell down the crow nest!\n");
            pirateObj.Translate(new Vector3(0, -1, 0));

            graph.Render(Transform.Origin, false);
        }

        #region Implementation
        class Transform
        {
            public Vector3 Pos { get; }

            public static readonly Transform Origin = new Transform(Vector3.Zero);

            public Transform(Vector3 pos)
            {
                Pos = pos;
            }

            public Transform Combine(Transform other)
            {
                return new Transform(new Vector3(Pos.X + other.Pos.X, Pos.Y + other.Pos.Y, Pos.Z + other.Pos.Z));
            }
        }

        class GraphNode
        {
            private Mesh mesh;
            private Transform local;

            private Transform world;
            private bool dirty;

            private readonly List<GraphNode> children;

            public GraphNode(Mesh mesh)
            {
                this.mesh = mesh;
                local = Transform.Origin;
                children = new List<GraphNode>();
            }

            public void AddChild(GraphNode child)
            {
                children.Add(child);
            }

            public void Translate(Vector3 delta)
            {
                var newPos = new Transform(local.Pos + delta);
                SetTransform(newPos);
            }

            public void SetTransform(Transform local)
            {
                this.local = local;
                dirty = true;
            }

            public void Render(Transform parentWorld, bool dirty)
            {
                dirty |= this.dirty;

                if (dirty)
                {
                    Console.WriteLine("Recomputing position...");
                    world = local.Combine(parentWorld);
                    this.dirty = false;
                }

                if (mesh != null)
                {
                    RenderMesh(mesh, world);
                }

                for (int i = 0; i < children.Count; i++)
                {
                    children[i].Render(world, dirty);
                }
            }

            public void RenderMesh(Mesh mesh, Transform transform)
            {
                Console.WriteLine($"Rendering mesh {mesh.Name} at position {transform.Pos}");
            }
        }

        class Mesh
        {
            public string Name { get; }

            public Mesh(string name)
            {
                Name = name;
            }
        }

        class Vector3
        {
            public float X { get; }
            public float Y { get; }
            public float Z { get; }

            public static readonly Vector3 Zero = new Vector3(0, 0, 0);

            public Vector3(float x, float y, float z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public override string ToString()
            {
                return $"({X}, {Y}, {Z})";
            }

            public static Vector3 operator+(Vector3 v1, Vector3 v2)
            {
                return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
            }
        }
        #endregion
    }
}