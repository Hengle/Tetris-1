using UnityEngine;
using System.Collections;
using Assets.Scripts;
using com.tinylabproductions.TLPLib.Components.Interfaces;
using com.tinylabproductions.TLPLib.Extensions;
using com.tinylabproductions.TLPLib.Functional;
using UnityEngine.SocialPlatforms;

public class Group : MonoBehaviour {

    private float lastFall = 0;
    private float fallSpeed = 1;

    //Possible change in the future
    //Different ways to calculate score, different objects to hold it, etc
    private IScore score;
    private MatchController matchController;

    public void Initialize(MatchController controller, IScore score)
    {
        matchController = controller;
        this.score = score;
    }

    // Use this for initialization
    void Start() {
        // Default position not valid? Then it's game over
        if (!Grid.isValidGridPos(transform))
                matchController.GameOver();
    }

    // Update is called once per frame
    void Update() {
        // Move Left
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            // Modify position
            transform.position += new Vector3(-1, 0, 0);

            // See if valid
            if (Grid.isValidGridPos(transform))
                // It's valid. Update grid.
                Grid.updateGrid(transform);
            else
                // It's not valid. revert.
                transform.position += new Vector3(1, 0, 0);
        }

        // Move Right
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            // Modify position
            transform.position += new Vector3(1, 0, 0);

            // See if valid
            if (Grid.isValidGridPos(transform))
                // It's valid. Update grid.
                Grid.updateGrid(transform);
            else
                // It's not valid. revert.
                transform.position += new Vector3(-1, 0, 0);
        }

        // Rotate
        else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            transform.Rotate(0, 0, -90);

            // See if valid
            if (Grid.isValidGridPos(transform))
                // It's valid. Update grid.
                Grid.updateGrid(transform);
            else
                // It's not valid. revert.
                transform.Rotate(0, 0, 90);
        }

        // Move Downwards and Fall
        else if (Input.GetKey(KeyCode.DownArrow) ||
                 Time.time - lastFall >= fallSpeed) {
            // Modify position
            transform.position += new Vector3(0, -1, 0);

            // See if valid
            if (Grid.isValidGridPos(transform)) {
                // It's valid. Update grid.
                Grid.updateGrid(transform);
            }
            else {
                // It's not valid. revert.
                transform.position += new Vector3(0, 1, 0);
                
                // Finnish the game if a piece is in an invalid position
                if (!Grid.isValidGridPos(transform)) 
                    matchController.GameOver();
                

                // Clear filled horizontal lines
                var fullRows = Grid.amountOfFullRows();
                if (Grid.amountOfFullRows() > 0) {
                    score.AddScore(fullRows);
                    Grid.deleteFullRows();
                }

                // Spawn next Group
                matchController.SpawnNext();

                // Disable script
                enabled = false;
            }

            lastFall = Time.time;
        }
    }

}
