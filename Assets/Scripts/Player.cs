﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left = 0,
    Right
}

public enum PlayerState
{
    Idle = 0,
    Move,
    PullStick,
    PushStick
}

public class Player : MonoBehaviour
{
    #region Properties
    private Animator m_Animator;
    private SpriteRenderer m_Renderer;

    private PlayerState m_State = PlayerState.Idle;
    private Direction m_LookDirection = Direction.Right;
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        m_Animator = this.GetComponent<Animator>();
        m_Renderer = this.GetComponent<SpriteRenderer>();

        Init();
    }

    private void Update()
    {
        ProcessKey();
    }
    #endregion

    private void Init()
    {
        m_State = PlayerState.Idle;
        m_LookDirection = Direction.Right;
    }

    private void ChangeAnimation( PlayerState _state )
    {
        if ( m_State == _state ) return;

        m_State = _state;

        switch( m_State )
        {
            case PlayerState.Idle:
            m_Animator.SetBool( "move", false );
            break;

            case PlayerState.Move:
            m_Animator.SetBool( "move", true );
            break;

            case PlayerState.PullStick:
            break;

            case PlayerState.PushStick:
            break;

            default:
            break;
        }
    }

    private void ProcessKey()
    {

        if ( Input.GetKey( KeyCode.RightArrow ) == true )
        {
            m_LookDirection = Direction.Right;
            m_Renderer.flipX = false;

            ChangeAnimation( PlayerState.Move );
        }

        if ( Input.GetKeyUp( KeyCode.RightArrow ) == true )
        {
            ChangeAnimation( PlayerState.Idle );
        }


        if ( Input.GetKey( KeyCode.LeftArrow ) == true )
        {
            m_LookDirection = Direction.Left;
            m_Renderer.flipX = true;

            ChangeAnimation( PlayerState.Move );
        }

        if ( Input.GetKeyUp( KeyCode.LeftArrow ) == true )
        {
            ChangeAnimation( PlayerState.Idle );
        }
    }
}