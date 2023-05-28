using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeButtonImage : MonoBehaviour
{
    private Sprite soundOn;
    public Sprite soundOff;
    public Button button;
    private bool isOn = true;
    // Start is called before the first frame update
    void Start()
    {
        soundOn = button.image.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeImage(){
        if (isOn){
            button.image.sprite = soundOff;
            isOn = false;
        }else{
            button.image.sprite = soundOn;
            isOn = true;
        }
    }
}
