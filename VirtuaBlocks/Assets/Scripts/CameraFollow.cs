using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {


    [SerializeField] Transform target;
    [SerializeField] float distance;
    [SerializeField] float targetHeight;

    private float x = 0;
    private float y = 0;


    //This script is intended to be used with a non-vr version of the game

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

        if(!target) {
            return;
        }
        y = target.eulerAngles.y;

        Quaternion rotation = Quaternion.Euler(x, y, 0);
        transform.rotation = rotation;

        var position = target.position - (rotation * Vector3.forward * distance + new Vector3(0, -targetHeight, 0));



	}
}
