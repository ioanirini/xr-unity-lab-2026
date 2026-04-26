using UnityEngine;

public enum StepType
{
    GrabObject,
    PlaceObjectInZone
}

[System.Serializable]
public class TrainingStep
{
    [TextArea]
    public string instruction;

    public StepType stepType;
    public GrabbableTracker targetObject;
    public PlaceZone targetZone;
}