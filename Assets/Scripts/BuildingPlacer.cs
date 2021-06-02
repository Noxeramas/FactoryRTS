using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    private Building _placedBuilding = null;
    // Start is called before the first frame update
    void Start()
    {
        

        
    }
    private Ray _ray;
    private RaycastHit _raycastHit;
    private Vector3 _lastPlacementPosition;
    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.B))
         {
            _PreparePlacedBuilding(0);
         }
        
        if (_placedBuilding != null)
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                _CancelPlacedBuilding();
                return;
            }
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(
                _ray,
                out _raycastHit,
                1000f,
                Globals.GROUND_LAYER_MASK
              ))
            {
                _placedBuilding.SetPosition(_raycastHit.point);
                if(_lastPlacementPosition != _raycastHit.point)
                {
                    _placedBuilding.CheckValidPlacement();
                }
                _lastPlacementPosition = _raycastHit.point;
            }
            if(_placedBuilding.HasValidPlacement && Input.GetMouseButtonDown(0))
            {
                _PlaceBuilding();
            }
        }
        
        
    }
    void _PreparePlacedBuilding(int buildingDataIndex)
    {
        if (_placedBuilding != null && !_placedBuilding.IsFixed) 
            Destroy(_placedBuilding.Transform.gameObject);
        Building building = new Building(
            Globals.BUILDING_DATA[buildingDataIndex]
            );
        building.Transform.GetComponent<BuildingManager>().Initialize(building);
        _placedBuilding = building;
        _lastPlacementPosition = Vector3.zero;
    }
    void _PlaceBuilding()
    {
        _placedBuilding.Place();
        _PreparePlacedBuilding(_placedBuilding.DataIndex);
    }
    
    void _CancelPlacedBuilding()
    {
        Destroy(_placedBuilding.Transform.gameObject);
        _placedBuilding = null;
    }
}
