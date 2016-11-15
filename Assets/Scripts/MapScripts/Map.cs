using UnityEngine;
using System.Collections;
namespace HexFight
{
    public class Map : MonoBehaviour
    {
        public int height;
        public int width;
        public GameObject hexPrefab;
        public HexCell[] cells;

        public void ReCreateMap()
        {
            HexCell[] allCells = this.transform.GetComponentsInChildren<HexCell>();
            if (allCells != null)
            {
                foreach (var item in allCells)
                {
                    DestroyImmediate(item.gameObject);
                }
            }
            cells = new HexCell[width* height]; // -> fix this somehow
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    float xOffset = 0;
                    float yOffset = -0.23f;
                    if (y % 2 == 1)
                    {
                        xOffset += 0.45f;
                    }
                    GameObject cell = (GameObject)Instantiate(hexPrefab, new Vector3(x + xOffset - x*0.11f, 0, y + y*yOffset), Quaternion.identity);
                    cell.transform.parent = this.transform;
                    cell.name = "HexCell_" + x + "_" + y;

                    cell.GetComponent<HexCell>().position = new HexCoordinates(x, y);
                }
            }
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000))
                {
                    GameObject hitObj = hit.transform.parent.gameObject;
                    HexCell hitCell = hitObj.GetComponent<HexCell>();
                    HexCell[] neighbours = hitCell.GetNeighbours();

                    hitObj.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color = Color.red;

                    foreach (var item in neighbours)
                    {
                        item.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                    }
                }
                else{
                    Debug.Log("Nothing");
                }
            }
        }
        public HexCell GetCellByPosition(HexCoordinates pos)
        {
            cells = transform.GetComponentsInChildren<HexCell>(); //not with good performance
            foreach (var item in cells)
            {
                if (item.position == pos)
                {
                    return item;
                }
            }
            return null;
        }
    }
}