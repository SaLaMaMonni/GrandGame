using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    private Queue<string> sentences;

    private bool isTalking = false;

    void Start()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if (isTalking)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                DisplayNextSentence();
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isTalking = true;
        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();    // Stops the coroutine if it's still going when the next sentence begins
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        isTalking = false;
        animator.SetBool("isOpen", false);
    }
}
