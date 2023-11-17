using System.Globalization;
using System.Runtime.InteropServices;
using Yannick.Extensions.EnumExtensions;
using Yannick.Lang.Attribute;
using Unit = Yannick.Physic.SI.Converter;

namespace Yannick.Physic.SI.Electricity;

/// <summary>
/// Represent the si unit for electricity
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Ohm : INone
{
    internal readonly decimal m_value; // Do not rename (binary serialization)

    public Ohm(decimal val, Prefix from = Prefix.None)
    {
        m_value = from == Prefix.None ? val : Unit.BaseValue(val, from);
    }

    public decimal Convert(Prefix target) => Unit.Convert(m_value, INone.Prefix, target);

    public static implicit operator Ohm(decimal val) => new(val);

    public static implicit operator Ohm<IYotta>(Ohm m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Yotta));
    public static implicit operator Ohm<IZetta>(Ohm m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Zetta));
    public static implicit operator Ohm<IExa>(Ohm m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Exa));
    public static implicit operator Ohm<IPeta>(Ohm m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Peta));
    public static implicit operator Ohm<ITera>(Ohm m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Tera));
    public static implicit operator Ohm<IGiga>(Ohm m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Giga));
    public static implicit operator Ohm<IMega>(Ohm m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Mega));
    public static implicit operator Ohm<IKilo>(Ohm m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Kilo));
    public static implicit operator Ohm<IHecto>(Ohm m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Hecto));
    public static implicit operator Ohm<IDeca>(Ohm m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Deca));
    public static implicit operator Ohm<INone>(Ohm m) => new(m.m_value);
    public static implicit operator Ohm<IDeci>(Ohm m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Deci));
    public static implicit operator Ohm<ICenti>(Ohm m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Centi));
    public static implicit operator Ohm<IMilli>(Ohm m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Milli));
    public static implicit operator Ohm<IMicro>(Ohm m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Micro));
    public static implicit operator Ohm<INano>(Ohm m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Nano));
    public static implicit operator Ohm<IPico>(Ohm m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Pico));
    public static implicit operator Ohm<IFemto>(Ohm m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Femto));
    public static implicit operator Ohm<IAtto>(Ohm m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Atto));
    public static implicit operator Ohm<IZepto>(Ohm m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Zepto));
    public static implicit operator Ohm<IYocto>(Ohm m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Yocto));


    #region PLUS

    public static Ohm operator +(Ohm a, Ohm<IYotta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Ohm operator +(Ohm a, Ohm<IZetta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Ohm operator +(Ohm a, Ohm<IExa> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Ohm operator +(Ohm a, Ohm<IPeta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Ohm operator +(Ohm a, Ohm<ITera> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Ohm operator +(Ohm a, Ohm<IGiga> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Ohm operator +(Ohm a, Ohm<IMega> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Ohm operator +(Ohm a, Ohm<IKilo> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Ohm operator +(Ohm a, Ohm<IHecto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Ohm operator +(Ohm a, Ohm<IDeca> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Ohm operator +(Ohm a, Ohm<INone> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Ohm operator +(Ohm a, Ohm<IDeci> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Ohm operator +(Ohm a, Ohm<ICenti> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Ohm operator +(Ohm a, Ohm<IMilli> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Ohm operator +(Ohm a, Ohm<IMicro> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Ohm operator +(Ohm a, Ohm<INano> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Ohm operator +(Ohm a, Ohm<IPico> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Ohm operator +(Ohm a, Ohm<IFemto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Ohm operator +(Ohm a, Ohm<IAtto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Ohm operator +(Ohm a, Ohm<IZepto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region MINUS

    public static Ohm operator -(Ohm a, Ohm<IYotta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Ohm operator -(Ohm a, Ohm<IZetta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Ohm operator -(Ohm a, Ohm<IExa> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Ohm operator -(Ohm a, Ohm<IPeta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Ohm operator -(Ohm a, Ohm<ITera> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Ohm operator -(Ohm a, Ohm<IGiga> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Ohm operator -(Ohm a, Ohm<IMega> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Ohm operator -(Ohm a, Ohm<IKilo> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Ohm operator -(Ohm a, Ohm<IHecto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Ohm operator -(Ohm a, Ohm<IDeca> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Ohm operator -(Ohm a, Ohm<INone> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Ohm operator -(Ohm a, Ohm<IDeci> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Ohm operator -(Ohm a, Ohm<ICenti> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Ohm operator -(Ohm a, Ohm<IMilli> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Ohm operator -(Ohm a, Ohm<IMicro> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Ohm operator -(Ohm a, Ohm<INano> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Ohm operator -(Ohm a, Ohm<IPico> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Ohm operator -(Ohm a, Ohm<IFemto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Ohm operator -(Ohm a, Ohm<IAtto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Ohm operator -(Ohm a, Ohm<IZepto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region MULTI

    public static Ohm operator *(Ohm a, Ohm<IYotta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Ohm operator *(Ohm a, Ohm<IZetta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Ohm operator *(Ohm a, Ohm<IExa> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Ohm operator *(Ohm a, Ohm<IPeta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Ohm operator *(Ohm a, Ohm<ITera> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Ohm operator *(Ohm a, Ohm<IGiga> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Ohm operator *(Ohm a, Ohm<IMega> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Ohm operator *(Ohm a, Ohm<IKilo> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Ohm operator *(Ohm a, Ohm<IHecto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Ohm operator *(Ohm a, Ohm<IDeca> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Ohm operator *(Ohm a, Ohm<INone> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Ohm operator *(Ohm a, Ohm<IDeci> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Ohm operator *(Ohm a, Ohm<ICenti> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Ohm operator *(Ohm a, Ohm<IMilli> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Ohm operator *(Ohm a, Ohm<IMicro> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Ohm operator *(Ohm a, Ohm<INano> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Ohm operator *(Ohm a, Ohm<IPico> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Ohm operator *(Ohm a, Ohm<IFemto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Ohm operator *(Ohm a, Ohm<IAtto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Ohm operator *(Ohm a, Ohm<IZepto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region DIV

    public static Ohm operator /(Ohm a, Ohm<IYotta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Ohm operator /(Ohm a, Ohm<IZetta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Ohm operator /(Ohm a, Ohm<IExa> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Ohm operator /(Ohm a, Ohm<IPeta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Ohm operator /(Ohm a, Ohm<ITera> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Ohm operator /(Ohm a, Ohm<IGiga> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Ohm operator /(Ohm a, Ohm<IMega> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Ohm operator /(Ohm a, Ohm<IKilo> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Ohm operator /(Ohm a, Ohm<IHecto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Ohm operator /(Ohm a, Ohm<IDeca> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Ohm operator /(Ohm a, Ohm<INone> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Ohm operator /(Ohm a, Ohm<IDeci> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Ohm operator /(Ohm a, Ohm<ICenti> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Ohm operator /(Ohm a, Ohm<IMilli> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Ohm operator /(Ohm a, Ohm<IMicro> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Ohm operator /(Ohm a, Ohm<INano> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Ohm operator /(Ohm a, Ohm<IPico> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Ohm operator /(Ohm a, Ohm<IFemto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Ohm operator /(Ohm a, Ohm<IAtto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Ohm operator /(Ohm a, Ohm<IZepto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region PERCENT

    public static Ohm operator %(Ohm a, Ohm<IYotta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Ohm operator %(Ohm a, Ohm<IZetta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Ohm operator %(Ohm a, Ohm<IExa> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Ohm operator %(Ohm a, Ohm<IPeta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Ohm operator %(Ohm a, Ohm<ITera> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Ohm operator %(Ohm a, Ohm<IGiga> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Ohm operator %(Ohm a, Ohm<IMega> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Ohm operator %(Ohm a, Ohm<IKilo> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Ohm operator %(Ohm a, Ohm<IHecto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Ohm operator %(Ohm a, Ohm<IDeca> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Ohm operator %(Ohm a, Ohm<INone> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Ohm operator %(Ohm a, Ohm<IDeci> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Ohm operator %(Ohm a, Ohm<ICenti> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Ohm operator %(Ohm a, Ohm<IMilli> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Ohm operator %(Ohm a, Ohm<IMicro> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Ohm operator %(Ohm a, Ohm<INano> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Ohm operator %(Ohm a, Ohm<IPico> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Ohm operator %(Ohm a, Ohm<IFemto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Ohm operator %(Ohm a, Ohm<IAtto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Ohm operator %(Ohm a, Ohm<IZepto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region EQUAL

    public static bool operator ==(Ohm a, Ohm<IYotta> i)
        => Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix) == a.m_value;

    public static bool operator !=(Ohm a, Ohm<IYotta> i)
        => Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix) != a.m_value;

    public static bool operator >(Ohm a, Ohm<IYotta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator <(Ohm a, Ohm<IYotta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator >=(Ohm a, Ohm<IYotta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator <=(Ohm a, Ohm<IYotta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator ==(Ohm a, Ohm<IZetta> i)
        => Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix) == a.m_value;

    public static bool operator !=(Ohm a, Ohm<IZetta> i)
        => Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix) != a.m_value;

    public static bool operator >(Ohm a, Ohm<IZetta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static bool operator <(Ohm a, Ohm<IZetta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static bool operator >=(Ohm a, Ohm<IZetta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static bool operator <=(Ohm a, Ohm<IZetta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static bool operator ==(Ohm a, Ohm<IExa> i)
        => Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix) == a.m_value;

    public static bool operator !=(Ohm a, Ohm<IExa> i)
        => Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix) != a.m_value;

    public static bool operator >(Ohm a, Ohm<IExa> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator <(Ohm a, Ohm<IExa> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator >=(Ohm a, Ohm<IExa> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator <=(Ohm a, Ohm<IExa> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator ==(Ohm a, Ohm<IPeta> i)
        => Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix) == a.m_value;

    public static bool operator !=(Ohm a, Ohm<IPeta> i)
        => Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix) != a.m_value;

    public static bool operator >(Ohm a, Ohm<IPeta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static bool operator <(Ohm a, Ohm<IPeta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static bool operator >=(Ohm a, Ohm<IPeta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static bool operator <=(Ohm a, Ohm<IPeta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static bool operator ==(Ohm a, Ohm<ITera> i)
        => Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix) == a.m_value;

    public static bool operator !=(Ohm a, Ohm<ITera> i)
        => Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix) != a.m_value;

    public static bool operator >(Ohm a, Ohm<ITera> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static bool operator <(Ohm a, Ohm<ITera> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static bool operator >=(Ohm a, Ohm<ITera> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static bool operator <=(Ohm a, Ohm<ITera> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static bool operator ==(Ohm a, Ohm<IGiga> i)
        => Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix) == a.m_value;

    public static bool operator !=(Ohm a, Ohm<IGiga> i)
        => Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix) != a.m_value;

    public static bool operator >(Ohm a, Ohm<IGiga> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static bool operator <(Ohm a, Ohm<IGiga> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static bool operator >=(Ohm a, Ohm<IGiga> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static bool operator <=(Ohm a, Ohm<IGiga> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static bool operator ==(Ohm a, Ohm<IMega> i)
        => Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix) == a.m_value;

    public static bool operator !=(Ohm a, Ohm<IMega> i)
        => Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix) != a.m_value;

    public static bool operator >(Ohm a, Ohm<IMega> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static bool operator <(Ohm a, Ohm<IMega> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static bool operator >=(Ohm a, Ohm<IMega> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static bool operator <=(Ohm a, Ohm<IMega> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static bool operator ==(Ohm a, Ohm<IKilo> i)
        => Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix) == a.m_value;

    public static bool operator !=(Ohm a, Ohm<IKilo> i)
        => Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix) != a.m_value;

    public static bool operator >(Ohm a, Ohm<IKilo> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static bool operator <(Ohm a, Ohm<IKilo> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static bool operator >=(Ohm a, Ohm<IKilo> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static bool operator <=(Ohm a, Ohm<IKilo> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static bool operator ==(Ohm a, Ohm<IHecto> i)
        => Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix) == a.m_value;

    public static bool operator !=(Ohm a, Ohm<IHecto> i)
        => Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix) != a.m_value;

    public static bool operator >(Ohm a, Ohm<IHecto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static bool operator <(Ohm a, Ohm<IHecto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static bool operator >=(Ohm a, Ohm<IHecto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static bool operator <=(Ohm a, Ohm<IHecto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static bool operator ==(Ohm a, Ohm<IDeca> i)
        => Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix) == a.m_value;

    public static bool operator !=(Ohm a, Ohm<IDeca> i)
        => Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix) != a.m_value;

    public static bool operator >(Ohm a, Ohm<IDeca> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static bool operator <(Ohm a, Ohm<IDeca> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static bool operator >=(Ohm a, Ohm<IDeca> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static bool operator <=(Ohm a, Ohm<IDeca> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static bool operator ==(Ohm a, Ohm<INone> i)
        => Unit.Convert(i.m_value, Prefix.None, INone.Prefix) == a.m_value;

    public static bool operator !=(Ohm a, Ohm<INone> i)
        => Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix) != a.m_value;

    public static bool operator >(Ohm a, Ohm<INone> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static bool operator <(Ohm a, Ohm<INone> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static bool operator >=(Ohm a, Ohm<INone> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static bool operator <=(Ohm a, Ohm<INone> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static bool operator ==(Ohm a, Ohm<IDeci> i)
        => Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix) == a.m_value;

    public static bool operator !=(Ohm a, Ohm<IDeci> i)
        => Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix) != a.m_value;

    public static bool operator >(Ohm a, Ohm<IDeci> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static bool operator <(Ohm a, Ohm<IDeci> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static bool operator >=(Ohm a, Ohm<IDeci> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static bool operator <=(Ohm a, Ohm<IDeci> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static bool operator ==(Ohm a, Ohm<ICenti> i)
        => Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix) == a.m_value;

    public static bool operator !=(Ohm a, Ohm<ICenti> i)
        => Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix) != a.m_value;

    public static bool operator >(Ohm a, Ohm<ICenti> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static bool operator <(Ohm a, Ohm<ICenti> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static bool operator >=(Ohm a, Ohm<ICenti> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static bool operator <=(Ohm a, Ohm<ICenti> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static bool operator ==(Ohm a, Ohm<IMilli> i)
        => Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix) == a.m_value;

    public static bool operator !=(Ohm a, Ohm<IMilli> i)
        => Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix) != a.m_value;

    public static bool operator >(Ohm a, Ohm<IMilli> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static bool operator <(Ohm a, Ohm<IMilli> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static bool operator >=(Ohm a, Ohm<IMilli> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static bool operator <=(Ohm a, Ohm<IMilli> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static bool operator ==(Ohm a, Ohm<IMicro> i)
        => Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix) == a.m_value;

    public static bool operator !=(Ohm a, Ohm<IMicro> i)
        => Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix) != a.m_value;

    public static bool operator >(Ohm a, Ohm<IMicro> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static bool operator <(Ohm a, Ohm<IMicro> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static bool operator >=(Ohm a, Ohm<IMicro> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static bool operator <=(Ohm a, Ohm<IMicro> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static bool operator ==(Ohm a, Ohm<INano> i)
        => Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix) == a.m_value;

    public static bool operator !=(Ohm a, Ohm<INano> i)
        => Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix) != a.m_value;

    public static bool operator >(Ohm a, Ohm<INano> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static bool operator <(Ohm a, Ohm<INano> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static bool operator >=(Ohm a, Ohm<INano> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static bool operator <=(Ohm a, Ohm<INano> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static bool operator ==(Ohm a, Ohm<IPico> i)
        => Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix) == a.m_value;

    public static bool operator !=(Ohm a, Ohm<IPico> i)
        => Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix) != a.m_value;

    public static bool operator >(Ohm a, Ohm<IPico> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static bool operator <(Ohm a, Ohm<IPico> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static bool operator >=(Ohm a, Ohm<IPico> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static bool operator <=(Ohm a, Ohm<IPico> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static bool operator ==(Ohm a, Ohm<IFemto> i)
        => Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix) == a.m_value;

    public static bool operator !=(Ohm a, Ohm<IFemto> i)
        => Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix) != a.m_value;

    public static bool operator >(Ohm a, Ohm<IFemto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static bool operator <(Ohm a, Ohm<IFemto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static bool operator >=(Ohm a, Ohm<IFemto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static bool operator <=(Ohm a, Ohm<IFemto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static bool operator ==(Ohm a, Ohm<IAtto> i)
        => Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix) == a.m_value;

    public static bool operator !=(Ohm a, Ohm<IAtto> i)
        => Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix) != a.m_value;

    public static bool operator >(Ohm a, Ohm<IAtto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static bool operator <(Ohm a, Ohm<IAtto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static bool operator >=(Ohm a, Ohm<IAtto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static bool operator <=(Ohm a, Ohm<IAtto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static bool operator ==(Ohm a, Ohm<IZepto> i)
        => Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix) == a.m_value;

    public static bool operator !=(Ohm a, Ohm<IZepto> i)
        => Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix) != a.m_value;

    public static bool operator >(Ohm a, Ohm<IZepto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    public static bool operator <(Ohm a, Ohm<IZepto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    public static bool operator >=(Ohm a, Ohm<IZepto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    public static bool operator <=(Ohm a, Ohm<IZepto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    public override bool Equals(object? obj) => m_value.Equals(obj) && INone.Prefix.Equals(obj);

    public override int GetHashCode() => HashCode.Combine(m_value, INone.Prefix);

    public override string ToString() => m_value.ToString(NumberFormatInfo.CurrentInfo) + "Ω";
}

/// <summary>
/// Represent the si unit for electricity
/// </summary>
/// <typeparam name="T">The SI Prefix</typeparam>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Ohm<T> where T : SI
{
    internal readonly decimal m_value; // Do not rename (binary serialization)

    public Ohm(decimal val)
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

    public static implicit operator Ohm<T>(decimal val) => new(val);

    #region IMPL_Ohm

    public static implicit operator Ohm<T>(Ohm<IYotta> i)
    {
        return Prefix switch
        {
            Prefix.Yotta => new Ohm<T>(i.m_value),
            Prefix.None => new Ohm<T>(Unit.BaseValue(i.m_value, Prefix.Yotta)),
            _ => new Ohm<T>(Unit.Convert(i.m_value, Prefix.Yotta, Prefix))
        };
    }

    public static implicit operator Ohm<T>(Ohm<IZetta> i)
    {
        return Prefix switch
        {
            Prefix.Zetta => new Ohm<T>(i.m_value),
            Prefix.None => new Ohm<T>(Unit.BaseValue(i.m_value, Prefix.Zetta)),
            _ => new Ohm<T>(Unit.Convert(i.m_value, Prefix.Zetta, Prefix))
        };
    }

    public static implicit operator Ohm<T>(Ohm<IExa> i)
    {
        return Prefix switch
        {
            Prefix.Exa => new Ohm<T>(i.m_value),
            Prefix.None => new Ohm<T>(Unit.BaseValue(i.m_value, Prefix.Exa)),
            _ => new Ohm<T>(Unit.Convert(i.m_value, Prefix.Exa, Prefix))
        };
    }

    public static implicit operator Ohm<T>(Ohm<IPeta> i)
    {
        return Prefix switch
        {
            Prefix.Peta => new Ohm<T>(i.m_value),
            Prefix.None => new Ohm<T>(Unit.BaseValue(i.m_value, Prefix.Peta)),
            _ => new Ohm<T>(Unit.Convert(i.m_value, Prefix.Peta, Prefix))
        };
    }

    public static implicit operator Ohm<T>(Ohm<ITera> i)
    {
        return Prefix switch
        {
            Prefix.Tera => new Ohm<T>(i.m_value),
            Prefix.None => new Ohm<T>(Unit.BaseValue(i.m_value, Prefix.Tera)),
            _ => new Ohm<T>(Unit.Convert(i.m_value, Prefix.Tera, Prefix))
        };
    }

    public static implicit operator Ohm<T>(Ohm<IGiga> i)
    {
        return Prefix switch
        {
            Prefix.Giga => new Ohm<T>(i.m_value),
            Prefix.None => new Ohm<T>(Unit.BaseValue(i.m_value, Prefix.Giga)),
            _ => new Ohm<T>(Unit.Convert(i.m_value, Prefix.Giga, Prefix))
        };
    }

    public static implicit operator Ohm<T>(Ohm<IMega> i)
    {
        return Prefix switch
        {
            Prefix.Mega => new Ohm<T>(i.m_value),
            Prefix.None => new Ohm<T>(Unit.BaseValue(i.m_value, Prefix.Mega)),
            _ => new Ohm<T>(Unit.Convert(i.m_value, Prefix.Mega, Prefix))
        };
    }

    public static implicit operator Ohm<T>(Ohm<IKilo> i)
    {
        return Prefix switch
        {
            Prefix.Kilo => new Ohm<T>(i.m_value),
            Prefix.None => new Ohm<T>(Unit.BaseValue(i.m_value, Prefix.Kilo)),
            _ => new Ohm<T>(Unit.Convert(i.m_value, Prefix.Kilo, Prefix))
        };
    }

    public static implicit operator Ohm<T>(Ohm<IHecto> i)
    {
        return Prefix switch
        {
            Prefix.Hecto => new Ohm<T>(i.m_value),
            Prefix.None => new Ohm<T>(Unit.BaseValue(i.m_value, Prefix.Hecto)),
            _ => new Ohm<T>(Unit.Convert(i.m_value, Prefix.Hecto, Prefix))
        };
    }

    public static implicit operator Ohm<T>(Ohm<IDeca> i)
    {
        return Prefix switch
        {
            Prefix.Deca => new Ohm<T>(i.m_value),
            Prefix.None => new Ohm<T>(Unit.BaseValue(i.m_value, Prefix.Deca)),
            _ => new Ohm<T>(Unit.Convert(i.m_value, Prefix.Deca, Prefix))
        };
    }

    public static implicit operator Ohm<T>(Ohm<INone> i) => new Ohm<T>(i.m_value);

    public static implicit operator Ohm<T>(Ohm<IDeci> i)
    {
        return Prefix switch
        {
            Prefix.Deci => new Ohm<T>(i.m_value),
            Prefix.None => new Ohm<T>(Unit.BaseValue(i.m_value, Prefix.Deci)),
            _ => new Ohm<T>(Unit.Convert(i.m_value, Prefix.Deci, Prefix))
        };
    }

    public static implicit operator Ohm<T>(Ohm<ICenti> i)
    {
        return Prefix switch
        {
            Prefix.Centi => new Ohm<T>(i.m_value),
            Prefix.None => new Ohm<T>(Unit.BaseValue(i.m_value, Prefix.Centi)),
            _ => new Ohm<T>(Unit.Convert(i.m_value, Prefix.Centi, Prefix))
        };
    }

    public static implicit operator Ohm<T>(Ohm<IMilli> i)
    {
        return Prefix switch
        {
            Prefix.Milli => new Ohm<T>(i.m_value),
            Prefix.None => new Ohm<T>(Unit.BaseValue(i.m_value, Prefix.Milli)),
            _ => new Ohm<T>(Unit.Convert(i.m_value, Prefix.Milli, Prefix))
        };
    }

    public static implicit operator Ohm<T>(Ohm<IMicro> i)
    {
        return Prefix switch
        {
            Prefix.Micro => new Ohm<T>(i.m_value),
            Prefix.None => new Ohm<T>(Unit.BaseValue(i.m_value, Prefix.Micro)),
            _ => new Ohm<T>(Unit.Convert(i.m_value, Prefix.Micro, Prefix))
        };
    }

    public static implicit operator Ohm<T>(Ohm<INano> i)
    {
        return Prefix switch
        {
            Prefix.Nano => new Ohm<T>(i.m_value),
            Prefix.None => new Ohm<T>(Unit.BaseValue(i.m_value, Prefix.Nano)),
            _ => new Ohm<T>(Unit.Convert(i.m_value, Prefix.Nano, Prefix))
        };
    }

    public static implicit operator Ohm<T>(Ohm<IPico> i)
    {
        return Prefix switch
        {
            Prefix.Pico => new Ohm<T>(i.m_value),
            Prefix.None => new Ohm<T>(Unit.BaseValue(i.m_value, Prefix.Pico)),
            _ => new Ohm<T>(Unit.Convert(i.m_value, Prefix.Pico, Prefix))
        };
    }

    public static implicit operator Ohm<T>(Ohm<IFemto> i)
    {
        return Prefix switch
        {
            Prefix.Femto => new Ohm<T>(i.m_value),
            Prefix.None => new Ohm<T>(Unit.BaseValue(i.m_value, Prefix.Femto)),
            _ => new Ohm<T>(Unit.Convert(i.m_value, Prefix.Femto, Prefix))
        };
    }

    public static implicit operator Ohm<T>(Ohm<IAtto> i)
    {
        return Prefix switch
        {
            Prefix.Atto => new Ohm<T>(i.m_value),
            Prefix.None => new Ohm<T>(Unit.BaseValue(i.m_value, Prefix.Atto)),
            _ => new Ohm<T>(Unit.Convert(i.m_value, Prefix.Atto, Prefix))
        };
    }

    public static implicit operator Ohm<T>(Ohm<IZepto> i)
    {
        return Prefix switch
        {
            Prefix.Zepto => new Ohm<T>(i.m_value),
            Prefix.None => new Ohm<T>(Unit.BaseValue(i.m_value, Prefix.Zepto)),
            _ => new Ohm<T>(Unit.Convert(i.m_value, Prefix.Zepto, Prefix))
        };
    }

    #endregion

    #region PLUS

    public static Ohm<T> operator +(Ohm<T> a, Ohm<IYotta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Ohm<T> operator +(Ohm<T> a, Ohm<IZetta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Ohm<T> operator +(Ohm<T> a, Ohm<IExa> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Ohm<T> operator +(Ohm<T> a, Ohm<IPeta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Ohm<T> operator +(Ohm<T> a, Ohm<ITera> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Ohm<T> operator +(Ohm<T> a, Ohm<IGiga> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Ohm<T> operator +(Ohm<T> a, Ohm<IMega> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Ohm<T> operator +(Ohm<T> a, Ohm<IKilo> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Ohm<T> operator +(Ohm<T> a, Ohm<IHecto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Ohm<T> operator +(Ohm<T> a, Ohm<IDeca> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Ohm<T> operator +(Ohm<T> a, Ohm<INone> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Ohm<T> operator +(Ohm<T> a, Ohm<IDeci> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Ohm<T> operator +(Ohm<T> a, Ohm<ICenti> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Ohm<T> operator +(Ohm<T> a, Ohm<IMilli> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Ohm<T> operator +(Ohm<T> a, Ohm<IMicro> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Ohm<T> operator +(Ohm<T> a, Ohm<INano> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Ohm<T> operator +(Ohm<T> a, Ohm<IPico> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Ohm<T> operator +(Ohm<T> a, Ohm<IFemto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Ohm<T> operator +(Ohm<T> a, Ohm<IAtto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Ohm<T> operator +(Ohm<T> a, Ohm<IZepto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region MINUS

    public static Ohm<T> operator -(Ohm<T> a, Ohm<IYotta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Ohm<T> operator -(Ohm<T> a, Ohm<IZetta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Ohm<T> operator -(Ohm<T> a, Ohm<IExa> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Ohm<T> operator -(Ohm<T> a, Ohm<IPeta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Ohm<T> operator -(Ohm<T> a, Ohm<ITera> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Ohm<T> operator -(Ohm<T> a, Ohm<IGiga> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Ohm<T> operator -(Ohm<T> a, Ohm<IMega> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Ohm<T> operator -(Ohm<T> a, Ohm<IKilo> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Ohm<T> operator -(Ohm<T> a, Ohm<IHecto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Ohm<T> operator -(Ohm<T> a, Ohm<IDeca> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Ohm<T> operator -(Ohm<T> a, Ohm<INone> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Ohm<T> operator -(Ohm<T> a, Ohm<IDeci> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Ohm<T> operator -(Ohm<T> a, Ohm<ICenti> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Ohm<T> operator -(Ohm<T> a, Ohm<IMilli> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Ohm<T> operator -(Ohm<T> a, Ohm<IMicro> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Ohm<T> operator -(Ohm<T> a, Ohm<INano> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Ohm<T> operator -(Ohm<T> a, Ohm<IPico> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Ohm<T> operator -(Ohm<T> a, Ohm<IFemto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Ohm<T> operator -(Ohm<T> a, Ohm<IAtto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Ohm<T> operator -(Ohm<T> a, Ohm<IZepto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region MULTI

    public static Ohm<T> operator *(Ohm<T> a, Ohm<IYotta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Ohm<T> operator *(Ohm<T> a, Ohm<IZetta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Ohm<T> operator *(Ohm<T> a, Ohm<IExa> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Ohm<T> operator *(Ohm<T> a, Ohm<IPeta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Ohm<T> operator *(Ohm<T> a, Ohm<ITera> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Ohm<T> operator *(Ohm<T> a, Ohm<IGiga> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Ohm<T> operator *(Ohm<T> a, Ohm<IMega> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Ohm<T> operator *(Ohm<T> a, Ohm<IKilo> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Ohm<T> operator *(Ohm<T> a, Ohm<IHecto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Ohm<T> operator *(Ohm<T> a, Ohm<IDeca> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Ohm<T> operator *(Ohm<T> a, Ohm<INone> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Ohm<T> operator *(Ohm<T> a, Ohm<IDeci> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Ohm<T> operator *(Ohm<T> a, Ohm<ICenti> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Ohm<T> operator *(Ohm<T> a, Ohm<IMilli> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Ohm<T> operator *(Ohm<T> a, Ohm<IMicro> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Ohm<T> operator *(Ohm<T> a, Ohm<INano> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Ohm<T> operator *(Ohm<T> a, Ohm<IPico> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Ohm<T> operator *(Ohm<T> a, Ohm<IFemto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Ohm<T> operator *(Ohm<T> a, Ohm<IAtto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Ohm<T> operator *(Ohm<T> a, Ohm<IZepto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region DIV

    public static Ohm<T> operator /(Ohm<T> a, Ohm<IYotta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Ohm<T> operator /(Ohm<T> a, Ohm<IZetta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Ohm<T> operator /(Ohm<T> a, Ohm<IExa> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Ohm<T> operator /(Ohm<T> a, Ohm<IPeta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Ohm<T> operator /(Ohm<T> a, Ohm<ITera> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Ohm<T> operator /(Ohm<T> a, Ohm<IGiga> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Ohm<T> operator /(Ohm<T> a, Ohm<IMega> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Ohm<T> operator /(Ohm<T> a, Ohm<IKilo> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Ohm<T> operator /(Ohm<T> a, Ohm<IHecto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Ohm<T> operator /(Ohm<T> a, Ohm<IDeca> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Ohm<T> operator /(Ohm<T> a, Ohm<INone> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Ohm<T> operator /(Ohm<T> a, Ohm<IDeci> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Ohm<T> operator /(Ohm<T> a, Ohm<ICenti> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Ohm<T> operator /(Ohm<T> a, Ohm<IMilli> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Ohm<T> operator /(Ohm<T> a, Ohm<IMicro> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Ohm<T> operator /(Ohm<T> a, Ohm<INano> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Ohm<T> operator /(Ohm<T> a, Ohm<IPico> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Ohm<T> operator /(Ohm<T> a, Ohm<IFemto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Ohm<T> operator /(Ohm<T> a, Ohm<IAtto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Ohm<T> operator /(Ohm<T> a, Ohm<IZepto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region PERCENT

    public static Ohm<T> operator %(Ohm<T> a, Ohm<IYotta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Ohm<T> operator %(Ohm<T> a, Ohm<IZetta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Ohm<T> operator %(Ohm<T> a, Ohm<IExa> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Ohm<T> operator %(Ohm<T> a, Ohm<IPeta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Ohm<T> operator %(Ohm<T> a, Ohm<ITera> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Ohm<T> operator %(Ohm<T> a, Ohm<IGiga> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Ohm<T> operator %(Ohm<T> a, Ohm<IMega> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Ohm<T> operator %(Ohm<T> a, Ohm<IKilo> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Ohm<T> operator %(Ohm<T> a, Ohm<IHecto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Ohm<T> operator %(Ohm<T> a, Ohm<IDeca> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Ohm<T> operator %(Ohm<T> a, Ohm<INone> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Ohm<T> operator %(Ohm<T> a, Ohm<IDeci> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Ohm<T> operator %(Ohm<T> a, Ohm<ICenti> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Ohm<T> operator %(Ohm<T> a, Ohm<IMilli> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Ohm<T> operator %(Ohm<T> a, Ohm<IMicro> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Ohm<T> operator %(Ohm<T> a, Ohm<INano> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Ohm<T> operator %(Ohm<T> a, Ohm<IPico> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Ohm<T> operator %(Ohm<T> a, Ohm<IFemto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Ohm<T> operator %(Ohm<T> a, Ohm<IAtto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Ohm<T> operator %(Ohm<T> a, Ohm<IZepto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region EQUAL

    public static bool operator ==(Ohm<T> a, Ohm<IYotta> i)
        => Unit.Convert(i.m_value, Prefix.Exa, Prefix) == a.m_value;

    public static bool operator !=(Ohm<T> a, Ohm<IYotta> i)
        => Unit.Convert(i.m_value, Prefix.Exa, Prefix) != a.m_value;

    public static bool operator >(Ohm<T> a, Ohm<IYotta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator <(Ohm<T> a, Ohm<IYotta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator >=(Ohm<T> a, Ohm<IYotta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator <=(Ohm<T> a, Ohm<IYotta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator ==(Ohm<T> a, Ohm<IZetta> i)
        => Unit.Convert(i.m_value, Prefix.Zetta, Prefix) == a.m_value;

    public static bool operator !=(Ohm<T> a, Ohm<IZetta> i)
        => Unit.Convert(i.m_value, Prefix.Zetta, Prefix) != a.m_value;

    public static bool operator >(Ohm<T> a, Ohm<IZetta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static bool operator <(Ohm<T> a, Ohm<IZetta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static bool operator >=(Ohm<T> a, Ohm<IZetta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static bool operator <=(Ohm<T> a, Ohm<IZetta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static bool operator ==(Ohm<T> a, Ohm<IExa> i)
        => Unit.Convert(i.m_value, Prefix.Exa, Prefix) == a.m_value;

    public static bool operator !=(Ohm<T> a, Ohm<IExa> i)
        => Unit.Convert(i.m_value, Prefix.Exa, Prefix) != a.m_value;

    public static bool operator >(Ohm<T> a, Ohm<IExa> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator <(Ohm<T> a, Ohm<IExa> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator >=(Ohm<T> a, Ohm<IExa> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator <=(Ohm<T> a, Ohm<IExa> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator ==(Ohm<T> a, Ohm<IPeta> i)
        => Unit.Convert(i.m_value, Prefix.Peta, Prefix) == a.m_value;

    public static bool operator !=(Ohm<T> a, Ohm<IPeta> i)
        => Unit.Convert(i.m_value, Prefix.Peta, Prefix) != a.m_value;

    public static bool operator >(Ohm<T> a, Ohm<IPeta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static bool operator <(Ohm<T> a, Ohm<IPeta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static bool operator >=(Ohm<T> a, Ohm<IPeta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static bool operator <=(Ohm<T> a, Ohm<IPeta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static bool operator ==(Ohm<T> a, Ohm<ITera> i)
        => Unit.Convert(i.m_value, Prefix.Tera, Prefix) == a.m_value;

    public static bool operator !=(Ohm<T> a, Ohm<ITera> i)
        => Unit.Convert(i.m_value, Prefix.Tera, Prefix) != a.m_value;

    public static bool operator >(Ohm<T> a, Ohm<ITera> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static bool operator <(Ohm<T> a, Ohm<ITera> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static bool operator >=(Ohm<T> a, Ohm<ITera> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static bool operator <=(Ohm<T> a, Ohm<ITera> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static bool operator ==(Ohm<T> a, Ohm<IGiga> i)
        => Unit.Convert(i.m_value, Prefix.Giga, Prefix) == a.m_value;

    public static bool operator !=(Ohm<T> a, Ohm<IGiga> i)
        => Unit.Convert(i.m_value, Prefix.Giga, Prefix) != a.m_value;

    public static bool operator >(Ohm<T> a, Ohm<IGiga> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static bool operator <(Ohm<T> a, Ohm<IGiga> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static bool operator >=(Ohm<T> a, Ohm<IGiga> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static bool operator <=(Ohm<T> a, Ohm<IGiga> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static bool operator ==(Ohm<T> a, Ohm<IMega> i)
        => Unit.Convert(i.m_value, Prefix.Mega, Prefix) == a.m_value;

    public static bool operator !=(Ohm<T> a, Ohm<IMega> i)
        => Unit.Convert(i.m_value, Prefix.Mega, Prefix) != a.m_value;

    public static bool operator >(Ohm<T> a, Ohm<IMega> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static bool operator <(Ohm<T> a, Ohm<IMega> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static bool operator >=(Ohm<T> a, Ohm<IMega> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static bool operator <=(Ohm<T> a, Ohm<IMega> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static bool operator ==(Ohm<T> a, Ohm<IKilo> i)
        => Unit.Convert(i.m_value, Prefix.Kilo, Prefix) == a.m_value;

    public static bool operator !=(Ohm<T> a, Ohm<IKilo> i)
        => Unit.Convert(i.m_value, Prefix.Kilo, Prefix) != a.m_value;

    public static bool operator >(Ohm<T> a, Ohm<IKilo> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static bool operator <(Ohm<T> a, Ohm<IKilo> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static bool operator >=(Ohm<T> a, Ohm<IKilo> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static bool operator <=(Ohm<T> a, Ohm<IKilo> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static bool operator ==(Ohm<T> a, Ohm<IHecto> i)
        => Unit.Convert(i.m_value, Prefix.Hecto, Prefix) == a.m_value;

    public static bool operator !=(Ohm<T> a, Ohm<IHecto> i)
        => Unit.Convert(i.m_value, Prefix.Hecto, Prefix) != a.m_value;

    public static bool operator >(Ohm<T> a, Ohm<IHecto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static bool operator <(Ohm<T> a, Ohm<IHecto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static bool operator >=(Ohm<T> a, Ohm<IHecto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static bool operator <=(Ohm<T> a, Ohm<IHecto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static bool operator ==(Ohm<T> a, Ohm<IDeca> i)
        => Unit.Convert(i.m_value, Prefix.Deca, Prefix) == a.m_value;

    public static bool operator !=(Ohm<T> a, Ohm<IDeca> i)
        => Unit.Convert(i.m_value, Prefix.Deca, Prefix) != a.m_value;

    public static bool operator >(Ohm<T> a, Ohm<IDeca> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static bool operator <(Ohm<T> a, Ohm<IDeca> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static bool operator >=(Ohm<T> a, Ohm<IDeca> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static bool operator <=(Ohm<T> a, Ohm<IDeca> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static bool operator ==(Ohm<T> a, Ohm<INone> i)
        => Unit.Convert(i.m_value, Prefix.None, Prefix) == a.m_value;

    public static bool operator !=(Ohm<T> a, Ohm<INone> i)
        => Unit.Convert(i.m_value, Prefix.Nano, Prefix) != a.m_value;

    public static bool operator >(Ohm<T> a, Ohm<INone> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static bool operator <(Ohm<T> a, Ohm<INone> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static bool operator >=(Ohm<T> a, Ohm<INone> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static bool operator <=(Ohm<T> a, Ohm<INone> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static bool operator ==(Ohm<T> a, Ohm<IDeci> i)
        => Unit.Convert(i.m_value, Prefix.Deci, Prefix) == a.m_value;

    public static bool operator !=(Ohm<T> a, Ohm<IDeci> i)
        => Unit.Convert(i.m_value, Prefix.Deci, Prefix) != a.m_value;

    public static bool operator >(Ohm<T> a, Ohm<IDeci> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static bool operator <(Ohm<T> a, Ohm<IDeci> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static bool operator >=(Ohm<T> a, Ohm<IDeci> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static bool operator <=(Ohm<T> a, Ohm<IDeci> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static bool operator ==(Ohm<T> a, Ohm<ICenti> i)
        => Unit.Convert(i.m_value, Prefix.Centi, Prefix) == a.m_value;

    public static bool operator !=(Ohm<T> a, Ohm<ICenti> i)
        => Unit.Convert(i.m_value, Prefix.Centi, Prefix) != a.m_value;

    public static bool operator >(Ohm<T> a, Ohm<ICenti> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static bool operator <(Ohm<T> a, Ohm<ICenti> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static bool operator >=(Ohm<T> a, Ohm<ICenti> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static bool operator <=(Ohm<T> a, Ohm<ICenti> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static bool operator ==(Ohm<T> a, Ohm<IMilli> i)
        => Unit.Convert(i.m_value, Prefix.Milli, Prefix) == a.m_value;

    public static bool operator !=(Ohm<T> a, Ohm<IMilli> i)
        => Unit.Convert(i.m_value, Prefix.Milli, Prefix) != a.m_value;

    public static bool operator >(Ohm<T> a, Ohm<IMilli> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static bool operator <(Ohm<T> a, Ohm<IMilli> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static bool operator >=(Ohm<T> a, Ohm<IMilli> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static bool operator <=(Ohm<T> a, Ohm<IMilli> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static bool operator ==(Ohm<T> a, Ohm<IMicro> i)
        => Unit.Convert(i.m_value, Prefix.Micro, Prefix) == a.m_value;

    public static bool operator !=(Ohm<T> a, Ohm<IMicro> i)
        => Unit.Convert(i.m_value, Prefix.Micro, Prefix) != a.m_value;

    public static bool operator >(Ohm<T> a, Ohm<IMicro> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static bool operator <(Ohm<T> a, Ohm<IMicro> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static bool operator >=(Ohm<T> a, Ohm<IMicro> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static bool operator <=(Ohm<T> a, Ohm<IMicro> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static bool operator ==(Ohm<T> a, Ohm<INano> i)
        => Unit.Convert(i.m_value, Prefix.Nano, Prefix) == a.m_value;

    public static bool operator !=(Ohm<T> a, Ohm<INano> i)
        => Unit.Convert(i.m_value, Prefix.Nano, Prefix) != a.m_value;

    public static bool operator >(Ohm<T> a, Ohm<INano> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static bool operator <(Ohm<T> a, Ohm<INano> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static bool operator >=(Ohm<T> a, Ohm<INano> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static bool operator <=(Ohm<T> a, Ohm<INano> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static bool operator ==(Ohm<T> a, Ohm<IPico> i)
        => Unit.Convert(i.m_value, Prefix.Pico, Prefix) == a.m_value;

    public static bool operator !=(Ohm<T> a, Ohm<IPico> i)
        => Unit.Convert(i.m_value, Prefix.Pico, Prefix) != a.m_value;

    public static bool operator >(Ohm<T> a, Ohm<IPico> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static bool operator <(Ohm<T> a, Ohm<IPico> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static bool operator >=(Ohm<T> a, Ohm<IPico> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static bool operator <=(Ohm<T> a, Ohm<IPico> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static bool operator ==(Ohm<T> a, Ohm<IFemto> i)
        => Unit.Convert(i.m_value, Prefix.Femto, Prefix) == a.m_value;

    public static bool operator !=(Ohm<T> a, Ohm<IFemto> i)
        => Unit.Convert(i.m_value, Prefix.Femto, Prefix) != a.m_value;

    public static bool operator >(Ohm<T> a, Ohm<IFemto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static bool operator <(Ohm<T> a, Ohm<IFemto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static bool operator >=(Ohm<T> a, Ohm<IFemto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static bool operator <=(Ohm<T> a, Ohm<IFemto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static bool operator ==(Ohm<T> a, Ohm<IAtto> i)
        => Unit.Convert(i.m_value, Prefix.Atto, Prefix) == a.m_value;

    public static bool operator !=(Ohm<T> a, Ohm<IAtto> i)
        => Unit.Convert(i.m_value, Prefix.Atto, Prefix) != a.m_value;

    public static bool operator >(Ohm<T> a, Ohm<IAtto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static bool operator <(Ohm<T> a, Ohm<IAtto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static bool operator >=(Ohm<T> a, Ohm<IAtto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static bool operator <=(Ohm<T> a, Ohm<IAtto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static bool operator ==(Ohm<T> a, Ohm<IZepto> i)
        => Unit.Convert(i.m_value, Prefix.Zepto, Prefix) == a.m_value;

    public static bool operator !=(Ohm<T> a, Ohm<IZepto> i)
        => Unit.Convert(i.m_value, Prefix.Zepto, Prefix) != a.m_value;

    public static bool operator >(Ohm<T> a, Ohm<IZepto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    public static bool operator <(Ohm<T> a, Ohm<IZepto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    public static bool operator >=(Ohm<T> a, Ohm<IZepto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    public static bool operator <=(Ohm<T> a, Ohm<IZepto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    public override bool Equals(object? obj) => m_value.Equals(obj) && Prefix.Equals(obj);

    public override int GetHashCode() => HashCode.Combine(m_value, Prefix);

    public override string ToString() => m_value.ToString(NumberFormatInfo.CurrentInfo) +
                                         Prefix.Attribute<SuffixAttribute>()!.Suffix + "Ω";
}