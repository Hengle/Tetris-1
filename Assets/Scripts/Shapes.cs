using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class Shapes
    {
        static Vector2[,,] shapes = new Vector2[7, 4, 4]
        {
            {
                {new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0) },
                {new Vector2(0, 1), new Vector2(1, 1), new Vector2(0, 0), new Vector2(0,-1) },
                {new Vector2(0, 1), new Vector2(1, 1), new Vector2(2, 1), new Vector2(2, 0) },
                {new Vector2(1, 1), new Vector2(1, 0), new Vector2(1,-1), new Vector2(0,-1) }
            },
            {
                {new Vector2(2, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0)},
                {new Vector2(0, 1), new Vector2(0, 0), new Vector2(0,-1), new Vector2(1,-1)},
                {new Vector2(0, 1), new Vector2(1, 1), new Vector2(2, 1), new Vector2(0, 0)},
                {new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0), new Vector2(1,-1)}
            },
            {
                {new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0)},
                {new Vector2(0, 1), new Vector2(0, 0), new Vector2(0,-1), new Vector2(1, 0)},
                {new Vector2(0, 1), new Vector2(1, 1), new Vector2(2, 1), new Vector2(1, 0)},
                {new Vector2(0, 0), new Vector2(1, 1), new Vector2(1, 0), new Vector2(1,-1)}
            },
            {
                {new Vector2(0, 1), new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, 0)},
                {new Vector2(0, 1), new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, 0)},
                {new Vector2(0, 1), new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, 0)},
                {new Vector2(0, 1), new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, 0)}
            },
            {
                {new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(3, 0)},
                {new Vector2(0, 1), new Vector2(0, 0), new Vector2(0,-1), new Vector2(0,-2)},
                {new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(3, 0)},
                {new Vector2(0, 1), new Vector2(0, 0), new Vector2(0,-1), new Vector2(0,-2)}
            },
            {
                {new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(2, 1)},
                {new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(1,-1)},
                {new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(2, 1)},
                {new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(1,-1)}
            },
            {
                {new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0), new Vector2(2, 0)},
                {new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(0,-1)},
                {new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0), new Vector2(2, 0)},
                {new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(0,-1)}
            }
        };
    }
}
