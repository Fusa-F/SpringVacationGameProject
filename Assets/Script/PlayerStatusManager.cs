using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusManager : MonoBehaviour
{
    public GameObject playerLife;
    private Image lifeImage;
    public PlayerLifeManager[] lm;

    void Start()
    {
        int i = 0;
        foreach (Transform child in playerLife.transform){
            lifeImage = child.GetComponent<Image>();
            lm[i] = lifeImage.GetComponent<PlayerLifeManager>();
            i++;
        }
    }

    void Update()
    {
    }

    public void PlayerLifeUp(int num){
        if(num-1 > 0 && num-1 < lm.Length){
            lm[num].LifeImageFill();
        }
    }

    public void PlayerLifeDown(int num){
        lm[num].LifeImageLost();
    }
}