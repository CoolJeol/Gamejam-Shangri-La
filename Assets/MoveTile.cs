using UnityEngine;
using UnityEngine.EventSystems;

public class MoveTile : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool followMouse = false;
    public LayerMask Groundlayer;
    bool didHit;

    public void OnPointerDown(PointerEventData eventData)
    {
        followMouse = true;
        Debug.Log(name + "Game Object Click in Progress");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        followMouse = false;
        Debug.Log(name + "No longer being clicked");
       
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (followMouse == true) { 
        
            Vector2 pos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, Groundlayer);
            transform.position = hit.point;

            

            //mousePosition = Input.mousePosition;
            //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            //transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        }

        
    }
}
