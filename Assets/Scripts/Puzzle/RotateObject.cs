using UnityEngine;
using UnityEngine.InputSystem;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private Transform objectRotated;
    [SerializeField] private float rotationSpeedKey = 100f;
    [SerializeField] private float rotationSpeedMouse = 50f;
    [SerializeField] private float accelerationTime = 0.2f; 
    [SerializeField] private float decelerationTime = 0.3f;

    private Vector2 _rotateInput;
    private Vector2 _currentVelocity;
    private Vector2 _smoothedRotation;
    private Quaternion _currentRotation;
    private bool _isMouseDown = false;

    private void Update()
    {
        if(objectRotated != null)
            SmoothRotationMovement();
    }

    public void OnMouseDown(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isMouseDown = true;
        }

        if (context.canceled)
        {
            _isMouseDown = false;
        }
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        if(_isMouseDown)
            _rotateInput = context.ReadValue<Vector2>() * rotationSpeedMouse;
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        _rotateInput = context.ReadValue<Vector2>() * rotationSpeedKey;
    }

    private void SmoothRotationMovement()
    {
        // Appliquer une interpolation exponentielle sur la vitesse
        _smoothedRotation.x = Mathf.SmoothDamp(_smoothedRotation.x, _rotateInput.x, ref _currentVelocity.x, _rotateInput.sqrMagnitude > 0.01f ? accelerationTime : decelerationTime);
        _smoothedRotation.y = Mathf.SmoothDamp(_smoothedRotation.y, _rotateInput.y, ref _currentVelocity.y, _rotateInput.sqrMagnitude > 0.01f ? accelerationTime : decelerationTime);

        // Appliquer la rotation accumul�e progressivement
        _currentRotation = Quaternion.Euler(_smoothedRotation.y * Time.deltaTime, -_smoothedRotation.x * Time.deltaTime, 0f) * objectRotated.rotation;

        // Mise � jour de la rotation de l'objet
        objectRotated.rotation = _currentRotation;
    }

    /// <summary>
    /// Initial rotation movement without smooth
    /// </summary>
    private void RotationMovement()
    {
        float mouseX = _rotateInput.x * rotationSpeedKey;
        float mouseY = _rotateInput.y * rotationSpeedKey;

        Quaternion _targetRotation = Quaternion.Euler(mouseY, -mouseX, 0f) * objectRotated.rotation;
        objectRotated.rotation = _targetRotation;
    }

    public void ChangeObjectRotated(Transform tr)
    {
        objectRotated = tr;
    }

}
