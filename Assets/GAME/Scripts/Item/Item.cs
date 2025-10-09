using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour, IHitable
{

    [SerializeField] protected float maxHpItem =30f;
    [SerializeField] private int scoreItem = 5;
    [SerializeField] private GameObject effectPerfab;
    protected PlayerControl player;
    private float currentHpItem;

    [SerializeField] List<Transform> bonusItemsPrefabs = new List<Transform>();

    ////// Item no gay sat thuong cho vat xung quanh
    [SerializeField] protected float damageItem = 10f; //// damage sat thuong
    [SerializeField] protected float radiusDamage = 3f; // ban kinh gay sat thuong
    [SerializeField] private LayerMask layerDamage;

    protected virtual void Start()
    {
        currentHpItem  = maxHpItem;
        player = FindAnyObjectByType<PlayerControl>();
    }

    protected virtual void TakeDamage(float damage)
    {
        currentHpItem -= damage;
        currentHpItem = Mathf.Max(currentHpItem, 0);
        if (currentHpItem <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        GameManager.Instance.AddScore(scoreItem);

        if (effectPerfab != null)
        {
            GameObject effect = Instantiate(effectPerfab, this.transform.position, Quaternion.identity);// sinh effect khi chet
            Destroy(effect, 1f);
            AudioManager.Instance.PlayExplosionMusic();
        }

        var itemInstantiate = this.ReturnBonusItem();
        if (itemInstantiate != null)
        {
            GameObject itemBonus = Instantiate(this.ReturnBonusItem(), this.transform.position, Quaternion.identity);// sinh item co thuong
        }
        Destroy(gameObject);
        ExplosionItem();
       
    }
   

    protected virtual GameObject ReturnBonusItem()
    {
        if (bonusItemsPrefabs.Count == 0)
            return null;
        int indexItem = Random.Range(0, bonusItemsPrefabs.Count);
        return this.bonusItemsPrefabs[indexItem].gameObject;
    }
    protected virtual void ExplosionItem()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, radiusDamage, layerDamage);

        foreach (Collider2D hit in hits)
        {

            if (hit.CompareTag("Player"))
            {
                PlayerControl targetPlayer = hit.GetComponent<PlayerControl>();
                if (targetPlayer != null)
                {
                    targetPlayer.GetComponent<HealthManager>().TakeDamage(damageItem);
                }
            }
            else if (hit.CompareTag("Enemy"))
            {
                EnemyGrunt enemy = hit.GetComponent<EnemyGrunt>();
                if (enemy != null)
                {
                    enemy.GetComponent<HealthManager>().TakeDamage(damageItem);
                }
            }
        }
    }
    /////// test
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusDamage);
    }

    public void GetHit(float dmg)
    {
        this.TakeDamage(dmg);
    }
}
