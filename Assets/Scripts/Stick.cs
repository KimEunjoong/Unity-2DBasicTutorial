using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour 
{
    public enum State
    {
        Grounded = 0,
        Growing,
        Falling,
        Landed
    }

    #region Properties
    public int m_ID = 0;
    [SerializeField]
	private float m_Height = 0;	
	[SerializeField]
	private float m_GrowSpeed = 10f;
    [SerializeField]
    private float m_FallSpeed = 100f;
    private Transform m_Transform;
    private Rigidbody2D m_Rigidbody;

    public int m_CurrentTileID = 0;   // 막대기가 위치해 있는 현재 타일의 ID
    public State m_State;
    #endregion

    #region MonoBehaviour
    void Awake()
	{
        m_Transform = transform;
        m_Rigidbody = transform.GetComponent<Rigidbody2D>();

        m_State = State.Grounded;
	}

    private void FixedUpdate()
    {
        if (m_ID == m_CurrentTileID)
        {
            return;
        }

        switch (m_State)
        {
            case State.Grounded:                
                ProcessMouseInput();             
                break;
            case State.Growing:                
                ProcessMouseInput();
                GrowUp();                
                break;
            case State.Falling:
                FallRight();
                break;
            case State.Landed:
                {
                    Player.instance.m_State = Player.State.Move;
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
            if ( collision.transform.GetComponent<Tile>().m_ID != m_ID)
            {
                m_State = State.Landed;
            }
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
            m_State = State.Growing;
        }
            
        if ( Input.GetMouseButtonUp( 0 ) == true )
        {
            m_State = State.Falling;
        }
    }

    /// <summary>
    /// 
    /// </summary>
	private void GrowUp()
	{
        if (m_State != State.Growing) return;

		m_Height += m_GrowSpeed * Time.deltaTime;
        Vector3 scale = transform.localScale;
        m_Transform.localScale = new Vector3( scale.x, m_Height, scale.z );
	}

    /// <summary>
    /// 
    /// </summary>
    private void FallRight()
    {
        if (m_State != State.Falling) return;

        m_Transform.Rotate(Vector3.back, m_FallSpeed * Time.deltaTime);

    }
}
