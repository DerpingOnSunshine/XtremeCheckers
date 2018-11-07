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
        gameCamera = Camera.main;
        tileSelectorObject = GameObject.Find("tileSelection");
        pieceSelectorObject = GameObject.Find("selectionPiece");

        isVisible = GetComponent<Renderer>();
        isVisible.enabled = false;

        redDirection.Set(-5, 0, 0); // Up/Down directions ("Forward")
        blackDirection.Set(5, 0, 0);

        right.Set(0, 0, 5);
        left.Set(0, 0, -5);

        selectedObject = GameObject.Find("boardMiddle");
    }

    IEnumerator selectionWait(float countdownValue)
    {
        canClick = false;
        float currCountdownValue = countdownValue;
        while(currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(.5f);
            currCountdownValue--;
        }
        canClick = true;
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

                    gameCamera.GetComponent<CameraControl>().SetLookAtTarget(selectedObject);
                    Debug.Log("Doing a thing");
                    StartCoroutine(selectionWait(.5f));
                    Debug.Log("Did the thing");

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
                        Vector3 selectedPosition = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
                        GameObject tileDood = Instantiate(tileSelectorObject, selectedPosition + redDirection + right, Quaternion.identity);
                        GameObject tileDood1 = Instantiate(tileSelectorObject, selectedPosition + redDirection + left, Quaternion.identity);

                        tileDood.transform.SetParent(tileSelectorObject.transform);
                        tileDood1.transform.SetParent(tileSelectorObject.transform);

                        tileDood.transform.localScale = tileSelectorObject.transform.localScale;
                        tileDood1.transform.localScale = tileSelectorObject.transform.localScale;

                        //need to set the y positon to 0 somehow!
                    }
                    else if (selectedObject.tag == "gamePiece_b") //Checks selection type for direction
                    {
                        Vector3 selectedPosition = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z);
                        GameObject tileDood = Instantiate(tileSelectorObject, selectedPosition + blackDirection + right, Quaternion.identity);
                        GameObject tileDood1 = Instantiate(tileSelectorObject, selectedPosition + blackDirection + left, Quaternion.identity);

                        tileDood.transform.SetParent(tileSelectorObject.transform);
                        tileDood1.transform.SetParent(tileSelectorObject.transform);

                        tileDood.transform.localScale = tileSelectorObject.transform.localScale;
                        tileDood1.transform.localScale = tileSelectorObject.transform.localScale;
                    }
                }
            }
        }
        
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

