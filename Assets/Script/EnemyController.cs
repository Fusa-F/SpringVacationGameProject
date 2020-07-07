using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private AudioSource audioSource;
    private BoxCollider2D boxCollider;
    public AudioClip groundSound;
    public Slider enemyHP;
    private bool isStart;
    private bool isNext;
    public Vector2 enemyPos = new Vector2(10f, 10f); 
    public GameObject enemyPrefab;
    public int enemyMaxHP = 200;

    public GameObject gManager;
    public GManager gm;


    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        boxCollider = this.GetComponent<BoxCollider2D>();

        gManager = GameObject.Find("GManager");
        gm = gManager.GetComponent<GManager>();


        enemyHP = GameObject.Find("EnemyHP").GetComponent<Slider>();
        enemyHP.maxValue = enemyMaxHP;
        enemyHP.value = 0;
        isStart = false;
        isNext = false;

    }

    void FixedUpdate()
    {
        if(isStart != true){
            SetStartEnemy(enemyMaxHP);
            Debug.Log(enemyHP.value);
        }
        if(enemyHP.value <= 0){
            if(isNext != true){
                gm.enemyKillFlg++;

                enemyPrefab = (GameObject)Resources.Load ("Prefab/enemy_00");
                Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
                isNext = true;
            }
            DestroyEnemy();
        }
    }

    public void DestroyEnemy(){
        boxCollider.enabled = false;
        if(this.transform.position.y <= -15f){
            Destroy(this.gameObject);
        }
    }

    public void SetStartEnemy(int maxHP){

        if(enemyHP.value < maxHP){
            enemyHP.value += 250f;
        }else {
            isStart = true;
        }
    }

    public void SetEnemyHP(int damage){
        enemyHP.value -= damage;
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Ground"){
            audioSource.PlayOneShot(groundSound);
        }else if(col.gameObject.tag == "Block"){
            // Debug.Log(enemyHP.value);
        }

    }
}
