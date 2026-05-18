namespace LifeSim;
using System;

public class Predator : Animal
{
    public Predator(World world, Point2 pos, Gender? gender = null)
        : base(world, pos, gender)
    {
    }

    private const int PredatorVision = 12;
    protected override int Vision => PredatorVision;

    private const int PredatorMoveCost = 3;
    protected override int MoveCost => PredatorMoveCost;

    private const int PredatorBiteGain = 28;
    protected override int BiteGain => PredatorBiteGain;

    private const int PredatorReproduceThreshold = 80;
    protected override int ReproduceThreshold => PredatorReproduceThreshold;

    private const int PredatorInitialEnergy = 40;
    protected override int InitialEnergy => PredatorInitialEnergy;

    protected override char DisplayGlyph => 'W';

    public override ConsoleColor? Color => ConsoleColor.Red;

    protected override Organism? FindPrey() => World.FindNearest<Herbivore>(Pos, Vision);

    private static readonly IAnimalFactory Factory = new PredatorFactory();

    protected override Animal MakeChild(Point2 p)
    {
        return Factory.Create(World, p);
    }
}
