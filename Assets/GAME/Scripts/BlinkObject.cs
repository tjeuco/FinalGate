using UnityEngine;

public class BlinkObject : MonoBehaviour
{
    [SerializeField] private float blinkInterval = 0.1f; // thời gian giữa 2 lần nhấp nháy
    private Renderer render;
    private float timer;

    void Start()
    {
        render = GetComponent<Renderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= blinkInterval)
        {
            render.enabled = !render.enabled; // bật/tắt hiển thị
            timer = 0f;
        }
    }
}
