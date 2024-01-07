using System.Numerics;

namespace VacanciesMovementUnderLoad;

public class BCCLattice : Lattice
{
    public BCCLattice(MyVector<double> startingPoint, ElementInfo elementInfo, int vertexAmountInEdge) : base(
        startingPoint, elementInfo, vertexAmountInEdge)
    {
        EdgeLength = ElementInfo.EquilibriumDistance * 2 / Math.Sqrt(3);
    }

    public BCCLattice(ElementInfo elementInfo, int vertexAmountInEdge) :
        this(MyVector<double>.Zero, elementInfo, vertexAmountInEdge)
    {
    }

    protected override void FillWithAtoms()
    {
        Atoms.AddRange(CubicLattice(StartingPoint, VertexAmountInEdge));
        Atoms.AddRange(CubicLattice(
            StartingPoint + new MyVector<double>
                (new[] { EdgeLength / 2, EdgeLength / 2, EdgeLength / 2, 0 }),
            VertexAmountInEdge - 1));
    }
}