using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    private bool walking = false;
    private Vector3 sprawnPoint;

	// Use this for initialization
	void Start () {
        sprawnPoint = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (walking)
        {
            transform.position = transform.position + Camera.main.transform.forward * .7f * Time.deltaTime;
        }

        if(transform.position.y<-10f)
        {
            transform.position = sprawnPoint;
        }

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            if (hit.collider.name.Contains("Plane"))
            {
                walking = false;
            }else
            {
                walking = true;
            }
        }
	}
}
