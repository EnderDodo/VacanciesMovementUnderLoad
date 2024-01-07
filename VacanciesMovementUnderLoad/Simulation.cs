namespace VacanciesMovementUnderLoad;

public class Simulation
{
    public double CurrTime { get; set; }
    public double MaxTime { get; }
    public double DeltaTime { get; }
    public Lattice SimulatedLattice { get; }

    public Simulation(double maxTime, double deltaTime, Lattice simulatedLattice)
    {
        CurrTime = 0;
        MaxTime = maxTime;
        DeltaTime = deltaTime;
        SimulatedLattice = simulatedLattice;
    }
    
    public void SimulateAllAtoms()
    {
        while (CurrTime <= MaxTime)
        {
            Parallel.ForEach(SimulatedLattice.Atoms, SetStermerVerletCoordinates);

            Parallel.ForEach(SimulatedLattice.Atoms, SimulatedLattice.RepositionOneAtom);  //reposition each atom
            
            CurrTime += DeltaTime;
        }
    }
    
    public void SetStermerVerletCoordinates(Atom atom)
    {
        SimulatedLattice.CountInteractionForceSumForOneAtom(atom);
        atom.StermerVerletCoordinates = atom.StermerVerletPosition(CurrTime, DeltaTime);
        atom.InteractionForceSum = MyVector<double>.Zero;
    }

}