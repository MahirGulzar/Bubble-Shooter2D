using UnityEngine;
using System.Collections;

public class EventLoggingGoogleAnalytics : MonoBehaviour {

	

    public void Failed_Restart()
    {
        //#if UNITY_ANDROID
        //GoogleAnalytics.instance.SendEvent(GameAnalytics_Events.Game_Play_Activity, "Level" + (GamePrefs.CURRENT_LEVEL + 1), "LevelFailed", "Restart");
        //#endif
    }

    public void Failed_MainMenu()
    {
        #if UNITY_ANDROID
        //GoogleAnalytics.instance.SendEvent(GameAnalytics_Events.Game_Play_Activity, "Level" + (GamePrefs.CURRENT_LEVEL + 1), "LevelFailed", "MainMenu");
        #endif
    }


    // Paused
    public void Paused_Restart()
    {
        #if UNITY_ANDROID
       // GoogleAnalytics.instance.SendEvent(GameAnalytics_Events.Game_Play_Activity, "Level" + (GamePrefs.CURRENT_LEVEL + 1), "Paused", "Restart");
        #endif
    }

    public void Paused_MainMenu()
    {
        #if UNITY_ANDROID
       // GoogleAnalytics.instance.SendEvent(GameAnalytics_Events.Game_Play_Activity, "Level" + (GamePrefs.CURRENT_LEVEL + 1), "Paused", "MainMenu");
        #endif
    }

    public void Paused_Resume()
    {
        #if UNITY_ANDROID
       // GoogleAnalytics.instance.SendEvent(GameAnalytics_Events.Game_Play_Activity, "Level" + (GamePrefs.CURRENT_LEVEL + 1), "Paused", "Resume");
        #endif
    }



    // Cleared
    public void Completed_Restart()
    {
        #if UNITY_ANDROID
       // GoogleAnalytics.instance.SendEvent(GameAnalytics_Events.Game_Play_Activity, "Level" + (GamePrefs.CURRENT_LEVEL + 1), "Completed", "Restart");
        #endif
    }

    public void Completed_MainMenu()
    {
        #if UNITY_ANDROID
        //GoogleAnalytics.instance.SendEvent(GameAnalytics_Events.Game_Play_Activity, "Level" + (GamePrefs.CURRENT_LEVEL + 1), "Completed", "MainMenu");
        #endif
    }

    public void Completed_Next()
    {
        #if UNITY_ANDROID
       // GoogleAnalytics.instance.SendEvent(GameAnalytics_Events.Game_Play_Activity, "Level" + (GamePrefs.CURRENT_LEVEL + 1), "Completed", "NextLevel");
        #endif
    }



    // Buy Bubbles


    public void Continue_Buy_Bubbles()
    {
        #if UNITY_ANDROID
       // GoogleAnalytics.instance.SendEvent(GameAnalytics_Events.Game_Play_Activity, "Level" + (GamePrefs.CURRENT_LEVEL + 1), "ContinueGame", "Coins");
        #endif

        #if UNITY_IOS
        AGameUtils.LogAnalyticEvent("ContinueGame : WithCoins");
#endif

    }

    public void Continue_Free_Bubbles()
    {
        #if UNITY_ANDROID
       // GoogleAnalytics.instance.SendEvent(GameAnalytics_Events.Game_Play_Activity, "Level" + (GamePrefs.CURRENT_LEVEL + 1), "ContinueGame", "FreeClicked");
        #endif

#if UNITY_IOS
        AGameUtils.LogAnalyticEvent("ContinueGame : RewardedVideo");
#endif
    }
    public void Continue_End_Game()
    {
        #if UNITY_ANDROID
       // GoogleAnalytics.instance.SendEvent(GameAnalytics_Events.Game_Play_Activity, "Level" + (GamePrefs.CURRENT_LEVEL + 1), "ContinueGame", "EndGame");
        #endif
    }



    // Store


    public void Store_Free_Coins()
    {
#if UNITY_ANDROID
       // GoogleAnalytics.instance.SendEvent(GameAnalytics_Events.Game_Play_Activity, "Level" + (GamePrefs.CURRENT_LEVEL + 1), "CoinsStore", "CoinsFreeClicked");
        #endif

#if UNITY_IOS
        AGameUtils.LogAnalyticEvent("Store : FreeCoins");

#endif
    }
}
