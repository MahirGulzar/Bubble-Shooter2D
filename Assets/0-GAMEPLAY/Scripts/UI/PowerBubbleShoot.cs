using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerBubbleShoot : MonoBehaviour {
    public GameManager gameManager;
    
    public GameObject Overlay;
    public Text text_count;
    private Obstacle obstacle;
    public SpecialType special_type;
    
    public Sprite[] dulls, actives;

    void OnEnable()
    {

        
        switch (this.special_type)
        {
            case SpecialType.Blade:
                iTween.MoveFrom(this.gameObject.transform.parent.gameObject, iTween.Hash("x",10,"time",1,"ignoretimescale",true));
                if (PlayerPrefs.GetInt("NO_OF_BLADE") > 0)
                {
                    this.GetComponent<Image>().sprite = actives[0];
                }
                else
                {
                    this.GetComponent<Image>().sprite = dulls[0];
                }
                text_count.text = PlayerPrefs.GetInt("NO_OF_BLADE")+"";
               // Debug.Log(text_count.text);
                break;
            case SpecialType.Fire:
                if (PlayerPrefs.GetInt("NO_OF_FIRE") > 0)
                {
                    this.GetComponent<Image>().sprite = actives[1];
                }
                else
                {
                    this.GetComponent<Image>().sprite = dulls[1];
                }
                text_count.text = PlayerPrefs.GetInt("NO_OF_FIRE")+"";;
                //Debug.Log(text_count.text);

                break;
            case SpecialType.Ice:
                if (PlayerPrefs.GetInt("NO_OF_ICE") > 0)
                {
                    this.GetComponent<Image>().sprite = actives[2];
                }
                else
                {
                    this.GetComponent<Image>().sprite = dulls[2];
                }
                text_count.text = PlayerPrefs.GetInt("NO_OF_ICE")+"";;
                //Debug.Log(text_count.text);

                break;
        }
    }

	public void Select_Special()
    {
        obstacle = GameObject.FindObjectOfType<Obstacle>();
        
        switch(this.special_type)
        {
            case SpecialType.Blade:
                if (PlayerPrefs.GetInt("NO_OF_BLADE") > 0)
                {
#if UNITY_ANDROID
                    //GoogleAnalytics.instance.SendEvent(GameAnalytics_Events.Game_Play_Activity, "Level" + (GamePrefs.CURRENT_LEVEL + 1), "UsePowerUp", "Blade");
#endif

#if UNITY_IOS
                     AGameUtils.LogAnalyticEvent("PowerUp : Blade");
#endif
                    SoundManager.Instance.ClickNext_PreviousButton();
                    PlayerPrefs.SetInt("NO_OF_BLADE",PlayerPrefs.GetInt("NO_OF_BLADE")-1);
                    obstacle.isBlade = true;
                    obstacle.special_type = this.special_type;
                    gameManager.OnSpecialBubbleSelected();
                    Overlay.SetActive(false);
                    this.transform.parent.gameObject.SetActive(false);
                    
                    Time.timeScale = 1;

                    
                }
                else
                {
                    gameManager.PanelEnabler.GetComponent<Activators>().ActiveBuyPowerUpStore();
                    //Debug.Log("Take me to store");
                }
                break;
            case SpecialType.Fire:
                if (PlayerPrefs.GetInt("NO_OF_FIRE") > 0)
                {
#if UNITY_ANDROID
                    //GoogleAnalytics.instance.SendEvent(GameAnalytics_Events.Game_Play_Activity, "Level" + (GamePrefs.CURRENT_LEVEL + 1), "UsePowerUp", "Fire");
#endif

#if UNITY_IOS
                    AGameUtils.LogAnalyticEvent("PowerUp : Fire");
#endif
                    SoundManager.Instance.ClickNext_PreviousButton();
                    PlayerPrefs.SetInt("NO_OF_FIRE", PlayerPrefs.GetInt("NO_OF_FIRE") - 1);
                    obstacle.isSpecial = true;
                    obstacle.special_type = this.special_type;
                    gameManager.OnSpecialBubbleSelected();
                    Overlay.SetActive(false);
                    this.transform.parent.gameObject.SetActive(false);
                    
                    Time.timeScale = 1;
                }
                else
                {
                    gameManager.PanelEnabler.GetComponent<Activators>().ActiveBuyPowerUpStore();
                    //Debug.Log("Take me to store");
                }
                break;
            case SpecialType.Ice:
                if (PlayerPrefs.GetInt("NO_OF_ICE") > 0)
                {
#if UNITY_ANDROID
                    //GoogleAnalytics.instance.SendEvent(GameAnalytics_Events.Game_Play_Activity, "Level" + (GamePrefs.CURRENT_LEVEL + 1), "UsePowerUp", "Ice");
#endif

#if UNITY_IOS
                    AGameUtils.LogAnalyticEvent("PowerUp : Ice");
#endif
                    SoundManager.Instance.ClickNext_PreviousButton();
                    PlayerPrefs.SetInt("NO_OF_ICE", PlayerPrefs.GetInt("NO_OF_ICE") - 1);
                    obstacle.isIce = true;
                    obstacle.special_type = this.special_type;
                    gameManager.OnSpecialBubbleSelected();
                    Overlay.SetActive(false);
                    this.transform.parent.gameObject.SetActive(false);
                    
                    Time.timeScale = 1;
                }
                else
                {
                    gameManager.PanelEnabler.GetComponent<Activators>().ActiveBuyPowerUpStore();
                    //Debug.Log("Take me to store");
                }
                break;
        }
        PlayerPrefs.Save();
        
        //


    }

    void Update()
    {
        switch (this.special_type)
        {
            case SpecialType.Blade:
                
                if (PlayerPrefs.GetInt("NO_OF_BLADE") > 0)
                {
                    this.GetComponent<Image>().sprite = actives[0];
                }
                else
                {
                    this.GetComponent<Image>().sprite = dulls[0];
                }
                text_count.text = PlayerPrefs.GetInt("NO_OF_BLADE") + "";
                // Debug.Log(text_count.text);
                break;
            case SpecialType.Fire:
                if (PlayerPrefs.GetInt("NO_OF_FIRE") > 0)
                {
                    this.GetComponent<Image>().sprite = actives[1];
                }
                else
                {
                    this.GetComponent<Image>().sprite = dulls[1];
                }
                text_count.text = PlayerPrefs.GetInt("NO_OF_FIRE") + ""; ;
                //Debug.Log(text_count.text);

                break;
            case SpecialType.Ice:
                if (PlayerPrefs.GetInt("NO_OF_ICE") > 0)
                {
                    this.GetComponent<Image>().sprite = actives[2];
                }
                else
                {
                    this.GetComponent<Image>().sprite = dulls[2];
                }
                text_count.text = PlayerPrefs.GetInt("NO_OF_ICE") + ""; ;
                //Debug.Log(text_count.text);

                break;
        }
    }
}
