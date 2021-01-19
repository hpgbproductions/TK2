using System.Collections;
using System.Collections.Generic;
using Jundroo.SimplePlanes.ModTools;
using UnityEngine;

public class Mission3a : AssaultLevelBase
{
    private static readonly string Name = "Mission 3a";
    private static readonly string LevelMap = "Simushir";
    private static readonly string LevelDesc =
        "Vehicle: Attacker\n" +
        "Location: Simushir\n" +
        "Weather: Clear\n" +
        "Time: 0630\n\n" +
        "Destroy anti-aircraft weaponry around the enemy-occupied communications station. It will prove to be a useful forward base for our supporting unit.\n\n" +
        "TGT:\n" +
        "- 7x Missile Launcher\n" +
        "- 3x Long Range Missile Launcher\n\n" +
        "Additional:\n" +
        "- 3x CIWS Destroyer\n" +
        "- 3x Destroyer";

    private static readonly string LevelGameObjectName = "Mission3a";

    public Mission3a()
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
                Position = new Vector3(-29171f, 2000f, -39430f),
                Rotation = new Vector3(5f, 35f, 0f),
                StartOnGround = false
            };
        }
    }

    protected override string StartMessage
    {
        get
        {
            return "Destroy all SAMs around the antenna site.";
        }
    }

    protected override float TimeOfDay
    {
        get
        {
            return 6.5f;
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
