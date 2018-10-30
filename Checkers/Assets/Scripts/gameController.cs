using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    int[] selectedPieceLocation;
    int[,] boardArray;
    public Transform selection;
    public Camera gameCamera;

    // Use this for initialization
    void Start()
    {
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
    void Update()
    {
        //not exactly sure what this does, but I think it will help?
        RaycastHit hit;
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;

            // Do something with the object that was hit by the raycast.
            if (Input.GetMouseButton(0)) //Selection of pieces and stuff and things
            { 
                RaycastHit hitInfo = new RaycastHit();
                bool hit1 = Physics.Raycast(gameCamera.ScreenPointToRay(Input.mousePosition), out hitInfo);
                if (hit1)
                {
                    GameObject.Find("selectionPiece").GetComponent<selectionScript>().MoveSelection(hitInfo.transform.gameObject);
                    if (hitInfo.transform.gameObject.tag == "gamePiece_r")
                    {
                        Debug.Log("Red!");
                    }
                    if (hitInfo.transform.gameObject.tag == "gamePiece_b")
                    {
                        Debug.Log("Black!");
                    }
                    if (hitInfo.transform.gameObject.tag == "redTile")
                    {
                        Debug.Log("Black Tile!");
                    }
                    if (hitInfo.transform.gameObject.tag == "blackTile")
                    {
                        Debug.Log("Black Tile!");
                    }
                }
                else
                {
                    Debug.Log("Did not hit!");
                }
            }
        }
    }
}
