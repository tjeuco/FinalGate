using UnityEngine;
using static UnityEngine.InputManagerEntry;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    PlayerControl playerControl;
    private void Start()
    {
        animator = GetComponent<Animator>();
        playerControl = GetComponentInParent<PlayerControl>();
    }
    private void Update()
    {
        this.animator.SetTrigger(this.playerControl.PlayerState.ToString());
        //Debug.Log("Player State: " + this.playerControl.PlayerState.ToString());
    }

    public void updateInputY(float inputY)
    {
        /*if(inputY >= 0.3f)
        {
            this.animator.SetFloat("IsIdleShoot", 0);
        }
        else
        if (inputY <= -0.3f)
        {
            this.animator.SetFloat("IsIdleShoot", 0.5f);
        }
        else
        {
            this.animator.SetFloat("IsIdleShoot", 1);
        }*/
        // ánh sạ B = (A+1)/2 
        float animValue = (inputY + 1f) / 2f;
        Debug.Log("Set Float" + animValue);
        animator.SetFloat("IsIdleShoot", animValue);
    }

}
