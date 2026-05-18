namespace LifeSim;

public class PredatorFactory : IAnimalFactory
{
    public Animal Create(World world, Point2 pos)
    {
        return new Predator(world, pos);
    }
}