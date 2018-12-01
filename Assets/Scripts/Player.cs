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
    private Transform m_Transform;
    [SerializeField]
    private float m_MoveSpeed = 1.0f;
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        m_Animator = this.GetComponent<Animator>();
        m_Renderer = this.GetComponent<SpriteRenderer>();
        m_Transform = transform;

        Init();
    }

    private void Update()
    {
        ProcessMouseInput();
        ProcessKeyInput();
    }
    #endregion

    /// <summary>
    /// 초기화
    /// </summary>
    private void Init()
    {
        m_State = PlayerState.Idle;
        m_LookDirection = Direction.Right;
    }

    private void ProcessKeyInput()
    {
        
    }

    private void ProcessMouseInput()
    {
        if ( Input.GetMouseButton( 0 ) == true )
        {
            //MoveX();
        }

        if ( Input.GetMouseButtonUp( 0 ) == true )
        {
            
        }
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

    private void MoveX()
    {
        float x = m_Transform.position.x + m_MoveSpeed * Time.deltaTime;
        Vector3 newPosition = new Vector3(x, m_Transform.position.y, m_Transform.position.z);
        m_Transform.position = newPosition;
    }
}
