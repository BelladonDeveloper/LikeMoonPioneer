using System;
using UnityEngine;

public class ProducedResources : Storage
{
    [Header("Production")]
    /// <summary>
    /// Resouces needs to producing new resouces in this building
    /// </summary>
    public ResourceType[] neededResourcesType = new ResourceType[3];
    /// <summary>
    /// Resource which will be product in with this parent building
    /// </summary>
    public GameObject productedResource;
    /// <summary>
    /// Time before created new recource
    /// </summary>
    public float timeForNew = 1f;
    private float nextResource = 0.0f;

    /// <summary>
    /// Why production is stopped
    /// </summary>
    public static event Action<int, string> reportToUI;

    /// <summary>
    /// List of recources in the consumed resources storage
    /// </summary>
    private ConsumedResources consumedResources;

    private void Start()
    {
        consumedResources = transform.parent.gameObject.GetComponentInChildren<ConsumedResources>();
    }

    private void FixedUpdate()
    {
        // if the storage have empty place
        if (resources.Count < capacity)
        {
            if (HaveNeededResources())
            {
                if (Time.time > nextResource)
                {
                    nextResource = Time.time + timeForNew;

                    // start production
                    ProductNewResource();
                }
            }
        }
        else
        {
            // report to UI
            reportToUI?.Invoke(int.Parse(transform.parent.gameObject.name), "storage is full");
        }
    }

    /// <summary>
    /// Have consumed storage needed resources?
    /// </summary>
    /// <returns></returns>
    private bool HaveNeededResources()
    {
        // checking all needed resource types
        int resourceNeedsCounter = 0;
        for (int i = 0; i < neededResourcesType.Length; i++)
        {
            if (consumedResources.HasResource(neededResourcesType[i]))
            {
                resourceNeedsCounter++;
            }
        }
        // if storage has all needed resources
        if (resourceNeedsCounter == neededResourcesType.Length)
        {
            return true;
        }
        else
        {
            reportToUI?.Invoke(int.Parse(transform.parent.gameObject.name), "needs consumed resource");
            return false;
        }
    }

    /// <summary>
    /// Create new recource and put in this storage
    /// </summary>
    private void ProductNewResource()
    {
        // Remove one of each needed resource
        for (int i = 0; i < neededResourcesType.Length; i++)
        {
            consumedResources.RemoveResource(neededResourcesType[i]);
        }

        // Create new resource
        GameObject newResource = Instantiate(productedResource);
        newResource.transform.position = transform.parent.position;
        PutResource(newResource);
    }
}
