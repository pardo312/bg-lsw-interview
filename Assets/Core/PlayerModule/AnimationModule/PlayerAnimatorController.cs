using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    public Animator animator;
    public Animator outfitAnim;

    public void Start()
    {
        PlayerInputListener.Singleton.onMove += SetMovement;
    }

    public void SetOutfit(RuntimeAnimatorController animatorController)
    {
        outfitAnim.runtimeAnimatorController = animatorController;
    }

    public void SetMovement(Vector2 newDirection)
    {
        SetMovementAnimation(animator, newDirection);
        SetMovementAnimation(outfitAnim, newDirection);
    }
    public void SetMovementAnimation(Animator anim, Vector2 newDirection)
    {
        anim.SetFloat("Horizontal", newDirection.x);
        anim.SetFloat("Vertical", newDirection.y);
        anim.SetFloat("Speed", newDirection.magnitude);
    }
}
