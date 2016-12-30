using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;

public class BrickPool : MonoBehaviour
{
    [SerializeField] GameObject brickPrefab;
    List<GameObject> objectPool;
    
    public int currentSize { get; private set; }

    public void PoolUp(int size)
    {
        this.currentSize = size;

        var newPool = new List<GameObject>();

        //We pull up enough of bricks to fill every spot on the grid
        //Although it's impossible to do by playing 
        //It gives enough of bricks in the pool
        for (int i = 0; i < currentSize; i++)
        {
            var brick = Instantiate(brickPrefab);
            newPool.Add(brick);
            brick.SetActive(false);
        }

        objectPool = newPool;
    }

    public List<GameObject> GetPooled(int amount = 1)
    {
        var pooled = new List<GameObject>();

        for (int i = 0; i < amount; i++)
        {
            foreach (var objectFromPool in GetSinglePooled())
            {
                objectFromPool.SetActive(true);
                pooled.Add(objectFromPool);
            }
        }

        return pooled;
    }

    public Option<GameObject> GetSinglePooled()
    {
        foreach (var gameObject in objectPool)
        {
            if (!gameObject.activeInHierarchy)
            {
                return gameObject.some();
            }
        }

        return Option<GameObject>.None;
    }

    //Returns false if the object to be returned to pool
    //Doesn't exist in the pool
    public bool PutBack(GameObject toReturn)
    {
        if (objectPool.Contains(toReturn))
        {
            toReturn.SetActive(false);
            return true;
        }
        return false;
    }
}
