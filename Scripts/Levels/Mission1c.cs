using System.Collections;
using System.Collections.Generic;
using Jundroo.SimplePlanes.ModTools;
using UnityEngine;

public class Mission1c : AssaultLevelBase
{
    private static readonly string Name = "Mission 1c";
    private static readonly string LevelMap = "Shikotan";
    private static readonly string LevelDesc =
        "Vehicle: Attacker\n" +
        "Location: Shikotan\n" +
        "Weather: Broken Clouds\n" +
        "Time: 1100\n\n" +
        "Destroy the enemy reinforcements to ensure success in capturing the island.\n\n" +
        "TGT:\n" +
        "- 4x VLS Destroyer\n\n" +
        "Additional:\n" +
        "- 2x CIWS Destroyer\n" +
        "- 2x Flak Destroyer";

    private static readonly string LevelGameObjectName = "Mission1c";

    public Mission1c()
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
                Position = new Vector3(9984f, 2000f, 17630f),
                Rotation = new Vector3(10f, 60f, 0f),
                StartOnGround = false
            };
        }
    }

    protected override string StartMessage
    {
        get
        {
            return "Engage the ships straight ahead.";
        }
    }

    protected override float TimeOfDay
    {
        get
        {
            return 11f;
        }
    }

    protected override WeatherPreset Weather
    {
        get
        {
            return WeatherPreset.BrokenClouds;
        }
    }
}
