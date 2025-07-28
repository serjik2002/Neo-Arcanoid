using UnityEngine;

public class Platform : MonoBehaviour
{
    private void Start()
    {
        transform.position = new Vector3(0, -3.5f, 0);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float clampedX = Mathf.Clamp(mousePosition.x, -1.85f, 1.85f);

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(clampedX, transform.position.y, transform.position.z), Time.deltaTime * 35f);

            //transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        }
    }
}
