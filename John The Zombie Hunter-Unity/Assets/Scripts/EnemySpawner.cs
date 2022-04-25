using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject ZombiePrefab;
    public Transform player;
    public int counter = 0;
    public int zombies_per_wave = 50;
    public int min_distance_to_player = 15;
    public Vector2 max_zombie_per_floor = new Vector2(2, 8);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (counter != 0) return;

        List<GameObject> floorsAtDistance = GameObject.FindGameObjectsWithTag("Floor")
            .Where(x => Vector3.Distance(player.position, x.transform.position) >= min_distance_to_player)
            .OrderBy(x => Random.value).ToList();

        int remaining_zombies = zombies_per_wave;

        foreach (GameObject floor in floorsAtDistance)
        {
            if (remaining_zombies == 0) return;

            int spawn_amount = (int)Mathf.Min(remaining_zombies, Random.Range(max_zombie_per_floor.x, max_zombie_per_floor.y));
            for (int i = 0; i < spawn_amount; i++)
            {
                counter++;
                GameObject enemy = Instantiate(ZombiePrefab, floor.transform.position, Quaternion.identity);
                EnemyAI npc = enemy.GetComponent<EnemyAI>();
                npc.playerTransform = player;
                npc.spawner = this;
            }
            remaining_zombies -= spawn_amount;
        }
    }

    public void UpdateCounter()
    {
        counter--;
    }
}
