using UnityEngine;

public class Camera : MonoBehaviour
{
    public float panSpeed = 15f;
    public float panBorderThickness = 10f;
    public Vector2 panLimit;
    // public float scrollSpeed = 2f;
    
    void Update()
    {
        Vector3 pos = transform.position;
        
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        // float scroll = Input.GetAxis("Mouse ScrollWheel");
        // pos.z -= scroll * scrollSpeed * Time.deltaTime;
        
        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);
        
        transform.position = pos;
    }
}
