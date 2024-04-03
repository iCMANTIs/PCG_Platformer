using UnityEngine;

public class DisappearOnContact : MonoBehaviour
{
    public float maxContactTime = 3.0f; // 定义玩家停留的最长时间
    private float contactTime = 0f; // 玩家与对象接触的时间
    private bool isPlayerContacting = false; // 玩家是否正在接触对象

    void Update()
    {
        if (isPlayerContacting)
        {
            contactTime += Time.deltaTime; // 更新接触时间
            if (contactTime >= maxContactTime)
            {
                Destroy(gameObject); // 接触时间过长，销毁对象
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") // 假设PlayerCharacter是你的玩家对象
        {
            isPlayerContacting = true; // 玩家开始接触对象
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerContacting = false; // 玩家停止接触对象
            contactTime = 0f; // 重置接触时间
        }
    }
}