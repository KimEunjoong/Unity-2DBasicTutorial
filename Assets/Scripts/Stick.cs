using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour 
{
	#region Properties
	[SerializeField]
	private float m_Height = 0;

	[SerializeField]
	private bool m_IsGrowing = false;

	[SerializeField]
	private float m_GrowSpeed = 1f;
	#endregion

	#region MonoBehaviour
	void Awake()
	{

	}

	void Update()
	{
        ProcessMouseInput();
		ProcessKeyInput();
	}
	#endregion

	private void ProcessKeyInput()
	{
		
	}

    private void ProcessMouseInput()
    {
        if ( Input.GetMouseButton( 0 ) == true )
        {
            m_IsGrowing = true;

            GrowUp();
        }
            
        if ( Input.GetMouseButtonUp( 0 ) == true )
        {
            m_IsGrowing = false;

            FallRight();
        }
    }

    /// <summary>
    /// 
    /// </summary>
	private void GrowUp()
	{
		if ( m_IsGrowing == false ) return;

		m_Height += m_GrowSpeed * Time.deltaTime;
        Vector3 scale = transform.localScale;
		transform.localScale = new Vector3( scale.x, m_Height, scale.z );
	}

    /// <summary>
    /// 
    /// </summary>
    private void FallRight()
    {

    }
}
