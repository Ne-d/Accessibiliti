using UnityEngine;
using System.Collections;

public class GoodRotation : MonoBehaviour
{
    [SerializeField] private Transform objectTransform;
    [SerializeField] private Quaternion finalRotation;
    [SerializeField] private float rotationDuration = 1f; // Dur�e de l'animation en secondes

    private bool _isRotating = false; // Emp�che le lancement de plusieurs coroutines en m�me temps

    public void SmoothRotate()
    {
        if (!_isRotating) // V�rifie qu'une rotation n'est pas d�j� en cours
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
