using UnityEngine;
using System.Collections;
using com.tinylabproductions.TLPLib.Components.Interfaces;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;
using UnityEditor;

public class UIController : MonoBehaviour
{

    public GameObject Menu;
    public GameObject Spawner;
    public GameObject GameUI;
    public GameObject Continue;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.Escape))
	    {
	        Menu.gameObject.SetActive(true);
            Spawner.gameObject.SetActive(false);
            GameUI.gameObject.SetActive(false);

	        foreach (var matchController in MatchController.instance)
	        {
	            matchController.Pause();
	        }
	    }

	    foreach (var matchController in MatchController.instance)
	    {
	        foreach (var isPaused in matchController.isPaused)
	        {
	            Continue.gameObject.SetActive(isPaused);
	        }
	    }
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

}
