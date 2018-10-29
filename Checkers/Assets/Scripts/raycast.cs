using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0))
        {
            Debug.Log("You done clicked!");

            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                if (hitInfo.transform.gameObject.tag == "gamePiece")
                {
                    Debug.Log("SUCCESS!");
                } else
                {
                    Debug.Log("FAIL!");
                }
            } else
            {
                Debug.Log("Did not hit!");
            }

        }
	}
}
