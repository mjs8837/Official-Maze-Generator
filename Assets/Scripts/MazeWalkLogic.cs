using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeWalkLogic : MonoBehaviour
{
    [SerializeField] private GameObject infinadeck;
    [SerializeField] private GameObject cameraRig;
    [SerializeField] private Camera VRCam;
    [SerializeField] private GameObject maze;
    MazeCreation mazeCreationScript;
    private InfinadeckLocomotion locomotion;
    private float rotationScale = 2.5f;

    private Vector3 spawnPosition = new Vector3(4.5f, 0.0f, 4.5f);

    // Start is called before the first frame update
    void Start()
    {
        mazeCreationScript = maze.GetComponent<MazeCreation>();
        locomotion = infinadeck.GetComponent<InfinadeckCore>().locomotion.GetComponent<InfinadeckLocomotion>();

        // Setting the initial position of the camera rig when the maze is created
        spawnPosition.y = cameraRig.transform.position.y;
        cameraRig.transform.position = spawnPosition;
        /*cameraRig.transform.RotateAround(new Vector3(VRCam.transform.position.x, 0.0f, VRCam.transform.position.z), Vector3.up, 20.0f);*/

        //cameraRig.transform.localEulerAngles = new Vector3(0.0f, 50.0f, 0.0f);
        VRCam.transform.eulerAngles = new Vector3(0.0f, 50.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //VRCam.transform.eulerAngles += new Vector3(0.0f, 90.0f, 0.0f);
        //UpdateDistance();
        //RotationMath(rotationScale);

        //cameraRig.transform.RotateAround(new Vector3(VRCam.transform.position.x, 0.0f, VRCam.transform.position.z), Vector3.up, 20.0f * (Mathf.Abs(locomotion.xDistance) + Mathf.Abs(locomotion.yDistance)));
        //cameraRig.transform.RotateAround(new Vector3(VRCam.transform.position.x, 0.0f, VRCam.transform.position.z), Vector3.up, 20.0f * (0.1f + 0.1f));


        //VRCam.transform.eulerAngles = new Vector3(VRCam.transform.eulerAngles.x, VRCam.transform.eulerAngles.y + 90.0f, VRCam.transform.eulerAngles.z);
        //maze.transform.eulerAngles = new Vector3(0.0f, VRCam.transform.eulerAngles.y + 20.0f, 0.0f);
    }

    /*    // MAKE THIS MODULAR (aka call from distance tracker)
        private float UpdateDistance()
        {
            // Getting the distance from the locomotion of the treadmill
            distance.x += Mathf.Abs(locomotion.xDistance);
            distance.z += Mathf.Abs(locomotion.yDistance);
            float totalDistance = distance.magnitude;

            return totalDistance;
        }*/

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