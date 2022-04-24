using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public Weapon[] weaponsPrefabs;
    public GameObject spherePrefab;
    public Transform player;
    public int counter = 0;
    public int weapons_spawn_amount = 10;
    public int max_distance_to_player = 25;
    public Vector2 max_weapon_per_floor = new Vector2(1, 1);

    // Start is called before the first frame update
    void Start()
    {

    }
    float GetClosestSphereDistance(Vector3 position)
    {
        return GameObject.FindGameObjectsWithTag("WeaponPicker").Length > 0 ? GameObject.FindGameObjectsWithTag("WeaponPicker").Select(x => Vector3.Distance(position, x.transform.position)).Min() : Mathf.Infinity;
    }
    float GetClosestChestDistance(Vector3 position)
    {
        return GameObject.FindGameObjectsWithTag("Chest").Select(x => Vector3.Distance(position, x.transform.position)).Min();
    }

    private void Update()
    {
        if (counter != 0) return;
        List<GameObject> floorsAtDistance = GameObject.FindGameObjectsWithTag("Floor")
        .Where(x => Vector3.Distance(player.position, x.transform.position) <= max_distance_to_player && GetClosestChestDistance(x.transform.position) > 5).OrderBy(x => Random.value).ToList();

        int remaining_weapons = weapons_spawn_amount;
        foreach (GameObject floor in floorsAtDistance)
        {
            if (remaining_weapons == 0) return;
            if (GetClosestSphereDistance(floor.transform.position) < 10) continue;

            counter++;
            GameObject weapon_holder = Instantiate(spherePrefab, new Vector3(floor.transform.position.x, 1f, floor.transform.position.z), Quaternion.identity);
            weapon_holder.GetComponent<WeaponPickup>().weaponPrefab = weaponsPrefabs[Random.Range(0, weaponsPrefabs.Length)];
        
            remaining_weapons -= 1;
        }
    }
}
