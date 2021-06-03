using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Grid
{
    private int width;
    private int height;
    private int[,] gridArray;
    private float cellSize;
    private TextMesh[,] debugTextArray;
    public Grid(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];

        for(int x=0; x<gridArray.GetLength(0); x++)
        {
            for(int z = 0; z < gridArray.GetLength(1); z++)
            {
                debugTextArray[x, z] = UtilsClass.CreateWorldText(gridArray[x,z].ToString(), null, GetWorldPosition(x,0,z) + new Vector3(cellSize, 0, cellSize) *.5f,30, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, 0, z), GetWorldPosition(x, 0, z + 1),Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, 0, z), GetWorldPosition(x+1, 0, z),Color.white, 100f);
            }

        }
        Debug.DrawLine(GetWorldPosition(0, 0, height), GetWorldPosition(width, 0, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0, 0), GetWorldPosition(width, 0, height), Color.white, 100f);

        SetValue(2, 1, 56);

    }
    private Vector3 GetWorldPosition(int x,int y, int z)
    {
        y = 0;
        return new Vector3(x, y, z) * cellSize;
    }
    private void GetXZ(Vector3 worldPosition, out int x, out int z)
    {
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        z = Mathf.FloorToInt(worldPosition.z / cellSize);
    }
    public void SetValue(int x, int z, int value)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {

            gridArray[x, z] = value;
            debugTextArray[x, z].text = gridArray[x,z].ToString();
        }
    }
    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        SetValue(x, z, value);
    }
}
