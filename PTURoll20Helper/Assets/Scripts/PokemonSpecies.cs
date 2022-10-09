using System.Collections.Generic;

public enum Type
{
    NORMAL,
    FIRE,
    WATER,
    GRASS,
    ELECTRIC,
    ICE,
    FIGHTING,
    POISON,
    GROUND,
    FLYING,
    PSYCHIC,
    BUG,
    ROCK,
    GHOST,
    DRAGON,
    DARK,
    STEEL,
    FAIRY,
    NONE
}

public class PokemonSpecies
{
    public string Species { get; private set; }
    public StatSet BaseStats { get; private set; }
    public Type Type1 { get; private set; }
    public Type Type2 { get; private set; }

    private List<Capability> capabilities = new List<Capability>();
    public IEnumerable<Capability> Capabilities { get { return capabilities; } }

    public PokemonSpecies(string species, StatSet baseStats, Type type1, Type type2 = Type.NONE)
    {
        Species = species;
        BaseStats = baseStats;
        Type1 = type1;
        Type2 = type2;
    }

    public void AddCapability(Capability capability)
    {
        capabilities.Add(capability);
    }
}
