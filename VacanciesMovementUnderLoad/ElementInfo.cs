namespace VacanciesMovementUnderLoad;

public class ElementInfo
{
    public double EquilibriumDistance { get; }
    public double Epsilon { get; }
    public double AtomicMass { get; }

    public ElementInfo(double equilibriumDistance, double epsilon, double mass)
    {
        EquilibriumDistance = equilibriumDistance;
        Epsilon = epsilon;
        AtomicMass = mass;
    }
}