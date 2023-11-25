using UnityEngine;

public static class SaveManager
{
    public delegate void Action();

    public static Action Save;
    public static Action Remember;
    public static Action Forget;

    public static void AddToSaveManager(IMementeable m)
    {
        Remember += delegate { m.Remember(); };
        Forget += delegate { m.Forget(); };
        Save += delegate { m.Save(); };
    }
}
