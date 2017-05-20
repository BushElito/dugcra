using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudEffect : MonoBehaviour {

    [Range(0.15f, 0.25f)]
    public float speed = 0.2f;

	void Update () {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);

        if (transform.position.x > 17)
        {
            float yVal = Random.Range(-0.5f, 2);
            speed = Random.Range(0.15f, 0.25f);
            transform.position = new Vector3(-17, yVal, transform.position.z);
        }
	}
}
