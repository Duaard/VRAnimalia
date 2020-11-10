using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : Singleton<DataHandler> {

    private static int isAlreadyGoToPhylums = 0;

    public static int IsAlreadyGotoPhylums
    {
        get
        {
            return isAlreadyGoToPhylums;
        }
        set
        {
            isAlreadyGoToPhylums = value;
        }
    }
}
