using System.Numerics;

namespace VacanciesMovementUnderLoad;

public abstract class Lattice
{
    public MyVector<double> StartingPoint { get; }
    public Simulation Simulation { get; set; }
    public ElementInfo ElementInfo { get; }
    public double EdgeLength { get; protected set; }
    public int VertexAmountInEdge { get; }

    public List<Atom> Atoms { get; }

    protected Lattice(ElementInfo elementInfo, int vertexAmountInEdge) :
        this(MyVector<double>.Zero, elementInfo, vertexAmountInEdge)
    {
    }

    protected Lattice(MyVector<double> startingPoint, ElementInfo elementInfo, int vertexAmountInEdge)
    {
        StartingPoint = startingPoint;
        ElementInfo = elementInfo;
        VertexAmountInEdge = vertexAmountInEdge;
        Atoms = new List<Atom>();
    }

    protected List<Atom> CubicLattice(MyVector<double> startingPoint, int vertexAmountInEdge)
    {
        var atoms = new List<Atom>();
        for (int x = 0; x < vertexAmountInEdge; x++)
        {
            for (int y = 0; y < vertexAmountInEdge; y++)
            {
                for (int z = 0; z < vertexAmountInEdge; z++)
                {
                    atoms.Add(new Atom(ElementInfo,
                        new MyVector<double>(new[] { x * EdgeLength, y * EdgeLength, z * EdgeLength, 0 }) +
                        startingPoint));
                }
            }
        }

        return atoms;
    }

    protected abstract void FillWithAtoms();

    public void RemoveAtomByNumber(int n)
    {
        if (Atoms.Count >= 0)
            Atoms.RemoveAt(n);
    }

    public void RemoveCentralAtom()
    {
        if (VertexAmountInEdge % 2 == 1)
            RemoveAtomByNumber((VertexAmountInEdge - 1) / 2 *
                               ((int)Math.Pow(VertexAmountInEdge, 2) + VertexAmountInEdge + 1));
    }

    public void StartSimulation(Simulation simulation)
    {
        Simulation = simulation;
    }

    public void SimulateAllAtoms()
    {
        while (Simulation.CurrTime <= Simulation.MaxTime)
        {
            Parallel.ForEach(Atoms, SetStermerVerletCoordinates);

            Parallel.ForEach(Atoms, RepositionOneAtom);  //reposition each atom
            
            Simulation.CurrTime += Simulation.DeltaTime;
        }
    }

    public void CountInteractionForceSumForOneAtom(Atom atom)
    {
        foreach (var atom2 in Atoms)
        {
            if (atom2 == atom)
                continue;
            atom.InteractionForceSum += atom.LennardJonesInteractionForce(atom2);
        }
    }

    public void SetStermerVerletCoordinates(Atom atom)
    {
        CountInteractionForceSumForOneAtom(atom);
        atom.StermerVerletCoordinates = atom.StermerVerletPosition(Simulation.CurrTime, Simulation.DeltaTime);
        atom.InteractionForceSum = MyVector<double>.Zero;
    }

    public void RepositionOneAtom(Atom atom)
    {
        atom.PreviousCoordinates = atom.Coordinates; //weak point
        atom.Coordinates = atom.StermerVerletCoordinates;
    }
}