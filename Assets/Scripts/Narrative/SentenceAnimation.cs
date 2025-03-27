using UnityEngine;
using TMPro;
using System.Collections;

public class SentenceAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textZone;

    [Header("Values text"), Space(6)]

    [SerializeField] private float durationTextSpawn = .05f;

    private void Awake()
    {
        textZone.text = "";
    }

    public float SetChoiceText(string text, float delaySpeed = 1, float startDelay = 0)
    {
        StopAllCoroutines();
        textZone.text = "";
        float delay = durationTextSpawn * delaySpeed;
        StartCoroutine(DelaySpawnSentence(text, delay, startDelay));
        return delay * text.Replace(" ", "").Length;
    }

    private IEnumerator DelaySpawnSentence(string text, float delay, float startDelay)
    {
        string actualText = "";
        int index = 0;

        yield return new WaitForSeconds(startDelay);

        while(index < text.Length)
        {
            char letterAdded = text[index];
            if (!letterAdded.Equals(' '))
                yield return new WaitForSeconds(delay);

            actualText += letterAdded;
            textZone.text = actualText;
            index++;
        }
    }
}
