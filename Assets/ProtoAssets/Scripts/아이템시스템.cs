using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class 아이템시스템 : MonoBehaviour
{
    static public int GoldNugget, Bag;
    public Text GoldBarText;
    public GameObject 주문UI;

    public void GoldBarAdd()
    {
        GoldNugget = 2;
        GoldBarText.text = "보유 : " + GoldNugget.ToString("0");
        주문UI.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
