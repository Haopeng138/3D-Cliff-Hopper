using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider Col) {
        if(Col.gameObject.tag == "Coin"){
            ScoreManager.Instance.addCoin();
            AudioManager.instance.PlaySFX("CollectCoin");
            Destroy(Col.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
