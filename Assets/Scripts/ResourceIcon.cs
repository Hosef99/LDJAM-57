using UnityEngine;

[CreateAssetMenu(menuName = "Data/Resource Icon Set")]
public class ResourceIcon : ScriptableObject
{
    public Sprite goldIcon;
    public Sprite redstoneIcon;
    public Sprite diamondIcon;
    public Sprite lapisIcon;
    public Sprite fossil1;

    public Sprite GetIcon(Stat type)
    {
        return type switch
        {
            Stat.Gold => goldIcon,
            Stat.Redstone => redstoneIcon,
            Stat.Diamond => diamondIcon,
            Stat.LapisLazuli => lapisIcon,
            Stat.Fossil1 => fossil1,
            _ => null
        };
    }
}
