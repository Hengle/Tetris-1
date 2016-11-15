using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts;
using com.tinylabproductions.TLPLib.Components.Interfaces;
using com.tinylabproductions.TLPLib.Extensions;

public class Leaderboards : MonoBehaviour, IMB_Awake
{

    private IFakeLeaderboard<int> fakeLeaderboard;

    public void Awake()
    {
        foreach (var leaderboard in MatchController.instance)
        {
            fakeLeaderboard = leaderboard;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public string LeaderboardsToString()
    {
        var accumulator = "";
        var leaderboard = fakeLeaderboard.GetLeaderboards();
        if (leaderboard.isEmpty()) return "No Scores";

        for (int i = 0; i < leaderboard.Count && i < 10; i++)
        {
            accumulator += (i + 1) + ". " + leaderboard[i] + System.Environment.NewLine;
        }

        return accumulator;
    }


}
