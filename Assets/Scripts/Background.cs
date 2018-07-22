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
    
    private void Update()
    {
        ProcessMouseInput();
        ProcessKeyInput();
    }
    #endregion

    private void ProcessMouseInput()
    {
        if ( Input.GetMouseButton( 0 ) == true )
        {

        }

        if ( Input.GetMouseButtonUp( 0 ) == true )
        {

        }
    }

    private void ProcessKeyInput()
    {
        
    }

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
