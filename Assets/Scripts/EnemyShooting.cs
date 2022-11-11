using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject m_prefab;
    public Transform m_spawnPosition;

    private IEnumerator m_coroutine;
    public float m_shootDelay;
    private bool m_isShooting = false;

    void Start()
    {
        
    }

    IEnumerator ShootingDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //m_isShooting = false;
    }

    private void Update()
    {
        m_coroutine = ShootingDelay(m_shootDelay);

        Shooting();
        
    }

    public void Shooting()
    {
        // if not shooting and coroutine is null then instantiate bullet and say enemy shooting and start coroutine
        if (!m_isShooting && m_coroutine != null)
        {
            Instantiate(m_prefab, m_spawnPosition.position, m_spawnPosition.rotation);
            m_isShooting = true;
            StartCoroutine(m_coroutine);
        }
    }
}
