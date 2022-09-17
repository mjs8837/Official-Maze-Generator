using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ButtonBehavior : MonoBehaviour
{
    MeshRenderer mersh;

    // Start is called before the first frame update
    void Start()
    {
        mersh = gameObject.GetComponent<MeshRenderer>();
        mersh.material.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        mersh.material.color = Color.grey;
        gameObject.transform.localScale = gameObject.transform.localScale * 1.1f;
    }

    private void OnMouseExit()
    {
        mersh.material.color = Color.black;
        gameObject.transform.localScale = gameObject.transform.localScale * 0.9f;
    }

    private void OnMouseDown()
    {
        if (gameObject.name == "Start Button")
        {
            SceneManager.LoadScene("PreMaze");
        }

        if (gameObject.name == "Exit Button")
        {
            EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}
