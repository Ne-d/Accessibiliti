using System.Collections;
using UnityEngine;

public class FogMaterialModifier : MonoBehaviour
{
    [SerializeField] private Material initialMat;
    [SerializeField] private Material modifiedMat;
    [SerializeField] private float transitionDuration = 2.0f; // Durée de transition en secondes
    [SerializeField] private float speedAccessibility = 1;

    public static FogMaterialModifier Instance;
    private ShaderModifier _actualShaderModifier;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        modifiedMat.CopyPropertiesFromMaterial(initialMat);
    }

    /// <summary>
    /// Change the speed from 1 to 0 to go with accessibility
    /// </summary>
    /// <param name="speed"></param>
    public void SetSpeedAccessibility(float speed)
    {
        speedAccessibility = speed;
        ModifyMaterial(_actualShaderModifier);
    }

    public void ModifyMaterial(ShaderModifier sm)
    {
        _actualShaderModifier = sm;
        StartCoroutine(SmoothModifyer(sm.targetColor, sm.targetNoiseScale * speedAccessibility, sm.targetAmplitude, sm.texture, sm.targetNoiseSpeed * speedAccessibility));
    }

    private IEnumerator SmoothModifyer(Color targetColor, Vector2 targetNoiseScale, float targetAmplitude, Texture2D texture, Vector2 targetNoiseSpeed)
    {
        Color startColor = modifiedMat.GetColor("_FogTint");
        Vector2 startNoiseScale = modifiedMat.GetVector("_Noise_Scale");
        float startAmplitude = modifiedMat.GetFloat("_Amplitude");
        Vector2 startNoiseSpeed = modifiedMat.GetVector("_Noise_Speed");

        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;

            modifiedMat.SetColor("_FogTint", Color.Lerp(startColor, targetColor, t));
            modifiedMat.SetVector("_Noise_Scale", Vector2.Lerp(startNoiseScale, targetNoiseScale, t));
            modifiedMat.SetFloat("_Amplitude", Mathf.Lerp(startAmplitude, targetAmplitude, t));
            modifiedMat.SetVector("_Noise_Speed", Vector2.Lerp(startNoiseSpeed, targetNoiseSpeed, t));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Assurer la valeur finale
        modifiedMat.SetColor("_FogTint", targetColor);
        modifiedMat.SetVector("_Noise_Scale", targetNoiseScale);
        modifiedMat.SetFloat("_Amplitude", targetAmplitude);
        modifiedMat.SetVector("_Noise_Speed", targetNoiseSpeed);

        if (texture != null)
        {
            modifiedMat.SetTexture("_Gradient", texture);
        }
    }
}
