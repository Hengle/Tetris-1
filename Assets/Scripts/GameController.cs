using System;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;

public class GameController : MonoBehaviour
{
    [SerializeField] Vector2 spawnerPosition;
    //[SerializeField] MatchController matchControllerPrefab;
    [SerializeField] Tetris tetrisPrefab;
    [SerializeField] UIController uiControllerPrefab;
    [SerializeField] List<Group> pieces;

    IScore scoreData;
    IFakeLeaderboard<int> leaderboardsData;

    //MatchController currMatch;
    UIController currUI;
    Tetris currTetris;

    public Option<bool> isPaused { get; private set; }

	// Use this for initialization
	void Start () {

        scoreData = new Score();
	    leaderboardsData = new Leaderboards();

	    foreach (var loadedLeaderboard in LeaderboardsDataManager.Load())
            leaderboardsData.SetLeaderboard(loadedLeaderboard.leaderboard);

        currUI = Instantiate(uiControllerPrefab);
        currUI.Initialize(
            leaderboardsData,
            scoreData,
            this
            );

	    currTetris = Instantiate(tetrisPrefab);
	}

    public void StartGame()
    {
        isPaused = false.some();
        Debug.Log("Begin controller");

        currTetris.Begin();
    }

    public void ContinueGame()
    {
        isPaused = false.some();
        Debug.Log("Continue controller");

        currTetris.Continue();
    }

    public void PauseMatch()
    {
        isPaused = true.some();
        Debug.Log("Pause controller");

        currTetris.Pause();
    }

    public void EndMatch()
    {
        isPaused = Option<bool>.None;
        Debug.Log("End controller");

        currTetris.End();
    }

}
