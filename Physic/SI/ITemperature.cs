using System.Numerics;
using Yannick.Physic.SI.Temperature;

namespace Yannick.Physic.SI;

public interface ITemperature<TSelf> : 
    IAdditionOperators<TSelf, TSelf, TSelf>,
    IAdditionOperators<TSelf, decimal, TSelf>,
    IDecrementOperators<TSelf>,
    IDivisionOperators<TSelf, TSelf, TSelf>,
    IDivisionOperators<TSelf, decimal, TSelf>,
    IEquatable<TSelf>,
    IEqualityOperators<TSelf, TSelf, bool>,
    IEqualityOperators<TSelf, decimal, bool>,
    IIncrementOperators<TSelf>,
    IMultiplyOperators<TSelf, TSelf, TSelf>,
    IMultiplyOperators<TSelf, decimal, TSelf>,
    ISpanFormattable,
    ISpanParsable<TSelf>,
    ISubtractionOperators<TSelf, TSelf, TSelf>,
    ISubtractionOperators<TSelf, decimal, TSelf>,
    IUnaryPlusOperators<TSelf, TSelf>,
    IUnaryNegationOperators<TSelf, TSelf> 
    where TSelf : IAdditionOperators<TSelf, TSelf, TSelf>?, IDecrementOperators<TSelf>?,
    IDivisionOperators<TSelf, TSelf, TSelf>?, IEqualityOperators<TSelf, TSelf, bool>?,
    IIncrementOperators<TSelf>?, IMultiplyOperators<TSelf, TSelf, TSelf>?, ISpanParsable<TSelf>?,
    ISubtractionOperators<TSelf, TSelf, TSelf>?, IUnaryPlusOperators<TSelf, TSelf>?, IUnaryNegationOperators<TSelf, TSelf>?,
    IAdditionOperators<TSelf, decimal, TSelf>?, IDivisionOperators<TSelf, decimal, TSelf>?, IEqualityOperators<TSelf, decimal, bool>?,
    IMultiplyOperators<TSelf, decimal, TSelf>?, ISubtractionOperators<TSelf, decimal, TSelf>?
{
    public Kelvin ToKelvin();
    public static abstract Kelvin ToKelvin(TSelf i);
}