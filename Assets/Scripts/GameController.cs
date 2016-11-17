using UnityEngine;
using System.Collections;
using Assets.Scripts;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;

public class GameController : MonoBehaviour
{
    public Vector2 spawnerPosition;
    public MatchController matchControllerPrefab;
    public UIController uiControllerPrefab;
    public GameObject[] pieces;

    private IScore scoreData;
    private IFakeLeaderboard<int> leaderboardsData;

    private MatchController currMatch;
    private UIController currUI;

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

        currMatch = Instantiate(matchControllerPrefab);
        currMatch.Initialize(
            leaderboardsData, 
            scoreData, 
            spawnerPosition, 
            pieces
            );

        currUI.setMatchController(currMatch);
    }

    public void StartMatch() {
        currMatch.PrepNewMatch();

        currMatch.SpawnNext();
    }

    public void ContinueMatch() {
        currMatch.Continue();
    }

    public void BeforeClosing() {
        LeaderboardsDataManager.Save((Leaderboards)leaderboardsData);
    }

}
