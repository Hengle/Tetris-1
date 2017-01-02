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

    public int points { get; private set; }
    public bool IsPaused { get; private set; }

    float lastFall = 0;
    float fallSpeed = 1;

    // Use this for initialization
    void Start () {
        if (!brickPrefab.GetComponent<Brick>()) throw new Exception("Given brickPrefab does not contain Brick script");

        IsPaused = true;

        instantiatedPool = Instantiate(pool);
        instantiatedPool.PoolUp(width * height);

        tetrisGrid = new TetrisGrid(width,height, groupSize, instantiatedPool);

    }

    // Update is called once per frame
    void Update ()
    {
        if (!IsPaused)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                tetrisGrid.MoveLeft();
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                tetrisGrid.MoveRight();
            }

            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                tetrisGrid.RotateActive();
            }

            else if (Input.GetKey(KeyCode.DownArrow) ||
                     Time.time - lastFall >= fallSpeed)
            {
                if (!tetrisGrid.Drop())
                {
                    Debug.Log("MATCH LOST");    
                }

                points += tetrisGrid.GetPoints(10);
                lastFall = Time.time;
            }
        }

    }


    //I made GetFilledRowBlocks public, so that objects outside of TetrisGrid
    //Could choose how to process line clearing (animations, point counting, etc.)


    public void Begin()
    {
        tetrisGrid.StartGame();
        IsPaused = false;
    }

    public void Pause()
    {
        IsPaused = true;
    }

    public void Continue()
    {
        IsPaused = false;
    }

    public void End() {
        Debug.Log("End match");
        tetrisGrid.EndGame();
    }

}
