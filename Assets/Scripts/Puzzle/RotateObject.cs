using UnityEngine;
using UnityEngine.InputSystem;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private Transform objectRotated;
    [SerializeField] private float maxRotationSpeed = 100f; 
    [SerializeField] private float accelerationTime = 0.2f; 
    [SerializeField] private float decelerationTime = 0.3f;

    private Vector2 _rotateInput;
    private Vector2 _currentVelocity;
    private Vector2 _smoothedRotation;
    private Quaternion _currentRotation;

    private void Start()
    {
        _currentRotation = objectRotated.rotation;
    }

    private void Update()
    {
        SmoothRotationMovement();
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        _rotateInput = context.ReadValue<Vector2>();
    }

    private void SmoothRotationMovement()
    {
        // Appliquer une interpolation exponentielle sur la vitesse
        _smoothedRotation.x = Mathf.SmoothDamp(_smoothedRotation.x, _rotateInput.x * maxRotationSpeed, ref _currentVelocity.x, _rotateInput.sqrMagnitude > 0.01f ? accelerationTime : decelerationTime);
        _smoothedRotation.y = Mathf.SmoothDamp(_smoothedRotation.y, _rotateInput.y * maxRotationSpeed, ref _currentVelocity.y, _rotateInput.sqrMagnitude > 0.01f ? accelerationTime : decelerationTime);

        // Appliquer la rotation accumulée progressivement
        _currentRotation = Quaternion.Euler(_smoothedRotation.y * Time.deltaTime, -_smoothedRotation.x * Time.deltaTime, 0f) * objectRotated.rotation;

        // Mise à jour de la rotation de l'objet
        objectRotated.rotation = _currentRotation;
    }

    /// <summary>
    /// Initial rotation movement without smooth
    /// </summary>
    private void RotationMovement()
    {
        float mouseX = _rotateInput.x * maxRotationSpeed;
        float mouseY = _rotateInput.y * maxRotationSpeed;

        Quaternion _targetRotation = Quaternion.Euler(mouseY, -mouseX, 0f) * objectRotated.rotation;
        objectRotated.rotation = _targetRotation;
    }

}
