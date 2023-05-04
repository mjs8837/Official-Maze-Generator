using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class FileWriter
{
    static string path;
    static StreamWriter fileWriter = null;

    /// <summary>
    /// Static function to write vector data to a file
    /// </summary>
    /// <param name="currentPoint">Vector3 to be written into the file</param>
    public static void WritePositions(string fileTitle, string lineTitle, Vector3 point)
    {
        path = "Data/PositionData-" + fileTitle + System.DateTime.Now.Hour + "-" + System.DateTime.Now.Minute + ".txt";

        try
        {
            fileWriter = new StreamWriter(path, true);
            fileWriter.WriteLine(lineTitle + ": " + point.x + ", " + point.z);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }

        if (fileWriter != null )
        {
            fileWriter.Close();
        }
    }
}
