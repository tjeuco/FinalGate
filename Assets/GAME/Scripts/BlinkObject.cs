using UnityEngine;

public class BlinkObject : MonoBehaviour
{
    [SerializeField] private float blinkInterval = 0.1f; // thời gian giữa 2 lần nhấp nháy
    [SerializeField] private float blinkDuration = 3f; // tổng thời gian nhấp nháy
    private Renderer render;
    private float timerInterval = 0f;
    private float timerDuration = 0f;
    private bool isBlinking = false;

    void OnEnable()
    {
        this.render = GetComponent<Renderer>();
    }

    void Update()
    {
        if (!this.isBlinking) return;

        this.timerInterval += Time.deltaTime;
        this.timerDuration += Time.deltaTime;
        if (this.timerDuration <= this.blinkDuration)
        {
            this.render.enabled = !render.enabled; // bật/tắt hiển thị
            this.timerInterval = 0f;
        }
        
        if (this.timerDuration > this.blinkInterval)
        {
            StopBlinking();
        }

    }

    public void Blinking(float timeBlink, bool isActive)
    {
        this.blinkDuration = timeBlink;
        this.timerDuration = 0f;
        this.timerInterval = 0f;
        this.isBlinking = isActive;

        if (isActive)
            this.render.enabled = false; // tắt 1 lần để tạo hiệu ứng ngay
    }

    public void StopBlinking()
    {
        this.isBlinking = false;
        this.render.enabled = true; // dam bao hien thi len khi het thoi giang blink
    }
}