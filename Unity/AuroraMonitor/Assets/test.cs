using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
		gameObject.transform.localEulerAngles += new Vector3(0, 0, 3) * Time.deltaTime * 65;
	}
}
