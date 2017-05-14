using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBehaviourScript : MonoBehaviour {

    public bool toggle;
	

	void Update () {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ToggleSun();
        }
	}

    public void ToggleSun()
    {
        if (toggle)
        {
            transform.Rotate(new Vector3(180, 0, 0));
        }
        else
        {
            transform.Rotate(new Vector3(180, 0, 0));
        }
        toggle = !toggle;
    }
}
