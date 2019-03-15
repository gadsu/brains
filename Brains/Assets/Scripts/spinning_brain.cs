using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinning_brain : MonoBehaviour {

    public float speed = 30;

	void Update () {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
		
	}
}
