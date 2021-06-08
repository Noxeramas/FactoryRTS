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
    private Vector3 originPosition;
    private TextMesh[,] debugTextArray;
    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];

        for(int x=0; x<gridArray.GetLength(0); x++)
        {
            for(int z = 0; z < gridArray.GetLength(1); z++)
            {
                
            }

        }
        

     
        


    }
    private Vector3 GetWorldPosition(int x,int y, int z)
    {
        y = 3;
        return new Vector3(x, y, z) * cellSize + originPosition;
    }
    public Vector2 GetXZ(Vector3 worldPosition)
    {
        return new Vector2(Mathf.FloorToInt((worldPosition - originPosition).x / cellSize), Mathf.FloorToInt((worldPosition- originPosition).z / cellSize));
        

    }
    public void SetValue(int x, int z, int value)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {

            gridArray[x, z] = value;
            //debugTextArray[x, z].text = gridArray[x,z].ToString();
        }
    }   
}
