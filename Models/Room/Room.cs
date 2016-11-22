﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using MonogameWindows.Models.Graphs;

namespace MonogameWindows.Models.Room
{
    class Room
    {

        private Vector3 origin;
        private const int ID = 1;
        private float width, height, depth;
        private HashSet<Graph> graphs;

        // CONSTRUCTOR
        // -----------------------------------------------
        public Room(Vector3 origin, float width, float height, float depth)
        {
            this.origin = origin;
            this.width = width;
            this.height = height;
            this.depth = depth;
        }


        // METHODS & FUNCTIONS
        // -----------------------------------------------

        public HashSet<Graph> GetGraphs()
        {
            return graphs;
        }

        public void AddGraph(Graph g)
        {
            graphs.Add(g);
        }

        public void RemoveGraph(Graph g)
        {
            //TODO
        }

        // PROPERTIES
        // -----------------------------------------------
        public Vector3 Origin
        {
            get { return origin; }
        }

        public float Width
        {
            get { return width; }
        }

        public float Height
        {
            get { return height; }
        }

        public float Depth
        {
            get { return depth; }
        }

    }
}
