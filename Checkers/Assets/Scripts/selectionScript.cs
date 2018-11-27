using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectionScript : MonoBehaviour
{
    public Camera gameCamera;

    private bool isRedTurn = true;

    private GameObject tileCheck;
    private GameObject selectedObject;

    public GameObject pieceSelectorObject;
    public GameObject tileSelectorObject;

    private Renderer render;

    private bool canClick = true;

    private Vector3 redDirection; //Negative x
    private Vector3 right; //z = 5
    private Vector3 left; //Negative right
    private Vector3 blackDirection; //Positive x

    private GameObject[] boardPieces;
    private GameObject[] gamePieces_r;
    private GameObject[] gamePieces_b;

    // Use this for initialization
    void Start()
    {
        render = GetComponent<Renderer>();
        render.enabled = true;

        gameCamera = Camera.main;
        tileSelectorObject = GameObject.Find("tileSelection");
        pieceSelectorObject = GameObject.Find("selectionPiece");

        redDirection.Set(-5, 0, 0); // Up/Down directions ("Forward")
        blackDirection.Set(5, 0, 0);

        right.Set(0, 0, 5);
        left.Set(0, 0, -5);

        selectedObject = GameObject.Find("boardMiddle");

        boardPieces = GameObject.FindGameObjectsWithTag("board");
        gamePieces_r = GameObject.FindGameObjectsWithTag("gamePiece_r");
        gamePieces_b = GameObject.FindGameObjectsWithTag("gamePiece_b");

        foreach (GameObject objekt in boardPieces) // ignore mispell
        {
            objekt.GetComponent<Collider>().isTrigger = true;
        }
        foreach (GameObject objekt in gamePieces_r)
        {
            objekt.GetComponent<Collider>().isTrigger = true;
        }
        foreach (GameObject objekt in gamePieces_b)
        {
            objekt.GetComponent<Collider>().isTrigger = true;
        }
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
                RaycastHit hitInfo = new RaycastHit();
                bool hit1 = Physics.Raycast(gameCamera.ScreenPointToRay(Input.mousePosition), out hitInfo);
                if (hit1)
                {
                    MoveSelection(hitInfo.transform.gameObject);

                    //Debug.Log("Selected Object: " + selectedObject);
                    //Debug.Log("Attemping to assign " + selectedObject + " to camera lookAt...");

                    gameCamera.GetComponent<CameraControl>().SetLookAtTarget(selectedObject);

                    CreateTiles(selectedObject, false);

                    StartCoroutine(selectionWait(.3f));
                   
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    //  So far it only works on the gamepiece that you've selected. We need the tileSelector(s) instead
    //of the pieceSelector to detect collisions.
    {
        if(this.gameObject.name != "selectionPiece")
        {
            Debug.Log(other.gameObject.name + " " + this.gameObject.name);
            if (this.gameObject.GetComponent<Collider>().name != "selectionPiece")
            {
                Debug.Log("Trigger Enter: " + other.gameObject + " is touching " + this.gameObject);
            }

            if (this.gameObject.GetComponent<Collider>().tag == "board")
            {
                Debug.Log("Touching board!");
                other.GetComponent<Renderer>().enabled = false;
            }
        }
    }
    void CreateTiles(GameObject gamePiece, bool isKing)
    {
        if (gamePiece.tag == "gamePiece_r")
        {
            Vector3 selectedPosition = new Vector3(selectedObject.transform.position.x, 
                selectedObject.transform.position.y, selectedObject.transform.position.z);

            GameObject tileDood = Instantiate(tileSelectorObject, 
                selectedPosition + redDirection + right, Quaternion.identity);
            GameObject tileDood1 = Instantiate(tileSelectorObject, 
                selectedPosition + redDirection + left, Quaternion.identity);

            tileDood.tag = "temp"; 
            tileDood1.tag = "temp"; 

            tileDood.transform.SetParent(tileSelectorObject.transform);
            tileDood1.transform.SetParent(tileSelectorObject.transform);

            tileDood.transform.localScale = tileSelectorObject.transform.localScale;
            tileDood1.transform.localScale = tileSelectorObject.transform.localScale;

            tileDood.transform.position = new Vector3(tileDood.transform.position.x, //.1737 difference between parent and board (Sets to global 0)
                .1737f, tileDood.transform.position.z);

            tileDood1.transform.position = new Vector3(tileDood1.transform.position.x, //.1737 difference between parent and board (Sets to global 0)
                .1737f, tileDood1.transform.position.z);
        }
        if (gamePiece.tag == "gamePiece_b")
        {
            Vector3 selectedPosition = new Vector3(selectedObject.transform.position.x,
                           selectedObject.transform.position.y, selectedObject.transform.position.z);

            GameObject tileDood = Instantiate(tileSelectorObject, 
                selectedPosition + blackDirection + right, Quaternion.identity);
            GameObject tileDood1 = Instantiate(tileSelectorObject, 
                selectedPosition + blackDirection + left, Quaternion.identity);

            tileDood.tag = "temp"; 
            tileDood1.tag = "temp"; 

            tileDood.transform.SetParent(tileSelectorObject.transform);
            tileDood1.transform.SetParent(tileSelectorObject.transform);

            tileDood.transform.localScale = tileSelectorObject.transform.localScale;
            tileDood1.transform.localScale = tileSelectorObject.transform.localScale;

            tileDood.transform.position = new Vector3(tileDood.transform.position.x - .1f, //.1737 difference between parent and board (Sets to global 0)
                .1737f, tileDood.transform.position.z);

            tileDood1.transform.position = new Vector3(tileDood1.transform.position.x - .1f, //.1737 difference between parent and board (Sets to global 0)
                .1737f, tileDood1.transform.position.z);
        }
    }

    //

    void MoveSelection(GameObject target)
    {

        if (GameObject.Find("tileSelection(Clone)") != null)
        {

            ClearBoard();
        }
        selectedObject = target; //Assigns target to gameObject
        pieceSelectorObject.transform.SetPositionAndRotation //Move selection piece to target
            (target.transform.position, pieceSelectorObject.transform.rotation);
    }
    void Move(GameObject selection, GameObject target)
    {
        if(moveCheck(selection, target))
        {
            if(selection.tag == "gamePiece_r" && isRedTurn == true && target.gameObject.GetComponent<Collider>().tag == "logic_Selection")
            {
                Debug.Log("banath");
            }
        }
    }
    bool moveCheck(GameObject selection, GameObject target)
    {
        return true;
    }
    public GameObject getSelectedPiece()
        //Cannot call the method from other scripts??
    {
        Debug.Log("SELECTION SCRIPT: Selected Object: " + selectedObject.name + " " + selectedObject.tag);
        return (selectedObject);
    }
    void ClearBoard()
    {

        foreach (Transform child in tileSelectorObject.transform) //Brute force deletion of clone tiles
        {
            for(int qw = 0; qw < 2; qw++)
            {
                foreach (Transform child1 in child)
                {
                    if (child.gameObject.transform != null)
                    {
                            Debug.Log("Attempting to DESTROY: " + child.gameObject);
                            
                            DestroyImmediate(child.gameObject);
                            Debug.Log("Child: " + child);
                    }
                    else if(child.gameObject.transform == null)
                    {
                        break;
                    }
                }
                Debug.Log("Attempting to DESTROY: " + child.gameObject);
                if (child.gameObject.transform != null)
                {
                        DestroyImmediate(child.gameObject);
                        Debug.Log("Child: " + child);
                }
                else if (child.gameObject.transform == null)
                {
                    break;
                }
            }
        }
    }
}

