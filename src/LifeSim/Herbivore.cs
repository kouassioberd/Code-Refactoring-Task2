namespace LifeSim;

public class Herbivore : Animal
{
    public Herbivore(World world, Point2 pos, Gender? gender = null)
        : base(world, pos, gender)
    {
    }

    private const int HerbivoreVision = 8;
    protected override int Vision => HerbivoreVision;

    private const int HerbivoreMoveCost = 2;
    protected override int MoveCost => HerbivoreMoveCost;

    private const int HerbivoreBiteGain = 18;
    protected override int BiteGain => HerbivoreBiteGain;

    private const int HerbivoreReproduceThreshold = 60;
    protected override int ReproduceThreshold => HerbivoreReproduceThreshold;

    private const int HerbivoreInitialEnergy = 30;
    protected override int InitialEnergy => HerbivoreInitialEnergy;

    protected override char DisplayGlyph => 'h';

    public override System.ConsoleColor? Color => System.ConsoleColor.Yellow;

    protected override Organism? FindPrey() => World.FindNearest<Plant>(Pos, Vision);

    protected override Animal MakeChild(Point2 p) => new Herbivore(World, p);
}
