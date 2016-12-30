using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    class Shapes
    {
        public static Coordinate[,,] shapes = new Coordinate[7, 4, 4]
        {
            {
                //A single shape with all rotations
                {new Coordinate(0, 1), new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(2, 0) },
                {new Coordinate(0, 1), new Coordinate(1, 1), new Coordinate(0, 0), new Coordinate(0,-1) },
                {new Coordinate(0, 1), new Coordinate(1, 1), new Coordinate(2, 1), new Coordinate(2, 0) },
                {new Coordinate(1, 1), new Coordinate(1, 0), new Coordinate(1,-1), new Coordinate(0,-1) }
            },
            {
                {new Coordinate(2, 1), new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(2, 0)},
                {new Coordinate(0, 1), new Coordinate(0, 0), new Coordinate(0,-1), new Coordinate(1,-1)},
                {new Coordinate(0, 1), new Coordinate(1, 1), new Coordinate(2, 1), new Coordinate(0, 0)},
                {new Coordinate(0, 1), new Coordinate(1, 1), new Coordinate(1, 0), new Coordinate(1,-1)}
            },
            {
                {new Coordinate(1, 1), new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(2, 0)},
                {new Coordinate(0, 1), new Coordinate(0, 0), new Coordinate(0,-1), new Coordinate(1, 0)},
                {new Coordinate(0, 1), new Coordinate(1, 1), new Coordinate(2, 1), new Coordinate(1, 0)},
                {new Coordinate(0, 0), new Coordinate(1, 1), new Coordinate(1, 0), new Coordinate(1,-1)}
            },
            {
                {new Coordinate(0, 1), new Coordinate(1, 1), new Coordinate(0, 0), new Coordinate(1, 0)},
                {new Coordinate(0, 1), new Coordinate(1, 1), new Coordinate(0, 0), new Coordinate(1, 0)},
                {new Coordinate(0, 1), new Coordinate(1, 1), new Coordinate(0, 0), new Coordinate(1, 0)},
                {new Coordinate(0, 1), new Coordinate(1, 1), new Coordinate(0, 0), new Coordinate(1, 0)}
            },
            {
                {new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(2, 0), new Coordinate(3, 0)},
                {new Coordinate(0, 1), new Coordinate(0, 0), new Coordinate(0,-1), new Coordinate(0,-2)},
                {new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(2, 0), new Coordinate(3, 0)},
                {new Coordinate(0, 1), new Coordinate(0, 0), new Coordinate(0,-1), new Coordinate(0,-2)}
            },
            {
                {new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(1, 1), new Coordinate(2, 1)},
                {new Coordinate(0, 1), new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(1,-1)},
                {new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(1, 1), new Coordinate(2, 1)},
                {new Coordinate(0, 1), new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(1,-1)}
            },
            {
                {new Coordinate(0, 1), new Coordinate(1, 1), new Coordinate(1, 0), new Coordinate(2, 0)},
                {new Coordinate(1, 1), new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(0,-1)},
                {new Coordinate(0, 1), new Coordinate(1, 1), new Coordinate(1, 0), new Coordinate(2, 0)},
                {new Coordinate(1, 1), new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(0,-1)}
            }
        };

        public static Coordinate[,] RandomShape()
        {
            var random = new Random();
            var shapeNum = random.Next(0, shapes.GetLength(0));
            var shape = new Coordinate[4,4];
            for (int i = 0; i < shapes.GetLength(1); i++)
            {
                for (int j = 0; j < shapes.GetLength(2); j++)
                {
                    shape[i, j] = shapes[shapeNum, i, j];
                }
            }

            return shape;
        }
    }
}
