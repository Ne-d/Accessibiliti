using System;
using UnityEngine;

[Serializable]
public struct Choice
{
    [TextArea] public string sentence;
    public ShaderModifier shaderModifier;
}

[CreateAssetMenu(fileName = "DialogueAction", menuName = "Scriptable Objects/DialogueAction")]
public class DialogueAction : ScriptableObject
{
    [TextArea] public string questioner;

    [Header("Choices"), Space(6)]

    public Choice choiceA;

    [Space(4)]

    public Choice choiceB;

    [Space(4)]

    public Choice choiceC;
}
