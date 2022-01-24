using UnityEngine;

public class Triggers : MonoBehaviour
{
    private Inventory inventory;

    void Start()
    {
        inventory = GetComponentInChildren<Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        inventory.TriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        inventory.TriggerExit(other);
    }
}
