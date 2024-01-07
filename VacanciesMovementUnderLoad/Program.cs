using VacanciesMovementUnderLoad;

var info1 = new ElementInfo(1E-16, 1, 1);
var pcLattice = new PCLattice(info1, 11);

var po = new ElementInfo(5.8872E-14, 1, 1);

pcLattice.Fill();

pcLattice.RemoveCentralAtom();

Output.CreateLAMMPSAtomicDataFile(pcLattice, @"C:\Users\Denis\Desktop\dataNA.txt");

// var simulation = new Simulation(1E-13, 1E-16, pcLattice);
// simulation.SimulateAllAtoms();
//
// Output.CreateLAMMPSAtomicDataFile(pcLattice, @"C:\Users\Denis\Desktop\dataNA1.txt");
//
// pcLattice.ApplyExternalForceToAtomByNumber(60, new MyVector<double>(0,0,1));
// simulation = new Simulation(1E-13, 1E-16, pcLattice);
// simulation.SimulateAllAtoms();
//
// Output.CreateLAMMPSAtomicDataFile(pcLattice, @"C:\Users\Denis\Desktop\dataNA2.txt");