using UnityEngine;
using Assets.Scripts;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public GameObject Menu;
    public GameObject GameUI;
    public GameObject Continue;

    public Text scoreCounter;
    public Text leaderboards;

    private Option<IFakeLeaderboard<int>> leaderboardData;
    private Option<IScore> scoreData;

    private GameController gameController;

    public void Initialize(IFakeLeaderboard<int> leaderboard, IScore score, GameController gameController) {
        leaderboardData = leaderboard.some();
        scoreData = score.some();
        this.gameController = gameController;
    }

	void Update () {
	    if (Input.GetKey(KeyCode.Escape)) {
	        Menu.gameObject.SetActive(true);
            GameUI.gameObject.SetActive(false);
            gameController.PauseMatch();
	    }


	    foreach (var isPaused in gameController.isPaused)
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

    public void QuitGame() {
        //gameController.BeforeClosing();
        Application.Quit();
    }

    public void StartGame() {
        gameController.StartGame();
    }

    public void ContinueGame() {
        gameController.ContinueGame();
    }


}
