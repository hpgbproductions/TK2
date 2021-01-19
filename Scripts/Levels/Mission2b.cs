using System.Collections;
using System.Collections.Generic;
using Jundroo.SimplePlanes.ModTools;
using UnityEngine;

public class Mission2b : AssaultLevelBase
{
    private static readonly string Name = "Mission 2b";
    private static readonly string LevelMap = "Iturup";
    private static readonly string LevelDesc =
        "Vehicle: Bomber/Attacker\n" +
        "Location: Iturup\n" +
        "Weather: Few Clouds\n" +
        "Time: 0200\n\n" +
        "Both airfields on the island are being controlled by the enemy, who are operating them as air bases. To rid the island of any air resistance, a simultaneous attack on both airfields will be conducted.\n\n" +
        "Bomb the runway. Make sure to hit all three target points, else the attack would be ineffective. If possible, stay at high altitudes to avoid detection.\n\n" +
        "TGT:\n" +
        "- 3x Runway Target Point\n\n" +
        "Additional:\n" +
        "- 8x Missile Launcher\n" +
        "- 5x Fighter\n" +
        "- 3x Bomber";

    private static readonly string LevelGameObjectName = "Mission2b";

    public Mission2b()
        : base(Name, LevelMap, LevelDesc, LevelGameObjectName)
    {
    }

    protected override LevelStartLocation StartLocation
    {
        get
        {
            return new LevelStartLocation
            {
                InitialSpeed = 200f,
                InitialThrottle = 1f,
                Position = new Vector3(-1000f, 9000f, -29000f),
                Rotation = new Vector3(0f, 335f, 0f),
                StartOnGround = false
            };
        }
    }

    protected override string StartMessage
    {
        get
        {
            return "Hit all three target points on the runway.";
        }
    }

    protected override float TimeOfDay
    {
        get
        {
            return 2f;
        }
    }

    protected override WeatherPreset Weather
    {
        get
        {
            return WeatherPreset.FewClouds;
        }
    }
}
