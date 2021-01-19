using System.Collections;
using System.Collections.Generic;
using Jundroo.SimplePlanes.ModTools;
using UnityEngine;

public class Mission2a : AssaultLevelBase
{
    private static readonly string Name = "Mission 2a";
    private static readonly string LevelMap = "Iturup";
    private static readonly string LevelDesc =
        "Vehicle: Interceptor\n" +
        "Location: Iturup\n" +
        "Weather: Clear\n" +
        "Time: 0700\n\n" +
        "Long range anti-ship missile launchers have been spotted on Iturup, the next island to capture.\n\n" +
        "Destroy the launchers quickly.\n\n" +
        "TGT:\n" +
        "- 8x Anti-Ship Missile\n\n" +
        "Additional:\n" +
        "- 8x Missile Launcher\n" +
        "- Missile Ship";

    private static readonly string LevelGameObjectName = "Mission2a";

    public Mission2a()
        : base(Name, LevelMap, LevelDesc, LevelGameObjectName)
    {
    }

    protected override LevelStartLocation StartLocation
    {
        get
        {
            return new LevelStartLocation
            {
                InitialSpeed = 300f,
                InitialThrottle = 1f,
                Position = new Vector3(-42609f, 1000f, -47401f),
                Rotation = new Vector3(0f, 10f, 0f),
                StartOnGround = false
            };
        }
    }

    protected override string StartMessage
    {
        get
        {
            return "Destroy all anti-ship missiles.";
        }
    }

    protected override string SpecialFailMessage
    {
        get
        {
            return "An anti-ship missile left the operation area.";
        }
    }

    protected override float TimeOfDay
    {
        get
        {
            return 7f;
        }
    }

    protected override WeatherPreset Weather
    {
        get
        {
            return WeatherPreset.Clear;
        }
    }
}
