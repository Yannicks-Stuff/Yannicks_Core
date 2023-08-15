using System.Numerics;
using Yannick.Physic.SI.Temperature;

namespace Yannick.Physic.SI;

public interface ITemperature<TSelf> : 
    IAdditionOperators<TSelf, TSelf, TSelf>,
    IDecrementOperators<TSelf>,
    IDivisionOperators<TSelf, TSelf, TSelf>,
    IEquatable<TSelf>,
    IEqualityOperators<TSelf, TSelf, bool>,
    IIncrementOperators<TSelf>,
    IMultiplyOperators<TSelf, TSelf, TSelf>,
    ISpanFormattable,
    ISpanParsable<TSelf>,
    ISubtractionOperators<TSelf, TSelf, TSelf>,
    IUnaryPlusOperators<TSelf, TSelf>,
    IUnaryNegationOperators<TSelf, TSelf> 
    where TSelf : IAdditionOperators<TSelf, TSelf, TSelf>?, IDecrementOperators<TSelf>?,
    IDivisionOperators<TSelf, TSelf, TSelf>?, IEqualityOperators<TSelf, TSelf, bool>?,
    IIncrementOperators<TSelf>?, IMultiplyOperators<TSelf, TSelf, TSelf>?, ISpanParsable<TSelf>?,
    ISubtractionOperators<TSelf, TSelf, TSelf>?, IUnaryPlusOperators<TSelf, TSelf>?, IUnaryNegationOperators<TSelf, TSelf>?
{
    public Kelvin ToKelvin();
    public static abstract Kelvin ToKelvin(TSelf i);
}