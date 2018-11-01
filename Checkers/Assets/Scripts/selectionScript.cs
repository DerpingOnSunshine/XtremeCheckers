using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectionScript : MonoBehaviour
{
    public Camera gameCamera;
    public GameObject pieceSelectorObject;
    public GameObject tileSelectorObject;

    private GameObject selectedObject;

    private Renderer isVisible;

    private GameObject tileCheck;

    private Vector3 redDirection; //Negative x
    private Vector3 right; //z = 5
    private Vector3 left; //Negative right
    private Vector3 blackDirection; //positive x

    public GameObject selection;

    // Use this for initialization
    void Start()
    {
        isVisible = GetComponent<Renderer>();
        isVisible.enabled = false;

        redDirection.Set(-5, 0, 0); // Up/Down directions ("Forward")
        blackDirection.Set(5, 0, 0);

        right.Set(0, 0, 5);
        left.Set(0, 0, -5);

        selection = GameObject.Find("boardMiddle");
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
                    MoveSelection(hitInfo.transform.gameObject);
                    selection = getSelectedPiece();
                    GetComponent<CameraControl>().SetLookAt(getSelectedPiece());
                    Debug.Log("assigned!");
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
                    if (selection.tag == "gamePiece_r") //Checks selection type for direction
                    {
                        Debug.Log("You've selected a red piece");
                        GameObject tileChecker = Instantiate(pieceSelectorObject, redDirection + right, tileSelectorObject.transform.rotation) as GameObject;
                        GameObject tileChecker1 = Instantiate(pieceSelectorObject, redDirection + left, tileSelectorObject.transform.rotation) as GameObject;
                    }
                    else if (selection.tag == "gamePiece_b") //Checks selection type for direction
                    {
                        Debug.Log("You've selected a black piece");
                        GameObject tileChecker = Instantiate(pieceSelectorObject, blackDirection + right, tileSelectorObject.transform.rotation) as GameObject;
                        GameObject tileChecker1 = Instantiate(pieceSelectorObject, blackDirection + left, tileSelectorObject.transform.rotation) as GameObject;
                    }
                    Debug.Log(selection);
                }
            }
        }
    }

        void MoveSelection(GameObject target)
        {
            Debug.Log(target);
            selectedObject = target;
            pieceSelectorObject.transform.SetPositionAndRotation(target.transform.position, pieceSelectorObject.transform.rotation);
            isVisible.enabled = true;
        }
        void Move(GameObject selection, GameObject target)
        {

        }
        void moveCheck()
        {

        }
        GameObject getSelectedPiece()
        {
            return (selectedObject);
        }
}

