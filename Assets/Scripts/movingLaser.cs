using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingLaser : MonoBehaviour {

    public bool isPlayer, isBoss;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        if (isPlayer)
        {
            transform.Translate(Vector3.forward, Space.World);

            //Self Destroy
            if (transform.position.z > 80)
            {
                Destroy(gameObject);
            }
        }

        if (isBoss)
        {
            transform.Translate(Vector3.back, Space.World);

            //Self Destroy
            if (transform.position.z < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isPlayer)
        {
            if (collision.gameObject.name == "Alphabet(Clone)")
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
