using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{
    private Grid grid;

    private void Start()
    {
        grid = new Grid(30,20, 2f, new Vector3(20,0));
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit _raycastHit;
            Ray _ray;
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(
                _ray,
                out _raycastHit,
                1000f,
                Globals.GROUND_LAYER_MASK
              ))
            {

                Vector2 GridPos = grid.GetXZ(_raycastHit.point);
               
                grid.SetValue((int)GridPos.x, (int)GridPos.y, 56);
            }


        }
    }
}
