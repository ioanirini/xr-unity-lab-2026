using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepDotsUI : MonoBehaviour
{
    public GameObject dotPrefab;
    public Transform dotsParent;

    public Color normalColor = Color.gray;
    public Color completedColor = Color.green;

    private readonly List<Image> baseDots = new();
    private readonly List<GameObject> highlights = new();

    public void CreateDots(int count)
    {
        foreach (Transform child in dotsParent)
            Destroy(child.gameObject);

        baseDots.Clear();
        highlights.Clear();

        for (int i = 0; i < count; i++)
        {
            GameObject dot = Instantiate(dotPrefab, dotsParent);

            Image baseImage = dot.GetComponent<Image>();
            baseDots.Add(baseImage);

            GameObject highlightObj = null;

            foreach (Transform child in dot.GetComponentsInChildren<Transform>(true))
            {
                if (child.name == "Highlighted")
                {
                    highlightObj = child.gameObject;
                    break;
                }
            }

            if (highlightObj != null)
                highlightObj.SetActive(false);

            highlights.Add(highlightObj);
        }

        UpdateDots(0);
    }

    public void UpdateDots(int activeIndex)
    {
        for (int i = 0; i < baseDots.Count; i++)
        {
            if (baseDots[i] != null)
                baseDots[i].color = i < activeIndex ? completedColor : normalColor;

            if (highlights[i] != null)
                highlights[i].SetActive(i == activeIndex);
        }
    }

    public void CompleteAll()
    {
        for (int i = 0; i < baseDots.Count; i++)
        {
            if (baseDots[i] != null)
                baseDots[i].color = completedColor;

            if (highlights[i] != null)
                highlights[i].SetActive(false);
        }
    }
}