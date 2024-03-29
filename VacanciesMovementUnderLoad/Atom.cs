﻿namespace VacanciesMovementUnderLoad;

public class Atom
{
    public ElementInfo ElementInfo { get; }
    public MyVector<double> Coordinates { get; set; }
    public MyVector<double> PreviousCoordinates { get; set; }
    public MyVector<double> StermerVerletCoordinates { get; set; }
    
    public List<Atom> ClosestAtoms { get; set; }

    public MyVector<double> InteractionForceSum { get; set; }
    public MyVector<double> ExternalForce { get; set; }
    public double ForceMultiplier { get; set; }

    public Atom(ElementInfo elementInfo, MyVector<double> coordinates)
    {
        ClosestAtoms = new List<Atom>();
        InteractionForceSum = new MyVector<double>(0, 0, 0);
        ExternalForce = new MyVector<double>(0, 0, 0);
        ElementInfo = elementInfo;
        Coordinates = coordinates;
        PreviousCoordinates = coordinates;
        StermerVerletCoordinates = coordinates;
        ForceMultiplier = 1;
    }

    public Atom(ElementInfo elementInfo, MyVector<double> coordinates, double forceMultiplier)
    {
        ClosestAtoms = new List<Atom>();
        InteractionForceSum = new MyVector<double>(0, 0, 0);
        ExternalForce = new MyVector<double>(0, 0, 0);
        ElementInfo = elementInfo;
        Coordinates = coordinates;
        PreviousCoordinates = coordinates;
        StermerVerletCoordinates = coordinates;
        ForceMultiplier = forceMultiplier;
    }

    public MyVector<double> LennardJonesInteractionForce(Atom other)
    {
        if (MyVector<double>.DistanceSquared(Coordinates, other.Coordinates) > 0)
            return -12 * ElementInfo.Epsilon /
                   Math.Pow(MyVector<double>.DistanceSquared(Coordinates, other.Coordinates), 7) *
                   (Math.Pow(MyVector<double>.DistanceSquared(Coordinates, other.Coordinates), 3) -
                    Math.Pow(ElementInfo.EquilibriumDistance, 6)) * Math.Pow(ElementInfo.EquilibriumDistance, 6) *
                   (Coordinates - other.Coordinates);
        return MyVector<double>.Zero;
    }

    public MyVector<double> Acceleration()
    {
        return (InteractionForceSum * ForceMultiplier + ExternalForce) / ElementInfo.AtomicMass;
    }

    public MyVector<double> StermerVerletPosition(double time, double deltaTime)
    {
        return 2 * Coordinates - PreviousCoordinates +
               Acceleration() * Math.Pow(deltaTime, 2);
    }
}