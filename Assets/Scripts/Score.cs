using UnityEngine;
using System.Collections;

public class Score : IScore
{

    private int score;

	// Use this for initialization
	void Start () {
	    score = 0;
	}

    public int GetScore() {
        return this.score;
    }

    public void AddScore(int score) {
        this.score += score * 100;
    }

    public override string ToString() {
        return score.ToString();
    }

    public void Reset() {
        score = 0;
    }
}
