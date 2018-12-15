using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 타일 크기가 모두 다르다.
public class Tile : MonoBehaviour
{
    #region Properties    
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
    }

    private void Update()
    {
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.transform.tag.Equals("Stick") == true )
        {
            Player.instance.m_State = PlayerState.Idle;
        }
    }
}
