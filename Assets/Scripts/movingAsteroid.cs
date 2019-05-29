using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingAsteroid : MonoBehaviour {

    public int moveSpeedPer = 1;
    public int rotateSpeed = 0;
    int hp = 3;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.back / moveSpeedPer, Space.World);
        transform.Rotate(Vector3.up * rotateSpeed, Space.World);

        //Self Destroy
        if (transform.position.z < 0 || GameObject.Find("PlayerObject").GetComponent<spawning>().started == false)
        {
            GameObject.Find("Score").GetComponent<gameStatus>().score-=5;
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Alphabet(Clone)")
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "Laser(Clone)")
        {
            Destroy(collision.gameObject);
            hp--;

            if(hp==0)
            Destroy(gameObject);
        }
    }
}
