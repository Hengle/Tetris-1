using UnityEngine;
using System.Collections;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;

public class Spawner : MonoBehaviour
{
    
    public static Option<Spawner> instance = Option<Spawner>.None;

    // Groups
    public GameObject[] groups;


    public void spawnNext()
    {
        // Random Index
        int i = Random.Range(0, groups.Length);

        // Spawn Group at current Position
        foreach (var matchController in MatchController.instance)
        {
            matchController.AddGroup(
                (GameObject)Instantiate(groups[i],
                    transform.position,
                    Quaternion.identity)
            );
        }
        
    }

    public void Awake()
    {
        if (instance == Option<Spawner>.None)
        {
            instance = this.some();
        }

        else if (instance != this.some())
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }



}