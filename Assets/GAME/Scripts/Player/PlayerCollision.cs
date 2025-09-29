using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private PlayerControl player;
    void Start()
    {
        player = FindAnyObjectByType<PlayerControl>();
    }
}
