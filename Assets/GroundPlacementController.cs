using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlacementController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeableObjectPrefabs;
	[SerializeField]
	private GameObject[] TranparentPrefabs;
	[SerializeField]
	private GameObject[] InvalidPrefabs;

    private GameObject currentPlaceableObject;

    private float mouseWheelRotation;
    private int currentPrefabIndex = -1;
	private int _collisions = 0;


    private void Update()
    {
        HandleNewObjectHotkey();

        if (currentPlaceableObject != null)
        {
            MoveCurrentObjectToMouse();
            RotateFromMouseWheel();
            ReleaseIfClicked();
        }
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Terrain") 
		{
			return;
		}
		//Debug.Log("Hello");
		_collisions++;
	}
	
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Terrain") 
		{
			return;
		}
		_collisions--;
	}
	
    private void HandleNewObjectHotkey()
    {
        for (int i = 0; i < placeableObjectPrefabs.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + 1 + i))
            {
                if (PressedKeyOfCurrentPrefab(i))
                {
                    Destroy(currentPlaceableObject);
                    currentPrefabIndex = -1;
                }
                else
                {
                    if (currentPlaceableObject != null)
                    {
                        Destroy(currentPlaceableObject);
                    }
					if (isValidPlacement())
					{
						currentPlaceableObject = Instantiate(TranparentPrefabs[i]);
						currentPrefabIndex = i;
					}
					else
					{
						currentPlaceableObject = Instantiate(InvalidPrefabs[i]);
						currentPrefabIndex = i;
					}
                }
                break;
            }
        }
    }

    private bool PressedKeyOfCurrentPrefab(int i)
    {
        return currentPlaceableObject != null && currentPrefabIndex == i;
    }

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            currentPlaceableObject.transform.position = hitInfo.point;
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }

    private void RotateFromMouseWheel()
    {
        Debug.Log(Input.mouseScrollDelta);
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
			Destroy(currentPlaceableObject);
            currentPlaceableObject = Instantiate(placeableObjectPrefabs[currentPrefabIndex]);
			MoveCurrentObjectToMouse();
			currentPlaceableObject = null;
        }
    }
	
	public bool isValidPlacement()
	{
		return _collisions == 0;
	}
}
