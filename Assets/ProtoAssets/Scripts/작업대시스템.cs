using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 작업대시스템 : MonoBehaviour
{
    public Text 재료;
    public Text 골드바, 골드바2;
    public GameObject 테이블UI, 말풍선, 확인팝업;


    void OnEnable()
    { 
        재료.text = "금광석 " + "<color=white>" + 아이템시스템.GoldNugget.ToString("0") + "</color>" + "/" + "2";
    }

    public void GoldBarCraft()
    {
        아이템시스템.GoldNugget -= 2;
        재료.text = "금광석 " + "<color=white>" + 아이템시스템.GoldNugget.ToString("0") + "</color>" + "/" + "2";
        골드바.text = "보유량 : 1 ";
        골드바2.text = "보유량 : 1 ";
    }

    public void 골드바진열()
    {
        확인팝업.SetActive(true);
    }
    public void 진열하기()
    {
        확인팝업.SetActive(false);
        테이블UI.SetActive(false);
        말풍선.SetActive(true);
    }
}
