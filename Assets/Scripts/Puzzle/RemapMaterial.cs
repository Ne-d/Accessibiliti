using UnityEngine;

public class RemapMaterial : MonoBehaviour
{
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
        }
    }
}
