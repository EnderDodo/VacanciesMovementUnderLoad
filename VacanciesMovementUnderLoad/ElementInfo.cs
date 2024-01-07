namespace VacanciesMovementUnderLoad;

public class ElementInfo
{
    public double EquilibriumDistance { get; }
    public double Epsilon { get; }
    public double AtomicMass { get; }

    public ElementInfo(double distance, double epsilon, double mass)
    {
        EquilibriumDistance = distance;
        Epsilon = epsilon;
        AtomicMass = mass;
    }
}