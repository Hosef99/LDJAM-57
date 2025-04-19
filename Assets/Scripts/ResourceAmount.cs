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
}