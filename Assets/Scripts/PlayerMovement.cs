using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    private float m_horizontal;
    [SerializeField] private float m_speed = 5f;
    [SerializeField] private Rigidbody2D m_RB;

    void Update()
    {
        m_horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        m_RB.velocity = new Vector2(m_horizontal * m_speed, m_RB.velocity.y);
    }
}
