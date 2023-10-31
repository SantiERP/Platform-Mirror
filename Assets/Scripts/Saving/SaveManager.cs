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

        ModelPlayer.entity.transform.position = Data.lastPos + Vector3.right * 0.5f;
        Physics.gravity = Data.gravity;
        CameraManager.Instance.Load(Data.cameraPoint, Data.cameraPos);
    }

    public static void Save()
    {
        if(Data.lastPos == Vector3.zero)
        {
            Data = new SaveData();
        }
        
        Data.lastPos = ModelPlayer.entity.transform.position;
        
        Debug.Log(Data.lastPos);
        Data.gravity = Physics.gravity;

        Data.level = 1;

        Data.cameraPos = CameraManager.Instance.transform.position;
        Data.cameraPoint = CameraManager.Instance.actualPos;
    }
}
