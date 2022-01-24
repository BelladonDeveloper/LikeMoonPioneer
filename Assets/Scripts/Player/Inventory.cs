using UnityEngine;

public class Inventory : Storage
{
    /// <summary>
    /// Time for transfered object to other storage
    /// </summary>
    public float transferTime = 0.1f;
    private float nextTransfer = 0.0f;

    private bool stayInConsumedStorage = false;
    private bool stayInProducedStorage = false;

    private ConsumedResources consumed;
    private ProducedResources produced;

    private void FixedUpdate()
    {
        if (stayInConsumedStorage)
        {
            if (Time.time > nextTransfer)
            {
                nextTransfer = Time.time + transferTime;

                // if bag have any resource
                if (resources.Count > 0)
                {
                    ThrowResource();
                }
            }
        }
        if (stayInProducedStorage)
        {
            if (Time.time > nextTransfer)
            {
                nextTransfer = Time.time + transferTime;

                // if can take resource into a bag
                if (resources.Count < capacity)
                {
                    TakeResource();
                }
            }
        }
    }

    /// <summary>
    /// Try take any resource
    /// </summary>
    private void TakeResource()
    {
        if (produced.GetLastResource(out GameObject newResource))
        {
            // put resource into a bag
            PutResource(newResource);
        }
    }

    /// <summary>
    /// Try throw resource with selected type
    /// </summary>
    private void ThrowResource()
    {
        // Have storage empty place?
        if (consumed.resources.Count < consumed.capacity)
        {
            // Check all supported types
            for (int i = 0; i < consumed.canResourcesType.Length; i++)
            {
                // if it is a valid type
                if (consumed.canResourcesType[i] == GetLastResourceType())
                {
                    // try take last resource
                    if (GetLastResource(out GameObject lastResource))
                    {
                        // put it into the storage
                        consumed.PutResource(lastResource);

                        // stop cheking types
                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Getting resource type which last lying down in the bag
    /// </summary>
    /// <returns> Type of resource </returns>
    private ResourceType GetLastResourceType()
    {
        return resources[resources.Count - 1].GetComponent<Resource>().type;
    }

    /// <summary>
    /// Define the tag of the object that triggers the object
    /// </summary>
    /// <param name="other"></param>
    public void TriggerEnter(Collider other)
    {
        if (other.CompareTag("Consumed"))
        {
            stayInConsumedStorage = true;
            consumed = other.gameObject.GetComponent<ConsumedResources>();
        }
        if (other.CompareTag("Produced"))
        {
            stayInProducedStorage = true;
            produced = other.gameObject.GetComponent<ProducedResources>();
        }
    }

    /// <summary>
    /// Define the tag of the object from the trigger of which the object comes out
    /// </summary>
    /// <param name="other"></param>
    public void TriggerExit(Collider other)
    {
        if (other.CompareTag("Consumed"))
        {
            stayInConsumedStorage = false;
        }
        if (other.CompareTag("Produced"))
        {
            stayInProducedStorage = false;
        }
    }
}
