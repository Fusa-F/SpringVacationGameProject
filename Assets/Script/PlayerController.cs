using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    private float horizontalKey;    //x向き
    public float speed = 900f;     //速度
    private float xSpeed;           //キー入力時、代入される速度(初期値０)
    public Vector2 tp = new Vector2(0.01f, 0);   //テレポート後の座標

    public float flap = 900f;
    private bool jump = false;

    GameObject obj;         //パンチオブジェクト
    BoxCollider2D col;

    public GameObject StatusManager;    //ライフ増減フラグ管理
    PlayerStatusManager psm;
    int lifeFlg = 2;

    public GameObject GManager;
    GManager gm;

    private void Start(){
        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();

        obj = this.transform.Find("Punch_obj").gameObject;
        col = obj.GetComponent<BoxCollider2D>();

        psm = StatusManager.GetComponent<PlayerStatusManager>();
        gm = GManager.GetComponent<GManager>();

    }

    private void Update(){

        //ジャンプ・急降下
        if(Input.GetKeyDown(KeyCode.UpArrow) && !jump){
            rb.AddForce(Vector2.up * flap);
            jump = true;
        }

        if(Input.GetKeyDown(KeyCode.DownArrow)){
            rb.AddForce(Vector2.down * flap);
        }

        //パンチ
        if(Input.GetKeyDown(KeyCode.Space)
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("Punch")){

            SetPunchCollider();
            animator.SetTrigger("Punch");
        }else {
            NonSetPunchCollider();
        }

        //左右移動
        horizontalKey = Input.GetAxis("Horizontal");
        xSpeed = 0.0f; 
        if (horizontalKey > 0){
            //anim.SetBool("run", true);
            xSpeed = speed;

        }else if (horizontalKey < 0){
            //anim.SetBool("run", true);
            xSpeed = -speed;

        }else{
            //anim.SetBool("run", false);
            xSpeed = 0.0f;

        }

        rb.velocity = new Vector2(xSpeed, rb.velocity.y);

        //左右テレポート
        if(Input.GetKeyDown(KeyCode.V)){
            SlowTimeScale();
            this.rb.position += tp;          
            
        }else if(Input.GetKeyDown(KeyCode.C)){
            SlowTimeScale();
            this.rb.position -= tp;

        }else {
            ReSetTimeScale();
        }

    }

    private void SlowTimeScale(){
        Time.timeScale = 0f;
    }
    private void ReSetTimeScale(){
        Time.timeScale = 1f;
    }

    //接地判定
    private void OnCollisionEnter2D(Collision2D col){
        jump = false;

        if(col.gameObject.tag == "Item"){
            if(lifeFlg < 2){
                psm.PlayerLifeUp(lifeFlg);
                lifeFlg++;
            }
            
        }
    }

    //Block底判定
    private void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "BlockBottom"){
            if(jump == false){
                psm.PlayerLifeDown(lifeFlg);
                animator.SetTrigger("Damaged");

                if(lifeFlg < 1){
                    animator.SetTrigger("Dead");
                    gm.GameOver();
                }else {
                    lifeFlg--;
                }
            }
        }
    }

    //パンチ当たり判定表示切替
    public void SetPunchCollider(){
        col.enabled = true;
    }

    public void NonSetPunchCollider(){
        col.enabled = false;
    }

}
