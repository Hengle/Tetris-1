using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using com.tinylabproductions.TLPLib.Components.Interfaces;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;
using UnityEngine.SocialPlatforms;
using System;

public class MatchController : MonoBehaviour, IMB_Awake, IScore, IFakeLeaderboard<int>
{

    public static Option<MatchController> instance = Option<MatchController>.None;
    public float fallSpeed = 1;

    private int score = 0;

    private List<GameObject> blocks = new List<GameObject>();

    public Option<bool> isPaused = Option<bool>.None;

    private List<int> Leaderboards = new List<int>();

    public void Awake()
    {
        if (instance == Option<MatchController>.None)
        {
            instance = this.some();
        }

        else if (instance != this.some())
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddGroup(GameObject block)
    {
        blocks.Add(block);
    }

    public void NewMatch()
    {
        foreach (var block in blocks)
        {
            Grid.clearAll();
            Destroy(block);
        }

        foreach (var spawner in Spawner.instance)
        {
            spawner.spawnNext();
        }

        isPaused = false.some();
        AddGameScore(score);
        score = 0;
    }

    public void Pause()
    {
        if (isPaused != Option<bool>.None)
        {
            isPaused = true.some();
            var lastObject = blocks[blocks.Count - 1];
            lastObject.GetComponent<Group>().enabled = false;
        }
    }

    public void Continue()
    {
        var lastObject = blocks[blocks.Count - 1];
        lastObject.GetComponent<Group>().enabled = true;
        isPaused = false.some();
    }

    public int GetScore()
    {
        return this.score;
    }

    public void AddScore(int score)
    {
        this.score += score*100;
    }


    public List<int> GetLeaderboards()
    {
        return this.Leaderboards;
    }

    public void AddNewScore(int score)
    {
        this.Leaderboards.Add(score);
    }

    public void AddGameScore(int score)
    {
        this.Leaderboards.Add(score);
        this.Leaderboards.Sort((x, y) => y.CompareTo(x));
    }

    public void GameOver()
    {
        AddGameScore(score);
        foreach (var block in blocks)
        {
            Grid.clearAll();
            Destroy(block);
        }
        
        isPaused = Option<bool>.None;
    }
}
