using System.Globalization;

namespace VacanciesMovementUnderLoad;

public static class Output
{
    public static void CreateLAMMPSAtomicDataFile(Lattice lattice, string path)
    {
        var sw = new StreamWriter(path, false);

        sw.Write("Some description\n\n");

        sw.Write("{0} atoms\n", lattice.Atoms.Count);
        sw.Write("1 atom types\n");

        sw.Write("Atoms\n\n");

        double scalingMultiplier = 1;
        if (lattice.ElementInfo.EquilibriumDistance < 1)
            scalingMultiplier = Math.Pow(lattice.ElementInfo.EquilibriumDistance, -1);
        int id = 1;
        foreach (var particle in lattice.Atoms)
        {
            sw.WriteLine("{0} 1 {1} {2} {3}", id, particle.Coordinates.X * scalingMultiplier,
                particle.Coordinates.Y * scalingMultiplier, particle.Coordinates.Z * scalingMultiplier);
            ++id;
        }

        sw.Close();
    }
}