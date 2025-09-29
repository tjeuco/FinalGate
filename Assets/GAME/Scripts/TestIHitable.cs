using UnityEngine;

public class TestIHitable : MonoBehaviour, IHitable
{
    public void GetHit(float dmg)
    {
        Debug.Log("Test IHitable OK");
        Destroy(gameObject);
    }

    
}
