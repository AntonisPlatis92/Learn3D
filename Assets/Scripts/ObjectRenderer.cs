using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRenderer : MonoBehaviour
{
    GameObject testobject;
    bool visible;
    void Start()
    {
        
        testobject = GameObject.Find("Dog_1");
        visible = true;
    }

    // Update is called once per frame
    public void OnClick()
    {
        if (visible==true)  {
            testobject.transform.GetComponent<Renderer>().enabled = false;
            visible = false;
        } 
        else {
             testobject.transform.GetComponent<Renderer>().enabled = true;
             visible = true;   
        }
            
    }
    
}
