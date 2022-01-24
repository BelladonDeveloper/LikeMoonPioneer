using UnityEngine;

public class Resource : MonoBehaviour
{
    public ResourceType type;

    private bool needMove = false;

    private Vector3 interpolatePosition;
    private Vector3 endPosition;

    private float speed = 3f;
    private float step = 0.0f;

    private void FixedUpdate()
    {
        if (needMove)
        {
            // incrementing step
            step += Time.fixedDeltaTime * speed;
            // calculate new position with interpolate
            interpolatePosition = Vector3.Lerp(transform.localPosition, endPosition, step);
            // moving
            transform.localPosition = interpolatePosition;

            if (transform.localPosition == endPosition)
            {
                // end smoothing
                transform.localPosition = endPosition;
                // finish moving
                needMove = false;
            }
        }
    }

    /// <summary>
    /// Start moving this object
    /// </summary>
    /// <param name="endPosition"> Position for finish moving </param>
    public void Move(Vector3 endPosition)
    {
        // Set target local position
        this.endPosition = endPosition;
        // Start moving
        needMove = true;
    }
}
