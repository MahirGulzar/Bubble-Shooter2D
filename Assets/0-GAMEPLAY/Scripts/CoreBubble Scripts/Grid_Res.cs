﻿using UnityEngine;
using System.Collections;

public class Grid_Res : MonoBehaviour {

    public Transform Android_Pos;
	void Awake()
    {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN
        this.transform.position = Android_Pos.position;
        this.transform.localScale = Android_Pos.transform.localScale;
#endif


    }
}
