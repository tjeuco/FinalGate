using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("-----HP------")]

    private Slider sliderHpBar;
    private GameObject sliderHpBarPrefab;
    private GameObject sliderHpBarPrefabLoaded;

    [SerializeField] private float maxHp;
    [SerializeField] private float currentHp;

    void OnEnable() //viết trong OnEnable để tránh lỗi khi object bị disable sd object pooling
    {
        if(sliderHpBarPrefabLoaded == null)
        {
            sliderHpBarPrefabLoaded = Resources.Load<GameObject>("UI/Slider-Hp");
        }
        if (sliderHpBarPrefabLoaded == null)
        {
            Debug.LogError("Loi khong load duoc sliderHP");
            return;
        }
        Canvas canvasHp = UIManager.Instance.canvasHp;
        if (canvasHp == null)
        {
            Debug.LogError("Canvas HP chua gan trong UIManager.");
            return;
        }

        sliderHpBarPrefab = LazyPooling.Instance.GetObject(sliderHpBarPrefabLoaded);
        sliderHpBarPrefab.transform.SetParent(canvasHp.transform,false);
        sliderHpBarPrefab.transform.gameObject.SetActive(true);

        sliderHpBar = sliderHpBarPrefab.GetComponent<Slider>();
        
        currentHp = maxHp;
        UpdateHpBar();
    }

    void OnDisable()
    {
        if (sliderHpBarPrefab != null)
        {
            sliderHpBarPrefab.SetActive(false);
        }
    }

    public void TakeDamage(float valueDamge)
    {
        currentHp -= valueDamge;
        currentHp = Mathf.Max(currentHp, 0);
        UpdateHpBar();
        if (currentHp <= 0)
        {
            Die();
        }
    }

    public void SetCurrentHpFull()
    {
        currentHp = maxHp;
    }

    private void Update()
    {
        UpdateHpBar();
        CheckPos();
    }

    public void Die()
    {
        //Destroy(this.gameObject);
        // check xem đối tượng đang mang là gì
        if (this.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.ShowCanvas(UIScreen.GameOver);
            Time.timeScale = 0; // Dừng game khi player chết
        }
        else if (this.gameObject.CompareTag("Enemy"))
        {
            EnemyGruntSpawner numberEnemy = FindAnyObjectByType<EnemyGruntSpawner>();
            numberEnemy.currentEnemy--; // giảm số lượng enemy hiện có trên scens

            ObserverManager.Notify(ObserverKey.addScore, this.GetComponent<EnemyGrunt>().ReturnScoreEnemy());
            GameManager.Instance.AddEnemyDied();
            this.gameObject.SetActive(false);
        }
        sliderHpBar.gameObject.SetActive(false);
    }

    public void UpdateHpBar()
    {
        if (sliderHpBar == null) return;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 1f, 0));
        sliderHpBar.transform.position = screenPos;
        sliderHpBar.value = (currentHp / maxHp) * 100;
    }

    public void HeathPlus(float heathPlus)
    {
        currentHp += heathPlus;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
        UpdateHpBar();
    }

    void CheckPos()
    {
        if(this.transform.position.y < -30f)
            this.Die();
    }
}
