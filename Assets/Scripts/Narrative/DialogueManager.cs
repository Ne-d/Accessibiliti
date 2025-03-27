using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private ChoiceButton choiceA;
    [SerializeField] private ChoiceButton choiceB;
    [SerializeField] private ChoiceButton choiceC;

    [SerializeField] private SentenceAnimation questioner;
    [SerializeField] private FirstSelectedSystem firstSelectedSystem;

    [SerializeField] private DialogueAction[] dialogues;

    private int _indexCurrentDialogue = 0;

    private void Start()
    {
        DialogueAppear(_indexCurrentDialogue);
    }

    public void NextDialogue()
    {
        _indexCurrentDialogue++;

        // End dialogues
        if (_indexCurrentDialogue >= dialogues.Length)
        {
            EndDialogues();
            return;
        }

        DialogueAppear(_indexCurrentDialogue);
    }

    private void DialogueAppear(int index)
    {
        DialogueAction currentDialogue = dialogues[index];

        float duration = questioner.SetChoiceText(currentDialogue.questioner);
        duration++;

        choiceA.SetSentenceText(currentDialogue.choiceA, startDelay: duration);
        choiceB.SetSentenceText(currentDialogue.choiceB, startDelay: duration);
        choiceC.SetSentenceText(currentDialogue.choiceC, startDelay: duration);

        firstSelectedSystem.SetSelected(choiceA.gameObject);
    }

    private void EndDialogues()
    {
        // TODO : disappear dialogues and the end of the scene
    }
}
