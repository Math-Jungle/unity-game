using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleSwitch : MonoBehaviour, IPointerClickHandler
{

    [Header("Toggle Switch Settings")]
    [SerializeField, Range(0, 1f)] private float sliderValue;

    public bool CurrentValue { get; private set; }//

    private Slider slider;

    [Header("Animation")]
    [SerializeField, Range(0, 1f)] private float animationDuration = 0.5f;
    [SerializeField] private AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Coroutine animationSliderCoroutine;

    [Header("Events")]
    [SerializeField] private UnityEvent onToggleOn;
    [SerializeField] private UnityEvent onToggleOff;

    protected void OnValidate()
    {
        SetupToggleSwitch();

        slider.value = sliderValue;


    }

    private void SetupToggleSwitch()
    {
        if (slider != null)
            return;

        SetupSliderComponent();
    }

    private void SetupSliderComponent()
    {
        slider = GetComponent<Slider>();
        if (slider == null)
        {
            Debug.LogError("ToggleSwitch: No Slider component found.", this);
            return;
        }

        slider.interactable = false; // so the users can't drag the slider. only clicking
        var sliderColors = slider.colors;
        sliderColors.disabledColor = Color.white;
        slider.transition = Selectable.Transition.None;

    }

    // public void SetupManager(ToggleSwitchManager manager)
    // {
    //     toggleSwitchManager = manager;
    // }

    private void Awake()
    {
        SetupToggleSwitch();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Toggle();
    }

    private void Toggle()
    {
        SetStateAndStartAnimation(!CurrentValue);
    }

    private void SetStateAndStartAnimation(bool state)
    {
        CurrentValue = state;

        if (CurrentValue)
        {
            onToggleOn.Invoke();
        }
        else
        {
            onToggleOff.Invoke();
        }

        if (animationSliderCoroutine != null)
        {
            StopCoroutine(animationSliderCoroutine);
        }

        animationSliderCoroutine = StartCoroutine(AnimateSlider());
    }

    private IEnumerator AnimateSlider()
    {
        float startValue = slider.value;
        float endValue = CurrentValue ? 1 : 0; // if the current value is true, set the end value to 1, else set it to 0

        float time = 0;

        if (animationDuration > 0)
        {
            while (time < animationDuration)
            {
                time += Time.deltaTime;
                float lerpFactor = animationCurve.Evaluate(time / animationDuration);
                slider.value = Mathf.Lerp(startValue, endValue, lerpFactor);
                yield return null;
            }
        }
        slider.value = endValue;

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
