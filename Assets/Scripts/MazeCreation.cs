using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCreation : MonoBehaviour
{
    List<GameObject> wallList;
    public float mazeLength = 50.0f;
    public float mazeHeight = 50.0f;
    public int mazeRows = 10;
    public int mazeColumns = 10;
    public float wallHeight = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        wallList = new List<GameObject>();
        MazeCreator(mazeLength, mazeHeight, mazeRows, mazeColumns, wallHeight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MazeCreator(float length, float width, int numRows, int numColumns, float wallHeight)
    {
        Vector3 xWallPos;
        Vector3 xWallScale;

        Vector3 zWallPos;
        Vector3 zWallScale;

        //Code to add the floor
        Vector3 floorPos = new Vector3(length / 2.0f, 0.0f, width / 2.0f);
        Vector3 floorScale = new Vector3(length, 2.0f, width);
        WallCreator("Maze-Floor", floorPos, floorScale);
        wallList.RemoveAt(0);

        //Creating the walls of the maze
        for (int i = 0; i <= numRows; i++)
        {
            for (int j = 0; j <= numColumns; j++)
            {
                xWallPos = new Vector3((i * 5) + 2.5f, 1.0f + (wallHeight / 2.0f), j * 5);
                xWallScale = new Vector3(5.0f, wallHeight, 0.25f);

                zWallPos = new Vector3(i * 5, 1.0f + (wallHeight / 2.0f), (j * 5) + 2.5f);
                zWallScale = new Vector3(0.25f, wallHeight, 5.0f);

                WallCreator("XWall", xWallPos, xWallScale);
                WallCreator("ZWall", zWallPos, zWallScale);
            }
        }

        WallDestroyer(125);
    }

    //Creating a helper function to make the walls of the maze
    void WallCreator(string name, Vector3 position, Vector3 scale)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.name = name;
        wall.transform.position = position;
        wall.transform.localScale = scale;
        wallList.Add(wall);
    }

    //Creating a helper function to destroy walls of the maze
    void WallDestroyer(int numWallsToDestroy)
    {
        for (int i = 0; i < numWallsToDestroy; i++)
        {
            int randomWallToDestroy;

            if (Random.Range(0.0f, 1.0f) > .75f)
            {
                randomWallToDestroy = Random.Range(0, 50);
            }
            else
            {
                randomWallToDestroy = Random.Range(0, wallList.Count - 1);
            }

            Destroy(wallList[randomWallToDestroy]);
            wallList.RemoveAt(randomWallToDestroy);
        }
    }
}
