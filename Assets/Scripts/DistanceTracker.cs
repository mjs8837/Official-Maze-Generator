using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DistanceTracker : MonoBehaviour
{
    [SerializeField] private GameObject infinadeck;
    [SerializeField] private GameObject targetPrefab;

    private InfinadeckLocomotion locomotion;
    private float runTimer;
    private Vector3 distance;
    private float maxDistance = 10.0f;
    private float locomotionConstraint = 0.00001f;

    private bool isStarted = false; 

    List<Vector3> positions = new List<Vector3>();
    List<Vector3> targets = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        if (targetPrefab != null)
        {
            targets.Add(new Vector3(Random.Range(-100.0f, 100.0f), transform.position.y, Random.Range(-100.0f, 100.0f)));
            Instantiate(targetPrefab, targets[0], Quaternion.identity);
        }

        if (SceneManager.GetActiveScene().name == "PreMazeData" || SceneManager.GetActiveScene().name == "PostMazeData")
        {
            runTimer = 60.0f;
        }
        if (SceneManager.GetActiveScene().name == "MazeProtocol")
        {
            runTimer = 600.0f;
        }

        locomotion = infinadeck.GetComponent<InfinadeckCore>().locomotion.GetComponent<InfinadeckLocomotion>();
        Destroy(GameObject.Find("InfinadeckReferenceObjects(Clone)"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(locomotion.xDistance) > locomotionConstraint || Mathf.Abs(locomotion.yDistance) > locomotionConstraint) { isStarted = true; }

        HandleSceneChange();

        positions.Add(transform.position);

        // DEBUGGING PURPOSES

        /*if (distance.magnitude > 0.0f) { isStarted = true; }

        distance.x += 0.1f;
        distance.z += 0.1f;
        transform.position += new Vector3(0.1f, 0.0f, 0.1f);

        if (distance.magnitude > maxDistance)
        {
            transform.position = Vector3.zero;
            if (SceneManager.GetActiveScene().name == "PreMaze")
            {
                SceneManager.LoadScene("MazeProtocol");
            }
            if (SceneManager.GetActiveScene().name == "MazeProtocol")
            {
                SceneManager.LoadScene("PreMaze");
            }
        }*/
    }

    private void FixedUpdate()
    {
        
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

    /// <summary>
    /// Checking if the constraints are met to trigger a scene transition
    /// </summary>
    private void HandleSceneChange()
    {
        if (isStarted)
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

    /*private void TimeOrDistance()
    {
        if (!isStarted)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                useTime = !useTime;
            }
        }
    }*/

    /// <summary>
    /// Switching the scene the user is currently in based on movement speed.
    /// </summary>
    private void SceneTransition()
    {
        // Set up an indicator for the player to stop moving
        Debug.Log("Stop moving");

        // Handle logic to check if the user stopped moving and transport them to the maze or back to pre/post maze
        if (Mathf.Abs(locomotion.xDistance) < locomotionConstraint && Mathf.Abs(locomotion.yDistance) < locomotionConstraint)
        {
            if (SceneManager.GetActiveScene().name == "PreMazeData" || SceneManager.GetActiveScene().name == "PostMazeData")
            {
                for (int i = 0; i < positions.Count; i++)
                {
                    FileWriter.WritePositions("User Position", positions[i]);
                }
                for (int i = 0; i < targets.Count; i++)
                {
                    FileWriter.WritePositions("Target Position " + i + 1, targets[i]);
                }
            }

            if (SceneManager.GetActiveScene().name == "PreMazeData")
            {
                SceneManager.LoadScene("MazeProtocol");
            }
            if (SceneManager.GetActiveScene().name == "MazeProtocol")
            {
                SceneManager.LoadScene("PostMazeData");
            }
        }
    }
}
