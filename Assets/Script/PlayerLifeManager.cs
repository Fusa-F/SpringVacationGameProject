using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeManager : MonoBehaviour
{
    private Image img;
    public Sprite[] sprites;

    void Start()
    {
        img = GetComponent<Image>();
    }

    void Update()
    {
        
    }

    public void LifeImageFill(){
        img.sprite = sprites[0];
    }
    
    public void LifeImageLost(){
        img.sprite = sprites[1];
    }
}
