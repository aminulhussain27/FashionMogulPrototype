using System.Collections.Generic;
using UnityEngine;

public class CustomerPointHandler : MonoBehaviour
{
    public bool isPointAvailable;
    public List<CustomerPoint> customerPoints;
    public CustomerPoint GetCustomerStandingPoint()
    {
        foreach (var point in customerPoints)
        {
            if (!point.isPointOccupied)
            {
                return point;
            }
        }
        return null;
    }
}
