using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.tinylabproductions.TLPLib.Data;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;
using UnityEngine.Networking.NetworkSystem;

public class Tetris : MonoBehaviour
{
    [SerializeField] GameObject brickPrefab;
    [SerializeField] TetrisGrid tetrisGrid;

    [SerializeField] int width, height, groupSize;

    List<GameObject> objectPool;

    // Use this for initialization
    void Start () {
        if (!brickPrefab.GetComponent<Brick>()) throw new Exception("Given brickPrefab does not contain Brick script");

        objectPool = PoolUp();
        tetrisGrid = new TetrisGrid(width,height, groupSize);


    }

    // Update is called once per frame
    void Update () {
	
	}


    List<GameObject> PoolUp() {
        var newPool = new List<GameObject>();

        //We pull up enough of bricks to fill every spot on the grid
        //Although it's impossible to do by playing 
        //It gives enough of bricks in the pool
        for (int i = 0; i < width * height; i++) {
            var brick = Instantiate(brickPrefab);
            newPool.Add(brick);
            brick.SetActive(false);
        }

        return newPool;
    }

    List<GameObject> GetPooled(int amount = 1) {
        var pooled = new List<GameObject>();

        for (int i = 0; i < amount; i++) {
            foreach (var objectFromPool in GetSinglePooled()) {
                pooled.Add(objectFromPool);
            }
        }

        return pooled;
    }

    Option<GameObject> GetSinglePooled() {
        foreach (var gameObject in objectPool) {
            if (!gameObject.activeInHierarchy) {
                return gameObject.some();
            }
        }

        return Option<GameObject>.None;
    }

    //Returns false if the object to be returned to pool
    //Doesn't exist in the pool
    bool PutBack(GameObject toReturn) {
        if (objectPool.Contains(toReturn)) {
            toReturn.SetActive(false);
            return true;
        }
        return false;
    }

    public void Begin() {
        Debug.Log("Begin match");
    }

    public void Pause() {
        Debug.Log("Pause match");
    }

    public void Continue() {
        Debug.Log("Continue match");
    }

    public void End() {
        Debug.Log("End match");
    }

}
