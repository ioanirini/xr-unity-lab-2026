using System.Collections.Generic;
using UnityEngine;

public class StepHighlighter : MonoBehaviour
{
    [SerializeField] private Renderer[] renderers;
    public Color highlightColor = Color.yellow;
    public float pulseSpeed = 3f;

    private readonly List<Material> materials = new();
    private readonly List<Color> originalColors = new();
    private bool initialized;
    private bool isHighlighting;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (initialized) return;

        if (renderers == null || renderers.Length == 0)
            renderers = GetComponentsInChildren<Renderer>(true);

        materials.Clear();
        originalColors.Clear();

        foreach (Renderer r in renderers)
        {
            if (r == null) continue;

            foreach (Material mat in r.materials)
            {
                if (mat == null) continue;

                materials.Add(mat);
                originalColors.Add(GetColor(mat));
            }
        }

        if (materials.Count == 0)
        {
            Debug.LogWarning("StepHighlighter: No valid materials found on " + gameObject.name);
            return;
        }

        initialized = true;
    }

    private void Update()
    {
        if (!isHighlighting) return;

        Init();
        if (!initialized) return;

        float t = (Mathf.Sin(Time.time * pulseSpeed) + 1f) * 0.5f;

        for (int i = 0; i < materials.Count; i++)
        {
            Color pulseColor = Color.Lerp(originalColors[i], highlightColor, t);
            SetColor(materials[i], pulseColor);
        }
    }

    public void Highlight(bool on)
    {
        Init();
        if (!initialized) return;

        isHighlighting = on;

        if (!on)
        {
            for (int i = 0; i < materials.Count; i++)
                SetColor(materials[i], originalColors[i]);
        }
    }

    private Color GetColor(Material mat)
    {
        if (mat.HasProperty("_BaseColor"))
            return mat.GetColor("_BaseColor");

        if (mat.HasProperty("_Color"))
            return mat.GetColor("_Color");

        return Color.white;
    }

    private void SetColor(Material mat, Color color)
    {
        if (mat.HasProperty("_BaseColor"))
            mat.SetColor("_BaseColor", color);

        if (mat.HasProperty("_Color"))
            mat.SetColor("_Color", color);
    }
}