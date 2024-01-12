using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    public Animator animator;

    public void Start()
    {
        PlayerInputListener.Singleton.onMove += SetMovementAnimation;
    }

    public void SetMovementAnimation(Vector2 newDirection)
    {
        animator.SetFloat("Horizontal", newDirection.x);
        animator.SetFloat("Vertical", newDirection.y);
        animator.SetFloat("Speed", newDirection.magnitude);
    }
}
