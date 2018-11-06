using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectionScript : MonoBehaviour
{
    public Camera gameCamera;

    private GameObject tileCheck;
    private GameObject selectedObject;

    public GameObject pieceSelectorObject;
    public GameObject tileSelectorObject;
    private Renderer isVisible;

    private bool canClick = true;

    private Vector3 redDirection; //Negative x
    private Vector3 right; //z = 5
    private Vector3 left; //Negative right
    private Vector3 blackDirection; //Positive x

    // Use this for initialization
    void Start()
    {
        isVisible = GetComponent<Renderer>();
        isVisible.enabled = false;

        redDirection.Set(-5, 0, 0); // Up/Down directions ("Forward")
        blackDirection.Set(5, 0, 0);

        right.Set(0, 0, 5);
        left.Set(0, 0, -5);

        selectedObject = GameObject.Find("boardMiddle");
    }

    IEnumerator selectionWait() //Cooldown for selecting new unit (Not being called as of 11/2)
    {
        Debug.Log("Can click: " + canClick);
        canClick = false;
        // suspend execution for 1f seconds
        yield return new WaitForSeconds(1f);
        print("WaitAndPrint " + Time.time);
        canClick = true;
        Debug.Log("Can click: " + canClick);
    }

    // Update is called once per frame
    void Update()
    {
        //not exactly sure what this does, but I think it will help?
        RaycastHit hit;
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && canClick)
        {
            Transform objectHit = hit.transform;

            // Do something with the object that was hit by the raycast.
            if (Input.GetMouseButton(0) && canClick) //Selection of pieces and stuff and things
            {
                //canClick = false; //Timer normally would reset this to true, but is currently unable to be called due to the error below
                RaycastHit hitInfo = new RaycastHit();
                bool hit1 = Physics.Raycast(gameCamera.ScreenPointToRay(Input.mousePosition), out hitInfo);
                if (hit1)
                {
                    MoveSelection(hitInfo.transform.gameObject);

                    Debug.Log("Selected Object: " + selectedObject);
                    Debug.Log("Attemping to assign " + selectedObject + " to camera lookAt...");

                    gameCamera.GetComponent<CameraControl>().SetLookAtTarget(selectedObject); //THE ERROR MAKER!!! Does not like the parameter selectedObject, even though I made sure it was a valid GameObject.

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
                    if (selectedObject.tag == "gamePiece_r") //Checks selection type for direction
                    {

                    }
                    else if (selectedObject.tag == "gamePiece_b") //Checks selection type for direction
                    {
       
                    }
                }
            }
        }
        selectionWait(); //Prevent selecting the same object multiple times (once per frame)
    }

        void MoveSelection(GameObject target)
        {
            GameObject.FindGameObjectsWithTag("logic_Selection");
            Debug.Log("Target: " + target); //Displays method input object name
            selectedObject = target; //Assigns target to gameObject
            pieceSelectorObject.transform.SetPositionAndRotation //Move selection piece to target
                (target.transform.position, pieceSelectorObject.transform.rotation);
            isVisible.enabled = true; //Make selection piece visible
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

