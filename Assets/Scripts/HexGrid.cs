using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class HexGrid : MonoBehaviour
{

    public int width = 12;
    public int height = 12;

    public HexCell cellPrefab;
    public Text cellLabelPrefab;
    public GameObject objectParent;
    public GameObject hexCellParent;

    public bool showCoordinates = false;

    HexCell[] cells;

    Canvas gridCanvas;
    HexMesh hexMesh;

    void Start()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();

        cells = new HexCell[height * width];

        foreach (var item in cells)
        {
            DestroyImmediate(item);
        }

        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
            }
        }

        hexMesh.Triangulate(cells);
    }

    void Update()
    {
        if (showCoordinates)
        {
            if (gridCanvas.GetComponentsInChildren<Transform>().Length == 1 || gridCanvas.GetComponentsInChildren<Transform>() == null)
            {
                foreach (var item in cells)
                {
                    Text label = Instantiate<Text>(cellLabelPrefab);
                    label.rectTransform.SetParent(gridCanvas.transform, false);
                    label.rectTransform.anchoredPosition =
                        new Vector2(item.transform.position.x, item.transform.position.z);
                    label.text = item.coordinates.ToStringOnSeparateLines();
                }
            }
        }
        else
        {
            Transform[] labels = gridCanvas.GetComponentsInChildren<Transform>();

            for (int i = 0; i < labels.Length; i++)
            {
                if (labels[i].name != "Canvas")
                {
                    DestroyImmediate(labels[i].gameObject);
                }
            }
        }
    }


    public GameObject placeObject(HexCoordinates coordinates, GameObject objectToPlace)
    {
        //position = transform.InverseTransformPoint(position);
        //HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
        HexCell cell = cells[index];
        GameObject placedObject;
        if (objectToPlace != null)
        {
            if ((cell.placedObject != null && cell.placedObject.name != objectToPlace.name) || cell.placedObject == null)
            {
                if (cell.placedObject != null)
                {
                    Destroy(cell.placedObject);
                    cell.placedObject = null;
                }
                placedObject = Instantiate(objectToPlace);
                cell.placedObject = placedObject;
                placedObject.transform.position = cell.transform.position;
                placedObject.transform.position = new Vector3(placedObject.transform.position.x, placedObject.transform.localScale.y / 2, placedObject.transform.position.z);
                placedObject.transform.SetParent(objectParent.transform, true);
                return placedObject;
            }
        }
        else
        {
            Destroy(cell.placedObject);
            cell.placedObject = null;
        }
        return null;
    }

    void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

        HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
        cell.transform.SetParent(hexCellParent.transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
        //cell.color = Color.white;
    }
}