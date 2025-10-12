using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform gun;
    [SerializeField] private Sprite[] spritesByAngle; // Gán các sprite theo góc
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint; // Điểm bắn đạn
    [SerializeField] private float shootDelay = 1f;
    [SerializeField] private GameObject turret;
    [SerializeField] private float speedBullet = 10f;

    private Transform player;
    private float shootTimer = 0f;

    ///// Luu huong nong sung
    private Vector2[] directionsByAngle =
    {
        Vector2.right,                          // 0 - trái
        (Vector2.up + Vector2.right).normalized, // 1 - trên trái
        Vector2.up,                            // 2 - trên
        (Vector2.up + Vector2.left).normalized, // 3 - trên phải
        Vector2.left,                         // 4 - phải
        (Vector2.down + Vector2.left).normalized, // 5 - dưới phải
        Vector2.down,                          // 6 - dưới
        (Vector2.down + Vector2.right).normalized  // 7 - dưới trái
    };
    private int currentSpriteIndex = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        Vector3 direction = player.position - gun.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        UpdateGunSprite(angle);

        // Bắn đạn mỗi X giây
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootDelay)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    void Shoot()
    {
        Debug.Log("Shoot...");
        Vector3 dir = directionsByAngle[currentSpriteIndex];
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = dir * speedBullet; // tốc độ đạn
    }

    void Shoot2()
    {
        Vector3 dir = directionsByAngle[currentSpriteIndex];
        float baseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        float[] angleOffsets = { -20f,-10f, 0f, 10f, 20f }; // lệch trái, giữa, phải

        foreach (float offset in angleOffsets)
        {
            float newAngle = baseAngle + offset;
            Vector2 shootDir = new Vector2(Mathf.Cos(newAngle * Mathf.Deg2Rad), Mathf.Sin(newAngle * Mathf.Deg2Rad));

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().linearVelocity = shootDir.normalized * speedBullet;
        }
    }

    void UpdateGunSprite(float angle)
    {
        angle = (angle + 360) % 360; // đổi về 0 - 360
        int index = Mathf.RoundToInt(angle / (360f / spritesByAngle.Length)) % spritesByAngle.Length;
        currentSpriteIndex = index;
        turret.GetComponent<SpriteRenderer>().sprite = spritesByAngle[index];
        //Debug.Log("Goc quay:" + angle +" Index: " + index); 
    }
}