using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CustomerView : MonoBehaviour
{
    private Vector3 spawnPosition;
    public CustomerPoint customerPoint;
    private Vector3 direction;
    [SerializeField] private Image clothImage;
    public bool isServed;
    private bool isWaiting;
    public Color requiredClothColor;


    void Start()
    {
        isWaiting = false;
        spawnPosition = transform.position;
    }
    public void SetCustomerData(Color clothColor, Vector3 standingPoint, CustomerPoint custPoint)
    {
        requiredClothColor = clothImage.color = clothColor;
        direction = standingPoint;
        customerPoint = custPoint;
    }
    public bool TryServed()
    {
        if (isServed || !isWaiting)
            return false;
        isServed = true;
        transform.GetComponent<Renderer>().material.color = clothImage.color;
        MoveCustomer();
        return true;
    }
    private void MoveCustomer()
    {
        direction = spawnPosition;
        customerPoint.SetCustomerStandPoint(false);
        customerPoint = null;
        StartCoroutine(DestroyWithDelay());
    }
    public void FixedUpdate()
    {
        var step = 12 * Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, direction, step);

        if (Vector3.Distance(transform.position, direction) < 0.1f)
        {
            isWaiting = true;
            transform.position = direction;
        }
    }

    private IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(9);
        CustomerHandler.OnCustomerLeft?.Invoke();
        Destroy(gameObject);
    }
}
