namespace LifeSim;

public class HerbivoreFactory : IAnimalFactory
{
    public Animal Create(World world, Point2 pos)
    {
        return new Herbivore(world, pos);
    }
}