using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                // Tìm trong scene trước
                instance = FindFirstObjectByType<T>();

                // Nếu chưa có -> tạo mới
                if (instance == null)
                {
                    GameObject g = new GameObject(typeof(T).Name);
                    instance = g.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else if (instance != this)
        {
            Destroy(gameObject); // đảm bảo chỉ có 1 instance
        }
    }
}
