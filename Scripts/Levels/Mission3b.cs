using System.Collections;
using System.Collections.Generic;
using Jundroo.SimplePlanes.ModTools;
using UnityEngine;

public class Mission3b : AssaultLevelBase
{
    private static readonly string Name = "Mission 3b";
    private static readonly string LevelMap = "Simushir";
    private static readonly string LevelDesc =
        "Vehicle: Attacker\n" +
        "Location: Simushir\n" +
        "Weather: Few Clouds\n" +
        "Time: 0730\n\n" +
        "With the communications station captured, our naval forces can begin advancing towards the main base.\n\n" +
        "Remove all enemy naval resistance around the island. Keep an eye out for enemy long-range SAMs, and destroy them if possible.\n\n" +
        "TGT:\n" +
        "- 4x Missile Ship\n\n" +
        "Additional:\n" +
        "- 5x Long Range Missile Launcher";

    private static readonly string LevelGameObjectName = "Mission3b";

    public Mission3b()
        : base(Name, LevelMap, LevelDesc, LevelGameObjectName)
    {
    }

    protected override LevelStartLocation StartLocation
    {
        get
        {
            return new LevelStartLocation
            {
                InitialSpeed = 250f,
                InitialThrottle = 1f,
                Position = new Vector3(-24000f, 1500f, -11000f),
                Rotation = new Vector3(5f, 90f, 0f),
                StartOnGround = false
            };
        }
    }

    protected override string StartMessage
    {
        get
        {
            return "Sink all four missile ships around the island.";
        }
    }

    protected override float TimeOfDay
    {
        get
        {
            return 7.5f;
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
