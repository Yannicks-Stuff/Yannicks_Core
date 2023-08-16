using Yannick.Lang.Attribute;

namespace Yannick.Physic.SI;

public enum Prefix
{
    [Suffix("y")] Yocto = -24,
    [Suffix("z")] Zepto = -21,
    [Suffix("a")] Atto = -18,
    [Suffix("f")] Femto = -15,
    [Suffix("p")] Pico = -12,
    [Suffix("n")] Nano = -9,
    [Suffix("u")] Micro = -6,
    [Suffix("m")] Milli = -3,
    [Suffix("c")] Centi = -2,
    [Suffix("d")] Deci = -1,
    [Suffix] None = 0,
    [Suffix("d")] Deca = 1,
    [Suffix("h")] Hecto = 2,
    [Suffix("k")] Kilo = 3,
    [Suffix("m")] Mega = 6,
    [Suffix("g")] Giga = 9,
    [Suffix("t")] Tera = 12,
    [Suffix("p")] Peta = 15,
    [Suffix("e")] Exa = 18,
    [Suffix("z")] Zetta = 21,
    [Suffix("y")] Yotta = 24
}