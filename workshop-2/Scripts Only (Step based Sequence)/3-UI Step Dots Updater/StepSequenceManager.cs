using TMPro;
using UnityEngine;

public class StepSequenceManager : MonoBehaviour
{
    public TrainingStep[] steps;
    public TMP_Text instructionText;
    public StepDotsUI stepDotsUI;

    private int currentStepIndex = 0;
    private TrainingStep currentHighlightedStep;

    private void Start()
    {
        if (steps == null || steps.Length == 0)
        {
            if (instructionText != null)
                instructionText.text = "No steps assigned.";

            return;
        }

        if (stepDotsUI != null)
            stepDotsUI.CreateDots(steps.Length);

        ShowCurrentStep();
    }

    private void Update()
    {
        if (steps == null || steps.Length == 0) return;
        if (currentStepIndex >= steps.Length) return;

        if (IsCurrentStepComplete())
        {
            ClearHighlights(steps[currentStepIndex]);

            currentStepIndex++;
            ShowCurrentStep();
        }
    }

    private void ShowCurrentStep()
    {
        if (currentStepIndex >= steps.Length)
        {
            if (instructionText != null)
                instructionText.text = "Completed!";

            if (stepDotsUI != null)
                stepDotsUI.CompleteAll();

            return;
        }

        TrainingStep step = steps[currentStepIndex];

        if (instructionText != null)
            instructionText.text = step.instruction;

        if (stepDotsUI != null)
            stepDotsUI.UpdateDots(currentStepIndex);

        HighlightStep(step);
        currentHighlightedStep = step;
    }

    private void HighlightStep(TrainingStep step)
    {
        if (step.targetObject != null)
        {
            StepHighlighter highlighter =
                step.targetObject.GetComponentInChildren<StepHighlighter>(true);

            if (highlighter != null)
                highlighter.Highlight(true);
        }

        if (step.targetZone != null)
        {
            StepHighlighter highlighter =
                step.targetZone.GetComponentInChildren<StepHighlighter>(true);

            if (highlighter != null)
                highlighter.Highlight(true);
        }
    }

    private void ClearHighlights(TrainingStep step)
    {
        if (step.targetObject != null)
        {
            StepHighlighter highlighter =
                step.targetObject.GetComponentInChildren<StepHighlighter>(true);

            if (highlighter != null)
                highlighter.Highlight(false);
        }

        if (step.targetZone != null)
        {
            StepHighlighter highlighter =
                step.targetZone.GetComponentInChildren<StepHighlighter>(true);

            if (highlighter != null)
                highlighter.Highlight(false);
        }
    }

    private bool IsCurrentStepComplete()
    {
        TrainingStep step = steps[currentStepIndex];

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