using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [Header("Storage")]
    /// <summary>
    /// Only left(-1) or right(1)
    /// Direction to puts new resourced in this storage
    /// </summary>
    public int storageDirection = 1;
    /// <summary>
    /// Capacity this storage
    /// </summary>
    public int capacity;
    /// <summary>
    /// Lenght each row resources in this storage
    /// </summary>
    public int rowLenght = 5;
    /// <summary>
    /// List of recources in this storage
    /// </summary>
    public List<GameObject> resources;

    /// <summary>
    /// Empty object for answer about storage have no resource now
    /// </summary>
    private GameObject noResource;

    void Start()
    {
        resources = new List<GameObject>(capacity);

        noResource = Instantiate(new GameObject(), transform);
        noResource.name = "Empty object in " + gameObject.name;
    }

    /// <summary>
    /// Putting resource in this storage
    /// </summary>
    /// <param name="resource">Resource for storage</param>
    public virtual void PutResource(GameObject resource)
    {
        if (resources.Count < capacity)
        {
            // calculate place position in the storage
            int heightInColumn = Mathf.RoundToInt(resources.Count / rowLenght);
            int numberInRow = resources.Count % rowLenght;

            // set this is new parent for resource
            resource.transform.parent = transform;

            // create new position for the resource
            Vector3 endPosition = new Vector3(storageDirection * numberInRow, heightInColumn, 0);
            resource.GetComponent<Resource>().Move(endPosition);

            // add resource into a list
            resources.Add(resource);
            print("Resource was putted");
        }
    }

    /// <summary>
    /// Get last resource form list of resources
    /// </summary>
    /// <returns> Last resource if it exists </returns>
    public bool GetLastResource(out GameObject lastResource)
    {
        // if list have any objects
        int resourcesCount = resources.Count;
        if (resourcesCount > 0)
        {
            // return resource
            lastResource = resources[resourcesCount - 1];
            // remove it form list
            resources.RemoveAt(resourcesCount - 1);

            // we have a resource
            return true;
        }
        else
        {
            print("Storage have no resources now!");
            //return empty object
            lastResource = noResource;

            // we have no resources
            return false;
        }
    }
}
