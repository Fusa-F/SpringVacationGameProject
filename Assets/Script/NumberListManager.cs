using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberListManager : MonoBehaviour
{

    public List<int> numberList = new List<int>();
    public GameObject numberStackPanel;
    public List<GameObject> numberBox = new List<GameObject>();
    public List<GameObject> boxPrefab = new List<GameObject>();
    public Text text;


    public GameObject calcPanel;
    public Text calcText;
    public List<string> textList = new List<string>();
    public string calc = "CALC SPACE";

    public int changeTextFlg = 0;

    public GameObject scoreManager;
    public ScoreManager sm;

    void Start()
    {
        calcText = calcPanel.GetComponentInChildren<Text>();
        sm = scoreManager.GetComponent<ScoreManager>();
    }

    void Update()
    {
        calcText.text = calc;

        if(changeTextFlg == 1){
            ChangeText();
        }
    }

    public void ChangeText(){
        if(calcText.transform.localPosition.x > 700f){
            textList.Clear();
            calc = "CALC SPACE";
            calcText.transform.localPosition = new Vector2(-700f, 0);
        }

        if(calcText.transform.localPosition.x < 0f && calcText.transform.localPosition.x >= -10f){
            calcText.transform.localPosition = new Vector2(0, 0);
            changeTextFlg = 0;
        }else {
            calcText.transform.Translate(10f, 0, 0);
        }
    }
    
    public void SetText(string txt){
        textList.Add(txt);

        calc = "";
        for(int i = 0; i < textList.Count; i++){
            calc += textList[i];
        }
    }

    public void SetNumber(int number){
        if(numberList.Count < 5){
            numberList.Add(number);
            string num = number.ToString();
            SetNumberStack(num);
            SetText(num);
        }
    }

    public void SetOperator(string ope){
        if(numberList.Count == 1 && ope == "="){
            sm.CountUpScore(numberList[0]); 
            string result = numberList[0].ToString();
            RemoveNumberStack();
            SetText(" " + ope + " ");
            SetText(result);

            changeTextFlg = 1;
            return;
        }

        if(numberList.Count >= 2){
            int prev = numberList.Count - 2;
            int next = numberList.Count - 1;
            switch(ope){
                case "+":
                    numberList[prev] += numberList[next];
                    break;
                case "-":
                    numberList[prev] -= numberList[next];
                    break;
                case "*":
                    numberList[prev] *= numberList[next];
                    break;
                case "/":
                    if(numberList[next] != 0){
                        float div = numberList[prev] / numberList[next];
                        numberList[prev] = (int)div;
                        break;
                    }else {
                        return;
                    }
                case "%":
                    if(numberList[next] != 0){
                        float per = numberList[prev] % numberList[next];
                        numberList[prev] = (int)per;
                        break;
                    }else {
                        return;
                    }
                case "=":
                    return;
            }
            RemoveNumberStack();
            string num = numberList[prev].ToString();
            SetNumberStackText(prev, num);
            SetText(ope);
        }
    }

    public void GetNumber(){
        foreach (int n in numberList){
            Debug.Log(n);
        }
        Debug.Log("end");
    }

    public void SetNumberStack(string num){
        numberBox.Add((GameObject)Resources.Load ("Prefab/Box"));

        Vector2 boxPos = new Vector2(0, -180 + (numberBox.Count * 60));
        boxPrefab.Add((GameObject)Instantiate(numberBox[numberBox.Count - 1], boxPos, Quaternion.identity));
        boxPrefab[boxPrefab.Count - 1].transform.SetParent (numberStackPanel.transform, false);
        SetNumberStackText(boxPrefab.Count - 1, num);
    }
    public void SetNumberStackText(int n, string num){
        foreach (Transform child in boxPrefab[n].transform){
            text = child.gameObject.GetComponent<Text>();
            text.text = num;       
            Debug.Log(num);  

            Image boxColor = boxPrefab[n].GetComponent<Image>();
            if(int.Parse(num) < 0){
                boxColor.color = new Color(3f/255f, 3f/255f, 3f/255f, 1f); //black
            }else if(int.Parse(num) == 0){
                boxColor.color = new Color(0f, 1f, 1f, 1f); //transparent
            }else {
                boxColor.color = new Color(1f, 1f, 1f, 1f); //white
            }
        }
    }

    public void RemoveNumberStack(){
        Destroy(boxPrefab[boxPrefab.Count - 1]);
        numberList.RemoveAt(numberList.Count - 1);
        numberBox.RemoveAt(numberBox.Count - 1);
        boxPrefab.RemoveAt(boxPrefab.Count - 1);
    }
}
