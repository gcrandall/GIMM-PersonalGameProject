using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//NOT USED RIGHT NOW
public class Controls : MonoBehaviour {

    //Keyboard
    public static UnityEngine.KeyCode moveForward = KeyCode.W;
    public static UnityEngine.KeyCode moveLeft = KeyCode.A;
    public static UnityEngine.KeyCode moveRight = KeyCode.D;
    public static UnityEngine.KeyCode moveBack = KeyCode.S;

    public static UnityEngine.KeyCode spawn = KeyCode.E;
    public static bool grab
    {
        get
        {
            Debug.Log("Pressed primary button.");
            return Input.GetMouseButtonDown(0);
        }
    }
    public static bool delete
    {
        get
        {
            Debug.Log("Pressed secondary button.");
            return Input.GetMouseButtonDown(1);
        }
    }
    public static UnityEngine.KeyCode interact = KeyCode.Space;
    public static UnityEngine.KeyCode glue = KeyCode.R;

}
