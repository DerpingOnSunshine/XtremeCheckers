using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectionScript : MonoBehaviour {
    public Camera camera;
    public GameObject selectorObject;
    private Renderer isVisible;


    // Use this for initialization
    void Start()
    {
        isVisible = GetComponent<Renderer>();
        isVisible.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveSelection(GameObject target)
    {
        selectorObject.transform.SetPositionAndRotation(target.transform.position, selectorObject.transform.rotation);
        isVisible.enabled = true;
    }
    public void Move(GameObject selection, GameObject target)
    {

    }
    public void getSelection()
    {

    }
}
