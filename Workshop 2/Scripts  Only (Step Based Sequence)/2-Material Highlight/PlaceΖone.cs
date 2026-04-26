using UnityEngine;

public class PlaceZone : MonoBehaviour
{
    public GrabbableTracker currentObject;

    private void OnTriggerEnter(Collider other)
    {
        var tracker = other.GetComponentInParent<GrabbableTracker>();
        if (tracker != null)
        {
            currentObject = tracker;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var tracker = other.GetComponentInParent<GrabbableTracker>();
        if (tracker != null && currentObject == tracker)
        {
            currentObject = null;
        }
    }

    public bool Contains(GrabbableTracker tracker)
    {
        return currentObject == tracker;
    }
}