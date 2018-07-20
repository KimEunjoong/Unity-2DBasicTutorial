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
		ProcessKeyInput();
	}
	#endregion

	private void ProcessKeyInput()
	{
		if ( Input.GetKey( KeyCode.UpArrow ) == true )
		{
			m_IsGrowing = true;

			Grow();
		}

		if ( Input.GetKeyUp( KeyCode.UpArrow ) == true )
		{
			m_IsGrowing = false;
		}
	}

	private void Grow()
	{
		if ( m_IsGrowing == false ) return;

		m_Height += m_GrowSpeed * Time.deltaTime;
		transform.localScale = new Vector3( 10f, m_Height, 1f );
	}

}
