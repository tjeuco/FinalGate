using UnityEngine;


[CreateAssetMenu(fileName ="PlayerDataSO",menuName ="Data/PlayerConfig")]
public class PlayerDataSO : ScriptableObject
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public int numberJump = 2;
    public float speedBullet = 20f;
    [Header("-----HP------")]
    public float maxHp = 100f;

}
