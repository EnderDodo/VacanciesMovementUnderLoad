using System.Numerics;

namespace VacanciesMovementUnderLoad;

public class PCLattice : Lattice
{
    public PCLattice(MyVector<double> startingPoint, ElementInfo elementInfo, int vertexAmountInEdge) : base(startingPoint, elementInfo, vertexAmountInEdge)
    {
        EdgeLength = ElementInfo.EquilibriumDistance;
    }
    
    public PCLattice(ElementInfo elementInfo, int vertexAmountInEdge) : 
        this(MyVector<double>.Zero, elementInfo, vertexAmountInEdge){}

    protected override void FillWithAtoms()
    {
        Atoms.AddRange(CubicLattice(StartingPoint, VertexAmountInEdge, true));
    }
}