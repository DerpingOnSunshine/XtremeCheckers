using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour {

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
        if (Input.GetMouseButton(0)) //Selection of pieces and stuff and things
        {
            Debug.Log("You done clicked!");

            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                if (hitInfo.transform.gameObject.tag == "gamePiece_r")
                {
                    Debug.Log("SUCCESS!");
                }
                else
                {
                    Debug.Log("FAIL!");
                }
            }
            else
            {
                Debug.Log("Did not hit!");
            }

        }
    }
}
