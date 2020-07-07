using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextMover : MonoBehaviour
{
    private float degree = 0f; //sinカーブ初期角度
    private Vector2 defaultPos;
    public Text damageText;
    public string text = "NULL";
    public Color color = new Color(1f, 1f, 1f, 1f);
    public int fontSize = 20;

    void Start()
    {
        defaultPos = this.gameObject.transform.position;

        damageText = this.gameObject.GetComponent<Text>();
        damageText.text = text;
        damageText.color = color;
        damageText.fontSize = fontSize;

    }

    public void SetText(string txt){
        this.text = txt;
    }

    public void SetColor(Color c){
        this.color = c;
    }

    public void SetFontSize(int size){
        this.fontSize = size;
    }

    void Update()
    {
        // if(damageTimer <= 0f){
        //     Destroy(this.gameObject);

        // }else {
            if(degree < 180f){
                this.gameObject.transform.position = defaultPos + Vector2.up * 20 * Mathf.Sin(degree * Mathf.Deg2Rad);
                if(degree > 90f){
                    this.gameObject.transform.Translate(-15f, 0, 0);
                }

                degree += 180f * 3 * Time.deltaTime;
            }else {
                Invoke("DestroyDamageText", 1f);
            }
        // }
    }

    public void DestroyDamageText(){
        Destroy(this.gameObject);
    }
}
