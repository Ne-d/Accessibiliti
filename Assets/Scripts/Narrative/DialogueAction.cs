using UnityEngine;

[CreateAssetMenu(fileName = "DialogueAction", menuName = "Scriptable Objects/DialogueAction")]
public class DialogueAction : ScriptableObject
{
    [TextArea] public string questioner;

    [Header("Choices"), Space(6)]

    [TextArea] public string choiceA;
    [TextArea] public string choiceB;
    [TextArea] public string choiceC;
}
