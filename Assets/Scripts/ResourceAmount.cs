public enum ResourceType
{
    Gold,
    Diamond,
    Redstone
}

[System.Serializable]
public class ResourceAmount
{
    public ResourceType resourceType;
    public float value;

    public Stat GetResourceTypeInStat()
    {
        switch (resourceType)
        {
            case ResourceType.Gold:
                return Stat.Gold;
            case ResourceType.Diamond:
                return Stat.Diamond;
            case ResourceType.Redstone:
                return Stat.Redstone;
            default:
                return Stat.Gold;
        }
    }
}