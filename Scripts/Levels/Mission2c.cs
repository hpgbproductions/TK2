using System.Collections;
using System.Collections.Generic;
using Jundroo.SimplePlanes.ModTools;
using UnityEngine;

public class Mission2c : AssaultLevelBase
{
    private static readonly string Name = "Mission 2c";
    private static readonly string LevelMap = "Iturup";
    private static readonly string LevelDesc =
        "Vehicle: Bomber/Attacker\n" +
        "Location: Iturup\n" +
        "Weather: Few Clouds\n" +
        "Time: 0200\n\n" +
        "Destroy the runway to ground the remaining enemy aircraft. As the enemy forces' leadership is stationed within the airport building, you will be tasked to destroy it as well.\n\n" +
        "You must stay in the narrow corridor between the towns. Just like before, fly at high altitudes to avoid detection.\n\n" +
        "TGT:\n" +
        "- Runway Target Point\n" +
        "- Enemy HQ\n\n" +
        "Additional:\n" +
        "- 7x Missile Launcher\n" +
        "- 6x Fighter";

    private static readonly string LevelGameObjectName = "Mission2c";

    public Mission2c()
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
                Position = new Vector3(8000f, 9000f, -7000f),
                Rotation = new Vector3(0f, 0f, 0f),
                StartOnGround = false
            };
        }
    }

    protected override string StartMessage
    {
        get
        {
            return "Hit the targets at the airport.";
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
