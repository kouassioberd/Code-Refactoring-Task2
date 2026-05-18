using System;
using System.Collections.Generic;
using System.Linq;

namespace LifeSim;

public abstract class Animal : Organism
{
    protected Animal(World world, Point2 pos, Gender? gender = null)
        : base(world, pos, gender)
    {
    }

    protected abstract int Vision { get; }

    protected abstract int MoveCost { get; }

    protected abstract int BiteGain { get; }

    protected abstract int ReproduceThreshold { get; }

    protected abstract int InitialEnergy { get; }

    protected abstract char DisplayGlyph { get; }

    public override char Glyph => DisplayGlyph;

    public override ConsoleColor? Color => ConsoleColor.White;

    public int Energy { get;private set; }

    public int MaxAge { get;private set; } = 1000;

    public override void Tick()
    {
        base.Tick();

        InitializeEnergy();
        HuntOrMove();
        ConsumeEnergy();
        TryReproduce();
        HandleDeath();
    }

    private void InitializeEnergy()
    {
        if (Age == 1 && Energy == 0)
        {
            Energy = InitialEnergy;
        }
    }

    private void HuntOrMove()
    {
        var prey = FindPrey();
        if (prey != null)
        {
            StepToward(prey.Pos);
            if (AreNeighborsOrSame(Pos, prey.Pos) && prey.IsAlive)
            {
                World.Remove(prey);
                GainEnergy(BiteGain);
            }
        }
        else
        {
            Wander();
        }
    }

    private void ConsumeEnergy()
    {
        ConsumeEnergy(MoveCost);
    }

    private bool CanReproduce()
    {
        return Energy >= ReproduceThreshold &&
               World.EmptyNeighbors8(Pos).Any();
    }

    private void TryReproduce()
    {
        if (!CanReproduce())
        {
            return;
        }
        var empty = World.EmptyNeighbors8(Pos).ToList();
        var child = MakeChild(empty.Pick()!);
        SplitEnergyWithChild();
        World.Add(child);
    }

    private const double DeathChanceAfterMaxAge = 0.02;
    private void HandleDeath()
    {
        if (Energy <= 0 || (Age > MaxAge && Rand.Chance(DeathChanceAfterMaxAge)))
        {
            World.Remove(this);
        }
    }

    protected void GainEnergy(int amount)
    {
        Energy += amount;
    }

    protected void ConsumeEnergy(int amount)
    {
        Energy -= amount;
    }

    private void SplitEnergyWithChild()
    {
        Energy /= 2;
    }

    protected abstract Organism? FindPrey();

    protected abstract Animal MakeChild(Point2 p);

    protected static bool AreNeighborsOrSame(Point2 a, Point2 b) =>
        Math.Abs(a.X - b.X) <= 1 && Math.Abs(a.Y - b.Y) <= 1;

    protected void StepToward(Point2 target)
    {
        var dx = BestToroidalStep(Pos.X, target.X, World.Width);
        var dy = BestToroidalStep(Pos.Y, target.Y, World.Height);

        var candidates = new List<Point2>();
        if (dx != 0)
        {
            candidates.Add(World.Wrap(new Point2(Pos.X + dx, Pos.Y)));
        }

        if (dy != 0)
        {
            candidates.Add(World.Wrap(new Point2(Pos.X, Pos.Y + dy)));
        }

        if (dx != 0 && dy != 0)
        {
            candidates.Add(World.Wrap(new Point2(Pos.X + dx, Pos.Y + dy)));
        }

        var free = candidates.Where(World.IsEmpty).ToList();
        if (free.Count == 0)
        {
            Wander();
            return;
        }

        World.MoveTo(this, free.Pick()!);
    }

    protected void Wander()
    {
        var options = World.EmptyNeighbors8(Pos).ToList();
        if (options.Count > 0)
        {
            World.MoveTo(this, options.Pick()!);
        }
    }

    private static int BestToroidalStep(int from, int to, int size)
    {
        var direct = to - from;
        var wrapA = (to + size) - from;
        var wrapB = to - (from + size);

        var best =
            Math.Abs(direct) <= Math.Abs(wrapA) && Math.Abs(direct) <= Math.Abs(wrapB)
                ? direct
                : Math.Abs(wrapA) < Math.Abs(wrapB)
                    ? wrapA
                    : wrapB;

        return Math.Sign(best);
    }
}
