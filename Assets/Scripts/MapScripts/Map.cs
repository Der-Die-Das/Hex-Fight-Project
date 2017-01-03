using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace HexFight
{
    [Serializable]
    public class Map : MonoBehaviour
    {
        public GameObject ConnectionPrefab = null;
        public static List<ConnectedHexCell> connections = new List<ConnectedHexCell>();
        public int height;
        public int width;
        public GameObject hexPrefab;
        public HexCell[] cells;

        public GameObject footman;

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
            cells = new HexCell[width * height]; // -> fix this somehow
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
                    GameObject cell = (GameObject)Instantiate(hexPrefab, new Vector3(x + xOffset - x * 0.11f, 0, y + y * yOffset), Quaternion.identity);
                    cell.transform.parent = this.transform;
                    cell.name = "HexCell_" + x + "_" + y;


                    cell.GetComponent<HexCell>().position = new HexCoordinates(x, y);
                    for (int i = 0; i < cells.Length; i++)
                    {
                        if (cells[i] == null)
                        {
                            cells[i] = cell.GetComponent<HexCell>();
                            break;
                        }
                    }

                }
            }
            Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y, -2);
            Camera.main.transform.position += new Vector3(width / 2, 0);
        }

        void Awake()
        {
            if (ConnectionPrefab == null)
            {
                throw new Exception("No WallPrefab ..");
            }
        }
        void Start()
        {
            //check for connections to make

            foreach (var item in cells)
            {
                if (item.placedObjectPrefab != null && item.placedObjectPrefab.GetComponent<DefenseStructure>() != null)
                {
                    foreach (var neighbour in item.GetNeighbours())
                    {
                        if (neighbour.placedObjectPrefab != null && neighbour.placedObjectPrefab.GetComponent<DefenseStructure>() != null)
                        {
                            if (checkIfConnectionExists(item, neighbour) == false)
                            {
                                connections.Add(new ConnectedHexCell(item, neighbour));
                            }
                        }
                    }
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
                    hitCell.placedObjectPrefab = footman;
                    hitCell.PlacePrefab();
                    //HexCell[] neighbours = hitCell.GetNeighbours();
                }
                else
                {
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


        public class ConnectedHexCell
        {
            public HexCell hexCell1;
            public HexCell hexCell2;
            GameObject connection;

            public ConnectedHexCell(HexCell firstCell, HexCell secondCell)
            {
                hexCell1 = firstCell;
                hexCell2 = secondCell;
                Vector3 pos = Vector3.Lerp(hexCell1.transform.position, hexCell2.transform.position, 0.5f);

                Quaternion rotation = Quaternion.LookRotation(hexCell2.transform.position - hexCell1.transform.position);
                connection = (GameObject)Instantiate(FindObjectOfType<Map>().ConnectionPrefab, pos, rotation);
                connection.transform.position += new Vector3(0, connection.transform.lossyScale.y / 2);
                connection.transform.parent = GameObject.Find("Connections").transform;
                connection.name = "Connection_" + hexCell1.position.x + ":" + hexCell1.position.y + "_" + hexCell2.position.x + ":" + hexCell2.position.y;

            }

        }


        public static bool checkIfConnectionExists(HexCell hexCell1, HexCell hexCell2)
        {
            if (connections.Count == 0)
            {
                return false;
            }
            foreach (var item in connections)
            {
                if (item.hexCell1 == hexCell1 || item.hexCell1 == hexCell2)
                {
                    if (item.hexCell2 == hexCell1 || item.hexCell2 == hexCell2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}