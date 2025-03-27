using UnityEngine;

[CreateAssetMenu(fileName = "ShaderModifier", menuName = "Scriptable Objects/ShaderModifier")]
public class ShaderModifier : ScriptableObject
{
    public Color targetColor;
    public Vector2 targetNoiseScale;
    public float targetAmplitude;
    public Texture2D texture;
    public Vector2 targetNoiseSpeed;
}
