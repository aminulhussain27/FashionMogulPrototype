using System;
using System.Collections;
using UnityEngine;

public class CustomerHandler : MonoBehaviour
{
    public static Action OnCustomerLeft;

    [SerializeField] private Transform customerSpawnPoint;
    [SerializeField] private GameObject customerPrefab;

    [SerializeField] private CustomerPointHandler[] customerPointHandlers;
    private void Start()
    {
        OnCustomerLeft += StartCustomerEntry;
        StartCustomerEntry();
    }

    private void StartCustomerEntry()
    {
        StartCoroutine(CheckEmptyCustomerStandingPoint());
    }
    private IEnumerator CheckEmptyCustomerStandingPoint()
    {
        foreach (var handler in customerPointHandlers)
        {
            if (handler.isPointAvailable)
            {
                foreach (var customerpoint in handler.customerPoints)
                {
                    CustomerPoint customerPoint = handler.GetCustomerStandingPoint();
                    if (customerPoint != null)
                    {
                        customerPoint.SetCustomerStandPoint(true);
                        yield return new WaitForSeconds(UnityEngine.Random.Range(2, 6));
                        CreateCustomer(customerPoint.transform, customerPoint);
                        Debug.Log(handler.name);
                    }
                }
            }
        }
    }
    private void CreateCustomer(Transform standPoint, CustomerPoint customerPoint)
    {
        int randomNumber = UnityEngine.Random.Range(0, 100);
        GameObject customer = Instantiate(customerPrefab, customerSpawnPoint);
        customer.GetComponent<CustomerView>().SetCustomerData(randomNumber > 50 ? Color.red : Color.green, 
            standPoint.transform.position, customerPoint);
        customer.transform.position = customerSpawnPoint.position;
    }
}
