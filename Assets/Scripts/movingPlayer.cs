using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class movingPlayer : MonoBehaviour
{
    public GameObject[] planes;
    public GameObject dieEffect;

    int xCur = 0;
    int topBorder = 15;
    int bottomBorder = 5;
    int hp=20;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Moving
        //Left
        if (Input.GetKeyDown("left") || Input.GetKeyDown("a"))
        {
            if (xCur == 11)
                xCur =0;
            else
                xCur++;

            transform.position=new Vector3(planes[xCur].transform.position.x, planes[xCur].transform.position.y, transform.position.z);
            transform.eulerAngles=new Vector3(0,0,planes[xCur].transform.eulerAngles.z);
        }

        //Right
        if (Input.GetKeyDown("right") || Input.GetKeyDown("d"))
        {
            if (xCur == 0)
                xCur = 11;
            else
                xCur--;

            transform.position = new Vector3(planes[xCur].transform.position.x, planes[xCur].transform.position.y, transform.position.z);
            transform.eulerAngles = new Vector3(0, 0, planes[xCur].transform.eulerAngles.z);
        }

        //Top && Bottom
        float z = Input.GetAxis("Vertical") * Time.deltaTime * 4;

        if (transform.position.z > bottomBorder)
            transform.Translate(0, 0, z);
        else
            transform.position = new Vector3(transform.position.x, transform.position.y, bottomBorder);

        if (transform.position.z < topBorder)
            transform.Translate(0, 0, z);
        else
            transform.position = new Vector3(transform.position.x, transform.position.y, topBorder);

        //Player HP check
        if (hp < 1)
        {
            GameObject.Find("Score").GetComponent<gameStatus>().gameOver = 1;
            GameObject.Find("Score").GetComponent<gameStatus>().messageTime = Time.time + 5;
            Instantiate(dieEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, 0));
            Destroy(gameObject);
        }
    }

    //Collisions
    void OnCollisionEnter(Collision collision)
    {
        //Alphabet
        if (collision.gameObject.name == "Alphabet(Clone)")
        {
            if (collision.gameObject.GetComponent<MeshFilter>().mesh.name[0] == GetComponent<spawning>().line[GetComponent<spawning>().progress] && GetComponent<spawning>().progress < GetComponent<spawning>().line.Length)
            {
                GetComponent<spawning>().progressChanged=true;
            }
            else
            {
                GameObject.Find("Score").GetComponent<gameStatus>().score--;
            }
            Destroy(collision.gameObject);
        }

        //Asteroid
        if (collision.gameObject.name == "Asteroid(Clone)")
        {
            hp -= 5;
            GameObject.Find("Health").GetComponent<gameStatus>().health=hp;
            Destroy(collision.gameObject);
        }

        //Boss Laser
        if (collision.gameObject.name == "bossLaser(Clone)")
        {
            hp -= 2;
            GameObject.Find("Health").GetComponent<gameStatus>().health = hp;
            Destroy(collision.gameObject);
        }
    }
}