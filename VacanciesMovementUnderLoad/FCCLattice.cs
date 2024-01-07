using System.Numerics;

namespace VacanciesMovementUnderLoad;

public class FCCLattice : Lattice
{
    public FCCLattice(MyVector<double> startingPoint, ElementInfo elementInfo, int vertexAmountInEdge) : base(startingPoint, elementInfo, vertexAmountInEdge)
    {
        EdgeLength = ElementInfo.EquilibriumDistance * Math.Sqrt(2);
    }
    
    public FCCLattice(ElementInfo elementInfo, int vertexAmountInEdge) : 
        this(MyVector<double>.Zero, elementInfo, vertexAmountInEdge){}

    protected override void FillWithAtoms()
    {
        Atoms.AddRange(CubicLattice(StartingPoint, VertexAmountInEdge));
        Atoms.AddRange(CubicLattice(
            StartingPoint + new MyVector<double>
                (new[] { EdgeLength / 2, EdgeLength / 2, 0, 0 }),
            VertexAmountInEdge));
        Atoms.AddRange(CubicLattice(
            StartingPoint + new MyVector<double>
                (new[] { EdgeLength / 2, 0, EdgeLength / 2, 0 }),
            VertexAmountInEdge));
        Atoms.AddRange(CubicLattice(
            StartingPoint + new MyVector<double>
                (new[] { 0, EdgeLength / 2, EdgeLength / 2, 0 }),
            VertexAmountInEdge));
    }
}