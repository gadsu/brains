using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningAnimation : MonoBehaviour {

    public float speed = 30;

	void Update () {
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
	}
}
