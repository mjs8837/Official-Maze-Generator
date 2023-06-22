using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Drawing;

public static class FileWriter
{
    static string path;
    static StreamWriter fileWriter = null;

    /// <summary>
    /// Static function to write a vector to a file
    /// </summary>
    /// <param name="fileTitle">Name of the file to write to</param>
    /// <param name="lineTitle">Title of the line currently being written to. This is what appears in the file before the value</param>
    /// <param name="point">The vector being written to the file</param>
    /// <param name="time">The time this value was recorded at</param>
    public static void WritePositions(string fileTitle, string lineTitle, Vector3 point, float time)
    {
        path = "Data/PositionData-" + fileTitle + System.DateTime.Now.Hour + "-" + System.DateTime.Now.Minute + ".txt";

        try
        {
            fileWriter = new StreamWriter(path, true);
            fileWriter.WriteLine(lineTitle + ": " + point.x + ", " + point.z + ", Time: " + time);
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

    /// <summary>
    /// Static function to write a float value to a file
    /// </summary>
    /// <param name="fileTitle">Name of the file to write to</param>
    /// <param name="lineTitle">Title of the line currently being written to. This is what appears in the file before the value</param>
    /// <param name="value">The float being written into the file</param>
    public static void WriteValue(string fileTitle, string lineTitle, float value)
    {
        path = "Data/PositionData-" + fileTitle + System.DateTime.Now.Hour + "-" + System.DateTime.Now.Minute + ".txt";

        try
        {
            fileWriter = new StreamWriter(path, true);
            fileWriter.WriteLine(lineTitle + ": " + value);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }

        if (fileWriter != null)
        {
            fileWriter.Close();
        }
    }
}
