using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject customer;
    [SerializeField]
    UIManager uiManager;    // 이건 본 개발 땐 따로 오브젝트를 두지 말고 dataManager처럼 그냥 AddComponent하기
    DataManager dataManager;

    // Start is called before the first frame update
    void Start()
    {
        dataManager = gameObject.AddComponent<DataManager>();
        BindEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BindEvent()
    {
        uiManager.Buy += dataManager.OnBuyMaterial;
        dataManager.ChangeData += uiManager.OnRefreshUI;
        uiManager.Create += dataManager.OnCreateThing;
        customer.GetComponent<CustomerManager>().BuyCustmer += dataManager.OnCustomerBuy;
    }

    void UnBindEvent()
    {
        uiManager.Buy -= dataManager.OnBuyMaterial;
        dataManager.ChangeData -= uiManager.OnRefreshUI;
        uiManager.Create -= dataManager.OnCreateThing;
        customer.GetComponent<CustomerManager>().BuyCustmer -= dataManager.OnCustomerBuy;
    }

    public void displayStart()
    {
        customer.SetActive(true);
    }
}
