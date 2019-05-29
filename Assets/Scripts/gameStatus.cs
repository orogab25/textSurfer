using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class gameStatus : MonoBehaviour {

    public GameObject message;

    public int score=0;
    public int health=20;
    public int bossHealth=20;
    public float time=0;
    public bool isScore, isHealth, isBossHealth, isTime;
    public int gameOver=0;
    public float messageTime;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        //Game over
        if (gameOver == 1)
        {
            message.active = true;
            GameObject.Find("message").GetComponent<Text>().text = "Game over!\nYou died!";

            if (Time.time >= messageTime)
            {
                SceneManager.LoadScene(0);
            }
        }
        if (gameOver == 2)
        {
            message.active = true;
            GameObject.Find("message").GetComponent<Text>().text = "Congratulations!\nYou won!";

            if (Time.time >= messageTime)
            {
                SceneManager.LoadScene(0);
            }
        }

        //Checking
        if (isScore)
            GetComponent < Text >().text="Score: "+score.ToString();

        if (isHealth)
            GetComponent<Text>().text = "Health: " + health.ToString();

        if (isBossHealth && GameObject.Find("PlayerObject").GetComponent<spawning>().bossFight==true)
            GetComponent<Text>().text = "Boss HP: " + bossHealth.ToString();

        if (isTime)
        {
            time +=Time.deltaTime;
            GetComponent<Text>().text = "Time: "+Mathf.Round(time).ToString();
        }
	}
}
