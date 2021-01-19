using System.Collections;
using System.Collections.Generic;
using Jundroo.SimplePlanes.ModTools;
using UnityEngine;

public class Mission2d : AssaultLevelBase
{
    private static readonly string Name = "Mission 2d";
    private static readonly string LevelMap = "Iturup";
    private static readonly string LevelDesc =
        "Vehicle: Attacker\n" +
        "Location: Iturup\n" +
        "Weather: Scattered Clouds\n" +
        "Time: 0900\n\n" +
        "Allow the capture force to proceed by destroying the enemy's anti-ship weapons.\n\n" +
        "The capture of a regional enemy headquarters will grant us a significant strategic advantage.\n\n" +
        "TGT:\n" +
        "- 6x Anti-Ship Missile Launcher\n\n" +
        "Additional:\n" +
        "- 3x Destroyer\n" +
        "- 3x Missile Launcher\n" +
        "- 2x Missile Ship\n" +
        "- 7x AA Tank";

    private static readonly string LevelGameObjectName = "Mission2d";

    public Mission2d()
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
                Position = new Vector3(-16642f, 1000f, 1911f),
                Rotation = new Vector3(0f, 45f, 0f),
                StartOnGround = false
            };
        }
    }

    protected override string StartMessage
    {
        get
        {
            return "Destroy all enemy anti-ship missile defenses.";
        }
    }

    protected override float TimeOfDay
    {
        get
        {
            return 9f;
        }
    }

    protected override WeatherPreset Weather
    {
        get
        {
            return WeatherPreset.ScatteredClouds;
        }
    }
}
