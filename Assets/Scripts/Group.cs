using UnityEngine;
using System.Collections;
using Assets.Scripts;
using com.tinylabproductions.TLPLib.Components.Interfaces;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;
using UnityEngine.SocialPlatforms;

public class Group : MonoBehaviour {

    float lastFall = 0;
    float fallSpeed = 1;

    //Possible change in the future
    //Different ways to calculate score, different objects to hold it, etc
    IScore score;
    MatchController matchController;

    public void Initialize(MatchController controller, IScore score) {
        matchController = controller;
        this.score = score;
    }

    void Start() {
        if (!Grid.isValidGridPos(transform))
                matchController.GameOver();
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            transform.position += new Vector3(-1, 0, 0);

            if (Grid.isValidGridPos(transform))
                Grid.updateGrid(transform);
            else
                transform.position += new Vector3(1, 0, 0);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            transform.position += new Vector3(1, 0, 0);

            if (Grid.isValidGridPos(transform))
                Grid.updateGrid(transform);
            else
                transform.position += new Vector3(-1, 0, 0);
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            transform.Rotate(0, 0, -90);

            if (Grid.isValidGridPos(transform))
                Grid.updateGrid(transform);
            else
                transform.Rotate(0, 0, 90);
        }

        else if (Input.GetKey(KeyCode.DownArrow) ||
                 Time.time - lastFall >= fallSpeed) {
            transform.position += new Vector3(0, -1, 0);

            if (Grid.isValidGridPos(transform))
                Grid.updateGrid(transform);
            else {
                transform.position += new Vector3(0, 1, 0);
                
                if (!Grid.isValidGridPos(transform)) 
                    matchController.GameOver();
                
                var fullRows = Grid.amountOfFullRows();
                if (Grid.amountOfFullRows() > 0) {

                    score.AddScore(fullRows);
                    Grid.deleteFullRows();

                }

                matchController.SpawnNext();

                enabled = false;
            }

            lastFall = Time.time;
        }
    }

}
