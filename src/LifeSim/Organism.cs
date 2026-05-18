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

    protected void MoveTo(Point2 newPos)
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

    private static Gender PickGender() => Rand.Chance(0.5) ? Gender.Female : Gender.Male;
}
