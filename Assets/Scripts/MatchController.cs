using UnityEngine;
using System.Collections;
using com.tinylabproductions.TLPLib.Components.Interfaces;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;

public class MatchController : MonoBehaviour, IMB_Awake
{

    public static Option<MatchController> instance = Option<MatchController>.None;
    public float fallSpeed = 1;
    public float score = 0;

    public void Awake()
    {
        if (instance == Option<MatchController>.None)
        {
            instance = this.some();
        }

        else if (instance != this.some())
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
