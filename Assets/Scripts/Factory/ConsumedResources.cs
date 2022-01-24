using UnityEngine;

public class ConsumedResources : Storage
{
    /// <summary>
    /// Array of struct with amount objects of each resource type
    /// </summary>
    public ResourcesType[] resourcesType;

    /// <summary>
    /// Resouces can place on the storage
    /// </summary>
    public ResourceType[] canResourcesType = new ResourceType[3];

    private void Start()
    {
        // Array init
        resourcesType = new ResourcesType[3];
        resourcesType[0].type = ResourceType.Red;
        resourcesType[1].type = ResourceType.Green;
        resourcesType[2].type = ResourceType.Blue;

        // set types like in produced storage
        canResourcesType = transform.parent.gameObject.GetComponentInChildren<ProducedResources>().neededResourcesType;
    }

    /// <summary>
    /// Ovverride base method with save amount of putted resources
    /// </summary>
    /// <param name="resource">Resource for this storage</param>
    public override void PutResource(GameObject resource)
    {
        if (resource.TryGetComponent<Resource>(out Resource resourceType))
        {
            ResourceType type = resourceType.type;

            // comparsion type of resource
            for (int i = 0; i < canResourcesType.Length; i++)
            {
                // if the resource type is valid
                if (canResourcesType[i] == type)
                {
                    // call base method
                    base.PutResource(resource);

                    // add type for list
                    resourcesType[(int)type].count++;

                    // stop comparsion types
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Is any resource in this storage now?
    /// </summary>
    /// <param name="type">resource type </param>
    /// <returns></returns>
    public bool HasResource(ResourceType type)
    {
        return resourcesType[(int)type].count > 0;
    }

    /// <summary>
    /// Removing last resource with the type
    /// </summary>
    /// <param name="type"> Type resource for removing from list </param>
    public void RemoveResource(ResourceType type)
    {
        // remove type form structure
        resourcesType[(int)type].count--;

        // remove from list
        GameObject lastResource = resources.FindLast(lastResource => lastResource.GetComponent<Resource>().type == type);
        resources.Remove(lastResource);

        // destroy it
        Destroy(lastResource);
    }
}
