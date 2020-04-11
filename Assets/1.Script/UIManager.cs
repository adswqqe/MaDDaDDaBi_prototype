using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Action<int> Buy;
    public Action<int> Create;

    [SerializeField]
    Text theNumber;
    [SerializeField]
    Text goldText;
    [SerializeField]
    Text goldbarTheNumber;
    [SerializeField]
    Text goldoreText;
    [SerializeField]
    Text goldbarText;
    [SerializeField]
    Text commodityPriceText;
    [SerializeField]
    Text expText;

    public void MenuPop(GameObject Go)
    {
        Go.SetActive(!Go.activeSelf);
    }

    public void BuyMaterial(Text number)
    {
        int MaterialNumber = int.Parse(number.text);

        Buy?.Invoke(MaterialNumber);
    }
    public void CreateThing(Text number)
    {
        int ThingNumber = int.Parse(number.text);

        Create?.Invoke(ThingNumber);

    }

    public void AddAndDropCount(GameObject GO)
    {
        int temp = int.Parse(theNumber.text);

        if (GO.name.Contains("줄이기"))
        {
            if (GO.name.Contains("10"))
                temp -= 10;
            else
                temp -= int.Parse(GO.name[0].ToString());
        }
        else if (GO.name.Contains("늘리기"))
        {
            if (GO.name.Contains("10"))
                temp += 10;
            else
                temp += int.Parse(GO.name[0].ToString());
        }

        if (temp <= -1)
            temp = 0;
        theNumber.text = temp.ToString();
    }

    public void AddAndDropCount2(GameObject GO)
    {
        int temp = int.Parse(goldbarTheNumber.text);

        if (GO.name.Contains("줄이기"))
        {
            if (GO.name.Contains("10"))
                temp -= 10;
            else
                temp -= int.Parse(GO.name[0].ToString());
        }
        else if (GO.name.Contains("늘리기"))
        {
            if (GO.name.Contains("10"))
                temp += 10;
            else
                temp += int.Parse(GO.name[0].ToString());
        }

        if (temp <= -1)
            temp = 0;
        goldbarTheNumber.text = temp.ToString();
    }

    public void AddAndDropCount3(GameObject GO)
    {
        int temp = int.Parse(commodityPriceText.text);

        if (GO.name.Contains("줄이기"))
        {
            if (GO.name.Contains("10"))
                temp -= 10;
            else
                temp -= int.Parse(GO.name[0].ToString());
        }
        else if (GO.name.Contains("늘리기"))
        {
            if (GO.name.Contains("10"))
                temp += 10;
            else
                temp += int.Parse(GO.name[0].ToString());
        }

        if (temp <= -1)
            temp = 0;
        commodityPriceText.text = temp.ToString();
    }

    public void OnRefreshUI(int gold, int goldOre, int goldbar, int curExp)
    {
        goldText.text = gold.ToString();
        goldoreText.text = "금광석 2/" + goldOre;
        goldbarText.text = "보유량 : " + goldbar;
        expText.text = "경험치 " + curExp + "/ 9999";
        Debug.Log("금광석 2/" + goldOre);
    }

    public void test()
    {
        Debug.Log("test");
    }
}
