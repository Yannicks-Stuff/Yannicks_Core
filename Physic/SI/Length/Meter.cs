using System.Globalization;
using System.Runtime.InteropServices;
using Yannick.Extensions.EnumExtensions;
using Yannick.Lang.Attribute;
using Unit = Yannick.Physic.SI.Converter;

namespace Yannick.Physic.SI.Length;

/// <summary>
/// Represent the si unit for length
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Meter : INone
{
    internal readonly decimal m_value; // Do not rename (binary serialization)

    public Meter(decimal val, Prefix from = Prefix.None)
    {
        m_value = from == Prefix.None ? val : Unit.BaseValue(val, from);
    }

    public decimal Convert(Prefix target) => Unit.Convert(m_value, INone.Prefix, target);

    public static implicit operator Meter(decimal val) => new(val);

    public static implicit operator Meter<IYotta>(Meter m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Yotta));
    public static implicit operator Meter<IZetta>(Meter m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Zetta));
    public static implicit operator Meter<IExa>(Meter m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Exa));
    public static implicit operator Meter<IPeta>(Meter m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Peta));
    public static implicit operator Meter<ITera>(Meter m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Tera));
    public static implicit operator Meter<IGiga>(Meter m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Giga));
    public static implicit operator Meter<IMega>(Meter m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Mega));
    public static implicit operator Meter<IKilo>(Meter m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Kilo));
    public static implicit operator Meter<IHecto>(Meter m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Hecto));
    public static implicit operator Meter<IDeca>(Meter m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Deca));
    public static implicit operator Meter<INone>(Meter m) => new(m.m_value);
    public static implicit operator Meter<IDeci>(Meter m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Deci));
    public static implicit operator Meter<ICenti>(Meter m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Centi));
    public static implicit operator Meter<IMilli>(Meter m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Milli));
    public static implicit operator Meter<IMicro>(Meter m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Micro));
    public static implicit operator Meter<INano>(Meter m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Nano));
    public static implicit operator Meter<IPico>(Meter m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Pico));
    public static implicit operator Meter<IFemto>(Meter m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Femto));
    public static implicit operator Meter<IAtto>(Meter m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Atto));
    public static implicit operator Meter<IZepto>(Meter m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Zepto));
    public static implicit operator Meter<IYocto>(Meter m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Yocto));


    #region PLUS

    public static Meter operator +(Meter a, Meter<IYotta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Meter operator +(Meter a, Meter<IZetta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Meter operator +(Meter a, Meter<IExa> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Meter operator +(Meter a, Meter<IPeta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Meter operator +(Meter a, Meter<ITera> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Meter operator +(Meter a, Meter<IGiga> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Meter operator +(Meter a, Meter<IMega> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Meter operator +(Meter a, Meter<IKilo> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Meter operator +(Meter a, Meter<IHecto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Meter operator +(Meter a, Meter<IDeca> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Meter operator +(Meter a, Meter<INone> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Meter operator +(Meter a, Meter<IDeci> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Meter operator +(Meter a, Meter<ICenti> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Meter operator +(Meter a, Meter<IMilli> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Meter operator +(Meter a, Meter<IMicro> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Meter operator +(Meter a, Meter<INano> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Meter operator +(Meter a, Meter<IPico> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Meter operator +(Meter a, Meter<IFemto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Meter operator +(Meter a, Meter<IAtto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Meter operator +(Meter a, Meter<IZepto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region MINUS

    public static Meter operator -(Meter a, Meter<IYotta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Meter operator -(Meter a, Meter<IZetta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Meter operator -(Meter a, Meter<IExa> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Meter operator -(Meter a, Meter<IPeta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Meter operator -(Meter a, Meter<ITera> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Meter operator -(Meter a, Meter<IGiga> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Meter operator -(Meter a, Meter<IMega> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Meter operator -(Meter a, Meter<IKilo> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Meter operator -(Meter a, Meter<IHecto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Meter operator -(Meter a, Meter<IDeca> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Meter operator -(Meter a, Meter<INone> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Meter operator -(Meter a, Meter<IDeci> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Meter operator -(Meter a, Meter<ICenti> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Meter operator -(Meter a, Meter<IMilli> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Meter operator -(Meter a, Meter<IMicro> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Meter operator -(Meter a, Meter<INano> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Meter operator -(Meter a, Meter<IPico> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Meter operator -(Meter a, Meter<IFemto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Meter operator -(Meter a, Meter<IAtto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Meter operator -(Meter a, Meter<IZepto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region MULTI

    public static Meter operator *(Meter a, Meter<IYotta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Meter operator *(Meter a, Meter<IZetta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Meter operator *(Meter a, Meter<IExa> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Meter operator *(Meter a, Meter<IPeta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Meter operator *(Meter a, Meter<ITera> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Meter operator *(Meter a, Meter<IGiga> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Meter operator *(Meter a, Meter<IMega> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Meter operator *(Meter a, Meter<IKilo> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Meter operator *(Meter a, Meter<IHecto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Meter operator *(Meter a, Meter<IDeca> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Meter operator *(Meter a, Meter<INone> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Meter operator *(Meter a, Meter<IDeci> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Meter operator *(Meter a, Meter<ICenti> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Meter operator *(Meter a, Meter<IMilli> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Meter operator *(Meter a, Meter<IMicro> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Meter operator *(Meter a, Meter<INano> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Meter operator *(Meter a, Meter<IPico> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Meter operator *(Meter a, Meter<IFemto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Meter operator *(Meter a, Meter<IAtto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Meter operator *(Meter a, Meter<IZepto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region DIV

    public static Meter operator /(Meter a, Meter<IYotta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Meter operator /(Meter a, Meter<IZetta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Meter operator /(Meter a, Meter<IExa> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Meter operator /(Meter a, Meter<IPeta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Meter operator /(Meter a, Meter<ITera> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Meter operator /(Meter a, Meter<IGiga> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Meter operator /(Meter a, Meter<IMega> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Meter operator /(Meter a, Meter<IKilo> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Meter operator /(Meter a, Meter<IHecto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Meter operator /(Meter a, Meter<IDeca> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Meter operator /(Meter a, Meter<INone> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Meter operator /(Meter a, Meter<IDeci> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Meter operator /(Meter a, Meter<ICenti> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Meter operator /(Meter a, Meter<IMilli> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Meter operator /(Meter a, Meter<IMicro> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Meter operator /(Meter a, Meter<INano> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Meter operator /(Meter a, Meter<IPico> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Meter operator /(Meter a, Meter<IFemto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Meter operator /(Meter a, Meter<IAtto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Meter operator /(Meter a, Meter<IZepto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region PERCENT

    public static Meter operator %(Meter a, Meter<IYotta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Meter operator %(Meter a, Meter<IZetta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Meter operator %(Meter a, Meter<IExa> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Meter operator %(Meter a, Meter<IPeta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Meter operator %(Meter a, Meter<ITera> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Meter operator %(Meter a, Meter<IGiga> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Meter operator %(Meter a, Meter<IMega> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Meter operator %(Meter a, Meter<IKilo> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Meter operator %(Meter a, Meter<IHecto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Meter operator %(Meter a, Meter<IDeca> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Meter operator %(Meter a, Meter<INone> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Meter operator %(Meter a, Meter<IDeci> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Meter operator %(Meter a, Meter<ICenti> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Meter operator %(Meter a, Meter<IMilli> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Meter operator %(Meter a, Meter<IMicro> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Meter operator %(Meter a, Meter<INano> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Meter operator %(Meter a, Meter<IPico> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Meter operator %(Meter a, Meter<IFemto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Meter operator %(Meter a, Meter<IAtto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Meter operator %(Meter a, Meter<IZepto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region EQUAL

    public static bool operator ==(Meter a, Meter<IYotta> i)
        => Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix) == a.m_value;

    public static bool operator !=(Meter a, Meter<IYotta> i)
        => Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix) != a.m_value;

    public static bool operator >(Meter a, Meter<IYotta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator <(Meter a, Meter<IYotta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator >=(Meter a, Meter<IYotta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator <=(Meter a, Meter<IYotta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator ==(Meter a, Meter<IZetta> i)
        => Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix) == a.m_value;

    public static bool operator !=(Meter a, Meter<IZetta> i)
        => Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix) != a.m_value;

    public static bool operator >(Meter a, Meter<IZetta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static bool operator <(Meter a, Meter<IZetta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static bool operator >=(Meter a, Meter<IZetta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static bool operator <=(Meter a, Meter<IZetta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static bool operator ==(Meter a, Meter<IExa> i)
        => Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix) == a.m_value;

    public static bool operator !=(Meter a, Meter<IExa> i)
        => Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix) != a.m_value;

    public static bool operator >(Meter a, Meter<IExa> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator <(Meter a, Meter<IExa> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator >=(Meter a, Meter<IExa> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator <=(Meter a, Meter<IExa> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator ==(Meter a, Meter<IPeta> i)
        => Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix) == a.m_value;

    public static bool operator !=(Meter a, Meter<IPeta> i)
        => Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix) != a.m_value;

    public static bool operator >(Meter a, Meter<IPeta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static bool operator <(Meter a, Meter<IPeta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static bool operator >=(Meter a, Meter<IPeta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static bool operator <=(Meter a, Meter<IPeta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static bool operator ==(Meter a, Meter<ITera> i)
        => Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix) == a.m_value;

    public static bool operator !=(Meter a, Meter<ITera> i)
        => Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix) != a.m_value;

    public static bool operator >(Meter a, Meter<ITera> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static bool operator <(Meter a, Meter<ITera> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static bool operator >=(Meter a, Meter<ITera> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static bool operator <=(Meter a, Meter<ITera> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static bool operator ==(Meter a, Meter<IGiga> i)
        => Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix) == a.m_value;

    public static bool operator !=(Meter a, Meter<IGiga> i)
        => Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix) != a.m_value;

    public static bool operator >(Meter a, Meter<IGiga> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static bool operator <(Meter a, Meter<IGiga> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static bool operator >=(Meter a, Meter<IGiga> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static bool operator <=(Meter a, Meter<IGiga> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static bool operator ==(Meter a, Meter<IMega> i)
        => Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix) == a.m_value;

    public static bool operator !=(Meter a, Meter<IMega> i)
        => Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix) != a.m_value;

    public static bool operator >(Meter a, Meter<IMega> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static bool operator <(Meter a, Meter<IMega> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static bool operator >=(Meter a, Meter<IMega> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static bool operator <=(Meter a, Meter<IMega> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static bool operator ==(Meter a, Meter<IKilo> i)
        => Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix) == a.m_value;

    public static bool operator !=(Meter a, Meter<IKilo> i)
        => Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix) != a.m_value;

    public static bool operator >(Meter a, Meter<IKilo> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static bool operator <(Meter a, Meter<IKilo> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static bool operator >=(Meter a, Meter<IKilo> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static bool operator <=(Meter a, Meter<IKilo> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static bool operator ==(Meter a, Meter<IHecto> i)
        => Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix) == a.m_value;

    public static bool operator !=(Meter a, Meter<IHecto> i)
        => Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix) != a.m_value;

    public static bool operator >(Meter a, Meter<IHecto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static bool operator <(Meter a, Meter<IHecto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static bool operator >=(Meter a, Meter<IHecto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static bool operator <=(Meter a, Meter<IHecto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static bool operator ==(Meter a, Meter<IDeca> i)
        => Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix) == a.m_value;

    public static bool operator !=(Meter a, Meter<IDeca> i)
        => Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix) != a.m_value;

    public static bool operator >(Meter a, Meter<IDeca> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static bool operator <(Meter a, Meter<IDeca> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static bool operator >=(Meter a, Meter<IDeca> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static bool operator <=(Meter a, Meter<IDeca> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static bool operator ==(Meter a, Meter<INone> i)
        => Unit.Convert(i.m_value, Prefix.None, INone.Prefix) == a.m_value;

    public static bool operator !=(Meter a, Meter<INone> i)
        => Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix) != a.m_value;

    public static bool operator >(Meter a, Meter<INone> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static bool operator <(Meter a, Meter<INone> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static bool operator >=(Meter a, Meter<INone> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static bool operator <=(Meter a, Meter<INone> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static bool operator ==(Meter a, Meter<IDeci> i)
        => Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix) == a.m_value;

    public static bool operator !=(Meter a, Meter<IDeci> i)
        => Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix) != a.m_value;

    public static bool operator >(Meter a, Meter<IDeci> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static bool operator <(Meter a, Meter<IDeci> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static bool operator >=(Meter a, Meter<IDeci> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static bool operator <=(Meter a, Meter<IDeci> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static bool operator ==(Meter a, Meter<ICenti> i)
        => Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix) == a.m_value;

    public static bool operator !=(Meter a, Meter<ICenti> i)
        => Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix) != a.m_value;

    public static bool operator >(Meter a, Meter<ICenti> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static bool operator <(Meter a, Meter<ICenti> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static bool operator >=(Meter a, Meter<ICenti> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static bool operator <=(Meter a, Meter<ICenti> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static bool operator ==(Meter a, Meter<IMilli> i)
        => Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix) == a.m_value;

    public static bool operator !=(Meter a, Meter<IMilli> i)
        => Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix) != a.m_value;

    public static bool operator >(Meter a, Meter<IMilli> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static bool operator <(Meter a, Meter<IMilli> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static bool operator >=(Meter a, Meter<IMilli> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static bool operator <=(Meter a, Meter<IMilli> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static bool operator ==(Meter a, Meter<IMicro> i)
        => Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix) == a.m_value;

    public static bool operator !=(Meter a, Meter<IMicro> i)
        => Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix) != a.m_value;

    public static bool operator >(Meter a, Meter<IMicro> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static bool operator <(Meter a, Meter<IMicro> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static bool operator >=(Meter a, Meter<IMicro> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static bool operator <=(Meter a, Meter<IMicro> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static bool operator ==(Meter a, Meter<INano> i)
        => Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix) == a.m_value;

    public static bool operator !=(Meter a, Meter<INano> i)
        => Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix) != a.m_value;

    public static bool operator >(Meter a, Meter<INano> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static bool operator <(Meter a, Meter<INano> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static bool operator >=(Meter a, Meter<INano> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static bool operator <=(Meter a, Meter<INano> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static bool operator ==(Meter a, Meter<IPico> i)
        => Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix) == a.m_value;

    public static bool operator !=(Meter a, Meter<IPico> i)
        => Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix) != a.m_value;

    public static bool operator >(Meter a, Meter<IPico> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static bool operator <(Meter a, Meter<IPico> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static bool operator >=(Meter a, Meter<IPico> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static bool operator <=(Meter a, Meter<IPico> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static bool operator ==(Meter a, Meter<IFemto> i)
        => Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix) == a.m_value;

    public static bool operator !=(Meter a, Meter<IFemto> i)
        => Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix) != a.m_value;

    public static bool operator >(Meter a, Meter<IFemto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static bool operator <(Meter a, Meter<IFemto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static bool operator >=(Meter a, Meter<IFemto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static bool operator <=(Meter a, Meter<IFemto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static bool operator ==(Meter a, Meter<IAtto> i)
        => Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix) == a.m_value;

    public static bool operator !=(Meter a, Meter<IAtto> i)
        => Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix) != a.m_value;

    public static bool operator >(Meter a, Meter<IAtto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static bool operator <(Meter a, Meter<IAtto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static bool operator >=(Meter a, Meter<IAtto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static bool operator <=(Meter a, Meter<IAtto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static bool operator ==(Meter a, Meter<IZepto> i)
        => Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix) == a.m_value;

    public static bool operator !=(Meter a, Meter<IZepto> i)
        => Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix) != a.m_value;

    public static bool operator >(Meter a, Meter<IZepto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    public static bool operator <(Meter a, Meter<IZepto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    public static bool operator >=(Meter a, Meter<IZepto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    public static bool operator <=(Meter a, Meter<IZepto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    public override bool Equals(object? obj) => m_value.Equals(obj) && INone.Prefix.Equals(obj);

    public override int GetHashCode() => HashCode.Combine(m_value, INone.Prefix);

    public override string ToString() => m_value.ToString(NumberFormatInfo.CurrentInfo) + "m";
}

/// <summary>
/// Represent the si unit for length
/// </summary>
/// <typeparam name="T">The SI Prefix</typeparam>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Meter<T> where T : SI
{
    internal readonly decimal m_value; // Do not rename (binary serialization)

    public Meter(decimal val)
    {
        m_value = val;
    }

    /// <summary>
    /// Get the Prefix from T
    /// </summary>
    /// <exception cref="InvalidOperationException">If no associate prefix found</exception>
    public static Prefix Prefix
    {
        get
        {
            var t = typeof(T);

            if (t == typeof(IYotta))
                return IYotta.Prefix;
            if (t == typeof(IZetta))
                return IZetta.Prefix;
            if (t == typeof(IExa))
                return IExa.Prefix;
            if (t == typeof(IPeta))
                return IPeta.Prefix;
            if (t == typeof(ITera))
                return ITera.Prefix;
            if (t == typeof(IGiga))
                return IGiga.Prefix;
            if (t == typeof(IMega))
                return IMega.Prefix;
            if (t == typeof(IKilo))
                return IKilo.Prefix;
            if (t == typeof(IHecto))
                return IHecto.Prefix;
            if (t == typeof(IDeca))
                return IDeca.Prefix;
            if (t == typeof(INone))
                return INone.Prefix;
            if (t == typeof(IDeci))
                return IDeci.Prefix;
            if (t == typeof(ICenti))
                return ICenti.Prefix;
            if (t == typeof(IMilli))
                return IMilli.Prefix;
            if (t == typeof(IMicro))
                return IMicro.Prefix;
            if (t == typeof(INano))
                return INano.Prefix;
            if (t == typeof(IPico))
                return IPico.Prefix;
            if (t == typeof(IFemto))
                return IFemto.Prefix;
            if (t == typeof(IAtto))
                return IAtto.Prefix;
            if (t == typeof(IZepto))
                return IZepto.Prefix;
            throw new InvalidOperationException("Invalid prefix type.");
        }
    }

    public static implicit operator Meter<T>(decimal val) => new(val);

    #region IMPL_METER

    public static implicit operator Meter<T>(Meter<IYotta> i)
    {
        return Prefix switch
        {
            Prefix.Yotta => new Meter<T>(i.m_value),
            Prefix.None => new Meter<T>(Unit.BaseValue(i.m_value, Prefix.Yotta)),
            _ => new Meter<T>(Unit.Convert(i.m_value, Prefix.Yotta, Prefix))
        };
    }

    public static implicit operator Meter<T>(Meter<IZetta> i)
    {
        return Prefix switch
        {
            Prefix.Zetta => new Meter<T>(i.m_value),
            Prefix.None => new Meter<T>(Unit.BaseValue(i.m_value, Prefix.Zetta)),
            _ => new Meter<T>(Unit.Convert(i.m_value, Prefix.Zetta, Prefix))
        };
    }

    public static implicit operator Meter<T>(Meter<IExa> i)
    {
        return Prefix switch
        {
            Prefix.Exa => new Meter<T>(i.m_value),
            Prefix.None => new Meter<T>(Unit.BaseValue(i.m_value, Prefix.Exa)),
            _ => new Meter<T>(Unit.Convert(i.m_value, Prefix.Exa, Prefix))
        };
    }

    public static implicit operator Meter<T>(Meter<IPeta> i)
    {
        return Prefix switch
        {
            Prefix.Peta => new Meter<T>(i.m_value),
            Prefix.None => new Meter<T>(Unit.BaseValue(i.m_value, Prefix.Peta)),
            _ => new Meter<T>(Unit.Convert(i.m_value, Prefix.Peta, Prefix))
        };
    }

    public static implicit operator Meter<T>(Meter<ITera> i)
    {
        return Prefix switch
        {
            Prefix.Tera => new Meter<T>(i.m_value),
            Prefix.None => new Meter<T>(Unit.BaseValue(i.m_value, Prefix.Tera)),
            _ => new Meter<T>(Unit.Convert(i.m_value, Prefix.Tera, Prefix))
        };
    }

    public static implicit operator Meter<T>(Meter<IGiga> i)
    {
        return Prefix switch
        {
            Prefix.Giga => new Meter<T>(i.m_value),
            Prefix.None => new Meter<T>(Unit.BaseValue(i.m_value, Prefix.Giga)),
            _ => new Meter<T>(Unit.Convert(i.m_value, Prefix.Giga, Prefix))
        };
    }

    public static implicit operator Meter<T>(Meter<IMega> i)
    {
        return Prefix switch
        {
            Prefix.Mega => new Meter<T>(i.m_value),
            Prefix.None => new Meter<T>(Unit.BaseValue(i.m_value, Prefix.Mega)),
            _ => new Meter<T>(Unit.Convert(i.m_value, Prefix.Mega, Prefix))
        };
    }

    public static implicit operator Meter<T>(Meter<IKilo> i)
    {
        return Prefix switch
        {
            Prefix.Kilo => new Meter<T>(i.m_value),
            Prefix.None => new Meter<T>(Unit.BaseValue(i.m_value, Prefix.Kilo)),
            _ => new Meter<T>(Unit.Convert(i.m_value, Prefix.Kilo, Prefix))
        };
    }

    public static implicit operator Meter<T>(Meter<IHecto> i)
    {
        return Prefix switch
        {
            Prefix.Hecto => new Meter<T>(i.m_value),
            Prefix.None => new Meter<T>(Unit.BaseValue(i.m_value, Prefix.Hecto)),
            _ => new Meter<T>(Unit.Convert(i.m_value, Prefix.Hecto, Prefix))
        };
    }

    public static implicit operator Meter<T>(Meter<IDeca> i)
    {
        return Prefix switch
        {
            Prefix.Deca => new Meter<T>(i.m_value),
            Prefix.None => new Meter<T>(Unit.BaseValue(i.m_value, Prefix.Deca)),
            _ => new Meter<T>(Unit.Convert(i.m_value, Prefix.Deca, Prefix))
        };
    }

    public static implicit operator Meter<T>(Meter<INone> i) => new Meter<T>(i.m_value);

    public static implicit operator Meter<T>(Meter<IDeci> i)
    {
        return Prefix switch
        {
            Prefix.Deci => new Meter<T>(i.m_value),
            Prefix.None => new Meter<T>(Unit.BaseValue(i.m_value, Prefix.Deci)),
            _ => new Meter<T>(Unit.Convert(i.m_value, Prefix.Deci, Prefix))
        };
    }

    public static implicit operator Meter<T>(Meter<ICenti> i)
    {
        return Prefix switch
        {
            Prefix.Centi => new Meter<T>(i.m_value),
            Prefix.None => new Meter<T>(Unit.BaseValue(i.m_value, Prefix.Centi)),
            _ => new Meter<T>(Unit.Convert(i.m_value, Prefix.Centi, Prefix))
        };
    }

    public static implicit operator Meter<T>(Meter<IMilli> i)
    {
        return Prefix switch
        {
            Prefix.Milli => new Meter<T>(i.m_value),
            Prefix.None => new Meter<T>(Unit.BaseValue(i.m_value, Prefix.Milli)),
            _ => new Meter<T>(Unit.Convert(i.m_value, Prefix.Milli, Prefix))
        };
    }

    public static implicit operator Meter<T>(Meter<IMicro> i)
    {
        return Prefix switch
        {
            Prefix.Micro => new Meter<T>(i.m_value),
            Prefix.None => new Meter<T>(Unit.BaseValue(i.m_value, Prefix.Micro)),
            _ => new Meter<T>(Unit.Convert(i.m_value, Prefix.Micro, Prefix))
        };
    }

    public static implicit operator Meter<T>(Meter<INano> i)
    {
        return Prefix switch
        {
            Prefix.Nano => new Meter<T>(i.m_value),
            Prefix.None => new Meter<T>(Unit.BaseValue(i.m_value, Prefix.Nano)),
            _ => new Meter<T>(Unit.Convert(i.m_value, Prefix.Nano, Prefix))
        };
    }

    public static implicit operator Meter<T>(Meter<IPico> i)
    {
        return Prefix switch
        {
            Prefix.Pico => new Meter<T>(i.m_value),
            Prefix.None => new Meter<T>(Unit.BaseValue(i.m_value, Prefix.Pico)),
            _ => new Meter<T>(Unit.Convert(i.m_value, Prefix.Pico, Prefix))
        };
    }

    public static implicit operator Meter<T>(Meter<IFemto> i)
    {
        return Prefix switch
        {
            Prefix.Femto => new Meter<T>(i.m_value),
            Prefix.None => new Meter<T>(Unit.BaseValue(i.m_value, Prefix.Femto)),
            _ => new Meter<T>(Unit.Convert(i.m_value, Prefix.Femto, Prefix))
        };
    }

    public static implicit operator Meter<T>(Meter<IAtto> i)
    {
        return Prefix switch
        {
            Prefix.Atto => new Meter<T>(i.m_value),
            Prefix.None => new Meter<T>(Unit.BaseValue(i.m_value, Prefix.Atto)),
            _ => new Meter<T>(Unit.Convert(i.m_value, Prefix.Atto, Prefix))
        };
    }

    public static implicit operator Meter<T>(Meter<IZepto> i)
    {
        return Prefix switch
        {
            Prefix.Zepto => new Meter<T>(i.m_value),
            Prefix.None => new Meter<T>(Unit.BaseValue(i.m_value, Prefix.Zepto)),
            _ => new Meter<T>(Unit.Convert(i.m_value, Prefix.Zepto, Prefix))
        };
    }

    #endregion

    #region PLUS

    public static Meter<T> operator +(Meter<T> a, Meter<IYotta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Meter<T> operator +(Meter<T> a, Meter<IZetta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Meter<T> operator +(Meter<T> a, Meter<IExa> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Meter<T> operator +(Meter<T> a, Meter<IPeta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Meter<T> operator +(Meter<T> a, Meter<ITera> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Meter<T> operator +(Meter<T> a, Meter<IGiga> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Meter<T> operator +(Meter<T> a, Meter<IMega> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Meter<T> operator +(Meter<T> a, Meter<IKilo> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Meter<T> operator +(Meter<T> a, Meter<IHecto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Meter<T> operator +(Meter<T> a, Meter<IDeca> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Meter<T> operator +(Meter<T> a, Meter<INone> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Meter<T> operator +(Meter<T> a, Meter<IDeci> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Meter<T> operator +(Meter<T> a, Meter<ICenti> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Meter<T> operator +(Meter<T> a, Meter<IMilli> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Meter<T> operator +(Meter<T> a, Meter<IMicro> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Meter<T> operator +(Meter<T> a, Meter<INano> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Meter<T> operator +(Meter<T> a, Meter<IPico> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Meter<T> operator +(Meter<T> a, Meter<IFemto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Meter<T> operator +(Meter<T> a, Meter<IAtto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Meter<T> operator +(Meter<T> a, Meter<IZepto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region MINUS

    public static Meter<T> operator -(Meter<T> a, Meter<IYotta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Meter<T> operator -(Meter<T> a, Meter<IZetta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Meter<T> operator -(Meter<T> a, Meter<IExa> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Meter<T> operator -(Meter<T> a, Meter<IPeta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Meter<T> operator -(Meter<T> a, Meter<ITera> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Meter<T> operator -(Meter<T> a, Meter<IGiga> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Meter<T> operator -(Meter<T> a, Meter<IMega> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Meter<T> operator -(Meter<T> a, Meter<IKilo> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Meter<T> operator -(Meter<T> a, Meter<IHecto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Meter<T> operator -(Meter<T> a, Meter<IDeca> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Meter<T> operator -(Meter<T> a, Meter<INone> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Meter<T> operator -(Meter<T> a, Meter<IDeci> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Meter<T> operator -(Meter<T> a, Meter<ICenti> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Meter<T> operator -(Meter<T> a, Meter<IMilli> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Meter<T> operator -(Meter<T> a, Meter<IMicro> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Meter<T> operator -(Meter<T> a, Meter<INano> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Meter<T> operator -(Meter<T> a, Meter<IPico> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Meter<T> operator -(Meter<T> a, Meter<IFemto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Meter<T> operator -(Meter<T> a, Meter<IAtto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Meter<T> operator -(Meter<T> a, Meter<IZepto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region MULTI

    public static Meter<T> operator *(Meter<T> a, Meter<IYotta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Meter<T> operator *(Meter<T> a, Meter<IZetta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Meter<T> operator *(Meter<T> a, Meter<IExa> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Meter<T> operator *(Meter<T> a, Meter<IPeta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Meter<T> operator *(Meter<T> a, Meter<ITera> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Meter<T> operator *(Meter<T> a, Meter<IGiga> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Meter<T> operator *(Meter<T> a, Meter<IMega> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Meter<T> operator *(Meter<T> a, Meter<IKilo> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Meter<T> operator *(Meter<T> a, Meter<IHecto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Meter<T> operator *(Meter<T> a, Meter<IDeca> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Meter<T> operator *(Meter<T> a, Meter<INone> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Meter<T> operator *(Meter<T> a, Meter<IDeci> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Meter<T> operator *(Meter<T> a, Meter<ICenti> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Meter<T> operator *(Meter<T> a, Meter<IMilli> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Meter<T> operator *(Meter<T> a, Meter<IMicro> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Meter<T> operator *(Meter<T> a, Meter<INano> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Meter<T> operator *(Meter<T> a, Meter<IPico> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Meter<T> operator *(Meter<T> a, Meter<IFemto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Meter<T> operator *(Meter<T> a, Meter<IAtto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Meter<T> operator *(Meter<T> a, Meter<IZepto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region DIV

    public static Meter<T> operator /(Meter<T> a, Meter<IYotta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Meter<T> operator /(Meter<T> a, Meter<IZetta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Meter<T> operator /(Meter<T> a, Meter<IExa> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Meter<T> operator /(Meter<T> a, Meter<IPeta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Meter<T> operator /(Meter<T> a, Meter<ITera> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Meter<T> operator /(Meter<T> a, Meter<IGiga> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Meter<T> operator /(Meter<T> a, Meter<IMega> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Meter<T> operator /(Meter<T> a, Meter<IKilo> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Meter<T> operator /(Meter<T> a, Meter<IHecto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Meter<T> operator /(Meter<T> a, Meter<IDeca> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Meter<T> operator /(Meter<T> a, Meter<INone> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Meter<T> operator /(Meter<T> a, Meter<IDeci> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Meter<T> operator /(Meter<T> a, Meter<ICenti> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Meter<T> operator /(Meter<T> a, Meter<IMilli> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Meter<T> operator /(Meter<T> a, Meter<IMicro> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Meter<T> operator /(Meter<T> a, Meter<INano> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Meter<T> operator /(Meter<T> a, Meter<IPico> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Meter<T> operator /(Meter<T> a, Meter<IFemto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Meter<T> operator /(Meter<T> a, Meter<IAtto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Meter<T> operator /(Meter<T> a, Meter<IZepto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region PERCENT

    public static Meter<T> operator %(Meter<T> a, Meter<IYotta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Meter<T> operator %(Meter<T> a, Meter<IZetta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Meter<T> operator %(Meter<T> a, Meter<IExa> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Meter<T> operator %(Meter<T> a, Meter<IPeta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Meter<T> operator %(Meter<T> a, Meter<ITera> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Meter<T> operator %(Meter<T> a, Meter<IGiga> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Meter<T> operator %(Meter<T> a, Meter<IMega> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Meter<T> operator %(Meter<T> a, Meter<IKilo> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Meter<T> operator %(Meter<T> a, Meter<IHecto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Meter<T> operator %(Meter<T> a, Meter<IDeca> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Meter<T> operator %(Meter<T> a, Meter<INone> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Meter<T> operator %(Meter<T> a, Meter<IDeci> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Meter<T> operator %(Meter<T> a, Meter<ICenti> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Meter<T> operator %(Meter<T> a, Meter<IMilli> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Meter<T> operator %(Meter<T> a, Meter<IMicro> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Meter<T> operator %(Meter<T> a, Meter<INano> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Meter<T> operator %(Meter<T> a, Meter<IPico> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Meter<T> operator %(Meter<T> a, Meter<IFemto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Meter<T> operator %(Meter<T> a, Meter<IAtto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Meter<T> operator %(Meter<T> a, Meter<IZepto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region EQUAL

    public static bool operator ==(Meter<T> a, Meter<IYotta> i)
        => Unit.Convert(i.m_value, Prefix.Exa, Prefix) == a.m_value;

    public static bool operator !=(Meter<T> a, Meter<IYotta> i)
        => Unit.Convert(i.m_value, Prefix.Exa, Prefix) != a.m_value;

    public static bool operator >(Meter<T> a, Meter<IYotta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator <(Meter<T> a, Meter<IYotta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator >=(Meter<T> a, Meter<IYotta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator <=(Meter<T> a, Meter<IYotta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator ==(Meter<T> a, Meter<IZetta> i)
        => Unit.Convert(i.m_value, Prefix.Zetta, Prefix) == a.m_value;

    public static bool operator !=(Meter<T> a, Meter<IZetta> i)
        => Unit.Convert(i.m_value, Prefix.Zetta, Prefix) != a.m_value;

    public static bool operator >(Meter<T> a, Meter<IZetta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static bool operator <(Meter<T> a, Meter<IZetta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static bool operator >=(Meter<T> a, Meter<IZetta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static bool operator <=(Meter<T> a, Meter<IZetta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static bool operator ==(Meter<T> a, Meter<IExa> i)
        => Unit.Convert(i.m_value, Prefix.Exa, Prefix) == a.m_value;

    public static bool operator !=(Meter<T> a, Meter<IExa> i)
        => Unit.Convert(i.m_value, Prefix.Exa, Prefix) != a.m_value;

    public static bool operator >(Meter<T> a, Meter<IExa> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator <(Meter<T> a, Meter<IExa> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator >=(Meter<T> a, Meter<IExa> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator <=(Meter<T> a, Meter<IExa> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator ==(Meter<T> a, Meter<IPeta> i)
        => Unit.Convert(i.m_value, Prefix.Peta, Prefix) == a.m_value;

    public static bool operator !=(Meter<T> a, Meter<IPeta> i)
        => Unit.Convert(i.m_value, Prefix.Peta, Prefix) != a.m_value;

    public static bool operator >(Meter<T> a, Meter<IPeta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static bool operator <(Meter<T> a, Meter<IPeta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static bool operator >=(Meter<T> a, Meter<IPeta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static bool operator <=(Meter<T> a, Meter<IPeta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static bool operator ==(Meter<T> a, Meter<ITera> i)
        => Unit.Convert(i.m_value, Prefix.Tera, Prefix) == a.m_value;

    public static bool operator !=(Meter<T> a, Meter<ITera> i)
        => Unit.Convert(i.m_value, Prefix.Tera, Prefix) != a.m_value;

    public static bool operator >(Meter<T> a, Meter<ITera> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static bool operator <(Meter<T> a, Meter<ITera> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static bool operator >=(Meter<T> a, Meter<ITera> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static bool operator <=(Meter<T> a, Meter<ITera> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static bool operator ==(Meter<T> a, Meter<IGiga> i)
        => Unit.Convert(i.m_value, Prefix.Giga, Prefix) == a.m_value;

    public static bool operator !=(Meter<T> a, Meter<IGiga> i)
        => Unit.Convert(i.m_value, Prefix.Giga, Prefix) != a.m_value;

    public static bool operator >(Meter<T> a, Meter<IGiga> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static bool operator <(Meter<T> a, Meter<IGiga> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static bool operator >=(Meter<T> a, Meter<IGiga> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static bool operator <=(Meter<T> a, Meter<IGiga> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static bool operator ==(Meter<T> a, Meter<IMega> i)
        => Unit.Convert(i.m_value, Prefix.Mega, Prefix) == a.m_value;

    public static bool operator !=(Meter<T> a, Meter<IMega> i)
        => Unit.Convert(i.m_value, Prefix.Mega, Prefix) != a.m_value;

    public static bool operator >(Meter<T> a, Meter<IMega> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static bool operator <(Meter<T> a, Meter<IMega> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static bool operator >=(Meter<T> a, Meter<IMega> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static bool operator <=(Meter<T> a, Meter<IMega> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static bool operator ==(Meter<T> a, Meter<IKilo> i)
        => Unit.Convert(i.m_value, Prefix.Kilo, Prefix) == a.m_value;

    public static bool operator !=(Meter<T> a, Meter<IKilo> i)
        => Unit.Convert(i.m_value, Prefix.Kilo, Prefix) != a.m_value;

    public static bool operator >(Meter<T> a, Meter<IKilo> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static bool operator <(Meter<T> a, Meter<IKilo> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static bool operator >=(Meter<T> a, Meter<IKilo> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static bool operator <=(Meter<T> a, Meter<IKilo> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static bool operator ==(Meter<T> a, Meter<IHecto> i)
        => Unit.Convert(i.m_value, Prefix.Hecto, Prefix) == a.m_value;

    public static bool operator !=(Meter<T> a, Meter<IHecto> i)
        => Unit.Convert(i.m_value, Prefix.Hecto, Prefix) != a.m_value;

    public static bool operator >(Meter<T> a, Meter<IHecto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static bool operator <(Meter<T> a, Meter<IHecto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static bool operator >=(Meter<T> a, Meter<IHecto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static bool operator <=(Meter<T> a, Meter<IHecto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static bool operator ==(Meter<T> a, Meter<IDeca> i)
        => Unit.Convert(i.m_value, Prefix.Deca, Prefix) == a.m_value;

    public static bool operator !=(Meter<T> a, Meter<IDeca> i)
        => Unit.Convert(i.m_value, Prefix.Deca, Prefix) != a.m_value;

    public static bool operator >(Meter<T> a, Meter<IDeca> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static bool operator <(Meter<T> a, Meter<IDeca> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static bool operator >=(Meter<T> a, Meter<IDeca> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static bool operator <=(Meter<T> a, Meter<IDeca> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static bool operator ==(Meter<T> a, Meter<INone> i)
        => Unit.Convert(i.m_value, Prefix.None, Prefix) == a.m_value;

    public static bool operator !=(Meter<T> a, Meter<INone> i)
        => Unit.Convert(i.m_value, Prefix.Nano, Prefix) != a.m_value;

    public static bool operator >(Meter<T> a, Meter<INone> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static bool operator <(Meter<T> a, Meter<INone> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static bool operator >=(Meter<T> a, Meter<INone> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static bool operator <=(Meter<T> a, Meter<INone> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static bool operator ==(Meter<T> a, Meter<IDeci> i)
        => Unit.Convert(i.m_value, Prefix.Deci, Prefix) == a.m_value;

    public static bool operator !=(Meter<T> a, Meter<IDeci> i)
        => Unit.Convert(i.m_value, Prefix.Deci, Prefix) != a.m_value;

    public static bool operator >(Meter<T> a, Meter<IDeci> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static bool operator <(Meter<T> a, Meter<IDeci> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static bool operator >=(Meter<T> a, Meter<IDeci> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static bool operator <=(Meter<T> a, Meter<IDeci> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static bool operator ==(Meter<T> a, Meter<ICenti> i)
        => Unit.Convert(i.m_value, Prefix.Centi, Prefix) == a.m_value;

    public static bool operator !=(Meter<T> a, Meter<ICenti> i)
        => Unit.Convert(i.m_value, Prefix.Centi, Prefix) != a.m_value;

    public static bool operator >(Meter<T> a, Meter<ICenti> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static bool operator <(Meter<T> a, Meter<ICenti> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static bool operator >=(Meter<T> a, Meter<ICenti> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static bool operator <=(Meter<T> a, Meter<ICenti> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static bool operator ==(Meter<T> a, Meter<IMilli> i)
        => Unit.Convert(i.m_value, Prefix.Milli, Prefix) == a.m_value;

    public static bool operator !=(Meter<T> a, Meter<IMilli> i)
        => Unit.Convert(i.m_value, Prefix.Milli, Prefix) != a.m_value;

    public static bool operator >(Meter<T> a, Meter<IMilli> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static bool operator <(Meter<T> a, Meter<IMilli> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static bool operator >=(Meter<T> a, Meter<IMilli> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static bool operator <=(Meter<T> a, Meter<IMilli> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static bool operator ==(Meter<T> a, Meter<IMicro> i)
        => Unit.Convert(i.m_value, Prefix.Micro, Prefix) == a.m_value;

    public static bool operator !=(Meter<T> a, Meter<IMicro> i)
        => Unit.Convert(i.m_value, Prefix.Micro, Prefix) != a.m_value;

    public static bool operator >(Meter<T> a, Meter<IMicro> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static bool operator <(Meter<T> a, Meter<IMicro> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static bool operator >=(Meter<T> a, Meter<IMicro> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static bool operator <=(Meter<T> a, Meter<IMicro> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static bool operator ==(Meter<T> a, Meter<INano> i)
        => Unit.Convert(i.m_value, Prefix.Nano, Prefix) == a.m_value;

    public static bool operator !=(Meter<T> a, Meter<INano> i)
        => Unit.Convert(i.m_value, Prefix.Nano, Prefix) != a.m_value;

    public static bool operator >(Meter<T> a, Meter<INano> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static bool operator <(Meter<T> a, Meter<INano> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static bool operator >=(Meter<T> a, Meter<INano> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static bool operator <=(Meter<T> a, Meter<INano> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static bool operator ==(Meter<T> a, Meter<IPico> i)
        => Unit.Convert(i.m_value, Prefix.Pico, Prefix) == a.m_value;

    public static bool operator !=(Meter<T> a, Meter<IPico> i)
        => Unit.Convert(i.m_value, Prefix.Pico, Prefix) != a.m_value;

    public static bool operator >(Meter<T> a, Meter<IPico> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static bool operator <(Meter<T> a, Meter<IPico> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static bool operator >=(Meter<T> a, Meter<IPico> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static bool operator <=(Meter<T> a, Meter<IPico> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static bool operator ==(Meter<T> a, Meter<IFemto> i)
        => Unit.Convert(i.m_value, Prefix.Femto, Prefix) == a.m_value;

    public static bool operator !=(Meter<T> a, Meter<IFemto> i)
        => Unit.Convert(i.m_value, Prefix.Femto, Prefix) != a.m_value;

    public static bool operator >(Meter<T> a, Meter<IFemto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static bool operator <(Meter<T> a, Meter<IFemto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static bool operator >=(Meter<T> a, Meter<IFemto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static bool operator <=(Meter<T> a, Meter<IFemto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static bool operator ==(Meter<T> a, Meter<IAtto> i)
        => Unit.Convert(i.m_value, Prefix.Atto, Prefix) == a.m_value;

    public static bool operator !=(Meter<T> a, Meter<IAtto> i)
        => Unit.Convert(i.m_value, Prefix.Atto, Prefix) != a.m_value;

    public static bool operator >(Meter<T> a, Meter<IAtto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static bool operator <(Meter<T> a, Meter<IAtto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static bool operator >=(Meter<T> a, Meter<IAtto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static bool operator <=(Meter<T> a, Meter<IAtto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static bool operator ==(Meter<T> a, Meter<IZepto> i)
        => Unit.Convert(i.m_value, Prefix.Zepto, Prefix) == a.m_value;

    public static bool operator !=(Meter<T> a, Meter<IZepto> i)
        => Unit.Convert(i.m_value, Prefix.Zepto, Prefix) != a.m_value;

    public static bool operator >(Meter<T> a, Meter<IZepto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    public static bool operator <(Meter<T> a, Meter<IZepto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    public static bool operator >=(Meter<T> a, Meter<IZepto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    public static bool operator <=(Meter<T> a, Meter<IZepto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    public override bool Equals(object? obj) => m_value.Equals(obj) && Prefix.Equals(obj);

    public override int GetHashCode() => HashCode.Combine(m_value, Prefix);

    public override string ToString() => m_value.ToString(NumberFormatInfo.CurrentInfo) +
                                         Prefix.Attribute<SuffixAttribute>()!.Suffix + "m";
}