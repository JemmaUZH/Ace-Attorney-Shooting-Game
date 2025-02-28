using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 8f; 
    public int damage = 1; 

    void Update()
    {
        // 子弹每帧向右移动
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 如果碰撞到 Gumshoe
        if (other.CompareTag("Gumshoe"))
        {
            GumshoeController gumshoe = other.GetComponent<GumshoeController>();
            if (gumshoe != null)
            {
                gumshoe.TakeDamage(damage);
            }
            Destroy(gameObject); // 销毁子弹
        }
        // 如果碰撞对象是 Edgeworth，则直接销毁子弹
        else if (other.CompareTag("Edgeworth"))
        {
            Destroy(gameObject);
        }
    }
}
