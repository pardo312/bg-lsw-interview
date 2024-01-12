using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    #region ----Fields----
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 direction;
    #endregion ----Fields----


    #region ----Methods----

    public void Start()
    {
        PlayerInputListener.Singleton.onMove += SetDirection;
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
        direction = new Vector2(newDirection.x, newDirection.y).normalized;
    }

    public void FixedUpdate()
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
    #endregion ----Methods----
}