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

    [SerializeField] BrickPool pool;
    List<GameObject> objectPool;

    BrickPool instantiatedPool;

    float lastFall = 0;
    float fallSpeed = 1;

    // Use this for initialization
    void Start () {
        if (!brickPrefab.GetComponent<Brick>()) throw new Exception("Given brickPrefab does not contain Brick script");

        instantiatedPool = Instantiate(pool);
        instantiatedPool.PoolUp(width * height);

        tetrisGrid = new TetrisGrid(width,height, groupSize, instantiatedPool);

        tetrisGrid.SetActiveGroup();
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKey(KeyCode.DownArrow) ||
                 Time.time - lastFall >= fallSpeed)
        {
            tetrisGrid.Drop();
            lastFall = Time.time;
        }
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
