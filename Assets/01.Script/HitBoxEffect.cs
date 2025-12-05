using UnityEngine;

public class HitBoxEffect : MonoBehaviour
{
    public void Init(Vector2 size, float duration)
    {
        // Box 크기 적용
        transform.localScale = new Vector3(size.x, size.y, 1);

        // 일정 시간이 지나면 자동 삭제
        Destroy(gameObject, duration);
    }
}