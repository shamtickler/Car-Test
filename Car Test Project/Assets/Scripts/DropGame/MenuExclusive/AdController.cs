﻿using UnityEngine;
using UnityEngine.Advertisements;

public class AdController : MonoBehaviour
{
    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
    }
}