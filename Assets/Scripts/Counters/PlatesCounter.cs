using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO plateSO;
 
    [SerializeField] private int plateCountMax = 5;

    private List<KitchenObject> platesList = new List<KitchenObject>();



    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
        {
            if (player.GetKitchenObject().TryGetComponent<PlateKitchenObject>
    (out PlateKitchenObject plateKitchenObject))
            {
                    if (IsHaveKitchenObject() == false && platesList.Count<plateCountMax)
                {
                    SpawnPlate();
                    player.DestroyKitchenObject();
                }
            }


        }
        else
        {
            if (platesList.Count > 0&& platesList.Count < plateCountMax)
            {
                player.AddKitchenObject(platesList[platesList.Count - 1]);
                platesList.RemoveAt(platesList.Count - 1);
            }
            if(platesList.Count ==  plateCountMax)
            {

                PlateGroupKitchenObject plateGroup = CreatePlateGroup2();
                player.AddKitchenObject(plateGroup);
            }

        }
    }


    public void SpawnPlate()
    {


        KitchenObject kitchenObject = GameObject.Instantiate(plateSO.prefab, GetHoldPoint()).GetComponent<KitchenObject>();

        kitchenObject.transform.localPosition = Vector3.zero + Vector3.up * 0.1f * platesList.Count;

        platesList.Add(kitchenObject);

    }


    private PlateGroupKitchenObject CreatePlateGroup()
    {
        // Create a new GameObject for the plate group
        GameObject plateGroupObject = new GameObject("PlateGroup");
        PlateGroupKitchenObject plateGroup = plateGroupObject.AddComponent<PlateGroupKitchenObject>();

        // Save the original local positions of the plates
        Dictionary<PlateGroupKitchenObject, Vector3> plateOriginalPositions = new Dictionary<PlateGroupKitchenObject, Vector3>();
        foreach (PlateGroupKitchenObject plate in platesList)
        {
            plateOriginalPositions[plate] = plate.transform.localPosition;
        }

        // Set each plate as a child of the plate group
        foreach (PlateGroupKitchenObject plate in platesList)
        {
            plate.transform.SetParent(plateGroupObject.transform);
        }

        // Adjust the plate group’s position to align with the hold point
        plateGroupObject.transform.position = GetHoldPoint().position;

        // Restore the original local positions of the plates
        foreach (var plateEntry in plateOriginalPositions)
        {
            plateEntry.Key.transform.localPosition = plateEntry.Value;
        }

        // Clear the plates list
        platesList.Clear();
        return plateGroup;
    }

    private PlateGroupKitchenObject CreatePlateGroup2()
    {
        // Create a new GameObject for the plate group
        GameObject plateGroupObject = new GameObject("PlateGroup");

        // Add the PlateGroupKitchenObject component
        PlateGroupKitchenObject plateGroup = plateGroupObject.AddComponent<PlateGroupKitchenObject>();

        // Save the original local positions of the plates
        Dictionary<KitchenObject, Vector3> plateOriginalPositions = new Dictionary<KitchenObject, Vector3>();
        foreach (KitchenObject plate in platesList)
        {
            plateOriginalPositions[plate] = plate.transform.localPosition;
        }

        // Set each plate as a child of the plate group
        foreach (KitchenObject plate in platesList)
        {
            plate.transform.SetParent(plateGroupObject.transform);
        }

        // Adjust the plate group’s position to align with the hold point
        plateGroupObject.transform.position = GetHoldPoint().position;

        // Restore the original local positions of the plates
        foreach (var plateEntry in plateOriginalPositions)
        {
            plateEntry.Key.transform.localPosition = plateEntry.Value;
        }

        // Clear the plates list
        platesList.Clear();

        return plateGroup;
    }
}


