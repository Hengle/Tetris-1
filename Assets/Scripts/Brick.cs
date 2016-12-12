using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour{

    public Coordinate coordinate()
    {
        return new Coordinate(transform.position);
    }

}
