using System.Collections.Generic;

public class Inventory
{
    public static int currentIndex = -1;
    public static List<bool> collectibles = new List<bool>();

    public static void Collect(int value)
    {
        collectibles[value] = true;
    }

    public static bool HasCollected(int value)
    {
        return collectibles[value];
    }

    public static int RegisterItem()
    {
        collectibles.Add(false);
        currentIndex++;
        return currentIndex;
    }
}
