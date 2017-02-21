using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeController : MonoBehaviour
{

    public bool Vertical = true;

    public int columnsOrRows = 3;
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("ReSize",0f, 2f);
    }
    void ReSize()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        GridLayoutGroup gridLayout = GetComponent<GridLayoutGroup>();
        int childCount = transform.childCount;
        int rowsOrColumns = Mathf.RoundToInt(childCount / columnsOrRows);
        int realRowsOrColumns = 0;
        Vector2 spacing = gridLayout.spacing;
        if (rowsOrColumns < (float)childCount / (float)columnsOrRows)
        {
            realRowsOrColumns = rowsOrColumns + 1;
        }
        else
        {
            realRowsOrColumns = rowsOrColumns;
        }

        if (Vertical)
        {
            int pixelHeight = (int)((realRowsOrColumns + 1) * spacing.y + realRowsOrColumns * gridLayout.cellSize.y);
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, pixelHeight);
        }
        else
        {
            int pixelWidth = (int)((realRowsOrColumns + 1) * spacing.x + realRowsOrColumns * gridLayout.cellSize.x);
            rectTransform.sizeDelta = new Vector2(pixelWidth, rectTransform.sizeDelta.y);
        }
    }
}
