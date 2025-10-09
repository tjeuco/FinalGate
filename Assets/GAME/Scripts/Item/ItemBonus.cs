using UnityEngine;

public class ItemBonus : MonoBehaviour
{
    public ItemType itemType;
    private Rigidbody2D rg;

    [SerializeField] private float timeActiveMine = 5f;
    [SerializeField] private float radiusExplosion = 4f;
    [SerializeField] private float damageMine = 20f;
    [SerializeField] LayerMask layerDamage;
    [SerializeField] GameObject explosionPrefab;

    private float timer;
    private bool activeMine = false; //// bien kich no min
    private void OnEnable()
    {
        this.rg = GetComponent<Rigidbody2D>();
        rg.gravityScale = 0f;
    }
    private void Update()
    {
        this.ActiveMine(activeMine);
    }

    public void SetActiveMine()
    {
        this.activeMine = true;
        this.timer = 0f;    
    }
    public void ActiveMine(bool inActive)
    {
        if (inActive == false) 
            return;
        this.timer += Time.deltaTime;
        if (this.timer >= this.timeActiveMine)
        {

            Debug.Log("Thoi gian no min:" +  this.timer);
            if (this.explosionPrefab == null)
                return;
            GameObject explo = Instantiate(explosionPrefab,this.transform.position,Quaternion.identity);
            AudioManager.Instance.PlayExplosionMusic();
            Destroy(explo,2f);
            Destroy(this.gameObject);

            this.activeMine = false;

            Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, radiusExplosion, layerDamage);
            foreach (Collider2D hit in hits)
            {
                var getHit = hit.GetComponent<IHitable>();
                if (getHit != null)
                {
                    getHit.GetHit(damageMine);
                }
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