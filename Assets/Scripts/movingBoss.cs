using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movingBoss : MonoBehaviour {

    System.Random rnd = new System.Random();

    public Transform bossLaser;
    public GameObject dieEffect;

    int rndX = 0;
    int hp = 20;

    float delay1, delay2;
    float timeStamp1, timeStamp2;

    // Use this for initialization
    void Start () {
        delay1 = 2;
        timeStamp1 = Time.time + delay1;
        delay2 = 0.5F;
        timeStamp2 = Time.time + delay2;
    }
	
	// Update is called once per frame
	void Update () {

        //Moving
        if (Time.time>=timeStamp1)
        {
            int tempX = rndX;
            rndX = rnd.Next(0, 12);

            float x = GameObject.Find("PlayerObject").GetComponent<movingPlayer>().planes[rndX].transform.position.x;
            float y = GameObject.Find("PlayerObject").GetComponent<movingPlayer>().planes[rndX].transform.position.y - 0.25f;

            transform.position = new Vector3(x, y, transform.position.z);

            timeStamp1 = Time.time + delay1;
        }

        if (Time.time >= timeStamp2)
        {
            //Firing
            Instantiate(bossLaser, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.5f), Quaternion.Euler(90, 0, 0));
            timeStamp2 = Time.time + delay2;
        }

        //Boss HP check
        if (hp < 1)
        {
            GameObject.Find("Score").GetComponent<gameStatus>().gameOver = 2;
            GameObject.Find("Score").GetComponent<gameStatus>().messageTime = Time.time + 5;
            Instantiate(dieEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 0));
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Player Laser
        if (collision.gameObject.name == "Laser(Clone)")
        {
            Destroy(collision.gameObject);
            GameObject.Find("bossHealth").GetComponent<gameStatus>().bossHealth -= 1;
            hp--;
        }
    }
}
