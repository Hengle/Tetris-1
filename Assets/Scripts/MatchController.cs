using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using com.tinylabproductions.TLPLib.Components.Interfaces;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;
using UnityEngine.SocialPlatforms;
using System;

public class MatchController : MonoBehaviour
{
    private List<GameObject> blocks = new List<GameObject>();
    public Option<bool> isPaused = Option<bool>.None;
    private bool isGameOver = false;
    private IScore score;
    private IFakeLeaderboard<int> leaderboard;

    private Spawner spawner;

    public void Initialize(IFakeLeaderboard<int> leaderboard, IScore score, Vector2 spawnerPos, GameObject[] groups) {
        this.leaderboard = leaderboard;
        this.score = score;

        var spawenerObject = new GameObject();
        spawner = spawenerObject.AddComponent<Spawner>();
        spawner.Initialize(groups);
        spawenerObject.transform.position = spawnerPos;
    }

    public void SpawnNext() {
        blocks.Add(spawner.spawnNext(this,score));
        isGameOver = false;
        isPaused = false.some();
    }

    public void ToMenu() {
        if (isPaused != Option<bool>.None) {
            isPaused = true.some();
            var lastObject = blocks[blocks.Count - 1];
            lastObject.GetComponent<Group>().enabled = false;
        }
    }

    public void Continue() {
        var lastObject = blocks[blocks.Count - 1];
        lastObject.GetComponent<Group>().enabled = true;
        isPaused = false.some();
    }

    public void GameOver() {
        Debug.Log("GAME OVER");

        isPaused = Option<bool>.None;
        isGameOver = true;
    }

    public bool isMatchOver()
    {
        return isGameOver;
    }

    public void FinishMatch() {
        leaderboard.AddNewScore(score.GetScore());

        foreach (var block in blocks)
        {
            Grid.clearAll();
            Destroy(block);
        }

        score.Reset();
    }
}
