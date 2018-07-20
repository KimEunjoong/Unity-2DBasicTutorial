using UnityEngine;

public class Background : MonoBehaviour
{
    #region Properties
    /// <summary>
    /// 
    /// </summary>
    private bool m_IsScrolling = false;

    private Direction m_ScrollDirection = Direction.Right;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private float m_ScrollSpeed = 1f;
    #endregion

    #region MonoBehaviour
    private void Start()
    {

    }

    private void Update()
    {
        m_IsScrolling = false;

        if ( Input.GetKey( KeyCode.RightArrow ) == true )
        {
            m_IsScrolling = true;
            m_ScrollDirection = Direction.Right;
        }

        if ( Input.GetKey( KeyCode.LeftArrow ) == true )
        {
            m_IsScrolling = true;
            m_ScrollDirection = Direction.Left;
        }

        Scroll();
    }
    #endregion

    /// <summary>
    /// do scroll
    /// </summary>
    private void Scroll()
    {
        if ( m_IsScrolling == false )
        {
            return;
        }

        Vector3 pos = new Vector3( m_ScrollSpeed * Time.deltaTime, 0f, 0f );

        if ( m_ScrollDirection == Direction.Left )
        {
            transform.position += pos;
        }
        else if ( m_ScrollDirection == Direction.Right )
        {
            transform.position -= pos;
        }
    }
}
