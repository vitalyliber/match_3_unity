﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tilePrefab;
    private BackgroundTile[,] allTiles;
    public GameObject[] dots;
    public GameObject[,] allDots;


    // Start is called before the first frame update
    void Start()
    {
        allTiles = new BackgroundTile[width, height];
        allDots = new GameObject[width, height];
        SetUp();
    }

    private void SetUp()
    {
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                var tempPosition = new Vector2(i, j);
                var backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity);
                backgroundTile.transform.SetParent(transform);
                backgroundTile.name = "( " + i + ", " + j + " )";
                var dotToUse = Random.Range(0, dots.Length);
                int maxIterations = 0;
                while (MatchesAt(i, j, dots[dotToUse]) && maxIterations < 100)
                {
                    dotToUse = Random.Range(0, dots.Length);
                    maxIterations++;
                }

                var dot = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
                dot.transform.SetParent(transform);
                dot.name = "( " + i + ", " + j + " )";
                allDots[i, j] = dot;
            }
        }
    }

    private bool MatchesAt(int column, int row, GameObject piece)
    {
        if (column > 1 && row > 1)
        {
            if (allDots[column - 1, row].CompareTag(piece.tag) || allDots[column - 2, row].CompareTag(piece.tag))
            {
                return true;
            }

            if (allDots[column, row - 1].CompareTag(piece.tag) || allDots[column, row - 2].CompareTag(piece.tag))
            {
                return true;
            }
        }
        else if (column <= 1 || row <= 1)
        {
            if (row > 1)
            {
                if (allDots[column, row - 1].CompareTag(piece.tag) && allDots[column, row - 2].CompareTag(piece.tag))
                {
                    return true;
                }
            }

            if (column > 1)
            {
                if (allDots[column - 1, row].CompareTag(piece.tag) && allDots[column - 2, row].CompareTag(piece.tag))
                {
                    return true;
                }
            }
        }

        return false;
    }
}