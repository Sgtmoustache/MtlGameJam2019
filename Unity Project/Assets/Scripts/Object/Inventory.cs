using System.Collections.Generic;

public class Inventory
{
    public static int currentIndex = -1;
    public static List<bool> collectibles = new List<bool>();
    public static bool nightlight;

    public static void Collect(int value)
    {
        nightlight = true;
        collectibles[value] = true;
    }

    public static bool HasCollected(int value)
    {
        return collectibles[value];
    }

    public static int RegisterItem()
    {
        nightlight = false;
        collectibles.Add(false);
        currentIndex++;
        return currentIndex;
    }

    public static bool HasNightlight()
    {
        //nightlight = !nightlight;
        return nightlight;
    }
    public static void ChangeNight()
    {
        nightlight = !nightlight;

    }
}
