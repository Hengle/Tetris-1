using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class Shapes
    {
        static Coordinate[,,] shapes = new Coordinate[7, 4, 4]
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
    }
}
