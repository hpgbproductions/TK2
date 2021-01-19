using System.Collections;
using System.Collections.Generic;
using Jundroo.SimplePlanes.ModTools;
using UnityEngine;

public class Mission4b : AssaultLevelBase
{
    private static readonly string Name = "Mission 4b";
    private static readonly string LevelMap = "Onekotan";
    private static readonly string LevelDesc =
        "Vehicle: Attacker\n" +
        "Location: Onekotan\n" +
        "Weather: Broken Clouds\n" +
        "Time: 1730\n\n" +
        "Due to the threat of an ICBM launch, you are granted permission to destroy the missile silos on the island.\n\n" +
        "Both short- and long-range AA defenses are present, be ready to respond to them at all times.\n\n" +
        "TGT:\n" +
        "- 5x Missile Silo\n\n" +
        "Additional:\n" +
        "- 24x Missile Launcher\n" +
        "- 7x Long Range Missile Launcher\n" +
        "- Destroyer\n" +
        "- Control Room\n" +
        "- Core";

    private static readonly string LevelGameObjectName = "Mission4b";

    public Mission4b()
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
                Position = new Vector3(13371f, 2000f, -12930f),
                Rotation = new Vector3(5f, 305f, 0f),
                StartOnGround = false
            };
        }
    }

    protected override string StartMessage
    {
        get
        {
            return "Destroy all five missile silos.";
        }
    }

    protected override float TimeOfDay
    {
        get
        {
            return 17.5f;
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
