using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    GameObject textObj;
    Text text;
    int score = 0;
    int oldScore;

    public GameObject enemyController;
    public EnemyController ec;
    public Vector2 enemyPos;

    public GameObject canvas;

    void Start(){
        textObj = GameObject.Find("ScorePanel/Text");
        oldScore = score;

        SetEnemyScript();
    }

    void Update()
    {
        if(score != oldScore){
            text = textObj.GetComponent<Text>();
            text.text = "Score: " + score;
            oldScore = score;
        }
    }

    public void SetEnemyScript(){
        enemyController = GameObject.Find("enemy_00");
        ec = enemyController.GetComponent<EnemyController>();
        enemyPos = enemyController.transform.position;
    }

    public void CountUpScore(int point){
        //pointの正負ゼロチェック
        bool minusJudge = false;
        bool zeroJudge = false;
        if(point < 0){
            point *= -1;
            minusJudge = true;
        }
        if(point == 0){
            zeroJudge = true;
        }

        //スコア加算　enemyHP減算
        score += point;
        ec.SetEnemyHP(point);

        //ダメージ量テキストプレハブ設定・生成
        GameObject damagetextPrefab = (GameObject)Resources.Load ("Prefab/DamageText");
        DamageTextMover dtm = damagetextPrefab.GetComponent<DamageTextMover>();
        dtm.SetText(point.ToString());

        Color txtColor;
        if(minusJudge){
            txtColor = new Color(3f/255f, 3f/255f, 3f/255f, 1f);
        }else if(zeroJudge){
            txtColor = new Color(0f, 1f, 1f, 1f);
        }else {
            txtColor = new Color(1f, 1f, 1f, 1f);
        }

        int fontSize;
        if(point > 999){
            fontSize = 35;
            txtColor = new Color(1f, 0f, 0f, 1f);
        }else {
            fontSize = 20;
        }

        dtm.SetColor(txtColor);
        dtm.SetFontSize(fontSize);

        GameObject damagetextobj = Instantiate(damagetextPrefab, new Vector2(130f, Random.Range(0f, 10f)), Quaternion.identity);
        damagetextobj.transform.SetParent (canvas.transform, false);
        // damagetextobj.transform.position = new Vector2(8f, 5f);

    }

    public int GetScore(){
        return score;
    }
}
