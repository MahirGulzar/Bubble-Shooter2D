using UnityEngine;
using System.Collections;

public class BuyPowerUps : MonoBehaviour {
    public GameObject Temp;
    public SpecialType sp_type;
    public int power_buy_price;
    public GameObject NeedMoreCoins;
    public bool close = true;
	
   public void OnClicked()
    {
        if ((PlayerPrefs.GetInt("NO_OF_COINS") - power_buy_price) >= 0)
        {
            SoundManager.Instance.ClickBuyButton();
            PlayerPrefs.SetInt("NO_OF_COINS", PlayerPrefs.GetInt("NO_OF_COINS") - power_buy_price);
            switch (this.sp_type)
            {
                case SpecialType.Blade:
                    PlayerPrefs.SetInt("NO_OF_BLADE", PlayerPrefs.GetInt("NO_OF_BLADE") + 1);
    
#if UNITY_ANDROID
                    if(Application.loadedLevelName=="Play")
                    {
                    //    GoogleAnalytics.instance.SendEvent(GameAnalytics_Events.Main_Menu_Activity, "MainMenu", "PowerUpStore", "BuyBlade");
                    }
                    if (Application.loadedLevel == 2)
                    {
                      //  GoogleAnalytics.instance.SendEvent(GameAnalytics_Events.Game_Play_Activity, "Level" + (GamePrefs.CURRENT_LEVEL + 1), "PowerUpStore", "BuyBlade");
                    }
#endif
                    break;
                case SpecialType.Fire:
                    PlayerPrefs.SetInt("NO_OF_FIRE", PlayerPrefs.GetInt("NO_OF_FIRE") + 2);
#if UNITY_ANDROID
                    if(Application.loadedLevelName=="Play")
                    {
                      //  GoogleAnalytics.instance.SendEvent(GameAnalytics_Events.Main_Menu_Activity, "MainMenu", "PowerUpStore", "BuyFire");
                    }
                    if (Application.loadedLevel == 2)
                    {
                      //  GoogleAnalytics.instance.SendEvent(GameAnalytics_Events.Game_Play_Activity, "Level" + (GamePrefs.CURRENT_LEVEL + 1), "PowerUpStore", "BuyFire");
                    }
#endif
                    break;
                case SpecialType.Ice:
                    PlayerPrefs.SetInt("NO_OF_ICE", PlayerPrefs.GetInt("NO_OF_ICE") + 1);
#if UNITY_ANDROID
                    if(Application.loadedLevelName=="Play")
                    {
                       // GoogleAnalytics.instance.SendEvent(GameAnalytics_Events.Main_Menu_Activity, "MainMenu", "PowerUpStore", "BuyIce");
                    }
                    if (Application.loadedLevel==2)
                    {
                       // GoogleAnalytics.instance.SendEvent(GameAnalytics_Events.Game_Play_Activity, "Level" + (GamePrefs.CURRENT_LEVEL + 1), "PowerUpStore", "BuyIce");
                    }
#endif
                    break;
            }
            PlayerPrefs.Save();
            if (close)
            {
                Temp.GetComponent<EnableAnObject>().OnClicked();
            }
        }
        else
        {
            SoundManager.Instance.ClickPlayButton();
            NeedMoreCoins.GetComponent<EnableAnObject>().OnClicked();
        }
    }

}
