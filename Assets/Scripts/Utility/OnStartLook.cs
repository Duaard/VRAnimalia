using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartLook : MonoBehaviour {

    [SerializeField] private Transform m_Camera;

	// Use this for initialization
	void Start () {
        Vector3 rot = this.gameObject.transform.rotation.eulerAngles;
        Vector3 camRotation = m_Camera.rotation.eulerAngles;
        rot = new Vector3(rot.x, camRotation.y, rot.z);
        this.gameObject.transform.rotation = Quaternion.Euler(rot);
	}
}
