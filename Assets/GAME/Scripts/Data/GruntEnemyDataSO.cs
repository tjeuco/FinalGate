using UnityEngine;

[CreateAssetMenu(fileName = "GruntEnemyDataSO", menuName = "Data/GruntEnemyConfig")]
public class GruntEnemyDataSO : ScriptableObject
{
    public GameObject bulletGruntPrefab;
    public float fireRate = 1f;
    public float shootRange = 10f;
    public float fireCoolDown;
    public int scoreEnemyGrunt = 1;
    public float speedGrunt = 0.5f;
    public float distance = 3f;
}
