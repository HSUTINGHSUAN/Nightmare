using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;


    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime); //unity的API (重複呼叫的對象，第一次要延遲多久時間，每次間隔多久)
    }


    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)//玩家血量<=0
        {
            return;//停止產生任何東西
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length); //隨機產生一個Range(最小是0，最大是Length)

        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation); //Instantiate 複製一個東西(複製的東西，產生位置，旋轉量<朝向哪裡>)
    }
}
