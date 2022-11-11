using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerDamage : MonoBehaviour
{
    // could probs use switch statement in future for health
    public Sprite m_flowerPink;
    public Sprite m_flowerOrange;
    public Sprite m_flowerRed;
    private float m_health = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet") && m_health == 2f)
        {
            Debug.Log("Collision hit");
            this.gameObject.GetComponent<SpriteRenderer>().sprite = m_flowerOrange;
            m_health--;
        }
        else if (collision.gameObject.CompareTag("EnemyBullet") && m_health == 1f)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = m_flowerRed;
            m_health--;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
