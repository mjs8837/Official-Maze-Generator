using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCreation : MonoBehaviour
{
    List<GameObject> wallList;

    // Start is called before the first frame update
    void Start()
    {
        wallList = new List<GameObject>();
        MazeCreator(50.0f, 50.0f, 20, 20, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(wallList.Count);
    }

    void MazeCreator(float length, float width, int numRows, int numColumns, float wallHeight)
    {
        //Code for creating the floor of the maze
        GameObject mazeFloor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        mazeFloor.name = "Maze-Floor";
        mazeFloor.transform.position = new Vector3(length / 2.0f, 0.0f, width / 2.0f);
        mazeFloor.transform.localScale = new Vector3(length, 2.0f, width);

        //Creating the walls of the maze
        for (int i = 0; i <= numRows; i++)
        {
            for (int j = 0; j <= numColumns; j++)
            {
                GameObject xWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                xWall.name = "XWall";
                xWall.transform.position = new Vector3((i * 5) + 2.5f, 1.0f + (wallHeight / 2.0f), j * 5);
                xWall.transform.localScale = new Vector3(5.0f, wallHeight, 0.25f);
                wallList.Add(xWall);

                GameObject zWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                zWall.name = "ZWall";
                zWall.transform.position = new Vector3(i * 5, 1.0f + (wallHeight / 2.0f), (j * 5) + 2.5f);
                zWall.transform.localScale = new Vector3(0.25f, wallHeight, 5.0f);
                wallList.Add(zWall);
            }
        }

        for (int i = 0; i < 50; i++)
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
