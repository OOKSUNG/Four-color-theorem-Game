using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] Balls;
    public GameObject RestartButton;
    public Text Score;
    public float spawntime = 5.1f;
    public float score = 30f;
    public int count = 0; 

    //ΩÃ±€≈Ê
    public static GameManager Instance;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RepeatEveryFiveSeconds());
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = "Score :" + score;
        if(count == 60)
        {
            Time.timeScale = 0;
            Text text = RestartButton.GetComponentInChildren<Text>();
            text.text = "Game Clear!";
            RestartButton.SetActive(true);
        }
    }

    IEnumerator RepeatEveryFiveSeconds()
    {
        while (true)
        {
            Instantiate(Balls[Random.Range(0,4)], new Vector3(0, 5.5f, 0), Quaternion.identity);
            count++;
            yield return new WaitForSeconds(spawntime);
        }
    }

    public void Restart()
    {
        count = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
