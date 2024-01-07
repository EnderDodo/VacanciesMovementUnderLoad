namespace VacanciesMovementUnderLoad;

public class Simulation
{
    public double CurrTime { get; set; }
    public double MaxTime { get; set; }
    public double DeltaTime { get; set; }

    public Simulation(double maxTime, double deltaTime)
    {
        CurrTime = 0;
        MaxTime = maxTime;
        DeltaTime = deltaTime;
    }
}