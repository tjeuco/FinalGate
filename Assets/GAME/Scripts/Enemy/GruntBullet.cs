using UnityEngine;

public class GruntBullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletGruntEffectPerfab;
    [SerializeField] private float speedBulletGrunt = 6f;
    [SerializeField] private float damageBullet = 5f;
    [SerializeField] private float maxDistance = 10f;

    private Vector3 movement;
    private GruntBulletPooling poolBulletGrunt;
    private Vector3 PosXStart;
    private PlayerControl player;

    void Start()
    {
        poolBulletGrunt = FindAnyObjectByType<GruntBulletPooling>();
        player = FindAnyObjectByType<PlayerControl>();
    }

    void Update()
    {
        if (Vector3.Distance(this.transform.position, PosXStart) >= maxDistance)
        {
            DisableBullet();
            poolBulletGrunt.ReturnBulletGrunt(this.gameObject);
        }
        MoveBullet();

    }
    public void SetDirectionBullet(Vector3 direction)
    {
        movement = direction;
        PosXStart = this.transform.position;
    }
    public void DisableBullet()
    {
        this.gameObject.SetActive(false);
    }
    public float GetDamageBulletGrunt()
    {
        return damageBullet;
    }
    public void MoveBullet()
    {
        this.transform.Translate(movement * speedBulletGrunt * Time.deltaTime);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<HealthManager>().TakeDamage(damageBullet);
            GameObject effect = Instantiate(bulletGruntEffectPerfab,this.transform.position, Quaternion.identity);
            Destroy(effect,1f);
            DisableBullet();
            poolBulletGrunt.ReturnBulletGrunt(this.gameObject);
        }
    }
}
