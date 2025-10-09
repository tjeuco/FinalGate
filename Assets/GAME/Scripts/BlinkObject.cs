using UnityEngine;

public class BlinkObject : MonoBehaviour
{
    [SerializeField] private float blinkInterval = 0.1f; // thời gian giữa 2 lần nhấp nháy
    [SerializeField] private float blinkDuration = 3f; // tổng thời gian nhấp nháy
    private Renderer render;
    private float timerInterval = 0f;
    private float timerDuration = 0f;

    void OnEnable()
    {
        this.render = GetComponent<Renderer>();
    }

    void Update()
    {
        this.timerInterval += Time.deltaTime;
        this.timerDuration += Time.deltaTime;
        if (this.timerDuration < this.blinkDuration)
        {
            this.Blinking();
        }
        else
        {
            this.GetComponent<BlinkObject>().enabled = false;
        }
        
    }

    public void Blinking()
    {
        Debug.Log("Blinking...");
        if (timerInterval >= blinkInterval)
        {
            render.enabled = !render.enabled; // bật/tắt hiển thị
            timerInterval = 0f;
        }
    }
}
