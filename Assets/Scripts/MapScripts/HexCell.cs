using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace HexFight
{
    [SelectionBase, Serializable, ExecuteInEditMode]
    public class HexCell : MonoBehaviour
    {
        public HexCoordinates position;
        public GameObject placedObjectPrefab;
        private GameObject placedObject;

        public HexCell(HexCoordinates pos)
        {
            position = pos;
        }

        public void PlacePrefab()
        {
            if (placedObjectPrefab != null)
            {
                if (placedObject == null)
                {
                    placedObject = (GameObject)Instantiate(placedObjectPrefab, gameObject.transform.position, placedObjectPrefab.transform.rotation);
                    placedObject.transform.position += new Vector3(0,placedObject.transform.lossyScale.y/2);
                    placedObject.transform.parent = gameObject.transform;
                    placedObject.name = placedObjectPrefab.name;
                }
            }
        }
        public void DestroyObject()
        {
            if (placedObject != null)
            {
                DestroyImmediate(placedObject);
            }
        }


        public HexCell[] GetNeighbours()
        {
            HexCell[] neighbours = new HexCell[6];

            HexCoordinates[] offsets = new HexCoordinates[6]
            {
            new HexCoordinates(-1,0),
            new HexCoordinates(1,0),
            null,null,null,null
            };
            if (position.y % 2 == 0)
            {
                offsets[2] = new HexCoordinates(0, 1);
                offsets[3] = new HexCoordinates(-1, 1);
                offsets[4] = new HexCoordinates(-1, -1);
                offsets[5] = new HexCoordinates(0, -1);
            }
            else
            {
                offsets[2] = new HexCoordinates(1, 1);
                offsets[3] = new HexCoordinates(0, 1);
                offsets[4] = new HexCoordinates(0, -1);
                offsets[5] = new HexCoordinates(1, -1);
            }
            for (int i = 0; i < offsets.Length; i++)
            {

                HexCoordinates realPos = position + offsets[i];
                GameObject map = GameObject.Find("Map");
                Map mapScript = map.GetComponent<Map>();
                HexCell cell = mapScript.GetCellByPosition(realPos);
                if (cell != null)
                {
                    neighbours[i] = cell;
                }
            }

            List<HexCell> returnValue = new List<HexCell>();
            foreach (var item in neighbours)
            {
                if (item != null)
                {
                    returnValue.Add(item);
                }
            }
            return returnValue.ToArray();
        }
    }
}
