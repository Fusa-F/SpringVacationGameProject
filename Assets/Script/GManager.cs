using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class GManager : MonoBehaviour
{
    public GameObject canvas;
    public GameObject gameOver;
    private Button[] buttons = new Button[2];

    public GameObject ObjectGenerator;
    ObjectGenerator og;

    public Text timerPanelText;
    CountDownTimer cdTimer;

    public Text statusPanelText;

    AudioSource audioSource;
    public AudioClip gameOverSound;
    public AudioClip retrySound;
    public AudioClip titleSound;

    public int enemyKillFlg = 0;
    private int oldEnemyKillFlg;
    public float killCount = 0;

    void Start()
    {
        og = ObjectGenerator.GetComponent<ObjectGenerator>();    
        cdTimer = timerPanelText.GetComponent<CountDownTimer>();  
        oldEnemyKillFlg = enemyKillFlg;

        audioSource = this.GetComponent<AudioSource>();
    } 

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            Retry();
        }
        if(Input.GetKeyDown(KeyCode.T)){
            Title();
        }
        if(Input.GetKeyDown(KeyCode.Q)){
            GameOver();
        }

        if(oldEnemyKillFlg != enemyKillFlg){
            killCount = enemyKillFlg / 2;
            statusPanelText.text = "[ENEMY COUNT] = " + killCount.ToString();
            oldEnemyKillFlg = enemyKillFlg;
        }
        
    }

    public void GameOver(){
        Time.timeScale = 0f;
        audioSource.Stop();
        audioSource.PlayOneShot(gameOverSound);
        og.CancelGenerateObject();
        cdTimer.StopTimer();

        gameOver = (GameObject)Resources.Load ("Prefab/GameOverPanel");
        GameObject gameOverPrefab = (GameObject)Instantiate(gameOver);
        gameOverPrefab.transform.SetParent (canvas.transform, false);

        buttons = gameOverPrefab.GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(Retry);
        buttons[1].onClick.AddListener(Title);
    }

    public void Retry(){
        Time.timeScale = 1f;
        audioSource.PlayOneShot(gameOverSound);
        SceneManager.LoadScene("MainScene");
    }

    public void Title(){
        Time.timeScale = 1f;
        audioSource.PlayOneShot(gameOverSound);
        SceneManager.LoadScene("TitleScene");
    }
    
}
