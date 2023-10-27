using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody player;
    private Vector3 startPosition;

    private Vector3 input = Vector3.zero;
    Vector3 offset = Vector3.zero;
    bool isDragging;

    public void FixedUpdate()
    {
        if (!isDragging) return;
        Vector3 direction = (Vector3.forward * input.y) + (Vector3.right * input.x);

        player.velocity = direction * Time.fixedDeltaTime * 4;
        if(transform.position.y >10)
        {
            transform.position = new Vector3(transform.position.x, 10, transform.position.z);
        }
    }

    private void OnMouseDown()
    {
        input = Vector3.zero;
        startPosition = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        if (offset.magnitude > 0)
            isDragging = true;

        offset = Input.mousePosition - startPosition;
        input = offset;
   

    }

    private void OnMouseUp()
    {
        isDragging = false;
        input = Vector3.zero;
        player.velocity = Vector3.zero;
    }
}