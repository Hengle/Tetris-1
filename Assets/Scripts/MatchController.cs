using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.tinylabproductions.TLPLib.Components.Interfaces;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;

public class MatchController : MonoBehaviour, IMB_Awake, IScore
{

    public static Option<MatchController> instance = Option<MatchController>.None;
    public float fallSpeed = 1;
    private int score = 0;
    private List<GameObject> blocks = new List<GameObject>();
    public Option<bool> isPaused = Option<bool>.None;

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

        score = 0;
    }

    public void Pause()
    {
        var lastObject = blocks[blocks.Count - 1];
        lastObject.GetComponent<Group>().enabled = false;
        isPaused = true.some();
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
}
