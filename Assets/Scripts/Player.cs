using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left = 0,
    Right
}

public class Player : MonoBehaviour
{
    #region Properties
    private Animator m_Animator;
    private SpriteRenderer m_Renderer;
    private Direction m_LookDirection = Direction.Right;
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        m_Animator = this.GetComponent<Animator>();
        m_Renderer = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        ProcessKey();
    }
    #endregion

    private void ProcessKey()
    {
        m_Animator.SetBool( "move", false );

        if ( Input.GetKey( KeyCode.RightArrow ) == true )
        {
            m_Animator.SetBool( "move", true );
            m_LookDirection = Direction.Right;
            m_Renderer.flipX = false;
        }

        if ( Input.GetKey( KeyCode.LeftArrow ) == true )
        {
            m_Animator.SetBool( "move", true );
            m_LookDirection = Direction.Left;
            m_Renderer.flipX = true;
        }
    }
}
