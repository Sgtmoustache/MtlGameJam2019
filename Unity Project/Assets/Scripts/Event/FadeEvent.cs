﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEvent : MonoBehaviour
{
    public Texture2D fadeTexture;
    public bool test;
    [Range(0.1f, 1f)]
    public float fadespeed;
    public int drawDepth = -1000;

    private float alpha = 1f;
    private float fadeDir = -1f;

    // Use this for initialization
    void Start()
    {
        test = false;
    }

    public void changebool()
    {
        test = !test;
    }

    public void OnGUI()
    {
        if (test)
        {
            alpha += fadeDir * fadespeed * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);

            Color newColor = GUI.color;
            newColor.a = alpha;

            GUI.color = newColor;

            GUI.depth = drawDepth;

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
            
        }
    }
}
