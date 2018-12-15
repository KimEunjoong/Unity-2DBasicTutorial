using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WanzyeeStudio;



public class Player : BaseSingleton<Player>
{
    public enum Direction
    {
        Left = 0,
        Right
    }

    public enum PlayerState
    {
        Idle = 0,
        Move
    }

    #region Properties
    private Animator m_Animator;
    private SpriteRenderer m_Renderer;

    public PlayerState m_State = PlayerState.Idle;
    private Direction m_LookDirection = Direction.Right;
    private Transform m_Transform;
    [SerializeField]
    private float m_MoveSpeed = 1.0f;
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        m_Animator = this.GetComponent<Animator>();
        m_Renderer = this.GetComponent<SpriteRenderer>();
        m_Transform = transform;

        Init();
    }

    protected void Update()
    {
        switch (m_State)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Move:
                MoveX();
                break;
            default:
                break;
        }
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
            default:
                break;
        }
    }

    public void BeginMove()
    {
        m_State = PlayerState.Move;
    }

    private void MoveX()
    {
        float x = m_Transform.position.x + m_MoveSpeed * Time.deltaTime;
        Vector3 newPosition = new Vector3(x, m_Transform.position.y, m_Transform.position.z);
        m_Transform.position = newPosition;
    }
}
