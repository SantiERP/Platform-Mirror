using UnityEngine;

public static class SaveManager
{
    public delegate void Action();

    public static Action Save;
    public static Action Remember;
    public static Action Forget;

    public static void AddToSaveManager(IMementeable memento)
    {
        Remember += delegate { memento.Remember(); };
        Forget += delegate { memento.Forget(); };
        Save += delegate { memento.Save(); };
    }
}
