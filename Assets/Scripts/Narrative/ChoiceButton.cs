using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
    public SentenceAnimation sentence;
    public Button button;

    public void SetSentenceText(string text, float delaySpeed = 1, float startDelay = 0)
    {
        sentence.SetChoiceText(text, delaySpeed, startDelay);
        StartCoroutine(DelaySetInteractableButton(startDelay));
    }

    private IEnumerator DelaySetInteractableButton(float delay)
    {
        button.interactable = false;
        yield return new WaitForSeconds(delay);
        button.interactable = true;
    }
}
