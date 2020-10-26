using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObjectRenderer : MonoBehaviour
{
    //GameObject testobject;
    bool visible;
    void Start()
    {
        visible = false;
    }

    // Update is called once per frame
    public void OnMouseDown()
    {
        
        //this.transform.position = new Vector3((float) -1.265f,(float) 0f,(float) 0.711f);
        //this.transform.position = new Vector3((float)a - 0.5f, (float)b - 0.5f, 0.0f);
        if (visible==true)  {
            this.transform.GetComponent<Renderer>().enabled = false;
            visible = false;
        } 
        else {
             this.transform.GetComponent<Renderer>().enabled = true;
             visible = true;   
        }
        
            
    }

    public void transformObj(int quarter)
    {
        Debug.Log("quarter " + quarter);
        Debug.Log("name: " + this.name);
        switch (quarter)
        {
            
            case (0):
                {
                    this.transform.localPosition = new Vector3((float)0.339f, 0, (float)0.139f);
                    this.transform.GetComponent<Renderer>().enabled = true;
                    visible = true;
                    break;
                }
            case (1):
                {
                    this.transform.localPosition = new Vector3((float)-0.325f, (float) 0, (float)0.146f);
                    this.transform.GetComponent<Renderer>().enabled = true;
                    visible = true;
                    break;
                }
            case (2):
                {
                    this.transform.localPosition = new Vector3((float)-0.337f, (float)0, (float)-0.244f);
                    this.transform.GetComponent<Renderer>().enabled = true;
                    visible = true;
                    break;
                }
            case (3):
                {
                    this.transform.localPosition = new Vector3((float)0.315f, (float)0, (float)-0.2f);
                    this.transform.GetComponent<Renderer>().enabled = true;
                    visible = true;
                    break;
                }
            default: break;
        }
        
        

    }
    
}
