using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DistanceTracker : MonoBehaviour
{
    [SerializeField] private GameObject infinadeck;
    private InfinadeckLocomotion locomotion;
    private float runTimer = 10.0f;
    private Vector3 distance;
    private float maxDistance = 10.0f;
    private float locomotionConstraint = 0.00001f;

    private bool isStarted = false; 
    [SerializeField] private bool useTime = false;

    // Start is called before the first frame update
    void Start()
    {
        locomotion = infinadeck.GetComponent<InfinadeckCore>().locomotion.GetComponent<InfinadeckLocomotion>();
        Destroy(GameObject.Find("InfinadeckReferenceObjects(Clone)"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(locomotion.xDistance) > locomotionConstraint || Mathf.Abs(locomotion.yDistance) > locomotionConstraint) { isStarted = true; }

        TimeOrDistance();

        HandleSceneChange();
    }

    // Helper function to calculate total distance traveled
    private float UpdateDistance()
    {
        // Getting the distance from the locomotion of the treadmill
        distance.x += Mathf.Abs(locomotion.xDistance);
        distance.z += Mathf.Abs(locomotion.yDistance);
        float totalDistance = distance.magnitude;

        return totalDistance;
    }

    private void HandleSceneChange()
    {
        if (isStarted)
        {
            // Checking if the user has traveled a certain distance
            if (!useTime)
            {
                if (UpdateDistance() >= maxDistance) // Fix magic number
                {
                    SceneTransition();
                }
            }
            else
            {
                if (runTimer > 0.0f)
                {
                    runTimer -= Time.deltaTime;
                }
                else
                {
                    SceneTransition();
                }
            }
        }
    }

    private void TimeOrDistance()
    {
        if (!isStarted)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                useTime = !useTime;
            }
        }
    }

    private void SceneTransition()
    {
        // Set up an indicator for the player to stop moving
        Debug.Log("Stop moving");

        // Handle logic to check if the user stopped moving and transport them to the maze
        if (Mathf.Abs(locomotion.xDistance) < locomotionConstraint && Mathf.Abs(locomotion.yDistance) < locomotionConstraint)
        {
            SceneManager.LoadScene("MazeProtocol");
        }
    }
}
