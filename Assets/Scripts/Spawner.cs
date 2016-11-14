using UnityEngine;
using System.Collections;
using com.tinylabproductions.TLPLib.Functional;

public class Spawner : MonoBehaviour
{

    // Groups
    public GameObject[] groups;


    public void spawnNext()
    {
        // Random Index
        int i = Random.Range(0, groups.Length);

        // Spawn Group at current Position
        Instantiate(groups[i],
                    transform.position,
                    Quaternion.identity);
    }

}