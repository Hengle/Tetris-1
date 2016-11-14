using UnityEngine;
using System.Collections;
using com.tinylabproductions.TLPLib.Functional;
using UnityEditor;

public class GameController : MonoBehaviour
{

    public GameObject Menu;
    public GameObject Spawner;
    public GameObject GameUI;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey("escape"))
	    {
	        Menu.gameObject.SetActive(true);
            Spawner.gameObject.SetActive(false);
            GameUI.gameObject.SetActive(false);

            Grid.clearAll();
	    }
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public static void StartNewMatch()
    {

    }

}
