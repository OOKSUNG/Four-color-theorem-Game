using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public GameObject[] Balls;
    public GameObject RestartButton;
    public GameObject StartButton;
    public InputField nameInput;
    public Text Score;
    public float spawntime = 5.1f;
    public float score = 0f;
    public int count = 0;

    public bool IsStart = false;

    private bool isScoreSent = false;
    private string playerName = "Guest"; // 기본값
    private string apiUrl = "http://api:3000";

    //싱글톤
    public static GameManager Instance;


    public void Awake()
    {
#if UNITY_EDITOR
        // 테스트 환경에서 DestroyImmediate로 즉시 삭제
        if (!Application.isPlaying)
        {
            if (Instance != null && Instance != this)
            {
                DestroyImmediate(gameObject);
                return;
            }
        }
        else
#endif
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        //if(IsStart) StartCoroutine(RepeatEveryFiveSeconds());
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = "Score :" + score;
        if (count == 60 && !isScoreSent)
        {
            isScoreSent = true;
            Time.timeScale = 0;
            Text text = RestartButton.GetComponentInChildren<Text>();
            text.text = "Game Clear!";
            RestartButton.SetActive(true);
            IsStart = false ;

            // 이름 점수 전송
            StartCoroutine(SendScoreToServer(playerName, score));
        }
    }

    IEnumerator RepeatEveryFiveSeconds()
    {
        while (true)
        {
            Instantiate(Balls[Random.Range(0, 4)], new Vector3(0, 5.5f, 0), Quaternion.identity);
            count++;
            score++;
            yield return new WaitForSeconds(spawntime);
        }
    }

    public void StartGame()
    {
        count = 0;
        Time.timeScale = 1;
        StartButton.SetActive(false);
        IsStart = true;
        isScoreSent = false;
        StopAllCoroutines();
        StartCoroutine(RepeatEveryFiveSeconds());

        if (!string.IsNullOrEmpty(nameInput.text))
        {
            playerName = nameInput.text;
            Debug.Log("Player name set: " + playerName);
        }
    }

    public void Restart()
    {
        count = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        RestartButton.SetActive(false);
        IsStart = true;
        isScoreSent = false;
    }


    // 이름+점수 API 전송
    IEnumerator SendScoreToServer(string name, float score)
    {
        string jsonData = JsonUtility.ToJson(new ScoreData(name, score));

        using (UnityWebRequest www = new UnityWebRequest(apiUrl + "/score", "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error: " + www.error);
            }
            else
            {
                Debug.Log("Score uploaded: " + www.downloadHandler.text);
            }
        }
    }
}

[System.Serializable]
public class ScoreData
{
    public string name;
    public float score;

    public ScoreData(string name, float score)
    {
        this.name = name;
        this.score = score;
    }
}


