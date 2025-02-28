using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    // —— 基础参数 ——
    // 向上移动的速度
    public float floatSpeed = 1f;
    // 对象存活时间，超过这个时间后会被销毁
    public float lifeTime = 1f;

    // —— 可选：淡出效果 ——
    // 是否开启淡出动画
    public bool enableFadeOut = true;
    // 在存活时间内，越接近销毁越透明
    private SpriteRenderer spriteRenderer; 
    private float timer = 0f;    // 记录已过去的时间

    void Start()
    {
        // 如果你的预制体上有 SpriteRenderer（比如图片“Performance -1”），可以在 Start 中获取
        spriteRenderer = GetComponent<SpriteRenderer>();
        // 如果脚本和 Sprite 不在同一个对象，可以在子对象上 GetComponentInChildren<SpriteRenderer>() 等
    }

    void Update()
    {
        // （1）向上移动
        transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);

        // （2）计时
        timer += Time.deltaTime;

        // （3）可选：淡出逻辑
        if (enableFadeOut && spriteRenderer != null)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / lifeTime);
            // Lerp 会根据当前时间占比从1过渡到0
            Color c = spriteRenderer.color;
            c.a = alpha; 
            spriteRenderer.color = c;
        }

        // （4）到达寿命上限则销毁
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
