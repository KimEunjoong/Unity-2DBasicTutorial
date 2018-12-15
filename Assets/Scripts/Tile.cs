using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 타일 크기가 모두 다르다.
public class Tile : MonoBehaviour
{
    #region Properties    
    public int m_ID = 0;
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
            //// 막대기가 현재 위치하고 있는 타일이 그 다음 타일이 아니라 타일이 원래 위치했던 곳이라면 패스
            //if ( collision.transform.GetComponent<Stick>().m_CurrentTileID == m_ID )
            //{
            //    return;
            //}

            //Player.instance.m_State = Player.State.Move;
        }
    }
}