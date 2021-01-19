using System.Collections;
using System.Collections.Generic;
using Jundroo.SimplePlanes.ModTools;
using UnityEngine;

public class Mission1a : AssaultLevelBase
{
    private static readonly string Name = "Mission 1a";
    private static readonly string LevelMap = "Shikotan";
    private static readonly string LevelDesc =
        "Vehicle: Attacker\n" +
        "Location: Shikotan\n" +
        "Weather: Scattered Clouds\n" +
        "Time: 1000\n\n" +
        "An unknown group has suddenly invaded most of the Kuril Islands. You have been hired to assist in their removal.\n\n" +
        "Your first mission is to aid the landing operation on the central village.\n\n" +
        "TGT:\n" +
        "- Destroyer\n" +
        "- 6x AA Tank\n\n" +
        "Additional:\n" +
        "- 4x Missile Launcher";

    private static readonly string LevelGameObjectName = "Mission1a";

    public Mission1a()
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
                Position = new Vector3(-21419f, 2000f, -11412f),
                Rotation = new Vector3(0f, 50f, 0f),
                StartOnGround = false
            };
        }
    }

    protected override string StartMessage
    {
        get
        {
            return "Destroy all enemies at the central village.";
        }
    }

    protected override float TimeOfDay
    {
        get
        {
            return 10f;
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
