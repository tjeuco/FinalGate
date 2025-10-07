using UnityEngine;

public class ItemBonus : MonoBehaviour
{
    public ItemType itemType;
    private Rigidbody2D rg;

    [SerializeField] private float timeDelay = 3f;
    [SerializeField] private float radiusExplosion = 4f;
    [SerializeField] private float damageMine = 20f;
    [SerializeField] LayerMask layerDamage;
    [SerializeField] GameObject explosionPrefab;

    private float timer;
    private PlayerControl playerControl;
    private void OnEnable()
    {
        this.rg = GetComponent<Rigidbody2D>();
        rg.gravityScale = 0f;
        this.playerControl = this.GetComponentInParent<PlayerControl>();
    }
    private void Update()
    {
        this.timer += Time.deltaTime;
        this.ActiveMine(this.playerControl.ActiveMine);
    }
    public void ActiveMine(bool inActive)
    {
        if (inActive == false) 
            return;
        if (this.timer >= this.timeDelay)
        {
            Debug.Log("Thoi gian no min:" +  this.timer);
            if (this.explosionPrefab == null)
                return;
            GameObject explo = Instantiate(explosionPrefab,this.transform.position,Quaternion.identity);
            Destroy(explo);
            Destroy(this.gameObject);

            Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, radiusExplosion, layerDamage);
            foreach (Collider2D hit in hits)
            {
                hit.GetComponent<HealthManager>().TakeDamage(damageMine);
            }
        }
    }
}



public enum ItemType
{
    Mine,
    Armo,
    Heath,
    Power
}