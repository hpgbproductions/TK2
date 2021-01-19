using System.Collections;
using System.Collections.Generic;
using Assets.SimplePlanesReflection.Assets.Scripts.Levels.Enemies;
using Jundroo.SimplePlanes.ModTools;
using Jundroo.SimplePlanes.ModTools.PrefabProxies;
using UnityEngine;

public class EnemyTargetMonitor : MonoBehaviour
{
    [SerializeField]
    private RotatingWeaponProxy[] TgtTurrets;

    [SerializeField]
    private ShipProxy[] TgtShips;

    [SerializeField]
    private AntiAircraftTankProxy[] TgtTanks;

    [SerializeField]
    private bool TgtConvoyEnabled = false;    // Whether convoys are targets

    private List<SimpleGroundVehicleScript> TgtConvoyVehicles;

    private bool HasTurrets;
    private bool HasShips;
    private bool HasTanks;
    private bool HasConvoyVehicles;

    public int TotalTgt = 0;
    public int DestroyedTgt = 0;

    public bool AllTgtDestroyed = false;

    [SerializeField]
    private SpecialFailSetting SpecialFailSetting;

    public bool SpecialFail = false;

    private void Start()
    {
        HasTurrets = TgtTurrets.Length > 0;
        HasShips = TgtShips.Length > 0;
        HasTanks = TgtTanks.Length > 0;
        HasConvoyVehicles = TgtConvoyEnabled & TgtConvoyVehicles != null;

        TotalTgt = TgtTurrets.Length + TgtShips.Length + TgtTanks.Length + (HasConvoyVehicles ? TgtConvoyVehicles.Count : 0);
    }

    private void FixedUpdate()
    {
        int CurrentTgt = 0;

        if (!AllTgtDestroyed)
        {
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

            if (HasConvoyVehicles)
            {
                for (int i = 0; i < TgtConvoyVehicles.Count; i++)
                {
                    if (TgtConvoyVehicles[i].IsDestroyed)
                    {
                        CurrentTgt++;
                    }
                }
            }

            if (DestroyedTgt != CurrentTgt)
            {
                DestroyedTgt = CurrentTgt;

                ServiceProvider.Instance.GameWorld.ShowStatusMessage(string.Format("{0}/{1} targets destroyed", DestroyedTgt, TotalTgt));
            }

            if (DestroyedTgt == TotalTgt)
            {
                AllTgtDestroyed = true;
            }
        }

        if (SpecialFailSetting != null)
        {
            SpecialFail = SpecialFailSetting.SpecialFail;
        }
    }
}
