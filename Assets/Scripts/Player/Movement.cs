using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;

    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical +
                            Vector3.right * variableJoystick.Horizontal;
        transform.Translate(transform.TransformVector(direction * speed * Time.fixedDeltaTime));
    }
}
