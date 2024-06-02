using UnityEngine;

public static class Extend
{
    static Extend()
    {

    }
    public static Transform DeepFind(this Transform parent, string name)
    {
        Transform child = parent.Find(name);
        if (child == null)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                child = DeepFind(parent.GetChild(i), name);
                if (child != null)
                {
                    return child;
                }
            }
        }
        return child;
    }
}
