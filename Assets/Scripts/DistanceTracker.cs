using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DistanceTracker : MonoBehaviour
{
    [SerializeField] private GameObject infinadeck;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private GameObject head;

    private InfinadeckLocomotion locomotion;
    
    private float runTimerStart;
    private Vector3 distance;
    private Scene currentScene;

    [SerializeField] private float runTimer;
    [SerializeField] private float angleOffset;
    [SerializeField] private bool isStarted; 

    private List<Vector3> positions = new List<Vector3>();
    private List<Vector3> targetPositions = new List<Vector3>();
    private List<GameObject> targetObjs = new List<GameObject>();
    private int numWallCollisions = 0;

    private const float LOCOMOTION_CONSTRAINT = 0.00001f;

    // Start is called before the first frame update
    void Start()
    {
        // Setting the current scene when first loaded in
        currentScene = SceneManager.GetActiveScene();

        // If there is a target prefab set, spawn a target where the user is facing
        if (targetPrefab != null)
        {
            // Getting the camera attached to this object
            Camera camera = GetComponentInChildren<Camera>();

            Vector3 tempPos = 100.0f * new Vector3(
                camera.transform.forward.x, 
                transform.position.y,
                camera.transform.forward.z);

            targetObjs.Add(Instantiate(targetPrefab, tempPos, Quaternion.identity));

            StartCoroutine(HideTarget(targetObjs[0]));
        }

        head.transform.eulerAngles = new Vector3(0.0f, angleOffset, 0.0f);

        runTimerStart = runTimer;

        // Getting the infinadeck locomotion component and destroying the uneccessary objects in the scene
        locomotion = infinadeck.GetComponent<InfinadeckCore>().locomotion.GetComponent<InfinadeckLocomotion>();
        Destroy(GameObject.Find("InfinadeckReferenceObjects(Clone)"));
        Destroy(GameObject.Find("InfinadeckSplashscreen(Clone)"));

        StartCoroutine(StartProtocol());
    }

    private void FixedUpdate()
    {
        if (isStarted)
        {
            // Updating user positions and distance travled 
            positions.Add(transform.position);
            UpdateDistance();

            // Decrementing the run timer
            if (runTimer > 0.0f)
            {
                runTimer -= Time.fixedDeltaTime;
            }

            // Plotting out the ideal path position
            if (targetObjs.Count > 0)
            {
                targetPositions.Add(Vector3.Lerp(Vector3.zero, targetObjs[0].transform.position, (runTimerStart - runTimer) / runTimerStart));
            }
        }
    }

    /// <summary>
    /// Creating a coroutine to run at the start of the pre and post maze to set the protocol to start
    /// </summary>
    /// <returns>Waiting until movement is detected</returns>
    private IEnumerator StartProtocol()
    {
        // Waiting until there is any movement
        yield return new WaitUntil(() => 
            Mathf.Abs(locomotion.xDistance) > LOCOMOTION_CONSTRAINT ||
            Mathf.Abs(locomotion.yDistance) > LOCOMOTION_CONSTRAINT);

        // Starting the protocol
        isStarted = true;
        StartCoroutine(HandleSceneChange());
    }

    /// <summary>
    /// Creating a coroutine to run at the start of the pre and post maze to hide the target when appropriate
    /// </summary>
    /// <param name="target">The current target</param>
    /// <returns>Waits until the user has moved and delays another 2 seconds</returns>
    private IEnumerator HideTarget(GameObject target)
    {
        yield return new WaitUntil(() => isStarted);
        yield return new WaitForSeconds(2.0f);

        target.SetActive(false);
    }

    /// <summary>
    /// Helper function to update distance traveled
    /// </summary>
    private void UpdateDistance()
    {
        // Getting the distance from the locomotion of the treadmill
        distance.x += Mathf.Abs(locomotion.xDistance);
        distance.z += Mathf.Abs(locomotion.yDistance);
    }

    /// <summary>
    /// Checking if the constraints are met to trigger a scene transition
    /// </summary>
    private IEnumerator HandleSceneChange()
    {
        // Waiting until the run timer is below 0
        yield return new WaitUntil(() => runTimer <= 0.0f);

        // Transitioning to the next scene or quitting the application
        StartCoroutine(SceneTransition());
    }

    /// <summary>
    /// Switching the scene the user is currently in based on movement speed.
    /// </summary>
    private IEnumerator SceneTransition()
    {
        // Set up an indicator for the player to stop moving
        Debug.Log("Stop moving");

        // Wait until the user has stopped moving to progress further
        yield return new WaitUntil(() =>
            Mathf.Abs(locomotion.xDistance) < LOCOMOTION_CONSTRAINT &&
            Mathf.Abs(locomotion.yDistance) < LOCOMOTION_CONSTRAINT);

        // Printing out user positions
        for (int i = 0; i < positions.Count; i++)
        {
            FileWriter.WritePositions(currentScene.name, "User Position", positions[i], i * Time.fixedDeltaTime);
        }

        if (targetObjs.Count > 0)
        {
            // Printing out the ideal path positions based on where the target spawns
            for (int i = 0; i < targetPositions.Count; i++)
            {
                FileWriter.WritePositions(currentScene.name, "Ideal Position", targetPositions[i], i * Time.fixedDeltaTime);
            }
        }

        // Plotting out extra useful information
        FileWriter.WriteValue(currentScene.name, "Total Distance Traveled (in meters)", distance.magnitude);
        FileWriter.WriteValue(currentScene.name, "Total Time (in seconds)", runTimerStart);

        // Loading the next scene in the order of the current build indices if there is a next one
        // Otherwise quits the program
        if (currentScene.buildIndex != 3)
        {
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }
        else
        {
            Application.Quit();
        }
    }
}
