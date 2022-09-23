using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DistanceTracker : MonoBehaviour
{
    [SerializeField] GameObject infinadeck;
    float runTimer;

    // Start is called before the first frame update
    void Start()
    {
        runTimer = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (runTimer > 0.0f)
        {
            runTimer -= Time.deltaTime;
        }

        if (runTimer <= 0.0f)
        {
            SceneManager.LoadScene("MazeProtocol");
        }
        //Debug.Log(infinadeck.GetComponent<InfinadeckCore>().locomotion.GetComponent<InfinadeckLocomotion>().xDistance);
    }
}
