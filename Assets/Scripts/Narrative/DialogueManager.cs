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

        // set text choices
        choiceA.SetSentenceText(currentDialogue.choiceA.sentence, startDelay: duration);
        choiceB.SetSentenceText(currentDialogue.choiceB.sentence, startDelay: duration);
        choiceC.SetSentenceText(currentDialogue.choiceC.sentence, startDelay: duration);

        // set shader answer
        choiceA.SetShaderAction(currentDialogue.choiceA.shaderModifier);
        choiceB.SetShaderAction(currentDialogue.choiceB.shaderModifier);
        choiceC.SetShaderAction(currentDialogue.choiceC.shaderModifier);

        firstSelectedSystem.SetSelected(choiceA.gameObject);
    }

    private void EndDialogues()
    {
        // TODO : disappear dialogues and the end of the scene
    }
}
