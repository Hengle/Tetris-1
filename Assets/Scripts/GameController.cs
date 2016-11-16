using UnityEngine;
using System.Collections;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;

public class GameController : MonoBehaviour
{
    public Vector2 spawnerPosition;
    public MatchController matchControllerPrefab;
    public UIController uiControllerPrefab;
    public GameObject[] pieces;

    public Score scoreData;
    public Leaderboards leaderboardsData;

    private MatchController currMatch;
    private UIController currUI;

	// Use this for initialization
	void Start () {
	    currUI = Instantiate(uiControllerPrefab);
        currUI.Initialize(
            leaderboardsData,
            scoreData
            );

        currMatch = Instantiate(matchControllerPrefab);
        currMatch.Initialize(
            leaderboardsData, 
            scoreData, 
            spawnerPosition, 
            pieces
            );

        currUI.setMatchController(currMatch);
    }

    // Update is called once per frame
    void Update () {
	    if (CheckForMatchOver())
            currMatch.FinishMatch();
    }

    public void StartMatch() {
        currMatch.FinishMatch();

        currMatch.SpawnNext();
    }

    public void ContinueMatch() {
        currMatch.Continue();
    }

    private bool CheckForMatchOver() {
        return currMatch.isMatchOver();
    }
}
