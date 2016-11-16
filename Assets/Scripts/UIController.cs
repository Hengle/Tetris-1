using UnityEngine;
using System.Collections;
using com.tinylabproductions.TLPLib.Components.Interfaces;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;
using UnityEditor;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public GameObject Menu;
    public GameObject GameUI;
    public GameObject Continue;

    public Text scoreCounter;
    public Text leaderboards;

    private Option<Leaderboards> leaderboardData;
    private Option<Score> scoreData;

    private MatchController matchController;

    public void Initialize(Leaderboards leaderboard, Score score)
    {
        leaderboardData = leaderboard.some();
        scoreData = score.some();
    }

	void Update () {
	    if (Input.GetKey(KeyCode.Escape)) {
	        Menu.gameObject.SetActive(true);
            GameUI.gameObject.SetActive(false);
            matchController.ToMenu();
	    }


	    foreach (var isPaused in matchController.isPaused)
            Continue.gameObject.SetActive(isPaused);

	    foreach (var leaderboard in leaderboardData)
            if (leaderboards.IsActive()) {
                leaderboards.text = leaderboard.ToString();
            }


	    foreach (var score in scoreData)
            if (scoreCounter.IsActive()) {
                scoreCounter.text = score.ToString();
            }

    }

    public static void QuitGame() {
        Application.Quit();
    }

    public void setMatchController(MatchController controller) {
        matchController = controller;
    }

}
