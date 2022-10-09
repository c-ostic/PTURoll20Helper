using System.Collections.Generic;

public class Pokemon
{
    public PokemonSpecies Species { get; private set; }
    public string Nickname { get; private set; }
    public int Level { get; private set; }
    public string Gender { get; private set; }
    public string Nature { get; private set; }
    public StatSet AllocatedStats { get; private set; }

    private List<Move> moveSet = new List<Move>();
    private List<Ability> abilities = new List<Ability>();

    public Pokemon(PokemonSpecies species, string nickname, int level, string gender, string nature, StatSet allocated)
    {
        Species = species;
        Nickname = nickname;
        Level = level;
        Gender = gender;
        Nature = nature;
        AllocatedStats = allocated;
    }

    public void AddMove(Move move)
    {
        moveSet.Add(move);
    }

    public void AddAbility(Ability ability)
    {
        abilities.Add(ability);
    }
}
