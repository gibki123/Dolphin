using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject water;
    public GameObject sand;
    public Transform playerTransform;

    private float topSpawningLimit;
    private float bottomSpawningLimit;
    private float minSpawnTime = 0.5f;
    private float maxSpawnTime = 0.75f;
    private float positionOffsetX = 30f;
    private IEnumerator spawnCoroutine;
    private List<GameObject> collectables;
    private List<GameObject> terrainObjects;
    private bool spawningCoroutine = false;
    private bool spawningMinesCoroutine = false;
    private int sandBoxQuantity = 0;
    private float sandWidth;
    private bool spawningRockCoroutine = false;

    void Awake() {
        sandWidth = sand.transform.localScale.x;
        terrainObjects = new List<GameObject>();
        collectables = new List<GameObject>();
        spawnCoroutine = spawnCollectables(minSpawnTime, maxSpawnTime);
        Vector3 waterPosition = water.transform.position;
        topSpawningLimit = waterPosition.y + water.transform.localScale.y / 2 - 1;
        bottomSpawningLimit = waterPosition.y - water.transform.localScale.y / 2 + 1;
        ButtonHandler.OnClickTryAgainButton += DisbaleAllObjectsAfterDeath;
    }
    void Start() {
    }

    void Update() {
        if(playerTransform.position.x > -10 + sandBoxQuantity*sandWidth)
        {
            sandBoxQuantity++;
            Vector3 spawnPosition;
            spawnPosition = new Vector3(sandBoxQuantity * sandWidth, sand.transform.position.y, sand.transform.position.z);
            terrainObjects.Add(ObjectPooler.Instance.SpawnFromPool("Sand", spawnPosition));
        }
        if (spawningCoroutine == false && GameState.state == GameState.gameState.Game) {
            spawningCoroutine = true;
            StartCoroutine(spawnCollectables(minSpawnTime,maxSpawnTime));
        }
        if (spawningRockCoroutine == false && GameState.state == GameState.gameState.Game) {
            spawningRockCoroutine = true;
            StartCoroutine(SpawnRocks());
        }
        if (spawningMinesCoroutine == false && GameState.state == GameState.gameState.Game) {
            spawningMinesCoroutine = true;
            StartCoroutine(SpawnMines());
        }
        if (collectables.Count > 0) {
        CheckForCollectableDisable();
        } 
        if(terrainObjects.Count > 0)
        {
            CheckForDisable(terrainObjects);
        }
    }

    // For now Spwaning only fish with random position in range of water maybe later something to add 
    IEnumerator spawnCollectables(float minSpawnTime, float maxSpawnTime) {
        float randomSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        float randomSpawnY = Random.Range(bottomSpawningLimit, topSpawningLimit);
        float spawnX = playerTransform.position.x + positionOffsetX;
        float spawnZ = -0.5f;
        Vector3 instantiatePosition = new Vector3(spawnX, randomSpawnY, spawnZ); 
        yield return new WaitForSeconds(randomSpawnTime);
        collectables.Add(ObjectPooler.Instance.SpawnFromPool("CollectableFish", instantiatePosition));
        spawningCoroutine = false;
    }

    //This checks if object schould be disabled or not
    private void CheckForCollectableDisable() {
        GameObject collectableToDisable = collectables.First();
        if(collectableToDisable.transform.position.x < playerTransform.position.x - positionOffsetX) {
            ObjectPooler.Instance.DisableFromPool(collectables.First());
            collectables.Remove(collectableToDisable);
        }
    }

    //This checks if object schould be disabled or not
    private void CheckForDisable(List<GameObject> objects)
    {
        GameObject ObjectToDisable = objects.First();
        if (ObjectToDisable.transform.position.x < playerTransform.position.x - 3*positionOffsetX)
        {
            ObjectPooler.Instance.DisableFromPool(objects.First());
            objects.Remove(ObjectToDisable);
        }
    }

    //This function reset collections to initial state and disable objects from pools
    private void DisbaleAllObjectsAfterDeath() {
        foreach(var item in collectables) {
            ObjectPooler.Instance.DisableFromPool(item);
        }
        foreach (var item in terrainObjects) {
            ObjectPooler.Instance.DisableFromPool(item);
        }
        collectables.Clear();
        terrainObjects.Clear();
        sandBoxQuantity = 0;
    }

    //Spawning rocks Randomcy with random scale and position on the map
    private IEnumerator SpawnRocks() {
        int randomSeconds = Random.Range(3, 6);
        yield return new WaitForSeconds(randomSeconds);
        float randomScaleA1 = Random.Range(0.3f, 0.6f);
        float randomScaleA2 = Random.Range(0.3f, 0.6f);
        float randomScaleB1 = Random.Range(0.2f, 0.7f);
        float randomScaleB2 = Random.Range(0.2f, 0.7f);
        float randomScaleC1 = Random.Range(0.5f, 3.0f);
        float randomScaleC2 = Random.Range(0.5f, 3.0f);
        int randomDistanceA = Random.Range(30, 70);
        int randomDistanceB = Random.Range(30, 70);
        int randomDistanceC = Random.Range(30, 70);
        Vector3 spawnPosition = new Vector3(playerTransform.position.x,-10,0);
        spawnPosition.x += randomDistanceA;
        GameObject rock = ObjectPooler.Instance.SpawnFromPool("RockA", spawnPosition);
        rock.transform.localScale = new Vector3(randomScaleA1, randomScaleA2, 0.1f);
        terrainObjects.Add(rock);
        spawnPosition.x = randomDistanceB + playerTransform.position.x;
        rock = ObjectPooler.Instance.SpawnFromPool("RockB", spawnPosition);
        rock.transform.localScale = new Vector3(randomScaleB1, randomScaleB2, 0.2f);
        terrainObjects.Add(rock);
        spawnPosition.x = randomDistanceC + playerTransform.position.x;
        rock = ObjectPooler.Instance.SpawnFromPool("RockC", spawnPosition);
        rock.transform.localScale = new Vector3(randomScaleC1, randomScaleC2, 0.4f);
        terrainObjects.Add(rock);
        spawningRockCoroutine = false;
    }

    //Function which randomly spwan mines in time intervals
    private IEnumerator SpawnMines() {
        int randomSeconds = Random.Range(15,30);
        Debug.Log(randomSeconds);
        int randomMinesNumber = Random.Range(5, 8);
        yield return new WaitForSeconds(randomSeconds);
        Vector3 spawnPosition = new Vector3(playerTransform.position.x + 50, 0, 0);
        for(int i = 0; i < randomMinesNumber; i++) {
            spawnPosition.y = Random.Range(8f, 9f);
            spawnPosition.x += Random.Range(5, 15);
            terrainObjects.Add(ObjectPooler.Instance.SpawnFromPool("Boat_Mine", spawnPosition));
        }
        spawningMinesCoroutine = false;
    }
}
