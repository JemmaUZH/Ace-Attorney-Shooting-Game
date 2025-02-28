using UnityEngine;

public class ObjectionProjectileMovement : MonoBehaviour
{
    // 定义子弹移动的速度（单位：单位/秒）
    public float speed = 5f;

    void Update()
    {
        // 使子弹沿着物体局部的右方向移动
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
