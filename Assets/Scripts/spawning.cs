using UnityEngine;
using System.Collections;
using System;
using System.Timers;
using UnityEngine.UI;

public class spawning : MonoBehaviour {

    public Mesh[] meshes;
    public Transform alphabet;
    public Transform laser;
    public Transform asteroid;
    public Transform boss;
    public GameObject message;

    float x, y, z;
    float messageDelay, delay1, delay2;
    float messageTime, timeStamp1, timeStamp2;

    bool bossSpawned = false;

    public bool bossFight = false;
    public string line = "Surfer".ToUpper();
    public string lineProgress;
    public int progress;
    public bool progressChanged;
    public bool started;

    System.Random rnd = new System.Random();

    // Use this for initialization
    void Start () {
        messageDelay = 5;
        delay1 = 1;
        delay2 = 3;
        messageTime = Time.time + messageDelay;
        timeStamp1 = Time.time + delay1;
        timeStamp2 = Time.time + delay2;
    }
	
	// Update is called once per frame
	void Update () {

        //Delay to start
        if (Time.time>=messageTime)
        {
            message.active = false;
            started = true;
        }

        //Game starts
        if (started)
        {
            GameObject.Find("Line").GetComponent<Text>().text = line;
            GameObject.Find("LineProgress").GetComponent<Text>().text = lineProgress;

            //Spawning
            if (bossFight == false)
            {
                if (Time.time>=timeStamp1)
                {
                    int rndMesh = rnd.Next(0, line.Length);
                    int rndX = rnd.Next(0, 12);

                    for (int i = 0; i < meshes.Length; i++)
                    {
                        if (line[rndMesh] == meshes[i].name[0])
                        {
                            alphabet.GetComponent<MeshFilter>().mesh = meshes[i];
                            alphabet.GetComponent<MeshCollider>().sharedMesh = meshes[i];
                            break;
                        }
                    }
                    x = GetComponent<movingPlayer>().planes[rndX].transform.position.x;
                    y = GetComponent<movingPlayer>().planes[rndX].transform.position.y - 0.25f;
                    z = GetComponent<movingPlayer>().planes[rndX].transform.eulerAngles.z;

                    Instantiate(alphabet, new Vector3(x, y, 80), Quaternion.Euler(0, 180, 0));

                    timeStamp1 = Time.time + delay1;
                }

                if (Time.time >= timeStamp2)
                {
                    int rndX = rnd.Next(0, 12);

                    x = GetComponent<movingPlayer>().planes[rndX].transform.position.x;
                    y = GetComponent<movingPlayer>().planes[rndX].transform.position.y;
                    z = GetComponent<movingPlayer>().planes[rndX].transform.eulerAngles.z;

                    Instantiate(asteroid, new Vector3(x, y, 80), Quaternion.Euler(-90, -90, 0));

                    timeStamp2 = Time.time + delay2;
                }
            }
            else if (bossSpawned == false)
            {
                int rndX = rnd.Next(0, 12);

                x = GetComponent<movingPlayer>().planes[rndX].transform.position.x;
                y = GetComponent<movingPlayer>().planes[rndX].transform.position.y;
                z = GetComponent<movingPlayer>().planes[rndX].transform.eulerAngles.z;

                Instantiate(boss, new Vector3(x, y, 60), Quaternion.Euler(0, 180, 0));

                bossSpawned = true;
            }

            //Firing
            if (Input.GetButtonDown("Fire1"))
            {
                Instantiate(laser, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.5f), Quaternion.Euler(90, 0, 0));
            }
        }

        //Checking Progress
        if(progressChanged==true)
        {
            string colored = "<color=#00ff00>" + GetComponent<spawning>().line[progress] + "</color>";
            GetComponent<spawning>().lineProgress += colored;
            GameObject.Find("Score").GetComponent<gameStatus>().score++;
            progress++;
            progressChanged = false;
        }

        if (progress >= line.Length)
            bossFight = true;
    }
}
