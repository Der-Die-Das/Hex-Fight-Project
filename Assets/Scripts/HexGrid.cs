using UnityEngine;
using UnityEngine.UI;

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


    void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();

        cells = new HexCell[height * width];

        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    void Start()
    {
        hexMesh.Triangulate(cells);
    }


    public void placeObject(Vector3 position, GameObject objectToPlace)
    {
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
        HexCell cell = cells[index];
        if (objectToPlace != null)
        {
            if ((cell.placedObject != null && cell.placedObject.name != objectToPlace.name) || cell.placedObject == null)
            {
                if (cell.placedObject != null)
                {
                    Destroy(cell.placedObject);
                    cell.placedObject = null;
                }
                    GameObject placedObject = Instantiate(objectToPlace);
                    cell.placedObject = placedObject;
                    placedObject.transform.position = cell.transform.position;
                    placedObject.transform.position = new Vector3(placedObject.transform.position.x, placedObject.transform.localScale.y / 2, placedObject.transform.position.z);
                    placedObject.transform.SetParent(objectParent.transform, true);
            }
        }else
        {
                Destroy(cell.placedObject);
                cell.placedObject = null;
        }
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

        if (showCoordinates)
        {
            Text label = Instantiate<Text>(cellLabelPrefab);
            label.rectTransform.SetParent(gridCanvas.transform, false);
            label.rectTransform.anchoredPosition =
                new Vector2(position.x, position.z);
            label.text = cell.coordinates.ToStringOnSeparateLines();
        }
    }
}