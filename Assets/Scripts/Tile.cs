using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    #region Properties
    private Stick m_Stick;
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        m_Stick = transform.Find( "Sprite_Stick" ).GetComponent<Stick>();
    }

    private void Update()
    {
        ProcessMouseInput();
    }
    #endregion

    private void ProcessMouseInput()
    {
        if ( Input.GetMouseButtonDown( 0 ) == true )
        {
            m_Stick.gameObject.SetActive( true );
        }
    }
}
