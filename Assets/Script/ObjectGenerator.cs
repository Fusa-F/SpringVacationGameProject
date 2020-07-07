using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject[] obj;      //block
    public GameObject item;      //item
    public GameObject ojama;      //ojama

    public GameObject[] operatorObj;     //記号

    private Vector2 objPlace;

    void Start()
    {
        InvokeRepeating("GenerateObject", 1f, 3f);
        InvokeRepeating("GenerateOperator", 1f, 5f);
        //StartCoroutine("Generate");
        
    }

    void Update()
    {
    }

    public void GenerateObject(){
        int rnd = Random.Range(-5, 3);
        objPlace = new Vector2(rnd, 15);
        Instantiate (obj[Random.Range(0, 9)], objPlace, Quaternion.identity);
    }

    public void GenerateOperator(){
        Instantiate(operatorObj[Random.Range(0, operatorObj.Length)], new Vector2(15, Random.Range(-5, 5)), Quaternion.identity);
    }

    IEnumerator Generate(){
        for(int i=0; i<obj.Length; i++){
            Instantiate (obj[i], new Vector2(Random.Range(-5, 3), 15), Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
        /*yield return new WaitForSeconds(1);
                for(int i=0; i<obj.Length; i++){
            Instantiate (obj[i], new Vector2(Random.Range(-5, 3), 15), Quaternion.identity);
            yield return new WaitForSeconds(1);
        }*/

    }

    public void CancelGenerateObject(){
        CancelInvoke("GenerateObject");
    }
}
