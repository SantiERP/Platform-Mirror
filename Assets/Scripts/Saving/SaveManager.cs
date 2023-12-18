using UnityEngine;
using System.Collections.Generic;

public static class SaveManager
{
    public delegate void Action();

    public static Action Save;
    public static Action Remember;
    public static Action Forget;

    static List<IMementeable> AllMementeables = new List<IMementeable>();

    public static void AddToSaveManager(IMementeable memento)
    {
        Remember += delegate { memento.Remember(); };
        Forget += delegate { memento.Forget(); };
        Save += delegate { memento.Save(); };
        AllMementeables.Add(memento);
    }

    public static void RestartSaveManager()
    {

            Remember = null;
            Forget = null;
            Save = null;
        

        AllMementeables = new List<IMementeable>();
        Debug.Log(AllMementeables.Count);
    }
}
