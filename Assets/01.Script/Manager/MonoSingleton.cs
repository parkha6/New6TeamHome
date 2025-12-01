using UnityEngine;
/// <summary>
/// 싱글턴 패턴 배치용 클래스
/// </summary>
public class MonoSingleton<T> : MonoBehaviour where T : Component
{
    /// <summary>
    /// 싱글턴 인스턴스
    /// </summary>
    private static T instance;
    /// <summary>
    /// 싱글턴 인스턴스 프로퍼티
    /// </summary>
    public static T Instance
    {
        get
        {
            if (instance == null)
            { SetupInstance(); }
            return instance;
        }
    }
    /// <summary>
    /// 인스턴스 세팅
    /// </summary>
    private static void SetupInstance()
    {
        instance = FindAnyObjectByType<T>();
        if (instance == null)
        {
            instance = new GameObject(typeof(T).Name).AddComponent<T>();
            DontDestroyOnLoad(instance.gameObject);
        }
    }
    /// <summary>
    /// 호출시 인스턴스 만들기
    /// </summary>
    private void Awake()
    {
        if (instance != null && instance != this)
        { Destroy(Instance.gameObject); }
        else
        { DontDestroyOnLoad(this.gameObject); }
    }
}
