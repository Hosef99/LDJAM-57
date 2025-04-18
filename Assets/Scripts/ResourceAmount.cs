public enum ResourceType
{
    gold,
    diamond,
    redstone
}

[System.Serializable]
public class ResourceAmount
{
    public ResourceType resourceType;
    public float value;
}