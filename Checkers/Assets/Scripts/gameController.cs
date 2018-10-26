using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour {
    int[] selectedPieceLocation;
    int[,] boardArray;
    public Transform selection;

    // Use this for initialization
    void Start () {
        boardArray = new int[,] //0 = Empty, 1 = Red, 2 = Black
        {
            {2, 0, 2, 0, 2, 0, 2, 0},
            {0, 2, 0, 2, 0, 2, 0, 2},
            {2, 0, 2, 0, 2, 0, 2, 0},
            {0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0},
            {1, 0, 1, 0, 1, 0, 1, 0},
            {0, 1, 0, 1, 0, 1, 0, 1},
            {1, 0, 1, 0, 1, 0, 1, 0},
        };
    }
	
	// Update is called once per frame
	void Update () {
        //not exactly sure what this does, but I think it will help?
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;

            // Do something with the object that was hit by the raycast.
        }
    }
}
