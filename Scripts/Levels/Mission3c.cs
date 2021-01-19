using System.Collections;
using System.Collections.Generic;
using Jundroo.SimplePlanes.ModTools;
using UnityEngine;

public class Mission3c : AssaultLevelBase
{
    private static readonly string Name = "Mission 3c";
    private static readonly string LevelMap = "Simushir";
    private static readonly string LevelDesc =
        "Vehicle: Attacker\n" +
        "Location: Simushir\n" +
        "Weather: Scattered Clouds\n" +
        "Time: 1000\n\n" +
        "We now have an opportunity to destroy most of the remaining enemy naval forces.\n\n" +
        "You will be heavily outnumbered. Focus only on the most valuable targets.\n\n" +
        "TGT:\n" +
        "- 3x VLS Destroyer\n" +
        "- Helicopter Carrier\n\n" +
        "Additional:\n" +
        "- 12x Missile Launcher\n" +
        "- 3x CIWS Destroyer\n" +
        "- Flak Destroyer\n" +
        "- Destroyer\n" +
        "- Tower";

    private static readonly string LevelGameObjectName = "Mission3c";

    public Mission3c()
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
                Position = new Vector3(4000f, 2000f, 2000f),
                Rotation = new Vector3(10f, 20f, 0f),
                StartOnGround = false
            };
        }
    }

    protected override string StartMessage
    {
        get
        {
            return "Sink all target ships.";
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
