using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class 테이블 : MonoBehaviour
{
    private GameObject target;

    public GameObject 테이블선택;

    public GameObject 작업대선택;

    public GameObject 가방UI;

    public GameObject 상점UI;

    public GameObject 기술UI;

    public GameObject 주인공기술;

    public GameObject 상점기술;

    public GameObject 건축UI;

    public Image 상점골드바;

    public GameObject 의뢰UI;

    public GameObject 의뢰종이;

    public GameObject 확인팝업;

    public GameObject 수량선택팝업;
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = GetClickedObject();
            if (target.transform.gameObject.name == "진열대") //선택된게 나라면
            {
                테이블선택.SetActive(true);
            }
            else if (target.transform.gameObject.name == "작업대")
            {
                Debug.Log("작업대");
                작업대선택.SetActive(true);
            }

        }
    }
    
    private GameObject GetClickedObject()
    {
        RaycastHit hit;
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //마우스 포인트 근처 좌표를 만든다. 

        if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))   //마우스 근처에 오브젝트가 있는지 확인
        {
            //있으면 오브젝트를 저장한다.
            target = hit.collider.gameObject;
        }
        return target;
    }

    public void 테이블닫기()
    {
        테이블선택.SetActive(false);
    }
    public void 확인팝업열기()
    {
        확인팝업.SetActive(true);
    }
    public void 확인팝업닫기()
    {
        확인팝업.SetActive(false);
    }
    public void 작업대닫기()
    {
        작업대선택.SetActive(false);
    }

    public void 가방열기()
    {
        가방UI.SetActive(true);
    }
    public void 가방닫기()
    {
        가방UI.SetActive(false);
    }
    public void 상점열기()
    {
        상점UI.SetActive(true);
    }
    public void 상점상품선택()
    {
        상점골드바.color = new Color(상점골드바.color.r, 상점골드바.color.g, 상점골드바.color.b, 0.7f);
    }
    
    public void 상점닫기()
    {
        상점UI.SetActive(false);
    }
    public void 수량선택팝업열기()
    {
        수량선택팝업.SetActive(true);
    }
    public void 수량선택팝업닫기()
    {
        수량선택팝업.SetActive(false);
    }


    public void 기술열기()
    {
        기술UI.SetActive(true);
    }

    public void 주인공기술열기()
    {
        상점기술.SetActive(false);
        주인공기술.SetActive(true);
    }
    public void 상점기술열기()
    {
        상점기술.SetActive(true);
        주인공기술.SetActive(false);
    }

    public void 기술닫기()
    {
        기술UI.SetActive(false);
    }
    public void 건축열기()
    {
        건축UI.SetActive(true);
    }
    public void 건축닫기()
    {
        건축UI.SetActive(false);
    }
    public void 의뢰열기()
    {
        의뢰UI.SetActive(true);
    }
    public void 의뢰닫기()
    {
        의뢰UI.SetActive(false);
    }
    public void 의뢰확대()
    {
        의뢰종이.SetActive(true);
    }
    public void 의뢰축소()
    {
        의뢰종이.SetActive(false);
    }
}
