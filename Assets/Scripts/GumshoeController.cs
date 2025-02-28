using System.Collections;
using UnityEngine;

public class GumshoeController : MonoBehaviour
{
    // Gumshoe 被击中的次数
    public int hitsTaken = 0;
    // 3 次后会出现 Edgeworth（改为 3 次）
    public int hitsThreshold = 3;
    
    // 显示 “Performance -1” 的预制体
    public GameObject performanceMinusOnePrefab;

    // Edgeworth 的预制体
    public GameObject edgeworthPrefab;

    // 放置 Edgeworth 的位置（在 Gumshoe 左边）
    public Transform edgeworthSpawnPoint;

    // “异议”预制体
    public GameObject objectionPrefab;

    // Gumshoe 是否免疫（Edgeworth 出现后）
    private bool isImmune = false;

    public void TakeDamage(int damage)
    {
        // 如果 Gumshoe 已经免疫，则不再受伤
        if (isImmune) return;

        hitsTaken += damage;

        Debug.Log("hitsTaken = " + hitsTaken);

        ShowPerformanceMinusOne();

        // 检查是否达到阈值（现在设为 3）
        if (hitsTaken >= hitsThreshold)
        {
            SummonEdgeworth();
        }
    }

    private void ShowPerformanceMinusOne()
    {
        if (performanceMinusOnePrefab != null)
        {
            // 在 Gumshoe 上方一点生成 “Performance -1”
            Vector3 spawnPos = transform.position + Vector3.up * 1.5f;
            GameObject perfText = Instantiate(performanceMinusOnePrefab, spawnPos, Quaternion.identity);

            // 1 秒后销毁该提示
            Destroy(perfText, 1f);
        }
    }

    private void SummonEdgeworth()
    {
        // 免疫后续伤害
        isImmune = true;
        StartCoroutine(ShowObjectionThenSummonEdgeworth());
    }

    /// <summary>
    /// 协程：同时生成“异议”预制体和 Edgeworth，
    /// 然后等待 1.5 秒后销毁“异议”，Edgeworth 保持不变
    /// </summary>
    private IEnumerator ShowObjectionThenSummonEdgeworth()
    {
        GameObject objectionInstance = null;

        // 生成异议预制体
        if (objectionPrefab != null)
        {
            // 定义屏幕中上方的位置，此处 y 设为屏幕高度的 50%（可根据需要调整）
            Vector3 screenPos = new Vector3(Screen.width / 2, Screen.height * 0.4f, 10f);
            // 将屏幕坐标转换为世界坐标
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            objectionInstance = Instantiate(objectionPrefab, worldPos, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("ObjectionPrefab 未指定！");
        }

        // 同时生成 Edgeworth
        if (edgeworthPrefab != null && edgeworthSpawnPoint != null)
        {
            Instantiate(edgeworthPrefab, edgeworthSpawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("EdgeworthPrefab 或 SpawnPoint 未指定！");
        }

        // 等待 1.5 秒后销毁异议（Edgeworth 保持不变）
        yield return new WaitForSeconds(1.5f);

        if (objectionInstance != null)
        {
            Destroy(objectionInstance);
        }
    }
}
