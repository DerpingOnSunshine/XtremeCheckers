using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionDetection : MonoBehaviour {

    Material enemyTile;

	// Use this for initialization
	void Start () {
        enemyTile = GameObject.Find("boardMiddle").GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    //  So far it only works on the gamepiece that you've selected. We need the tileSelector(s) instead
    //of the pieceSelector to detect collisions.
    {
        if (other.gameObject.tag == "board")
        {
            this.gameObject.GetComponent<Renderer>().enabled = false;
        }
        else if(other.gameObject.tag == "gamePiece_r")
        {
            if(GameObject.Find("tileSelection").GetComponent<selectionScript>().getSelectedPiece().tag == "gamePiece_b")
            {
                this.gameObject.GetComponent<Renderer>().material = enemyTile;
            }
        }
        else if (other.gameObject.tag == "gamePiece_b" &&
            GameObject.Find("tileSelection").GetComponent<selectionScript>().
            getSelectedPiece().tag == "gamePiece_r")
        {
            //this.gameObject.GetComponent<Renderer>().material = enemyTile;
        }
    }
}
