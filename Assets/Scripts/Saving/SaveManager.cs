using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    public static SaveData Data;

    public static void Load()
    {
        if (Data.lastPos == Vector3.zero)
        {
            Save();
        }

        Debug.Log("Loading");

        Jugador.entity.transform.position = Data.lastPos;
        Physics.gravity = Data.gravity;
        CameraManager.instance.Load(Data.cameraPoint, Data.cameraPos);
    }

    public static void Save()
    {
        if(Data.lastPos == Vector3.zero)
        {
            Data = new SaveData();
        }
        
        Data.lastPos = Jugador.entity.transform.position;
        
        Debug.Log(Data.lastPos);
        Data.gravity = Physics.gravity;

        Data.level = 1;

        Data.cameraPos = CameraManager.instance.transform.position;
        Data.cameraPoint = CameraManager.instance.posActual;
    }
}
