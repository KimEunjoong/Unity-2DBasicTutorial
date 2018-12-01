using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Player m_Player = null;

    private Transform m_Transform;

    #region MonoBehaviour
    private void Awake()
    {
        m_Transform = transform;
    }

    private void Update()
    {
        FollowPlayer();
    }
    #endregion

    private void FollowPlayer()
    {
        if ( m_Player == null ) { return; }

        Vector3 cameraPosition = m_Player.transform.position;
        cameraPosition.z = m_Transform.position.z;

        m_Transform.position = cameraPosition;
    }
}
