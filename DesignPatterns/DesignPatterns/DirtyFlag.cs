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
            GraphNode graph = new GraphNode(null);

            GraphNode boatObj = new GraphNode(new Mesh("Boat"));
            GraphNode crowNestObj = new GraphNode(new Mesh("CrowNest"));
            GraphNode pirateObj = new GraphNode(new Mesh("Pirate"));
            GraphNode parrotObj = new GraphNode(new Mesh("Parrot"));

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
            public Vector3 pos { get; }

            public static Transform Origin => new Transform(Vector3.zero);

            public Transform(Vector3 pos)
            {
                this.pos = pos;
            }

            public Transform Combine(Transform other)
            {
                return new Transform(new Vector3(pos.x + other.pos.x, pos.y + other.pos.y, pos.z + other.pos.z));
            }
        }

        class GraphNode
        {
            private Mesh mesh;
            private Transform local;

            private Transform world;
            private bool dirty;

            private List<GraphNode> children;

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
                Transform newPos = new Transform(local.pos + delta);
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
                Console.WriteLine($"Rendering mesh {mesh.name} at position {transform.pos}");
            }
        }

        class Mesh
        {
            public string name { get; }

            public Mesh(string name)
            {
                this.name = name;
            }
        }

        class Vector3
        {
            public float x, y, z;

            public static Vector3 zero => new Vector3(0, 0, 0);

            public Vector3(float x, float y, float z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            public override string ToString()
            {
                return $"({x}, {y}, {z})";
            }

            public static Vector3 operator+(Vector3 v1, Vector3 v2)
            {
                return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
            }
        }
        #endregion
    }
}