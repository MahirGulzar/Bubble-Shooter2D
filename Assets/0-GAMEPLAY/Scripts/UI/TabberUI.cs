﻿using UnityEngine;
using System.Collections;

public class TabberUI : MonoBehaviour {

    public GameObject ToActive, ToDeactive;

	
    public void OnClicked()
    {
        SoundManager.Instance.ClickPlayButton();
        ToActive.SetActive(true);
        ToDeactive.SetActive(false);
    }

}
