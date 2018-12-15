﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour 
{
    public enum StickState
    {
        Grounded = 0,
        Growing,
        Falling,
        Landed
    }

    #region Properties
    [SerializeField]
	private float m_Height = 0;	
	[SerializeField]
	private float m_GrowSpeed = 0.1f;
    [SerializeField]
    private float m_FallSpeed = 1f;
    private Transform m_Transform;
    private Rigidbody2D m_Rigidbody;
    
    public StickState m_State;
    #endregion

    #region MonoBehaviour
    void Awake()
	{
        m_Transform = transform;
        m_Rigidbody = transform.GetComponent<Rigidbody2D>();

        m_State = StickState.Grounded;
	}

    private void FixedUpdate()
    {
        switch (m_State)
        {
            case StickState.Grounded:
                ProcessMouseInput();
                break;
            case StickState.Growing:
                ProcessMouseInput();
                GrowUp();
                break;
            case StickState.Falling:
                FallRight();
                break;
            case StickState.Landed:
                {
                    Player.instance.m_State = PlayerState.Move;
                }
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Tile") == true)
        {
            m_State = StickState.Grounded;
            //m_State = StickState.Landed;
        }
    }
    #endregion

    private void ProcessKeyInput()
	{
		
	}

    private void ProcessMouseInput()
    {
        if ( Input.GetMouseButtonDown( 0 ) == true )
        {
            m_State = StickState.Growing;
        }
            
        if ( Input.GetMouseButtonUp( 0 ) == true )
        {
            m_State = StickState.Falling;
        }
    }

    /// <summary>
    /// 
    /// </summary>
	private void GrowUp()
	{
        if (m_State != StickState.Growing) return;

		m_Height += m_GrowSpeed * Time.deltaTime;
        Vector3 scale = transform.localScale;
        m_Transform.localScale = new Vector3( scale.x, m_Height, scale.z );
	}

    /// <summary>
    /// 
    /// </summary>
    private void FallRight()
    {
        if (m_State != StickState.Falling) return;

        m_Transform.Rotate(Vector3.back, m_FallSpeed * Time.deltaTime);
    }
}
