using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjManager : MonoBehaviour
{
    [SerializeField]
    GameObject WorkstationUl;
    [SerializeField]
    GameObject displayTableUI;
    // Start is called before the first frame update
    void Start()
    {

    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                if(hit.collider.tag == "Workstation")
                {
                    GameObject.Find("UIManager").GetComponent<UIManager>().MenuPop(WorkstationUl);
                }
                else if(hit.collider.tag == "displayTable")
                {
                    GameObject.Find("UIManager").GetComponent<UIManager>().MenuPop(displayTableUI);
                }
            }
        }

    }
}