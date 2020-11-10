using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChecker : MonoBehaviour {
    
	void Start () {
        DataHandler.IsAlreadyGotoPhylums++;
        Debug.Log(DataHandler.IsAlreadyGotoPhylums);
	}

}
