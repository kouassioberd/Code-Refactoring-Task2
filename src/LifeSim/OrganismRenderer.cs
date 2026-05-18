using System;
namespace LifeSim;

public static class OrganismRenderer
{
    public static void ApplyColor(Organism organism)
    {
        if (organism.Color.HasValue)
        {
            Console.ForegroundColor = organism.Color.Value;
        }
    }
}