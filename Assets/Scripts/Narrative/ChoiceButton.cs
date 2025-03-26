using UnityEngine;
using TMPro;
using System.Collections;

public class ChoiceButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textZone;

    [Header("Values text"), Space(6)]

    [SerializeField] private float durationTextSpawn = .05f;

    private void Awake()
    {
        textZone.text = "";
    }

    public void SetChoiceText(string text)
    {
        StopAllCoroutines();
        textZone.text = "";
        StartCoroutine(DelaySpawnSentence(text));
    }

    private IEnumerator DelaySpawnSentence(string text)
    {
        string actualText = "";
        int index = 0;

        while(index < text.Length)
        {
            actualText += text[index];
            textZone.text = actualText;
            index++;
            yield return new WaitForSeconds(durationTextSpawn);
        }
    }
}
