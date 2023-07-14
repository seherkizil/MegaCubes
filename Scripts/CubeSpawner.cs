using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public static CubeSpawner Instance;

    Queue<Cube> cubesQueue = new Queue<Cube>(); //Cube class tipinde bir nesne olu�turduk. Bu nesne koleksiyondaki nesnelerin referanslar�n� tutacakt�r. Queue i�erisine sadece belirtilen tipte ��eler eklenebilir.

    [SerializeField] private int cubesQueueCapacity = 20;
    [SerializeField] private bool autoQueueGrow = true;

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Color[] cubeColors;

    [HideInInspector] public int maxCubeNumber; //(2^12=4096)

    private int maxPower = 12; // K�p numaralar�: 2,4,8,16,32,64,128,256,512,1024,2048,4096

    private Vector3 defaultSpawnPosition;

    private void Awake()
    {
        Instance = this; //��ren

        defaultSpawnPosition = transform.position;
        maxCubeNumber = (int)Mathf.Pow(2, maxPower);

        InitializeCubesQueue();

    }

    private void InitializeCubesQueue()
    {
        for (int i = 0; i < cubesQueueCapacity; i++)
        {
            AddCubeToQueue();
        }
        
    }

    private void AddCubeToQueue()
    {
        Cube cube = Instantiate(cubePrefab, defaultSpawnPosition, Quaternion.identity, transform).GetComponent<Cube>();

        cube.gameObject.SetActive(false);
        cube.isMainCube = false;
        cubesQueue.Enqueue(cube);
        
    }

    public  Cube Spawn(int number, Vector3 position)
    {
        if (cubesQueue.Count == 0)
        {
            if (autoQueueGrow)
            {
                cubesQueueCapacity++;
                AddCubeToQueue();
            }
            else
            {
                Debug.LogError("[Cubes Queue]: No more cubes available in the pool");
                return null;
            }
            
        }

        Cube cube = cubesQueue.Dequeue();
        cube.transform.position = position;
        cube.SetNumber(number);
        cube.SetColor(GetColor (number));
        cube.gameObject.SetActive(true);

        return cube;

    }


    public Cube SpawnRandom()
    {
        return Spawn(GenerateRandomNumber(), defaultSpawnPosition);
    }

    public void DestroyCube(Cube cube)
    {
        cube.cubeRigidbody.velocity = Vector3.zero;
        cube.cubeRigidbody.angularVelocity = Vector3.zero;
        cube.transform.rotation = Quaternion.identity;
        cube.isMainCube = false;
        cube.gameObject.SetActive(false);
        cubesQueue.Enqueue(cube);
    }
    
   







    public int GenerateRandomNumber()
    {
        return (int)Mathf.Pow(2, Random.Range(1, 6)); //2,4,8,16,32 (5 tane)
    }

    public Color GetColor(int number)
    {
        return cubeColors[(int)(Mathf.Log(number) / Mathf.Log(2)) - 1]; // index numaras�n�n s�f�rdan ba�lamas� i�in -1 yapt�k.
    }








}
