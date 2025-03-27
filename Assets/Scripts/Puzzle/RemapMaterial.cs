using UnityEngine;
using UnityEngine.UI;

public class RemapMaterial : MonoBehaviour
{
    [SerializeField] private Image goodMatUi;
    [SerializeField] private Image badMatUi;
    [SerializeField] private PuzzleMat[] allMats;

    private int _indexMat = 0;

    public PuzzleMat GetActualPuzzleMat()
    {
        return allMats[_indexMat];
    }

    public void SwitchMaterials()
    {
        _indexMat = (_indexMat + 1) % allMats.Length;

        PuzzleMeshMaterials pmm = FindAnyObjectByType<PuzzleMeshMaterials>();
        if(pmm != null)
        {
            pmm.ChangeMaterials(allMats[_indexMat]);
            goodMatUi.material = allMats[_indexMat].goodColor;
            badMatUi.material = allMats[_indexMat].badColor;
        }
    }
}
