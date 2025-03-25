using UnityEngine;

public class FinishPuzzle : MonoBehaviour
{
    [SerializeField] private LayerMask finishLayerMask;
    [SerializeField] private RotateObject scriptRotateObject;
    [SerializeField] private float distanceRay = 5;

    private RaycastHit hit;
    private bool _shootRay = true;

    private void FixedUpdate()
    {
        // Does the ray intersect any objects excluding the player layer
        if (_shootRay && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distanceRay, finishLayerMask))
        {
            _shootRay = false;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            HitGoodRotation();
        }
    }

    private void HitGoodRotation()
    {
        scriptRotateObject.ChangeObjectRotated(null);
        hit.collider.transform.parent.GetComponent<GoodRotation>().SmoothRotate();
    }
}
