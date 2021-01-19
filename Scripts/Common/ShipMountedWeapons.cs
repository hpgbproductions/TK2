/*
 * Goes on a Missile Defense Base facing in the same direction as a moving ship.
 */

using Jundroo.SimplePlanes.ModTools.PrefabProxies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMountedWeapons : MonoBehaviour
{
    [SerializeField]
    private RotatingWeaponProxy[] ShipWeapons;

    [SerializeField]
    private ShipProxy HostShip;

    [SerializeField]
    private float speed;

    private bool destroyed = false;

    // Update is called once per frame
    void Update()
    {
        if (HostShip.IsCriticallyDamaged)
        {
            if (!destroyed)
            {
                gameObject.transform.Translate(Vector3.down * 20000f, Space.World);

                for (int i = 0; i < ShipWeapons.Length; i++)
                {
                    ShipWeapons[i].gameObject.SetActive(false);
                }

                destroyed = true;
            }
        }
        else
        {
            gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
