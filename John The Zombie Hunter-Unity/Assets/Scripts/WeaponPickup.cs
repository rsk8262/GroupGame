using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Weapon weaponPrefab;

    private void Start()
    {
        Weapon weapon_preview = Instantiate(weaponPrefab);
        weapon_preview.transform.parent = transform;
        weapon_preview.transform.localPosition = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();
        if(activeWeapon)
        {
            activeWeapon.Equip(weaponPrefab);
            Destroy(gameObject);
        }
    }
}
