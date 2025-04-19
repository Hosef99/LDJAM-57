using UnityEngine;

[CreateAssetMenu(menuName = "Data/Resource Icon Set")]
public class ResourceIcon : ScriptableObject
{
    public Sprite goldIcon;
    public Sprite redstoneIcon;
    public Sprite diamondIcon;

    public Sprite GetIcon(ResourceType type)
    {
        return type switch
        {
            ResourceType.Gold => goldIcon,
            ResourceType.Redstone => redstoneIcon,
            ResourceType.Diamond => diamondIcon,
            _ => null
        };
    }
}
