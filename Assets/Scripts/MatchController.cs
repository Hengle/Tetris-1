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
    public Option<bool> isPaused { get; private set; }

    List<Group> blocks = new List<Group>();
    IScore score;
    IFakeLeaderboard<int> leaderboard;

    Spawner spawner;

    public void Initialize(IFakeLeaderboard<int> leaderboard, IScore score, Vector2 spawnerPos, List<Group> groups) {
        this.isPaused = Option<bool>.None;
        this.leaderboard = leaderboard;
        this.score = score;

        var spawenerObject = new GameObject();
        spawner = spawenerObject.AddComponent<Spawner>();
        spawner.Initialize(groups);
        spawenerObject.transform.position = spawnerPos;
    }

    public void SpawnNext() {
        blocks.Add(
            spawner.spawnNext(this,score)
            );
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

        leaderboard.AddNewScore(
            score.GetScore()
            );

        score.Reset();

        PrepNewMatch();
    }


    public void PrepNewMatch() {
        foreach (var block in blocks) {
            Grid.clearAll();
            Destroy(block);
        }

    }
}
