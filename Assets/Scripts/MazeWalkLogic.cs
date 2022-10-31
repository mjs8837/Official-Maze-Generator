using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeWalkLogic : MonoBehaviour
{
    [SerializeField] private GameObject infinadeck;
    [SerializeField] private GameObject cameraRig;
    [SerializeField] private GameObject maze;
    MazeCreation mazeCreationScript;
    private InfinadeckLocomotion locomotion;
    private float rotationScale = 5.0f;
    private Vector3 distance;

    private Vector3 directionToEnd;

    // Start is called before the first frame update
    void Start()
    {
        mazeCreationScript = maze.GetComponent<MazeCreation>();
        locomotion = infinadeck.GetComponent<InfinadeckCore>().locomotion.GetComponent<InfinadeckLocomotion>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDistance();
        RotationMath(rotationScale);
        directionToEnd = (mazeCreationScript.EndPointObject.transform.position - cameraRig.transform.position).normalized;
    }

    // MAKE THIS MODULAR (aka call from distance tracker)
    private float UpdateDistance()
    {
        // Getting the distance from the locomotion of the treadmill
        distance.x += Mathf.Abs(locomotion.xDistance);
        distance.z += Mathf.Abs(locomotion.yDistance);
        float totalDistance = distance.magnitude;

        return totalDistance;
    }

    // Helper function to rotate the maze based upon user movement speed
    private void RotationMath(float rotationFactor)
    {
        Vector3 angleChange = new Vector3(0.0f, 0.25f, 0.0f) * ((Mathf.Abs(locomotion.xDistance) + Mathf.Abs(locomotion.yDistance)) * rotationFactor);

        //Checking whether to rotate clockwise or counter-clockwise
        if (rotationFactor <= 0.0f)
        {
            maze.transform.RotateAround(cameraRig.transform.position, Vector3.up, -angleChange.magnitude);
        }
        else
        {
            maze.transform.RotateAround(cameraRig.transform.position, Vector3.up, angleChange.magnitude);
        }
    }
}
