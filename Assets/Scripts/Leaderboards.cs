﻿using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using com.tinylabproductions.TLPLib.Extensions;

[Serializable]
public class Leaderboards : IFakeLeaderboard<int> {
    public List<int> leaderboard = new List<int>();

    public List<int> GetLeaderboards() {
        return leaderboard;
    }

    public void AddNewScore(int score) {
        if (leaderboard.Count < 10)  {
            leaderboard.Add(score);
        }
        else {
            var lesserScore = leaderboard.First(x => x < score);
            leaderboard.Insert(leaderboard.IndexOf(lesserScore),score);
            if (leaderboard.Count > 10)
                leaderboard.RemoveRange(9, leaderboard.Count - 1); 
        }
    }

    public void SetLeaderboard(List<int> leaderboard)
    {
        this.leaderboard = leaderboard;
    }

    public override string ToString() {
        var accumulator = "";
        if (leaderboard.isEmpty()) return "No Scores";

        for (int i = 0; i < leaderboard.Count && i < 10; i++) {
            accumulator += (i + 1) + ". " + leaderboard[i] + System.Environment.NewLine;
        }

        return accumulator;
    }


}
