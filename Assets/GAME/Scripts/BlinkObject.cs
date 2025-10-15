using UnityEngine;

public class BlinkObject : MonoBehaviour
{
    [SerializeField] private float blinkInterval = 0.1f; // thời gian giữa 2 lần nhấp nháy
    private Renderer render;
    private float timerInterval = 0f;

    void OnEnable()
    {
        this.render = GetComponent<Renderer>();
    }

    void Update()
    {
        this.timerInterval += Time.deltaTime;

        if (!this.gameObject.activeSelf)
            return;
        if (this.gameObject.activeSelf)
        {
            if (this.timerInterval > blinkInterval)
            {
                render.enabled = !render.enabled; // bật/tắt hiển thị
                timerInterval = 0f;
            }
        }
    }
  }
