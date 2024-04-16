Sword original = new Sword(MaterialType.Iron, GemstoneType.None, 100, 6);
Sword sword2 = original with { material = MaterialType.Steel, gemstone = GemstoneType.Sapphire };
Sword sword3 = original with { material = MaterialType.Binarium, gemstone = GemstoneType.Bitstone };

Console.WriteLine(original);
Console.WriteLine(sword2);
Console.WriteLine(sword3);


public record Sword(MaterialType material, GemstoneType gemstone, int length, int crossguardWidth);

public enum MaterialType
{
    Wood, Bronze, Iron, Steel, Binarium
}
public enum GemstoneType
{
    None, Emerald, Amber, Sapphire, Diamond, Bitstone
}