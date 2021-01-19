/*
 *  Goes on persistent object.
 *  Holds variables and handles I/O
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StatHolder : MonoBehaviour
{
    private string SaveFileLocation;
    private string SaveFilePath;
    private const string SaveFileName = "TEKI2.DAT";

    private const int LevelCount = 12;
    private string[] LevelNames = new string[] { "1a", "1b", "1c", "2a", "2b", "2c", "2d", "3a", "3b", "3c", "4a", "4b" };

    // Stats that are saved and loaded
    public int[] TotalRecords;
    public int[] DestRecords;
    public float[] TimeRecords;
    public int[] WinCount;
    public int[] AttemptCount;

    // Stats that are calculated
    public float[] WinRate;

    private void Awake()
    {
        ServiceProvider.Instance.DevConsole.RegisterCommand("TK2Check", TK2Check);
        ServiceProvider.Instance.DevConsole.RegisterCommand("TK2Save", TK2Save);
    }

    private void Start()
    {
        SaveFileLocation = Application.persistentDataPath + "/NACHSAVE/";
        SaveFilePath = SaveFileLocation + SaveFileName;

        // Initialize arrays
        TotalRecords = new int[LevelCount];
        DestRecords = new int[LevelCount];
        TimeRecords = new float[LevelCount];
        WinCount = new int[LevelCount];
        AttemptCount = new int[LevelCount];
        WinRate = new float[LevelCount];

        // Create directory (does nothing if it already exists)
        Directory.CreateDirectory(SaveFileLocation);

        // Check for save file
        if (File.Exists(SaveFilePath))
        {
            Debug.Log("TK2 save file detected. Loading...");

            // Load the save file
            using (BinaryReader reader = new BinaryReader(File.Open(SaveFilePath, FileMode.Open)))
            {
                reader.ReadString();

                for (int i = 0; i < LevelCount; i++)
                {
                    TotalRecords[i] = Convert.ToInt32(reader.ReadUInt16());
                    DestRecords[i] = Convert.ToInt32(reader.ReadUInt16());
                    TimeRecords[i] = reader.ReadSingle();
                    WinCount[i] = Convert.ToInt32(reader.ReadUInt16());
                    AttemptCount[i] = Convert.ToInt32(reader.ReadUInt16());
                }
            }

            Debug.Log("TK2 data load successful.");
        }
        else
        {
            Debug.LogWarning("No TK2 save file detected. Initialized without save data.");

            // Set default time of 99' 59" 99
            for (int i = 0; i < LevelCount; i++)
            {
                TimeRecords[i] = 5999.99f;
            }
        }

        // Set win rate
        for (int i = 0; i < LevelCount; i++)
        {
            UpdateWinRate(i);
        }
    }

    private void OnApplicationQuit()
    {
        TK2Save();
    }

    // Automatically calculates the win rate of a level using its data
    public void UpdateWinRate(int level_id)
    {
        if (AttemptCount[level_id] == 0)
        {
            WinRate[level_id] = 0f;
        }
        else
        {
            WinRate[level_id] = (float)WinCount[level_id] / (float)AttemptCount[level_id];
        }
    }

    // Save values
    // Called automatically on application quit, or manually with command
    private void TK2Save()
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(SaveFilePath, FileMode.Create)))
        {
            writer.Write("NACHSAVETEKI2");

            for (int i = 0; i < LevelCount; i++)
            {
                writer.Write(Convert.ToUInt16(TotalRecords[i]));
                writer.Write(Convert.ToUInt16(DestRecords[i]));
                writer.Write(TimeRecords[i]);
                writer.Write(Convert.ToUInt16(WinCount[i]));
                writer.Write(Convert.ToUInt16(AttemptCount[i]));
            }
        }

        Debug.Log("TK2 data saved.");
    }

    // Command to check variables
    private void TK2Check()
    {
        int SumTotal = 0;
        int SumDest = 0;

        string DisplayTime;

        string DebugText = string.Format("TK2 Mission Performance Statistics ({0}):\n", DateTime.Now);

        for (int i = 0; i < LevelCount; i++)
        {
            DisplayTime = string.Format("{0:D2}' {1:D2}\" {2:D2}", Mathf.FloorToInt(TimeRecords[i] / 60f), Mathf.FloorToInt(Mathf.Repeat(TimeRecords[i], 60f)), Mathf.FloorToInt(Mathf.Repeat(TimeRecords[i], 1f) * 100));

            DebugText += string.Format("\n[ Mission {0} ] Best Combined: {1} | Most Destruction: {2} | Fastest Clear: {3} | History: {4}/{5} ({6:P2})", LevelNames[i], TotalRecords[i], DestRecords[i], DisplayTime, WinCount[i], AttemptCount[i], WinRate[i]);

            SumTotal += TotalRecords[i];
            SumDest += DestRecords[i];
        }

        DebugText += string.Format("\n\n[All Missions] Total Combined: {0} | Total Destruction: {1}", SumTotal, SumDest);

        Debug.Log(DebugText);
    }
}
