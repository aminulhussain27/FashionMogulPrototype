using UnityEngine;

public class CustomerPoint : MonoBehaviour
{
    public bool isPointOccupied;
    public void SetCustomerStandPoint(bool isFree) => isPointOccupied = isFree;

}
