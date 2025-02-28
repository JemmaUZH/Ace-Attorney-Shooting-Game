using UnityEngine;

public class EdgeworthController : MonoBehaviour
{
    // 异议子弹预制体（请在 Inspector 中赋值）
    public GameObject objectionProjectilePrefab;
    // 发射点（建议 Edgeworth 下创建一个子对象作为发射点）
    public Transform firePoint;
    // 两次射击之间的间隔
    public float shootCooldown = 1.0f;
    private float shootTimer = 0f;

    void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootCooldown)
        {
            ShootObjection();
            shootTimer = 0f;
        }
    }

    void ShootObjection()
    {
        if (objectionProjectilePrefab != null && firePoint != null)
        {
            // 实例化异议子弹，沿着发射点的朝向生成
            Instantiate(objectionProjectilePrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            Debug.LogWarning("Edgeworth 的 ObjectionProjectile 或 FirePoint 未设置！");
        }
    }
}
