using System.Collections;
using System.Collections.Generic;
using Jundroo.SimplePlanes.ModTools;
using UnityEngine;

public class Mission1b : AssaultLevelBase
{
    private static readonly string Name = "Mission 1b";
    private static readonly string LevelMap = "Shikotan";
    private static readonly string LevelDesc =
        "Vehicle: Attacker\n" +
        "Location: Shikotan\n" +
        "Weather: Broken Clouds\n" +
        "Time: 1030\n\n" +
        "The enemy has a significant amount of firepower at the northeast village.\n\n" +
        "Destroy enemy ships and anti-ship weapons to allow our advance. You may take out enemy armored vehicles along the way.\n\n" +
        "TGT:\n" +
        "- 2x Destroyer\n" +
        "- 6x Anti-Ship Missile Launcher\n\n" +
        "Additional:\n" +
        "- 4x Missile Launcher\n" +
        "- 4x AA Tank";

    private static readonly string LevelGameObjectName = "Mission1b";

    public Mission1b()
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
                Position = new Vector3(-619f, 600f, 2391f),
                Rotation = new Vector3(0f, 50f, 0f),
                StartOnGround = false
            };
        }
    }

    protected override string StartMessage
    {
        get
        {
            return "Destroy all enemy ships and defenses at the bay.";
        }
    }

    protected override float TimeOfDay
    {
        get
        {
            return 10.5f;
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
