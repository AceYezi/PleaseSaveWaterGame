using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    [SerializeField] private TrashReciptListSO trashReciptList;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private SpawnCounter spawnCounter;
    [SerializeField] private Animator animator;

    private int maxItems = 3;
    private float minSpawnTime = 2.0f;
    private float maxSpawnTime = 5.0f;
    private bool isPaused = false;

    private List<GameObject> spawnedItems = new List<GameObject>();

    void Awake()
    {
        // 确保在场景加载时正确加载 TrashReciptListSO
        if (trashReciptList == null)
        {
            trashReciptList = Resources.Load<TrashReciptListSO>("TrashRecipesList");
            Debug.Assert(trashReciptList != null, "TrashReciptListSO is not assigned!");
            if (trashReciptList == null)
            {
                Debug.LogError("Failed to load TrashRecipesList from Resources");
            }
        }
    }
    void Start()
    {
        StartCoroutine(SpawnItems());
    }

    private void Update()
    {
        // Check if the clear counter is full and update the animation state
        if (spawnCounter.IsFull())
        {
            PauseAnimation();
        }
        else
        {
            ResumeAnimation();
        }
    }

    private IEnumerator SpawnItems()
    {
        while (true)
        {
            // Check if spawning should be paused
            if (!isPaused && !spawnCounter.IsFull())
            {
                // If there are less than maxItems and ClearCounter is not full
                if (spawnedItems.Count < maxItems)
                {
                    yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
                    KitchenObjectSO randomInput = GetRandomInput();
                    if (randomInput != null)
                    {
                        GameObject item = Instantiate(randomInput.prefab,
                            spawnPoint.position, Quaternion.identity);
                        spawnedItems.Add(item);
                        StartCoroutine(MoveToEnd(item));
                    }
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    private KitchenObjectSO GetRandomInput()
    {
        if (trashReciptList.list.Count == 0) return null;
        TrashReciptSO randomRecipt = trashReciptList.list[Random.Range(0, trashReciptList.list.Count)];
        return randomRecipt.input;
    }

    private IEnumerator MoveToEnd(GameObject item)
    {
        float travelTime = 3.0f; // Adjust as needed
        Vector3 startPosition = item.transform.position;
        Vector3 endPosition = endPoint.position;

        float elapsedTime = 0;
        while (elapsedTime < travelTime)
        {
            if (!spawnCounter.IsFull() && !isPaused) 
            {
                elapsedTime += Time.deltaTime;
                item.transform.position = Vector3.Lerp(startPosition,
                    endPosition, elapsedTime / travelTime);
            }
            yield return null;
        }
        item.transform.position = endPosition;

        // Check if ClearCounter is full after the item has arrived
        if (spawnCounter.IsFull())
        {
            isPaused = true;
            yield return new WaitUntil(() => !spawnCounter.IsFull());
            isPaused = false;
        }

        spawnCounter.ReceiveItem(item);
        spawnedItems.Remove(item);
    }

    public void NotifyClearCounterEmptied()
    {
        isPaused = false;
    }

    private void PauseAnimation()
    {
        animator.speed = 0;
    }

    private void ResumeAnimation()
    {
        animator.speed = 1;
    }

}
