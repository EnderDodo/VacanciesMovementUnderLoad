using VacanciesMovementUnderLoad;

// var info1 = new ElementInfo(1E-16, 1, 1);
// var pcLattice = new PCLattice(info1, 11);

var po = new ElementInfo(5.8872E-14, 1E-26, 2.08982E-29);
var poLattice = new PCLattice(po, 5);
poLattice.Fill();

// var na = new ElementInfo(7.06002E-14, 1, 2.29897E-28);
// var naLattice = new BCCLattice(na, 11);
// naLattice.Fill();

// var al = new ElementInfo(4.0931E-14, 1, 2.375E-27);
// var alLattice = new FCCLattice(al, 11);
// alLattice.Fill();

poLattice.RemoveCentralAtom();

Output.CreateLAMMPSAtomicDataFile(poLattice, @"C:\Users\Denis\Desktop\po-1.txt");

var simulation = new Simulation(1E-15, 1E-16, poLattice);
simulation.SimulateAllAtoms();

Output.CreateLAMMPSAtomicDataFile(poLattice, @"C:\Users\Denis\Desktop\po-2.txt");

poLattice.ApplyExternalForceToAtomByNumber(31, new MyVector<double>(0,0,1E-300));
simulation = new Simulation(1E-15, 1E-16, poLattice);
simulation.SimulateAllAtoms();

Output.CreateLAMMPSAtomicDataFile(poLattice, @"C:\Users\Denis\Desktop\po-3.txt");