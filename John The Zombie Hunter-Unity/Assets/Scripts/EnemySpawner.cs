using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject ZombiePrefab;
    public Transform player;
    public int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (counter > 30) return;
        counter++;
        GameObject enemy = Instantiate(ZombiePrefab);
        EnemyAI npc = enemy.GetComponent<EnemyAI>();
        npc.playerTransform = player;
    }
}
