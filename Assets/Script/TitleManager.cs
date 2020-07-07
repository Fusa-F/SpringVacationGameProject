using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip sound1;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S)){
            GameStart();
        }

    }

    public void GameStart(){
        audioSource.PlayOneShot(sound1);
        Invoke("MoveToMainScene",1f);
    }

    public void MoveToMainScene(){
        SceneManager.LoadScene("MainScene");
    }

}
