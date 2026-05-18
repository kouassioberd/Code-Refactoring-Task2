using System;

namespace LifeSim;

public abstract class Organism
{
    protected Organism(World world, Point2 pos, Gender? gender = null)
    {
        World = world;
        Pos = world.Wrap(pos);
        Gender = gender ?? PickGender();
    }

    public World World { get; }

    public Point2 Pos { get; protected set; }

    public bool IsAlive { get; private set; } = true;

    internal void MoveTo(Point2 newPos)
    {
        Pos = World.Wrap(newPos);
    }

    public void Die()
    {
        IsAlive = false;
    }

    public int Age { get; private set; }

    public abstract char Glyph { get; }

    public virtual ConsoleColor? Color => null;

    public Gender Gender { get; }

    public virtual void Tick() => Age++;

    private const double FemaleChance = 0.5;

    private static Gender PickGender()
    {
        return Rand.Chance(FemaleChance)
            ? Gender.Female
            : Gender.Male;
    }

}
