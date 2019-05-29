using UnityEngine;
using System.Collections;
using System;

public class movingAlphabetic : MonoBehaviour {

    public int moveSpeedPer = 2;
    public int rotateSpeed=0;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        transform.Translate(Vector3.back/moveSpeedPer, Space.World);
        transform.Rotate(Vector3.up*rotateSpeed , Space.World);

        //Self Destroy
        if (transform.position.z < 0 || GameObject.Find("PlayerObject").GetComponent<spawning>().started == false)
        {
            Destroy(gameObject);
        }
    }
}
