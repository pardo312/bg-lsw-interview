using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Singleton;
    public void Awake()
    {
        if (Singleton != null)
        {
            Destroy(this);
            return;
        }

        Singleton = this;
        DontDestroyOnLoad(this);
    }
    public PlayerAnimatorController playerAnimatorController;
}
