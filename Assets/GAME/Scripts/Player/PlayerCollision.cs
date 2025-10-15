using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    [SerializeField] Transform posCarryObj;
    [SerializeField] float stackOffset = 0.5f;
    [SerializeField] float offsetPos = 0.5f;

    [SerializeField] List<GameObject> carryObjects = new List<GameObject>();
    public IReadOnlyList<GameObject> CarryObjects => carryObjects;

    private void Update()
    {
       this.SortMine();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        ItemBonus item = collision.GetComponent<ItemBonus>();
        if (item == null)
            return;

        switch (item.itemType)
        {
            case ItemType.Mine:
                Debug.Log("Player nhat duoc Mine");
                this.AddMine(item.gameObject);
                break;
            case ItemType.Armo:
                Debug.Log("Player nhat duoc Armo");
                Destroy(collision.gameObject);
                break;
            case ItemType.Heath:
                Debug.Log("Player nhat duoc Heath");
                this.PickedHeath(50f);
                Destroy(collision.gameObject);
                break;
            case ItemType.Power:
                Debug.Log("Player nhat duoc Power");
                this.PickedPower(5f);
                Destroy(collision.gameObject);
                break;
        }
        //Destroy (collision.gameObject);
    }

    public void AddMine(GameObject obj)
    {
        Rigidbody2D rg = obj.GetComponent<Rigidbody2D>();
        if (rg != null)
        {
            rg.simulated = false;
        }
        Collider2D collider2D = obj.GetComponent<Collider2D>();
        if (collider2D != null)
        {
            collider2D.enabled = false;
        }

        carryObjects.Add(obj);
        this.SortMine();
    }

    public void SortMine()
    {
        if (carryObjects.Count <= 0)
            return;

        for (int i = 0; i < carryObjects.Count; i++)
        {
            GameObject obj = carryObjects[i];
            if (obj == null) continue;

            Vector3 offset = new Vector3(0, i * stackOffset, 0);
            obj.transform.position = posCarryObj.position + offset;
        }
    }


    public void DropMine(GameObject obj)
    {
        if (carryObjects.Count <=0) 
            return;
        carryObjects.Remove(obj);
        obj.transform.position = this.transform.position + new Vector3(offsetPos, 0,0);
        this.SortMine();
    }

    public void PickedHeath(float healAmount)
    {
        var playerHealth = this.GetComponent<HealthManager>();
        if (playerHealth != null)
        {
            playerHealth.HeathPlus(healAmount);
        }
    }

    public void PickedPower(float timePowerUp)
    {
        Debug.Log("Picked Power");
    }
}
