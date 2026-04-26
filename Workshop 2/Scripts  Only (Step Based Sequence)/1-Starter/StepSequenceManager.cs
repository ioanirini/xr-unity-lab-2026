using TMPro;
using UnityEngine;

public class StepSequenceManager : MonoBehaviour
{
    public TrainingStep[] steps;
    public TMP_Text instructionText;

    private int currentStepIndex = 0;

    private void Start()
    {
        ShowCurrentStep();
    }

    private void Update()
    {
        if (steps == null || steps.Length == 0) return;
        if (currentStepIndex >= steps.Length) return;

        if (IsCurrentStepComplete())
        {
            currentStepIndex++;
            ShowCurrentStep();
        }
    }

    private void ShowCurrentStep()
    {
        if (currentStepIndex >= steps.Length)
        {
            instructionText.text = "Completed!";
            return;
        }

        instructionText.text = steps[currentStepIndex].instruction;
    }

    private bool IsCurrentStepComplete()
    {
        var step = steps[currentStepIndex];

        switch (step.stepType)
        {
            case StepType.GrabObject:
                return step.targetObject != null && step.targetObject.IsGrabbed;

            case StepType.PlaceObjectInZone:
                return step.targetObject != null
                    && step.targetZone != null
                    && step.targetZone.Contains(step.targetObject)
                    && !step.targetObject.IsGrabbed;

            default:
                return false;
        }
    }
}