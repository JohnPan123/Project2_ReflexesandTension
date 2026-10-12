using UnityEngine;

public class PlayerBoostBarScript : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void ResetBoost()
    {
        animator.SetBool("PlayerPushedToBoost", false); 
        animator.SetBool("ReadyToBoost", false); 
    }
    private void SetReady()
    {
        animator.SetBool("ReadyToBoost", true);
    }
    private void IsBoosting()
    {
        animator.SetBool("IsBoosting", true);
    }
    private void IsNotBoosting()
    {
        animator.SetBool("IsBoosting", false);
    }
}
