using UnityEngine;


[CreateAssetMenu(fileName ="PlayerDataSO",menuName ="Data/PlayerConfig")]
public class PlayerDataSO : ScriptableObject
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public int numberJump = 2;
    [Header("-----HP------")]
    public float maxHp = 120f;

}
