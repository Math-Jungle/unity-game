using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;
using Unity.VisualScripting;
using Rive;
using Rive.Components;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public RectTransform dialogBox;
    // public string[] lines;

    public DialogEvent[] events;
    public float textSpeed;
    public float preTextDelay;
    public float postTextDelay;
    public event Action OnDialogComplete;

    // public Animator animator;
    [SerializeField] public RiveWidget riveWidget;
    private Rive.StateMachine riveStateMachine;



    private int eventIndex;
    private int messageIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogText.text = string.Empty;
        // StartDialog();
        dialogBox.transform.localScale = Vector3.zero;

        //Wait for the Rive widget to load before getting the state machine
        if (riveWidget != null)
        {
            StartCoroutine(LoadRiveStateMachine());
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (dialogText.text == events[eventIndex].messages[messageIndex])
        // {
        //     NextLine();
        // }
    }

    private IEnumerator LoadRiveStateMachine()
    {
        while (riveWidget.Status != WidgetStatus.Loaded)
        {
            yield return null; // Wait for the Rive widget to load
        }

        riveStateMachine = riveWidget.StateMachine;
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
        dialogBox.LeanScale(Vector3.one, 0.6f);
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

            dialogBox.LeanScale(Vector3.zero, 0.6f);
            // gameObject.SetActive(false);
            StartCoroutine(DeactivateAfterDelay());
        }
    }

    private IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(0.6f);
        gameObject.SetActive(false);

        // Invoke the dialog complete event
        OnDialogComplete?.Invoke();
    }

    void PlayAnimation()
    {
        // if (animator != null)
        // {
        //     animator.SetTrigger(events[eventIndex].animationTrigger);
        // }

        if (riveStateMachine == null) return;

        SMITrigger riveTrigger = riveStateMachine.GetTrigger(events[eventIndex].animationTrigger);
        if (riveTrigger != null)
        {
            riveTrigger.Fire(); // Fire the trigger
        }


    }





}


