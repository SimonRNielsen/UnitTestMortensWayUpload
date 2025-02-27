using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Numerics;
[assembly: InternalsVisibleTo("UnitTestMortensWay")]

namespace Testing
{
    public class Tile
    {
        private Enum type;
        private bool walkable = true;
        private bool fencePath = false;
        private HashSet<Edge> edges = new HashSet<Edge>();
        private HashSet<Edge> fakeEdges = new HashSet<Edge>();
        private bool discovered = false;
        private Tile parent;
        private Vector2 position;

        public int G { get; set; }
        public int H { get; set; }
        public int F => G + H;

        public Enum Type { get => type; set => type = value; }

        public HashSet<Edge> Edges
        {
            get
            {
                if (walkable)
                    return edges;
                else
                    return fakeEdges;
            }
        }
        public bool Discovered { get => discovered; set => discovered = value; }
        public Vector2 Position { get => position; set => position = value; }
        public Tile Parent { get => parent; set => parent = value; }
        public bool Walkable
        {
            get => walkable;
            set
            {
                if (value == false && walkable == true && type.Equals(TileTypes.FencePath))
                {
                    walkable = value;
                }
            }
        }
        public bool FencePath { get => fencePath; }

        public Tile(Enum type, Vector2 spawnPos, bool fencePath)
        {
            Type = type;
            position = spawnPos;
            switch (type)
            {
                case TileTypes.Stone:
                case TileTypes.Fence:
                    walkable = false;
                    break;
                case TileTypes.FencePath:
                    this.fencePath = true;
                    break;
                default:
                    break;
            }
        }


        public void CreateEdges(List<Tile> list)
        {
            if (walkable)
                foreach (Tile other in list)
                {
                    if (this != other && other.Walkable)
                    {
                        float distance = Vector2.Distance(position, other.Position);
                        if (distance < 91)
                        {
                            int weight;
                            if (distance < 65)
                                weight = 10;
                            else
                                weight = 14;
                            Edges.Add(new Edge(weight, this, other));
                        }
                    }
                }

        }

    }
}
