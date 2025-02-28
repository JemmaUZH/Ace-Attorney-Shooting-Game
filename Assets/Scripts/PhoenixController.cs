using UnityEngine;

public class PhoenixController : MonoBehaviour
{
    // 这个是需要用来生成的子弹预制体
    public GameObject badgeProjectilePrefab;

    // 子弹出生点（比如 Phoenix 的手部或枪口）
    public Transform firePoint;

    // 两次射击之间的冷却时间
    public float shootCooldown = 0.5f;
    private float shootTimer;

    // 成步堂的初始血量
    public int phoenixHealth = 100;

    void Update()
    {
        shootTimer += Time.deltaTime;

        // 按空格键射击
        if (Input.GetKeyDown(KeyCode.Space) && shootTimer >= shootCooldown)
        {
            ShootBadge();
            shootTimer = 0f;
        }
    }

    void ShootBadge()
    {
        // 在 firePoint 的位置、旋转生成一个徽章子弹
        Instantiate(badgeProjectilePrefab, firePoint.position, firePoint.rotation);
    }

    public void TakeDamage(int damage)
    {
        phoenixHealth -= damage;
        Debug.Log("Phoenix 受伤！当前血量: " + phoenixHealth);

        if (phoenixHealth <= 0)
        {
            Debug.Log("Phoenix 被击倒了！");
            // 可在这里触发游戏结束或其他逻辑
        }
} 
    
}


