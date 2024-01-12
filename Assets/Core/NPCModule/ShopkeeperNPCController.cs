using UnityEngine;
using UnityEngine.Events;

public class ShopkeeperNPCController : MonoBehaviour
{
    public TriggerEvent triggerArea;
    private bool insideTrigger = false;
    public UnityEvent openShop;

    public void Start()
    {
        PlayerInputListener.Singleton.onActionButtonPressed += ExecuteAction;
        triggerArea.onTriggerEnter += CheckIfInsideTriggerArea;
        triggerArea.onTriggerExit += CheckIfExitTriggerArea;
    }

    public void CheckIfInsideTriggerArea(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
            insideTrigger = true;
    }

    public void CheckIfExitTriggerArea(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
            insideTrigger = false;
    }

    public void ExecuteAction()
    {
        if (!insideTrigger)
            return;
        openShop?.Invoke();
    }
}
