using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;
using JetBrains.Annotations;

public class BetterGrid : MonoBehaviour
{
    [SerializeField] static int w = 10;
    [SerializeField] static int h = 20;
    
    [SerializeField] [NotNull] GameObject blockPrefab;
    GameObject[] blockPool = new GameObject[w * h];


    Option<GameObject>[,] Grid = new Option<GameObject>[w,h];
    GameObject[] activeGroup = new GameObject[4];

    Vector2 spawnerLocation;

    enum State { Settled, Falling };

    // Use this for initialization
    void Start ()
    {
        blockPool.map(_ => poolUpHelper(blockPrefab));
        foreach (var line in Grid)
        {
            line.map(_ => Option<GameObject>.None);
        }

        spawnerLocation = new Vector2(Mathf.Round(w/2),h);

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    GameObject poolUpHelper (GameObject prefeb)
    {
        var instantiated = Instantiate(blockPrefab);
        instantiated.SetActive(false);
        return instantiated;
    }

    void ChangeState (GameObject ofObject, State toState)
    {
        
    }


    GameObject[] createNewActiveGroup()
    {
        var picked = new List<GameObject>();
        while (picked.Count != 4)
        {
            
        }
    }
}
