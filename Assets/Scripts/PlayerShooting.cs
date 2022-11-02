using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject m_Prefab;
    public Transform m_spawnPosition;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(m_Prefab, m_spawnPosition.position, m_spawnPosition.rotation);
        }
    }
}
