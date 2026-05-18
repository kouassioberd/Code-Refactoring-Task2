namespace LifeSim;

public interface IAnimalFactory
{
	Animal Create(World world, Point2 pos);
}