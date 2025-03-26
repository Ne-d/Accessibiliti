using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private ChoiceButton choiceA;
    [SerializeField] private ChoiceButton choiceB;
    [SerializeField] private ChoiceButton choiceC;

    [SerializeField] private DialogueAction[] dialogues;

    private int _indexCurrentDialogue = 0;

    private void Start()
    {
        DialogueAppear(_indexCurrentDialogue);
    }

    public void NextDialogue()
    {
        _indexCurrentDialogue++;

        if (_indexCurrentDialogue >= dialogues.Length)
            return;

        DialogueAppear(_indexCurrentDialogue);
    }

    private void DialogueAppear(int index)
    {
        DialogueAction currentDialogue = dialogues[index];

        // TODO : add questioner dialogue

        choiceA.SetChoiceText(currentDialogue.choiceA);
        choiceB.SetChoiceText(currentDialogue.choiceB);
        choiceC.SetChoiceText(currentDialogue.choiceC);
    }
}
