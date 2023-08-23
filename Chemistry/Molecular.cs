using System.Text;

namespace Yannick.Chemistry;

/// <summary>
/// Das Molekühl
/// </summary>
public abstract class Molecular
{
    public abstract IReadOnlyList<Atom> Struct { get; }
    public abstract string Name { get; }


    public override string ToString()
    {
        var formula = new StringBuilder();
        var processedSymbols = new HashSet<string>();

        foreach (var atom in Struct)
        {
            if (processedSymbols.Contains(atom.Symbol))
                continue;

            var count = Struct.Count(a => a.Symbol == atom.Symbol);
            formula.Append(atom.Symbol);
            if (count > 1)
                formula.Append(count);
            processedSymbols.Add(atom.Symbol);
        }

        return formula.ToString();
    }
}