using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{

    public static bool Jump()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            return true;
        }else if (Input.GetMouseButtonDown(0))
        {
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }
        return false;
    }
    public static bool JumpHeld()
    {
        if (Input.GetKey(KeyCode.JoystickButton0))
        {
            return true;
        }
        else if (Input.GetMouseButton(0))
        {
            return true;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            return true;
        }
        return false;
    }

    public static bool Back()
    {
        if (Input.GetKey(KeyCode.JoystickButton1))
        {
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            return true;
        }
        return false;
    }

    public static Vector2 Pos()
    {
        if (Input.mousePresent)
        {
            return Input.mousePosition;
        }else if(Input.touchCount > 0)
        {
            return Input.GetTouch(0).position;
        }else
        {
            return Vector2.zero;
        }
    }
}
