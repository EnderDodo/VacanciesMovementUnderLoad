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
        for (int z = 0; z < vertexAmountInEdge; z++)
        {
            for (int y = 0; y < vertexAmountInEdge; y++)
            {
                for (int x = 0; x < vertexAmountInEdge; x++)
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

    public void Fill()
    {
        FillWithAtoms();
        Parallel.ForEach(Atoms, FillClosestAtoms);
    }

    public void RemoveAtomByNumber(int n)
    {
        if (Atoms.Count >= 0 && n < Atoms.Count && n >= 0)
            Atoms.RemoveAt(n);
    }

    public void RemoveCentralAtom()
    {
        if (VertexAmountInEdge % 2 == 1)
            RemoveAtomByNumber((VertexAmountInEdge - 1) / 2 *
                               ((int)Math.Pow(VertexAmountInEdge, 2) + VertexAmountInEdge + 1));
    }

    public void FillClosestAtoms(Atom atom)
    {
        foreach (var atom1 in Atoms)
        {
            if (atom1 == atom)
                continue;
            if (MyVector<double>.DistanceSquared(atom.Coordinates, atom1.Coordinates) <=
                4 * ElementInfo.EquilibriumDistance)
            {
                atom.ClosestAtoms.Add(atom1);
            }
        }
    }
    
    public void CountInteractionForceSumForOneAtom(Atom atom)
    {
        foreach (var atom2 in atom.ClosestAtoms)
        {
            atom.InteractionForceSum += atom.LennardJonesInteractionForce(atom2);
        }
    }

    public void RepositionOneAtom(Atom atom)
    {
        atom.PreviousCoordinates = atom.Coordinates; //weak point
        atom.Coordinates = atom.StermerVerletCoordinates;
    }

    public void ApplyExternalForceToAtomByNumber(int n, MyVector<double> force)
    {
        if (Atoms.Count > 0 && n < Atoms.Count && n >= 0)
            Atoms.ElementAt(n).ExternalForce = force;
    }
    
    //public void ReleaseExternalForceFromAllAtoms()
}