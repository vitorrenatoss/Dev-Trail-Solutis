namespace LifeCicle.Services;

public class LifeTimeService : ILifeTimeService
{
    private static int _staticCounter = 0;
    private int _instanceCounter = 0;

    public Guid Id { get; } = Guid.NewGuid();

    public LifeTimeService()
    {
        _staticCounter++;
    }

    public int GetOperationCount()
    {
        return ++_instanceCounter;
    }
}
