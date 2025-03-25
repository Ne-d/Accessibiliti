using UnityEngine;
using UnityEngine.InputSystem;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private Transform objectRotated;
    [SerializeField] private float rotationSpeed;

    private Vector2 _rotateAction;

    private void Update()
    {
        RotationMovement();
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        _rotateAction = context.ReadValue<Vector2>();
    }

    private void RotationMovement()
    {
        float mouseX = _rotateAction.x * rotationSpeed;
        float mouseY = _rotateAction.y * rotationSpeed;

        objectRotated.rotation = Quaternion.Euler(mouseY, -mouseX, 0f) * objectRotated.rotation;
    }
}
