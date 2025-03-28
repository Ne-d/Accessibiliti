using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchPuzzle : MonoBehaviour
{
    [SerializeField] private GoodRotation[] puzzles;
    [SerializeField] private float delaySwitchPuzzle = 3;
    [SerializeField] private float delayScaling = 1;
    [SerializeField] private RotateObject rotateObj;
    [SerializeField] private RemapMaterial remapMat;

    private int _indexPuzzle = 0;
    private GoodRotation _actualPuzzle = null;

    private void Start()
    {
        StartPuzzle();
    }

    public void NextPuzzle()
    {
        rotateObj.ChangeObjectRotated(null);
        StartCoroutine(DelayNextPuzzle());
    }

    private void StartPuzzle()
    {
        if (_actualPuzzle != null)
        {
            Destroy(_actualPuzzle.gameObject);
            _indexPuzzle++;
        }

        if (_indexPuzzle >= puzzles.Length)
            SceneManager.LoadScene(0);

        _actualPuzzle = Instantiate(puzzles[_indexPuzzle]);
        ScaleUp(_actualPuzzle.gameObject);

        // change Materials
        if(remapMat)
            _actualPuzzle.GetComponent<PuzzleMeshMaterials>().ChangeMaterials(remapMat.GetActualPuzzleMat());
    }

    private void ScaleUp(GameObject obj)
    {
        StartCoroutine(ScaleCoroutine(obj, Vector3.zero, Vector3.one, delayScaling));
    }

    private void ScaleDown(GameObject obj)
    {
        StartCoroutine(ScaleCoroutine(obj, Vector3.one, Vector3.zero, delayScaling));
    }

    private IEnumerator ScaleCoroutine(GameObject obj, Vector3 startScale, Vector3 endScale, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            obj.transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        obj.transform.localScale = endScale; // Assurer la valeur finale

        // start of the next puzzle
        if(endScale == Vector3.zero)
        {
            StartPuzzle();
        }
        else
        {
            rotateObj.ChangeObjectRotated(_actualPuzzle.transform);
            _actualPuzzle.PuzzleStarting();
        }
    }

    private IEnumerator DelayNextPuzzle()
    {
        yield return new WaitForSeconds(delaySwitchPuzzle);

        ScaleDown(_actualPuzzle.gameObject);
    }
}
