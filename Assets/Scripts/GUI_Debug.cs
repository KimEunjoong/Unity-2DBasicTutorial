﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_Debug : MonoBehaviour
{
    public Stick m_Stick = null;
    private Text m_StickState;
    private Text m_PlayerState;

    private void Awake()
    {
        m_StickState = transform.Find("Text_StickState").GetComponent<Text>();
        m_PlayerState = transform.Find("Text_PlayerState").GetComponent<Text>();
    }

    private void Update()
    {
        if (m_Stick == null) return;

        m_StickState.text = "Stick State = " + m_Stick.m_State.ToString();

        m_PlayerState.text = "Player State = " + Player.instance.m_State.ToString();
    }

}
