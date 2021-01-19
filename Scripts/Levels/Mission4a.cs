using System.Collections;
using System.Collections.Generic;
using Jundroo.SimplePlanes.ModTools;
using UnityEngine;

public class Mission4a : AssaultLevelBase
{
    private static readonly string Name = "Mission 4a";
    private static readonly string LevelMap = "Onekotan";
    private static readonly string LevelDesc =
        "Vehicle: Attacker\n" +
        "Location: Onekotan\n" +
        "Weather: Broken Clouds\n" +
        "Time: 1500\n\n" +
        "The enemy leaders are stationed in a captured nuclear silo. Capturing them will end the war once and for all.\n\n" +
        "Again, the enemies have the numbers. Focus on the anti-ship missile launchers and laser defense ships, and allow our naval force to take on the rest.\n\n" +
        "TGT:\n" +
        "- 3x Laser Defense Ship\n" +
        "- 6x Anti-Ship Missile Launcher\n\n" +
        "Additional:\n" +
        "- 6x Destroyer\n" +
        "- 3x Flak Destroyer\n" +
        "- 4x Long Range Missile Launcher";

    private static readonly string LevelGameObjectName = "Mission4a";

    public Mission4a()
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
                Position = new Vector3(7371f, 1500f, -37000f),
                Rotation = new Vector3(0f, 0f, 0f),
                StartOnGround = false
            };
        }
    }

    protected override string StartMessage
    {
        get
        {
            return "Destroy critical enemy defenses ahead.";
        }
    }

    protected override float TimeOfDay
    {
        get
        {
            return 15f;
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
