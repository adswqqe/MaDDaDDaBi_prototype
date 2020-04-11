using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public Action<int, int, int, int> ChangeData;

    private int curGold = 1000;
    private int goldOre = 0;
    private int goldbar = 0;
    private int curExp = 0;

    public int GetGold()
    {
        return curGold;
    }

    public void OnBuyMaterial(int number)
    {
        curGold -= 2 * number;
        goldOre += number;
        ChangeData?.Invoke(curGold, goldOre, goldbar, curExp);
    }

    public void OnCreateThing(int number)
    {
        goldOre -= 2 * number;
        goldbar += number;
        ChangeData?.Invoke(curGold, goldOre, goldbar, curExp);
    }

    public void OnCustomerBuy()
    {
        curExp += 5;
        curGold += 10;
        ChangeData?.Invoke(curGold, goldOre, goldbar, curExp);
    }

}
