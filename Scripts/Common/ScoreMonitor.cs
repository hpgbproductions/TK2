using System.Collections;
using System.Collections.Generic;
using Assets.SimplePlanesReflection.Assets.Scripts.Levels.Enemies;
using Jundroo.SimplePlanes.ModTools;
using Jundroo.SimplePlanes.ModTools.PrefabProxies;
using UnityEngine;

public class ScoreMonitor : MonoBehaviour
{
    [SerializeField]
    private RotatingWeaponProxy[] TgtTurrets;

    [SerializeField]
    private ShipProxy[] TgtShips;

    [SerializeField]
    private AntiAircraftTankProxy[] TgtTanks;

    [SerializeField]
    private bool TgtConvoyEnabled = false;    // Whether convoy vehicles award points when destroyed

    [SerializeField]
    private List<SimpleGroundVehicleScript> TgtConvoyVehicles;

    [SerializeField]
    private int DefaultTurretValue = 150;

    [SerializeField]
    private int DefaultShipValue = 400;

    [SerializeField]
    private int DefaultTankValue = 150;

    [SerializeField]
    private int DefaultConvoyVehicleValue = 100;

    [SerializeField]
    private int MaxBonus = 5000;

    [SerializeField]
    private int PerSecondBonus = 30;

    [SerializeField]
    private GUISkin Skin;

    private EnemyTargetMonitor etm;

    private bool HasTurrets;
    private bool HasShips;
    private bool HasTanks;
    private bool HasConvoyVehicles;

    private bool[] CustomValueTurrets;
    private bool[] CustomValueShips;
    private bool[] CustomValueTanks;

    private int DestroyedTgt = 0;

    public int DestScore = 0;
    public int TimeScore = 0;

    private GUIStyle ScoreStyle;
    private GUIStyle BonusStyle;
    private GUIStyle BgStyle;

    private void Start()
    {
        TimeScore = MaxBonus;

        HasTurrets = TgtTurrets.Length > 0;
        HasShips = TgtShips.Length > 0;
        HasTanks = TgtTanks.Length > 0;
        HasConvoyVehicles = TgtConvoyVehicles != null;

        if (HasTurrets)
        {
            CustomValueTurrets = new bool[TgtTurrets.Length];

            for (int i = 0; i < TgtTurrets.Length; i++)
            {
                CustomValueTarget cvt = TgtTurrets[i].gameObject.GetComponent<CustomValueTarget>();

                if (cvt != null)
                {
                    CustomValueTurrets[i] = true;
                }
            }
        }

        if (HasShips)
        {
            CustomValueShips = new bool[TgtShips.Length];

            for (int i = 0; i < TgtShips.Length; i++)
            {
                CustomValueTarget cvt = TgtShips[i].gameObject.GetComponent<CustomValueTarget>();

                if (cvt != null)
                {
                    CustomValueShips[i] = true;
                }
            }
        }

        if (HasTanks)
        {
            CustomValueTanks = new bool[TgtTanks.Length];

            for (int i = 0; i < TgtTanks.Length; i++)
            {
                CustomValueTarget cvt = TgtTanks[i].gameObject.GetComponent<CustomValueTarget>();

                if (cvt != null)
                {
                    CustomValueTanks[i] = true;
                }
            }
        }

        etm = gameObject.GetComponent<EnemyTargetMonitor>();

        BgStyle = Skin.box;
        ScoreStyle = Skin.label;
        BonusStyle = Skin.customStyles[0];
    }

    private void FixedUpdate()
    {
        // Only update values if the mission has not been accomplished
        if (etm.AllTgtDestroyed)
        {
            return;
        }

        int CurrentTgt = 0;

        if (HasTurrets)
        {
            for (int i = 0; i < TgtTurrets.Length; i++)
            {
                if (TgtTurrets[i].IsDisabled)
                {
                    CurrentTgt++;
                }
            }
        }

        if (HasShips)
        {
            for (int i = 0; i < TgtShips.Length; i++)
            {
                if (TgtShips[i].IsCriticallyDamaged)
                {
                    CurrentTgt++;
                }
            }
        }

        if (HasTanks)
        {
            for (int i = 0; i < TgtTanks.Length; i++)
            {
                if (TgtTanks[i].IsDead)
                {
                    CurrentTgt++;
                }
            }
        }

        if (HasConvoyVehicles && TgtConvoyEnabled)
        {
            for (int i = 0; i < TgtConvoyVehicles.Count; i++)
            {
                if (TgtConvoyVehicles[i].IsDestroyed)
                {
                    CurrentTgt++;
                }
            }
        }

        // Only recalculate the score if the number of destroyed enemies has changed
        if (DestroyedTgt != CurrentTgt)
        {
            int CurrentScore = 0;

            if (HasTurrets)
            {
                for (int i = 0; i < TgtTurrets.Length; i++)
                {
                    if (TgtTurrets[i].IsDisabled)
                    {
                        if (CustomValueTurrets[i])
                        {
                            CurrentScore += TgtTurrets[i].gameObject.GetComponent<CustomValueTarget>().Value;
                        }
                        else
                        {
                            CurrentScore += DefaultTurretValue;
                        }
                    }
                }
            }

            if (HasShips)
            {
                for (int i = 0; i < TgtShips.Length; i++)
                {
                    if (TgtShips[i].IsCriticallyDamaged)
                    {
                        if (CustomValueShips[i])
                        {
                            CurrentScore += TgtShips[i].gameObject.GetComponent<CustomValueTarget>().Value;
                        }
                        else
                        {
                            CurrentScore += DefaultShipValue;
                        }
                    }
                }
            }

            if (HasTanks)
            {
                for (int i = 0; i < TgtTanks.Length; i++)
                {
                    if (TgtTanks[i].IsDead)
                    {
                        if (CustomValueTanks[i])
                        {
                            CurrentScore += TgtTanks[i].gameObject.GetComponent<CustomValueTarget>().Value;
                        }
                        else
                        {
                            CurrentScore += DefaultTankValue;
                        }
                    }
                }
            }

            if (HasConvoyVehicles && TgtConvoyEnabled)
            {
                for (int i = 0; i < TgtConvoyVehicles.Count; i++)
                {
                    if (TgtConvoyVehicles[i].IsDestroyed)
                    {
                        CurrentScore += DefaultConvoyVehicleValue;
                    }
                }
            }

            DestroyedTgt = CurrentTgt;
            DestScore = CurrentScore;
        }

        // Calculate time bonus
        TimeScore = Mathf.Max(MaxBonus - Mathf.RoundToInt(Time.timeSinceLevelLoad * PerSecondBonus), 0);
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(Screen.width - 250, 0, 250, 100), "", BgStyle);

        GUI.Label(new Rect(Screen.width - 130, 15, 110, 50), string.Format("{0}", DestScore), ScoreStyle);

        GUI.Label(new Rect(Screen.width - 130, 60, 110, 30), string.Format("BONUS: {0}", TimeScore), BonusStyle);
    }
}
