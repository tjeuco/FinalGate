using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speedMoving = 3f;
    [SerializeField] private Transform posA, posB;

    private Vector3 lastPos;
    private Vector3 target;
    private bool playerOnPlatform = false;
    private Transform player;
    private bool playerOnTop = false;

    void Start()
    {
        this.target = posB.position;
        this.lastPos = transform.position;
    }

    void Update()
    {
        // Di chuyển platform
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.target, this.speedMoving * Time.deltaTime);

        // Nếu player đang đứng trên platform, di chuyển theo delta position
        if (this.playerOnTop && this.player != null)
        {
            Vector3 delta = this.transform.position - this.lastPos;
            this.player.position += delta;
        }

        // Đổi hướng khi tới biên
        if (Vector3.Distance(this.transform.position, this.posA.position) < 0.1f)
            this.target = this.posB.position;
        else if (Vector3.Distance(transform.position, this.posB.position) < 0.1f)
            this.target = this.posA.position;

        this.lastPos = this.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        // Kiểm tra Player có đang ở TRÊN platform không
        float playerBottom = collision.bounds.min.y;
        float platformTop = GetComponent<Collider2D>().bounds.max.y;

        // Nếu Player chạm từ trên xuống (đứng lên platform)
        if (playerBottom > platformTop - 0.05f)
        {
            this.playerOnTop = true;
            this.player = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.playerOnPlatform = false;
            this.player = null;
        }
    }
}
