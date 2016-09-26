using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class HexMapEditor : MonoBehaviour
{
    public HexGrid hexGrid;

    void Update()
    {
            //if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            //{
            //    HandleInput();
            //}
    }

    void HandleInput()
    {
        //Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //if (Physics.Raycast(inputRay, out hit))
        //{
        //    hexGrid.placeObject(hit.point, activeObject);

        //}
    }

}
