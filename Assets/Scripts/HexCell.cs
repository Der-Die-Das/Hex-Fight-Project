using UnityEngine;

[ExecuteInEditMode]
public class HexCell : MonoBehaviour
{

    public HexCoordinates coordinates;

    public GameObject placedObjectPrefab;

    [HideInInspector]
    public GameObject placedObject = null;
    [Range(-180, 180)]
    public int yRotation = 0;
    [Range(-90f, 90f)]
    public int localOffset = 0;

    public bool inner, outer = false;

    void Update()
    {
        if (placedObjectPrefab != null)
        {
            if (placedObject == null || placedObject.name + "(Clome)" != placedObjectPrefab.name)
            {
                if (placedObject != null)
                {
                    DestroyImmediate(placedObject);
                }
                placedObject = Instantiate(placedObjectPrefab);
                placedObject.transform.position = new Vector3(gameObject.transform.position.x, placedObject.transform.localScale.y / 2, gameObject.transform.position.z);
                placedObject.transform.parent = gameObject.transform;
            }
        }
        else
        {
            if (placedObject != null)
            {
                DestroyImmediate(placedObject);
            }
        }
        if (placedObject != null)
        {
            if (yRotation != placedObject.transform.rotation.x)
            {
                placedObject.transform.rotation = Quaternion.Euler(placedObject.transform.rotation.x, yRotation, placedObject.transform.rotation.z);
            }

            if (placedObject.transform.localPosition.x != (localOffset / 10f))
            {
                //placedObject.transform.localPosition = new Vector3(0, placedObject.transform.localScale.y/2, localOffset);
                Vector3 localPos = placedObject.transform.TransformDirection(Vector3.forward * (localOffset / 10f)) + new Vector3(0, placedObject.transform.localScale.y / 2, 0);
                placedObject.transform.localPosition = localPos;
            }

            if (inner && outer)
            {
                inner = true;
                outer = false;
            }
            else if (!inner && !outer)
            {
                inner = false;
                outer = true;
            }

            if (inner && placedObject.transform.localScale.x != 17.5)
            {
                placedObject.transform.localScale = new Vector3(17.5f, placedObject.transform.localScale.y, placedObject.transform.localScale.z);
            }
            else if (outer && placedObject.transform.localScale.x != 20)
            {
                placedObject.transform.localScale = new Vector3(20f, placedObject.transform.localScale.y, placedObject.transform.localScale.z);
            }
        }
    }
    //public Color color;
}