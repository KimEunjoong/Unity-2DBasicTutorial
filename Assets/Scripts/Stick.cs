using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour 
{
	#region Properties
	[SerializeField]
	private float m_Height;

	[SerializeField]
	private bool m_IsGrowing = false;
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
	}

}
