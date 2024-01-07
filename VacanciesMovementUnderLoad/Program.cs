using VacanciesMovementUnderLoad;

var info1 = new ElementInfo(3, 1, 1);
var pcLattice = new PCLattice(info1, 11);

var atom1 = new Atom(info1, MyVector<double>.Zero);

var a = new MyVector<double>(1, 2, 3);

// atom1.StermerVerletCoordinates=a;
// atom1.PreviousCoordinates = atom1.Coordinates;
// atom1.Coordinates = atom1.StermerVerletCoordinates;
//
// a = new MyVector<double>(3,4,5);
// Console.WriteLine(atom1.PreviousCoordinates);
//
// atom1.StermerVerletCoordinates=a;
// atom1.PreviousCoordinates = atom1.Coordinates;
// atom1.Coordinates = atom1.StermerVerletCoordinates;
//
// a = new MyVector<double>(8,4,5);
// Console.WriteLine(atom1.PreviousCoordinates);

double ar = 1E-16;
Console.WriteLine(ar);

pcLattice.Fill();

pcLattice.RemoveCentralAtom();

Output.CreateLAMMPSAtomicDataFile(pcLattice, @"C:\Users\Denis\Desktop\dataNA.txt");

var simulation = new Simulation(1E-13, 1E-16, pcLattice);
simulation.SimulateAllAtoms();

Output.CreateLAMMPSAtomicDataFile(pcLattice, @"C:\Users\Denis\Desktop\dataNA1.txt");

pcLattice.ApplyExternalForceToAtomByNumber(60, new MyVector<double>(0,0,1));
simulation = new Simulation(1E-13, 1E-16, pcLattice);
simulation.SimulateAllAtoms();

Output.CreateLAMMPSAtomicDataFile(pcLattice, @"C:\Users\Denis\Desktop\dataNA2.txt");