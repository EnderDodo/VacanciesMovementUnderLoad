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

    public void SimulateAllAtoms(bool ifToWrite)
    {
        int i = 0;
        int n = 0;
        while (CurrTime <= MaxTime)
        {
            Parallel.ForEach(SimulatedLattice.Atoms, SetStermerVerletCoordinates);

            Parallel.ForEach(SimulatedLattice.Atoms, SimulatedLattice.RepositionOneAtom); //reposition each atom
            if (ifToWrite)
            {
                i++;
                if (i == 9)
                {
                    Output.CreateLAMMPSAtomicDataFile(SimulatedLattice, @$"C:\Users\Denis\Desktop\test2\po-{n}.txt");
                    n++;
                    i = 0;
                }
            }


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