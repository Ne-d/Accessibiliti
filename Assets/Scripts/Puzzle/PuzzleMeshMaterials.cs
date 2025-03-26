using System.Collections.Generic;
using UnityEngine;

public class PuzzleMeshMaterials : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] allMeshes;

    public void ChangeMaterials(PuzzleMat mats)
    {
        List<Material> listMat = new List<Material>();
        listMat.Add(mats.badColor);
        listMat.Add(mats.goodColor);

        foreach(MeshRenderer mr in allMeshes)
        {
            mr.SetMaterials(listMat);
        }
    }
}
