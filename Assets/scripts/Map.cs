using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private Boolean[,] _grids;

    void Start()
    {
//        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        InitGrid1();
    }

    void InitGrid1()
    {
        _grids = new Boolean[13, 15];
        
        // set true for all boundaries
        for (int row = 0; row < _grids.GetLength(0); row++)
        {
            _grids[row, 0] = true;
            _grids[row, _grids.GetLength(1) - 1] = true;
        }
        
        for (int col = 0; col < _grids.GetLength(1); col++)
        {
            _grids[0, col] = true;
            _grids[_grids.GetLength(0) - 1, col] = true;
        }

        //set middle to true
        _grids[1, 2] = true;
        _grids[1, 6] = true;
        _grids[1, 11] = true;
        _grids[2, 4] = true;
        _grids[2, 8] = true;
        _grids[2, 9] = true;
        _grids[2, 10] = true;
        _grids[2, 11] = true;
        _grids[2, 13] = true;
        _grids[3, 1] = true;
        _grids[3, 2] = true;
        _grids[3, 4] = true;
        _grids[3, 6] = true;
        _grids[4, 4] = true;
        _grids[4, 5] = true;
        _grids[4, 6] = true;
        _grids[4, 7] = true;
        _grids[4, 9] = true;
        _grids[4, 11] = true;
        _grids[4, 12] = true;
        _grids[4, 13] = true;
        _grids[5, 2] = true;
        _grids[5, 3] = true;
        _grids[5, 4] = true;
        _grids[5, 10] = true;
        _grids[6, 10] = true;
        _grids[6, 12] = true;
        _grids[6, 13] = true;
        _grids[7, 2] = true;
        _grids[7, 4] = true;
        _grids[8, 1] = true;
        _grids[8, 2] = true;
        _grids[8, 4] = true;
        _grids[8, 5] = true;
        _grids[8, 6] = true;
        _grids[8, 8] = true;
        _grids[8, 9] = true;
        _grids[8, 10] = true;
        _grids[8, 11] = true;
        _grids[8, 13] = true;
        _grids[9, 9] = true;
        _grids[10, 1] = true;
        _grids[10, 2] = true;
        _grids[10, 3] = true;
        _grids[10, 5] = true;
        _grids[10, 6] = true;
        _grids[10, 7] = true;
        _grids[10, 9] = true;
        _grids[10, 11] = true;
        _grids[10, 12] = true;
        _grids[11, 5] = true;

        for (int row = 0; row < _grids.GetLength(0); row++)
        {
            for (int col = 0; col < _grids.GetLength(1); col++)
            {
                if (_grids[row, col])
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(row, 0, col);
                    cube.AddComponent<BoxCollider>();
                }
            }
        }
    }
}
