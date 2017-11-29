using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuyBubbles : MonoBehaviour {

    public int bubbles_buy_price;
    public int bubbles_to_buy;
    public GameObject Temp;


    void OnEnable()
    {
        if(PlayerPrefs.GetInt("NO_OF_COINS")>=25)
        {
            this.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            
            this.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
        }
    }


    public void OnClicked()
    {
        if((PlayerPrefs.GetInt("NO_OF_COINS")-bubbles_buy_price)>=0)
        {
            SoundManager.Instance.ClickBuyButton();
            PlayerPrefs.SetInt("NO_OF_COINS", PlayerPrefs.GetInt("NO_OF_COINS") - bubbles_buy_price);
            GamePrefs.NO_OF_BUBBLES += bubbles_to_buy;
            Temp.GetComponent<EnableAnObject>().OnClicked();
            PlayerPrefs.Save();
        }
    }

}
