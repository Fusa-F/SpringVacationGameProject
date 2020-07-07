using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    protected bool isPunched = false;
    public float moveSpeed = 0.5f;  //動く速さ

    public GameObject ScoreManager;
    protected ScoreManager sm;
    public int number;  //ブロック番号
    public int point;   //得点

    protected AudioSource audioSource;
    public AudioClip[] sounds = new AudioClip[3];       //パンチ当たり時サウンド
    public AudioClip blockSound;
    public AudioClip destroySound;
    protected int blockSoundCounter = 0;

    protected Rigidbody2D rb;

    public GameObject NumberListManager;
    public NumberListManager nlm;


    protected virtual void Start(){
        ScoreManager = GameObject.Find("ScoreManager");
        sm = ScoreManager.GetComponent<ScoreManager>();

        audioSource = this.GetComponent<AudioSource>();
        rb = this.GetComponent<Rigidbody2D>();

        NumberListManager = GameObject.Find("NumberListManager");
        nlm = NumberListManager.GetComponent<NumberListManager>();
    }

    protected virtual void Update(){

        if(isPunched){
            //rb.velocity += (new Vector2(moveSpeed, 0));
            //rb.velocity += (new Vector2(moveSpeed, 0)) / rb.mass;
            //rb.velocity += new Vector2(moveSpeed, 0) * Time.fixedDeltaTime;
            //rb.position += new Vector2(moveSpeed, 0);
            this.transform.Translate(moveSpeed, 0, 0);
        }
    }

    public virtual void CallDestroyObject(){    //Invokeで呼ぶ用
        Invoke("DestroyObject", 1f);
        sm.CountUpScore(point);
        audioSource.PlayOneShot(destroySound);
    }
    public virtual void DestroyObject(){
        Destroy(this.gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "Punch"){
            this.isPunched = true;
            audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);

            //ブロックのナンバーをリストにセット
            nlm.SetNumber(number);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D col){

        if(col.gameObject.tag == "Ground"){
            audioSource.PlayOneShot(blockSound);
        }
        else if(col.gameObject.tag == "Wall"){
            this.isPunched = false;
            CallDestroyObject();
        }else if(col.gameObject.tag == "Enemy"){
            this.isPunched = false;
            CallDestroyObject();
        }else {
            //this.isPunched = false;
        }

        if(blockSoundCounter == 0){
            audioSource.PlayOneShot(blockSound);
            blockSoundCounter++;
        }

        foreach(Transform child in this.transform){
            GameObject.Destroy(child.gameObject);
        }
    }

}
