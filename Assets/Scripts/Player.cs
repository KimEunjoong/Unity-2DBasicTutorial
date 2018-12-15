using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WanzyeeStudio;



public class Player : BaseSingleton<Player>
{
    public enum State
    {
        Idle = 0,
        Move
    }

    #region Properties    
    public int m_CurrentTileID = 0;
    private Animator m_Animator;
    private SpriteRenderer m_Renderer;

    public State m_State = State.Idle;
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
            case State.Idle:
                break;
            case State.Move:
                MoveX();
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Stick") == true)
        {
            // 캐릭터가 이동하다가 끝자락에 위치한 막대기에 걸리면 플레이어 멈추고 idle 상태
            if (collision.transform.GetComponent<Stick>().m_ID == m_CurrentTileID)
            {
                m_State = State.Idle;
            }
        }
    }
    #endregion

    /// <summary>
    /// 초기화
    /// </summary>
    private void Init()
    {
        m_State = State.Idle;
        m_LookDirection = Direction.Right;
    }

    private void ChangeAnimation( State _state )
    {
        if ( m_State == _state ) return;

        m_State = _state;

        switch( m_State )
        {
            case State.Idle:
                m_Animator.SetBool( "move", false );
                break;
            case State.Move:
                m_Animator.SetBool( "move", true );
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
