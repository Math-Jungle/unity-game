using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    // public string[] lines;

    public DialogEvent[] events;
    public float textSpeed;
    public float preTextDelay;
    public float postTextDelay;

    public Animator animator;

    private int eventIndex;
    private int messageIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogText.text = string.Empty;
        // StartDialog();
    }

    // Update is called once per frame
    void Update()
    {
        // if (dialogText.text == events[eventIndex].messages[messageIndex])
        // {
        //     NextLine();
        // }
    }

    void StartDialog()
    {
        eventIndex = 0;
        messageIndex = 0;
        PlayAnimation();
        StartCoroutine(TypeLine());
    }

    public void RunEvent(int newEventIndex)
    {
        if (newEventIndex < 0 || newEventIndex >= events.Length)
        {
            Debug.LogError("Invalid event index");
            return;
        }

        gameObject.SetActive(true);
        eventIndex = newEventIndex;
        messageIndex = 0;
        dialogText.text = string.Empty;

        StartCoroutine(StartEaventWithDelay());// Starting the event with a delay to ensure the game object is fully active
    }

    private IEnumerator StartEaventWithDelay()
    {
        //yield return null; // Wait for the current frame for the game object to be active
        yield return new WaitForSeconds(preTextDelay);
        PlayAnimation();
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in events[eventIndex].messages[messageIndex].ToCharArray())
        {
            dialogText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        yield return new WaitForSeconds(postTextDelay);
        NextLine();
    }

    void NextLine()
    {
        if (messageIndex < events[eventIndex].messages.Length - 1)
        {
            messageIndex++;
            dialogText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            // StartCoroutine(DeactivateAfterDelay());
            gameObject.SetActive(false);
        }
    }

    // private IEnumerator DeactivateAfterDelay()
    // {
    //     yield return new WaitForSeconds(postTextDelay);
    //     gameObject.SetActive(false);
    // }

    void PlayAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger(events[eventIndex].animationTrigger);
        }
    }




}


