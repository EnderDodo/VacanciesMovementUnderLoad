using VacanciesMovementUnderLoad;

var info1 = new ElementInfo(1E-11, 1, 1E-27);
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
