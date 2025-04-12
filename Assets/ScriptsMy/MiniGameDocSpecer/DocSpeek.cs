using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class DocSpeek : MonoBehaviour
{
    public GameObject objectPosition;
    public RectTransform objectSize;
    public GameObject FalseParent;
    public GameObject TrueParent;

    public GameObject prefabTrueDoc;
    public GameObject prefabFalseDoc;

    private List<GameObject> poolObjectDocTrue = new List<GameObject>(4);
    private List<GameObject> poolObjectDocFalse = new List<GameObject>(4);

    private float YPositon;
    public int numberCauntTrue;
    public int numberCauntFalse;

    private Vector3 positionSpawnT1;
    private Vector3 positionSpawnT2;

    public void Start()
    {
        YPositon = objectSize.rect.height / 2 + objectPosition.transform.position.y;
        PoolStarting();
        positionSpawnT1 = new Vector3(objectPosition.transform.position.x - objectSize.rect.width / 2, YPositon);
        positionSpawnT2 = new Vector3(objectSize.rect.width / 2 + objectPosition.transform.position.x, YPositon);

        numberCauntTrue = poolObjectDocTrue.Count;
        numberCauntFalse = poolObjectDocFalse.Count;
    }

    public void PoolStarting()
    {
        for(int i = 0; i < poolObjectDocTrue.Count; i++)
        {
            GameObject newObject = Instantiate(prefabTrueDoc);
            newObject.transform.parent = TrueParent.transform;
            newObject.name = "TrueeDoc." + i;
            poolObjectDocTrue[i] = newObject;
            newObject.SetActive(false);
        } //Создает пулл объектов типа True

        for (int i = 0; i < poolObjectDocTrue.Count; i++)
        {
            GameObject newObject = Instantiate(prefabFalseDoc);
            newObject.transform.parent = FalseParent.transform;
            newObject.name = "FalseDoc." + i;
            poolObjectDocFalse[i] = newObject;
            newObject.SetActive(false);
        } //Создает пулл объектов типа False
    }

    public IEnumerator SpawnDoc()
    {
        while (true)
        {
            float positionSpawnObjectX = Random.Range(positionSpawnT1.x, positionSpawnT2.x);
            float randomObjectSpawn = Random.Range(1, 2);

            if(randomObjectSpawn == 1 && poolObjectDocTrue.Count != 0)
            {
                SpawnObjectDoc(1, positionSpawnObjectX);
            }
            else if(randomObjectSpawn == 2 && poolObjectDocFalse.Count != 0)
            {
                SpawnObjectDoc(2, positionSpawnObjectX);
            }
            else if (poolObjectDocFalse.Count == 0 || poolObjectDocFalse.Count == 0)
            {
                yield return new WaitForSeconds(5);
            }
            yield return new WaitForSeconds(Random.Range(2, 5));
        }
    }

    public void SpawnObjectDoc(float spawnObject, float positionXSpawn)
    {
        Vector3 positionSpawn = new Vector3 (positionXSpawn, YPositon);

        if (spawnObject == 1)
        {
            GameObject objectS = poolObjectDocTrue[numberCauntTrue];
            numberCauntTrue -= 1;
            objectS.SetActive(true);
            objectS.transform.position = positionSpawn;
        }   
        else if (spawnObject == 2)
        {
            GameObject objectS = poolObjectDocFalse[numberCauntFalse];
            numberCauntFalse -= 1;
            objectS.SetActive(true);
            objectS.transform.position = positionSpawn;
        }
        else
        {
            Debug.Log("Ошибка в пулле объектов произошла при создании объекта!");
        }
    }
}
