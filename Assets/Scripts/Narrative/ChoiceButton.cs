using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
    public SentenceAnimation sentence;
    public Button button;

    private ShaderModifier _shaderModifier = null;

    public void SetSentenceText(string text, float delaySpeed = 1, float startDelay = 0)
    {
        sentence.SetChoiceText(text, delaySpeed, startDelay);
        StartCoroutine(DelaySetInteractableButton(startDelay));
    }

    public void SetShaderAction(ShaderModifier sm)
    {
        _shaderModifier = sm;
    }

    private IEnumerator DelaySetInteractableButton(float delay)
    {
        button.interactable = false;
        yield return new WaitForSeconds(delay);
        button.interactable = true;
    }

    public void ChangeShader()
    {
        if(_shaderModifier != null)
            FogMaterialModifier.Instance.ModifyMaterial(_shaderModifier);
    }
}
