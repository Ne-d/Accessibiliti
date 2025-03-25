using UnityEngine;
using System.Collections;

public class GoodRotation : MonoBehaviour
{
    [SerializeField] private Transform objectTransform;
    [SerializeField] private Quaternion finalRotation;
    [SerializeField] private Quaternion startRotation;
    [SerializeField] private float rotationDuration = 1f;

    private bool _isRotating = false;

    private void Awake()
    {
        objectTransform.rotation = startRotation;
    }

    public void SmoothRotate()
    {
        if (!_isRotating)
        {
            StartCoroutine(RotateOverTime(rotationDuration));
        }
    }

    private IEnumerator RotateOverTime(float duration)
    {
        _isRotating = true;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            objectTransform.rotation = Quaternion.Slerp(objectTransform.rotation, finalRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objectTransform.rotation = finalRotation; // Assure que la rotation atteint bien la valeur finale
        _isRotating = false;
    }
}
