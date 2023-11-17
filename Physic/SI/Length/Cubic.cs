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
public readonly struct Cubic : INone
{
    internal readonly decimal m_value; // Do not rename (binary serialization)

    public Cubic(decimal val, Prefix from = Prefix.None)
    {
        m_value = from == Prefix.None ? val : Unit.BaseValue(val, from);
    }

    public decimal Convert(Prefix target) => Unit.Convert(m_value, INone.Prefix, target);

    public static implicit operator Cubic(decimal val) => new(val);

    public static implicit operator Cubic<IYotta>(Cubic m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Yotta));
    public static implicit operator Cubic<IZetta>(Cubic m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Zetta));
    public static implicit operator Cubic<IExa>(Cubic m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Exa));
    public static implicit operator Cubic<IPeta>(Cubic m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Peta));
    public static implicit operator Cubic<ITera>(Cubic m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Tera));
    public static implicit operator Cubic<IGiga>(Cubic m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Giga));
    public static implicit operator Cubic<IMega>(Cubic m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Mega));
    public static implicit operator Cubic<IKilo>(Cubic m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Kilo));
    public static implicit operator Cubic<IHecto>(Cubic m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Hecto));
    public static implicit operator Cubic<IDeca>(Cubic m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Deca));
    public static implicit operator Cubic<INone>(Cubic m) => new(m.m_value);
    public static implicit operator Cubic<IDeci>(Cubic m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Deci));
    public static implicit operator Cubic<ICenti>(Cubic m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Centi));
    public static implicit operator Cubic<IMilli>(Cubic m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Milli));
    public static implicit operator Cubic<IMicro>(Cubic m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Micro));
    public static implicit operator Cubic<INano>(Cubic m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Nano));
    public static implicit operator Cubic<IPico>(Cubic m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Pico));
    public static implicit operator Cubic<IFemto>(Cubic m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Femto));
    public static implicit operator Cubic<IAtto>(Cubic m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Atto));
    public static implicit operator Cubic<IZepto>(Cubic m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Zepto));
    public static implicit operator Cubic<IYocto>(Cubic m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Yocto));


    #region PLUS

    public static Cubic operator +(Cubic a, Cubic<IYotta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Cubic operator +(Cubic a, Cubic<IZetta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Cubic operator +(Cubic a, Cubic<IExa> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Cubic operator +(Cubic a, Cubic<IPeta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Cubic operator +(Cubic a, Cubic<ITera> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Cubic operator +(Cubic a, Cubic<IGiga> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Cubic operator +(Cubic a, Cubic<IMega> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Cubic operator +(Cubic a, Cubic<IKilo> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Cubic operator +(Cubic a, Cubic<IHecto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Cubic operator +(Cubic a, Cubic<IDeca> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Cubic operator +(Cubic a, Cubic<INone> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Cubic operator +(Cubic a, Cubic<IDeci> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Cubic operator +(Cubic a, Cubic<ICenti> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Cubic operator +(Cubic a, Cubic<IMilli> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Cubic operator +(Cubic a, Cubic<IMicro> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Cubic operator +(Cubic a, Cubic<INano> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Cubic operator +(Cubic a, Cubic<IPico> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Cubic operator +(Cubic a, Cubic<IFemto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Cubic operator +(Cubic a, Cubic<IAtto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Cubic operator +(Cubic a, Cubic<IZepto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region MINUS

    public static Cubic operator -(Cubic a, Cubic<IYotta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Cubic operator -(Cubic a, Cubic<IZetta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Cubic operator -(Cubic a, Cubic<IExa> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Cubic operator -(Cubic a, Cubic<IPeta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Cubic operator -(Cubic a, Cubic<ITera> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Cubic operator -(Cubic a, Cubic<IGiga> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Cubic operator -(Cubic a, Cubic<IMega> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Cubic operator -(Cubic a, Cubic<IKilo> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Cubic operator -(Cubic a, Cubic<IHecto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Cubic operator -(Cubic a, Cubic<IDeca> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Cubic operator -(Cubic a, Cubic<INone> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Cubic operator -(Cubic a, Cubic<IDeci> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Cubic operator -(Cubic a, Cubic<ICenti> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Cubic operator -(Cubic a, Cubic<IMilli> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Cubic operator -(Cubic a, Cubic<IMicro> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Cubic operator -(Cubic a, Cubic<INano> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Cubic operator -(Cubic a, Cubic<IPico> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Cubic operator -(Cubic a, Cubic<IFemto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Cubic operator -(Cubic a, Cubic<IAtto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Cubic operator -(Cubic a, Cubic<IZepto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region MULTI

    public static Cubic operator *(Cubic a, Cubic<IYotta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Cubic operator *(Cubic a, Cubic<IZetta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Cubic operator *(Cubic a, Cubic<IExa> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Cubic operator *(Cubic a, Cubic<IPeta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Cubic operator *(Cubic a, Cubic<ITera> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Cubic operator *(Cubic a, Cubic<IGiga> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Cubic operator *(Cubic a, Cubic<IMega> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Cubic operator *(Cubic a, Cubic<IKilo> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Cubic operator *(Cubic a, Cubic<IHecto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Cubic operator *(Cubic a, Cubic<IDeca> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Cubic operator *(Cubic a, Cubic<INone> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Cubic operator *(Cubic a, Cubic<IDeci> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Cubic operator *(Cubic a, Cubic<ICenti> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Cubic operator *(Cubic a, Cubic<IMilli> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Cubic operator *(Cubic a, Cubic<IMicro> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Cubic operator *(Cubic a, Cubic<INano> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Cubic operator *(Cubic a, Cubic<IPico> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Cubic operator *(Cubic a, Cubic<IFemto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Cubic operator *(Cubic a, Cubic<IAtto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Cubic operator *(Cubic a, Cubic<IZepto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region DIV

    public static Cubic operator /(Cubic a, Cubic<IYotta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Cubic operator /(Cubic a, Cubic<IZetta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Cubic operator /(Cubic a, Cubic<IExa> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Cubic operator /(Cubic a, Cubic<IPeta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Cubic operator /(Cubic a, Cubic<ITera> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Cubic operator /(Cubic a, Cubic<IGiga> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Cubic operator /(Cubic a, Cubic<IMega> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Cubic operator /(Cubic a, Cubic<IKilo> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Cubic operator /(Cubic a, Cubic<IHecto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Cubic operator /(Cubic a, Cubic<IDeca> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Cubic operator /(Cubic a, Cubic<INone> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Cubic operator /(Cubic a, Cubic<IDeci> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Cubic operator /(Cubic a, Cubic<ICenti> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Cubic operator /(Cubic a, Cubic<IMilli> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Cubic operator /(Cubic a, Cubic<IMicro> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Cubic operator /(Cubic a, Cubic<INano> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Cubic operator /(Cubic a, Cubic<IPico> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Cubic operator /(Cubic a, Cubic<IFemto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Cubic operator /(Cubic a, Cubic<IAtto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Cubic operator /(Cubic a, Cubic<IZepto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region PERCENT

    public static Cubic operator %(Cubic a, Cubic<IYotta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Cubic operator %(Cubic a, Cubic<IZetta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Cubic operator %(Cubic a, Cubic<IExa> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Cubic operator %(Cubic a, Cubic<IPeta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Cubic operator %(Cubic a, Cubic<ITera> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Cubic operator %(Cubic a, Cubic<IGiga> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Cubic operator %(Cubic a, Cubic<IMega> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Cubic operator %(Cubic a, Cubic<IKilo> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Cubic operator %(Cubic a, Cubic<IHecto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Cubic operator %(Cubic a, Cubic<IDeca> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Cubic operator %(Cubic a, Cubic<INone> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Cubic operator %(Cubic a, Cubic<IDeci> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Cubic operator %(Cubic a, Cubic<ICenti> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Cubic operator %(Cubic a, Cubic<IMilli> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Cubic operator %(Cubic a, Cubic<IMicro> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Cubic operator %(Cubic a, Cubic<INano> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Cubic operator %(Cubic a, Cubic<IPico> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Cubic operator %(Cubic a, Cubic<IFemto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Cubic operator %(Cubic a, Cubic<IAtto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Cubic operator %(Cubic a, Cubic<IZepto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region EQUAL

    public static bool operator ==(Cubic a, Cubic<IYotta> i)
        => Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix) == a.m_value;

    public static bool operator !=(Cubic a, Cubic<IYotta> i)
        => Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix) != a.m_value;

    public static bool operator >(Cubic a, Cubic<IYotta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator <(Cubic a, Cubic<IYotta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator >=(Cubic a, Cubic<IYotta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator <=(Cubic a, Cubic<IYotta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator ==(Cubic a, Cubic<IZetta> i)
        => Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix) == a.m_value;

    public static bool operator !=(Cubic a, Cubic<IZetta> i)
        => Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix) != a.m_value;

    public static bool operator >(Cubic a, Cubic<IZetta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static bool operator <(Cubic a, Cubic<IZetta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static bool operator >=(Cubic a, Cubic<IZetta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static bool operator <=(Cubic a, Cubic<IZetta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static bool operator ==(Cubic a, Cubic<IExa> i)
        => Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix) == a.m_value;

    public static bool operator !=(Cubic a, Cubic<IExa> i)
        => Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix) != a.m_value;

    public static bool operator >(Cubic a, Cubic<IExa> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator <(Cubic a, Cubic<IExa> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator >=(Cubic a, Cubic<IExa> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator <=(Cubic a, Cubic<IExa> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator ==(Cubic a, Cubic<IPeta> i)
        => Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix) == a.m_value;

    public static bool operator !=(Cubic a, Cubic<IPeta> i)
        => Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix) != a.m_value;

    public static bool operator >(Cubic a, Cubic<IPeta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static bool operator <(Cubic a, Cubic<IPeta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static bool operator >=(Cubic a, Cubic<IPeta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static bool operator <=(Cubic a, Cubic<IPeta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static bool operator ==(Cubic a, Cubic<ITera> i)
        => Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix) == a.m_value;

    public static bool operator !=(Cubic a, Cubic<ITera> i)
        => Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix) != a.m_value;

    public static bool operator >(Cubic a, Cubic<ITera> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static bool operator <(Cubic a, Cubic<ITera> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static bool operator >=(Cubic a, Cubic<ITera> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static bool operator <=(Cubic a, Cubic<ITera> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static bool operator ==(Cubic a, Cubic<IGiga> i)
        => Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix) == a.m_value;

    public static bool operator !=(Cubic a, Cubic<IGiga> i)
        => Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix) != a.m_value;

    public static bool operator >(Cubic a, Cubic<IGiga> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static bool operator <(Cubic a, Cubic<IGiga> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static bool operator >=(Cubic a, Cubic<IGiga> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static bool operator <=(Cubic a, Cubic<IGiga> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static bool operator ==(Cubic a, Cubic<IMega> i)
        => Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix) == a.m_value;

    public static bool operator !=(Cubic a, Cubic<IMega> i)
        => Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix) != a.m_value;

    public static bool operator >(Cubic a, Cubic<IMega> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static bool operator <(Cubic a, Cubic<IMega> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static bool operator >=(Cubic a, Cubic<IMega> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static bool operator <=(Cubic a, Cubic<IMega> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static bool operator ==(Cubic a, Cubic<IKilo> i)
        => Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix) == a.m_value;

    public static bool operator !=(Cubic a, Cubic<IKilo> i)
        => Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix) != a.m_value;

    public static bool operator >(Cubic a, Cubic<IKilo> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static bool operator <(Cubic a, Cubic<IKilo> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static bool operator >=(Cubic a, Cubic<IKilo> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static bool operator <=(Cubic a, Cubic<IKilo> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static bool operator ==(Cubic a, Cubic<IHecto> i)
        => Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix) == a.m_value;

    public static bool operator !=(Cubic a, Cubic<IHecto> i)
        => Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix) != a.m_value;

    public static bool operator >(Cubic a, Cubic<IHecto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static bool operator <(Cubic a, Cubic<IHecto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static bool operator >=(Cubic a, Cubic<IHecto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static bool operator <=(Cubic a, Cubic<IHecto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static bool operator ==(Cubic a, Cubic<IDeca> i)
        => Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix) == a.m_value;

    public static bool operator !=(Cubic a, Cubic<IDeca> i)
        => Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix) != a.m_value;

    public static bool operator >(Cubic a, Cubic<IDeca> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static bool operator <(Cubic a, Cubic<IDeca> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static bool operator >=(Cubic a, Cubic<IDeca> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static bool operator <=(Cubic a, Cubic<IDeca> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static bool operator ==(Cubic a, Cubic<INone> i)
        => Unit.Convert(i.m_value, Prefix.None, INone.Prefix) == a.m_value;

    public static bool operator !=(Cubic a, Cubic<INone> i)
        => Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix) != a.m_value;

    public static bool operator >(Cubic a, Cubic<INone> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static bool operator <(Cubic a, Cubic<INone> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static bool operator >=(Cubic a, Cubic<INone> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static bool operator <=(Cubic a, Cubic<INone> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static bool operator ==(Cubic a, Cubic<IDeci> i)
        => Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix) == a.m_value;

    public static bool operator !=(Cubic a, Cubic<IDeci> i)
        => Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix) != a.m_value;

    public static bool operator >(Cubic a, Cubic<IDeci> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static bool operator <(Cubic a, Cubic<IDeci> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static bool operator >=(Cubic a, Cubic<IDeci> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static bool operator <=(Cubic a, Cubic<IDeci> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static bool operator ==(Cubic a, Cubic<ICenti> i)
        => Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix) == a.m_value;

    public static bool operator !=(Cubic a, Cubic<ICenti> i)
        => Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix) != a.m_value;

    public static bool operator >(Cubic a, Cubic<ICenti> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static bool operator <(Cubic a, Cubic<ICenti> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static bool operator >=(Cubic a, Cubic<ICenti> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static bool operator <=(Cubic a, Cubic<ICenti> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static bool operator ==(Cubic a, Cubic<IMilli> i)
        => Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix) == a.m_value;

    public static bool operator !=(Cubic a, Cubic<IMilli> i)
        => Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix) != a.m_value;

    public static bool operator >(Cubic a, Cubic<IMilli> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static bool operator <(Cubic a, Cubic<IMilli> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static bool operator >=(Cubic a, Cubic<IMilli> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static bool operator <=(Cubic a, Cubic<IMilli> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static bool operator ==(Cubic a, Cubic<IMicro> i)
        => Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix) == a.m_value;

    public static bool operator !=(Cubic a, Cubic<IMicro> i)
        => Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix) != a.m_value;

    public static bool operator >(Cubic a, Cubic<IMicro> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static bool operator <(Cubic a, Cubic<IMicro> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static bool operator >=(Cubic a, Cubic<IMicro> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static bool operator <=(Cubic a, Cubic<IMicro> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static bool operator ==(Cubic a, Cubic<INano> i)
        => Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix) == a.m_value;

    public static bool operator !=(Cubic a, Cubic<INano> i)
        => Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix) != a.m_value;

    public static bool operator >(Cubic a, Cubic<INano> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static bool operator <(Cubic a, Cubic<INano> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static bool operator >=(Cubic a, Cubic<INano> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static bool operator <=(Cubic a, Cubic<INano> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static bool operator ==(Cubic a, Cubic<IPico> i)
        => Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix) == a.m_value;

    public static bool operator !=(Cubic a, Cubic<IPico> i)
        => Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix) != a.m_value;

    public static bool operator >(Cubic a, Cubic<IPico> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static bool operator <(Cubic a, Cubic<IPico> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static bool operator >=(Cubic a, Cubic<IPico> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static bool operator <=(Cubic a, Cubic<IPico> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static bool operator ==(Cubic a, Cubic<IFemto> i)
        => Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix) == a.m_value;

    public static bool operator !=(Cubic a, Cubic<IFemto> i)
        => Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix) != a.m_value;

    public static bool operator >(Cubic a, Cubic<IFemto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static bool operator <(Cubic a, Cubic<IFemto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static bool operator >=(Cubic a, Cubic<IFemto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static bool operator <=(Cubic a, Cubic<IFemto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static bool operator ==(Cubic a, Cubic<IAtto> i)
        => Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix) == a.m_value;

    public static bool operator !=(Cubic a, Cubic<IAtto> i)
        => Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix) != a.m_value;

    public static bool operator >(Cubic a, Cubic<IAtto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static bool operator <(Cubic a, Cubic<IAtto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static bool operator >=(Cubic a, Cubic<IAtto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static bool operator <=(Cubic a, Cubic<IAtto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static bool operator ==(Cubic a, Cubic<IZepto> i)
        => Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix) == a.m_value;

    public static bool operator !=(Cubic a, Cubic<IZepto> i)
        => Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix) != a.m_value;

    public static bool operator >(Cubic a, Cubic<IZepto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    public static bool operator <(Cubic a, Cubic<IZepto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    public static bool operator >=(Cubic a, Cubic<IZepto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    public static bool operator <=(Cubic a, Cubic<IZepto> i)
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
public readonly struct Cubic<T> where T : SI
{
    internal readonly decimal m_value; // Do not rename (binary serialization)

    public Cubic(decimal val)
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

    public static implicit operator Cubic<T>(decimal val) => new(val);

    #region IMPL_METER

    public static implicit operator Cubic<T>(Cubic<IYotta> i)
    {
        return Prefix switch
        {
            Prefix.Yotta => new Cubic<T>(i.m_value),
            Prefix.None => new Cubic<T>(Unit.BaseValue(i.m_value, Prefix.Yotta)),
            _ => new Cubic<T>(Unit.Convert(i.m_value, Prefix.Yotta, Prefix))
        };
    }

    public static implicit operator Cubic<T>(Cubic<IZetta> i)
    {
        return Prefix switch
        {
            Prefix.Zetta => new Cubic<T>(i.m_value),
            Prefix.None => new Cubic<T>(Unit.BaseValue(i.m_value, Prefix.Zetta)),
            _ => new Cubic<T>(Unit.Convert(i.m_value, Prefix.Zetta, Prefix))
        };
    }

    public static implicit operator Cubic<T>(Cubic<IExa> i)
    {
        return Prefix switch
        {
            Prefix.Exa => new Cubic<T>(i.m_value),
            Prefix.None => new Cubic<T>(Unit.BaseValue(i.m_value, Prefix.Exa)),
            _ => new Cubic<T>(Unit.Convert(i.m_value, Prefix.Exa, Prefix))
        };
    }

    public static implicit operator Cubic<T>(Cubic<IPeta> i)
    {
        return Prefix switch
        {
            Prefix.Peta => new Cubic<T>(i.m_value),
            Prefix.None => new Cubic<T>(Unit.BaseValue(i.m_value, Prefix.Peta)),
            _ => new Cubic<T>(Unit.Convert(i.m_value, Prefix.Peta, Prefix))
        };
    }

    public static implicit operator Cubic<T>(Cubic<ITera> i)
    {
        return Prefix switch
        {
            Prefix.Tera => new Cubic<T>(i.m_value),
            Prefix.None => new Cubic<T>(Unit.BaseValue(i.m_value, Prefix.Tera)),
            _ => new Cubic<T>(Unit.Convert(i.m_value, Prefix.Tera, Prefix))
        };
    }

    public static implicit operator Cubic<T>(Cubic<IGiga> i)
    {
        return Prefix switch
        {
            Prefix.Giga => new Cubic<T>(i.m_value),
            Prefix.None => new Cubic<T>(Unit.BaseValue(i.m_value, Prefix.Giga)),
            _ => new Cubic<T>(Unit.Convert(i.m_value, Prefix.Giga, Prefix))
        };
    }

    public static implicit operator Cubic<T>(Cubic<IMega> i)
    {
        return Prefix switch
        {
            Prefix.Mega => new Cubic<T>(i.m_value),
            Prefix.None => new Cubic<T>(Unit.BaseValue(i.m_value, Prefix.Mega)),
            _ => new Cubic<T>(Unit.Convert(i.m_value, Prefix.Mega, Prefix))
        };
    }

    public static implicit operator Cubic<T>(Cubic<IKilo> i)
    {
        return Prefix switch
        {
            Prefix.Kilo => new Cubic<T>(i.m_value),
            Prefix.None => new Cubic<T>(Unit.BaseValue(i.m_value, Prefix.Kilo)),
            _ => new Cubic<T>(Unit.Convert(i.m_value, Prefix.Kilo, Prefix))
        };
    }

    public static implicit operator Cubic<T>(Cubic<IHecto> i)
    {
        return Prefix switch
        {
            Prefix.Hecto => new Cubic<T>(i.m_value),
            Prefix.None => new Cubic<T>(Unit.BaseValue(i.m_value, Prefix.Hecto)),
            _ => new Cubic<T>(Unit.Convert(i.m_value, Prefix.Hecto, Prefix))
        };
    }

    public static implicit operator Cubic<T>(Cubic<IDeca> i)
    {
        return Prefix switch
        {
            Prefix.Deca => new Cubic<T>(i.m_value),
            Prefix.None => new Cubic<T>(Unit.BaseValue(i.m_value, Prefix.Deca)),
            _ => new Cubic<T>(Unit.Convert(i.m_value, Prefix.Deca, Prefix))
        };
    }

    public static implicit operator Cubic<T>(Cubic<INone> i) => new Cubic<T>(i.m_value);

    public static implicit operator Cubic<T>(Cubic<IDeci> i)
    {
        return Prefix switch
        {
            Prefix.Deci => new Cubic<T>(i.m_value),
            Prefix.None => new Cubic<T>(Unit.BaseValue(i.m_value, Prefix.Deci)),
            _ => new Cubic<T>(Unit.Convert(i.m_value, Prefix.Deci, Prefix))
        };
    }

    public static implicit operator Cubic<T>(Cubic<ICenti> i)
    {
        return Prefix switch
        {
            Prefix.Centi => new Cubic<T>(i.m_value),
            Prefix.None => new Cubic<T>(Unit.BaseValue(i.m_value, Prefix.Centi)),
            _ => new Cubic<T>(Unit.Convert(i.m_value, Prefix.Centi, Prefix))
        };
    }

    public static implicit operator Cubic<T>(Cubic<IMilli> i)
    {
        return Prefix switch
        {
            Prefix.Milli => new Cubic<T>(i.m_value),
            Prefix.None => new Cubic<T>(Unit.BaseValue(i.m_value, Prefix.Milli)),
            _ => new Cubic<T>(Unit.Convert(i.m_value, Prefix.Milli, Prefix))
        };
    }

    public static implicit operator Cubic<T>(Cubic<IMicro> i)
    {
        return Prefix switch
        {
            Prefix.Micro => new Cubic<T>(i.m_value),
            Prefix.None => new Cubic<T>(Unit.BaseValue(i.m_value, Prefix.Micro)),
            _ => new Cubic<T>(Unit.Convert(i.m_value, Prefix.Micro, Prefix))
        };
    }

    public static implicit operator Cubic<T>(Cubic<INano> i)
    {
        return Prefix switch
        {
            Prefix.Nano => new Cubic<T>(i.m_value),
            Prefix.None => new Cubic<T>(Unit.BaseValue(i.m_value, Prefix.Nano)),
            _ => new Cubic<T>(Unit.Convert(i.m_value, Prefix.Nano, Prefix))
        };
    }

    public static implicit operator Cubic<T>(Cubic<IPico> i)
    {
        return Prefix switch
        {
            Prefix.Pico => new Cubic<T>(i.m_value),
            Prefix.None => new Cubic<T>(Unit.BaseValue(i.m_value, Prefix.Pico)),
            _ => new Cubic<T>(Unit.Convert(i.m_value, Prefix.Pico, Prefix))
        };
    }

    public static implicit operator Cubic<T>(Cubic<IFemto> i)
    {
        return Prefix switch
        {
            Prefix.Femto => new Cubic<T>(i.m_value),
            Prefix.None => new Cubic<T>(Unit.BaseValue(i.m_value, Prefix.Femto)),
            _ => new Cubic<T>(Unit.Convert(i.m_value, Prefix.Femto, Prefix))
        };
    }

    public static implicit operator Cubic<T>(Cubic<IAtto> i)
    {
        return Prefix switch
        {
            Prefix.Atto => new Cubic<T>(i.m_value),
            Prefix.None => new Cubic<T>(Unit.BaseValue(i.m_value, Prefix.Atto)),
            _ => new Cubic<T>(Unit.Convert(i.m_value, Prefix.Atto, Prefix))
        };
    }

    public static implicit operator Cubic<T>(Cubic<IZepto> i)
    {
        return Prefix switch
        {
            Prefix.Zepto => new Cubic<T>(i.m_value),
            Prefix.None => new Cubic<T>(Unit.BaseValue(i.m_value, Prefix.Zepto)),
            _ => new Cubic<T>(Unit.Convert(i.m_value, Prefix.Zepto, Prefix))
        };
    }

    #endregion

    #region PLUS

    public static Cubic<T> operator +(Cubic<T> a, Cubic<IYotta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Cubic<T> operator +(Cubic<T> a, Cubic<IZetta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Cubic<T> operator +(Cubic<T> a, Cubic<IExa> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Cubic<T> operator +(Cubic<T> a, Cubic<IPeta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Cubic<T> operator +(Cubic<T> a, Cubic<ITera> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Cubic<T> operator +(Cubic<T> a, Cubic<IGiga> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Cubic<T> operator +(Cubic<T> a, Cubic<IMega> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Cubic<T> operator +(Cubic<T> a, Cubic<IKilo> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Cubic<T> operator +(Cubic<T> a, Cubic<IHecto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Cubic<T> operator +(Cubic<T> a, Cubic<IDeca> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Cubic<T> operator +(Cubic<T> a, Cubic<INone> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Cubic<T> operator +(Cubic<T> a, Cubic<IDeci> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Cubic<T> operator +(Cubic<T> a, Cubic<ICenti> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Cubic<T> operator +(Cubic<T> a, Cubic<IMilli> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Cubic<T> operator +(Cubic<T> a, Cubic<IMicro> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Cubic<T> operator +(Cubic<T> a, Cubic<INano> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Cubic<T> operator +(Cubic<T> a, Cubic<IPico> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Cubic<T> operator +(Cubic<T> a, Cubic<IFemto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Cubic<T> operator +(Cubic<T> a, Cubic<IAtto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Cubic<T> operator +(Cubic<T> a, Cubic<IZepto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region MINUS

    public static Cubic<T> operator -(Cubic<T> a, Cubic<IYotta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Cubic<T> operator -(Cubic<T> a, Cubic<IZetta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Cubic<T> operator -(Cubic<T> a, Cubic<IExa> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Cubic<T> operator -(Cubic<T> a, Cubic<IPeta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Cubic<T> operator -(Cubic<T> a, Cubic<ITera> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Cubic<T> operator -(Cubic<T> a, Cubic<IGiga> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Cubic<T> operator -(Cubic<T> a, Cubic<IMega> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Cubic<T> operator -(Cubic<T> a, Cubic<IKilo> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Cubic<T> operator -(Cubic<T> a, Cubic<IHecto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Cubic<T> operator -(Cubic<T> a, Cubic<IDeca> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Cubic<T> operator -(Cubic<T> a, Cubic<INone> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Cubic<T> operator -(Cubic<T> a, Cubic<IDeci> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Cubic<T> operator -(Cubic<T> a, Cubic<ICenti> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Cubic<T> operator -(Cubic<T> a, Cubic<IMilli> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Cubic<T> operator -(Cubic<T> a, Cubic<IMicro> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Cubic<T> operator -(Cubic<T> a, Cubic<INano> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Cubic<T> operator -(Cubic<T> a, Cubic<IPico> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Cubic<T> operator -(Cubic<T> a, Cubic<IFemto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Cubic<T> operator -(Cubic<T> a, Cubic<IAtto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Cubic<T> operator -(Cubic<T> a, Cubic<IZepto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region MULTI

    public static Cubic<T> operator *(Cubic<T> a, Cubic<IYotta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Cubic<T> operator *(Cubic<T> a, Cubic<IZetta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Cubic<T> operator *(Cubic<T> a, Cubic<IExa> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Cubic<T> operator *(Cubic<T> a, Cubic<IPeta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Cubic<T> operator *(Cubic<T> a, Cubic<ITera> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Cubic<T> operator *(Cubic<T> a, Cubic<IGiga> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Cubic<T> operator *(Cubic<T> a, Cubic<IMega> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Cubic<T> operator *(Cubic<T> a, Cubic<IKilo> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Cubic<T> operator *(Cubic<T> a, Cubic<IHecto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Cubic<T> operator *(Cubic<T> a, Cubic<IDeca> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Cubic<T> operator *(Cubic<T> a, Cubic<INone> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Cubic<T> operator *(Cubic<T> a, Cubic<IDeci> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Cubic<T> operator *(Cubic<T> a, Cubic<ICenti> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Cubic<T> operator *(Cubic<T> a, Cubic<IMilli> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Cubic<T> operator *(Cubic<T> a, Cubic<IMicro> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Cubic<T> operator *(Cubic<T> a, Cubic<INano> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Cubic<T> operator *(Cubic<T> a, Cubic<IPico> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Cubic<T> operator *(Cubic<T> a, Cubic<IFemto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Cubic<T> operator *(Cubic<T> a, Cubic<IAtto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Cubic<T> operator *(Cubic<T> a, Cubic<IZepto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region DIV

    public static Cubic<T> operator /(Cubic<T> a, Cubic<IYotta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Cubic<T> operator /(Cubic<T> a, Cubic<IZetta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Cubic<T> operator /(Cubic<T> a, Cubic<IExa> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Cubic<T> operator /(Cubic<T> a, Cubic<IPeta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Cubic<T> operator /(Cubic<T> a, Cubic<ITera> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Cubic<T> operator /(Cubic<T> a, Cubic<IGiga> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Cubic<T> operator /(Cubic<T> a, Cubic<IMega> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Cubic<T> operator /(Cubic<T> a, Cubic<IKilo> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Cubic<T> operator /(Cubic<T> a, Cubic<IHecto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Cubic<T> operator /(Cubic<T> a, Cubic<IDeca> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Cubic<T> operator /(Cubic<T> a, Cubic<INone> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Cubic<T> operator /(Cubic<T> a, Cubic<IDeci> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Cubic<T> operator /(Cubic<T> a, Cubic<ICenti> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Cubic<T> operator /(Cubic<T> a, Cubic<IMilli> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Cubic<T> operator /(Cubic<T> a, Cubic<IMicro> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Cubic<T> operator /(Cubic<T> a, Cubic<INano> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Cubic<T> operator /(Cubic<T> a, Cubic<IPico> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Cubic<T> operator /(Cubic<T> a, Cubic<IFemto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Cubic<T> operator /(Cubic<T> a, Cubic<IAtto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Cubic<T> operator /(Cubic<T> a, Cubic<IZepto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region PERCENT

    public static Cubic<T> operator %(Cubic<T> a, Cubic<IYotta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Cubic<T> operator %(Cubic<T> a, Cubic<IZetta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Cubic<T> operator %(Cubic<T> a, Cubic<IExa> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Cubic<T> operator %(Cubic<T> a, Cubic<IPeta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Cubic<T> operator %(Cubic<T> a, Cubic<ITera> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Cubic<T> operator %(Cubic<T> a, Cubic<IGiga> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Cubic<T> operator %(Cubic<T> a, Cubic<IMega> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Cubic<T> operator %(Cubic<T> a, Cubic<IKilo> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Cubic<T> operator %(Cubic<T> a, Cubic<IHecto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Cubic<T> operator %(Cubic<T> a, Cubic<IDeca> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Cubic<T> operator %(Cubic<T> a, Cubic<INone> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Cubic<T> operator %(Cubic<T> a, Cubic<IDeci> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Cubic<T> operator %(Cubic<T> a, Cubic<ICenti> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Cubic<T> operator %(Cubic<T> a, Cubic<IMilli> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Cubic<T> operator %(Cubic<T> a, Cubic<IMicro> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Cubic<T> operator %(Cubic<T> a, Cubic<INano> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Cubic<T> operator %(Cubic<T> a, Cubic<IPico> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Cubic<T> operator %(Cubic<T> a, Cubic<IFemto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Cubic<T> operator %(Cubic<T> a, Cubic<IAtto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Cubic<T> operator %(Cubic<T> a, Cubic<IZepto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region EQUAL

    public static bool operator ==(Cubic<T> a, Cubic<IYotta> i)
        => Unit.Convert(i.m_value, Prefix.Exa, Prefix) == a.m_value;

    public static bool operator !=(Cubic<T> a, Cubic<IYotta> i)
        => Unit.Convert(i.m_value, Prefix.Exa, Prefix) != a.m_value;

    public static bool operator >(Cubic<T> a, Cubic<IYotta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator <(Cubic<T> a, Cubic<IYotta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator >=(Cubic<T> a, Cubic<IYotta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator <=(Cubic<T> a, Cubic<IYotta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator ==(Cubic<T> a, Cubic<IZetta> i)
        => Unit.Convert(i.m_value, Prefix.Zetta, Prefix) == a.m_value;

    public static bool operator !=(Cubic<T> a, Cubic<IZetta> i)
        => Unit.Convert(i.m_value, Prefix.Zetta, Prefix) != a.m_value;

    public static bool operator >(Cubic<T> a, Cubic<IZetta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static bool operator <(Cubic<T> a, Cubic<IZetta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static bool operator >=(Cubic<T> a, Cubic<IZetta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static bool operator <=(Cubic<T> a, Cubic<IZetta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static bool operator ==(Cubic<T> a, Cubic<IExa> i)
        => Unit.Convert(i.m_value, Prefix.Exa, Prefix) == a.m_value;

    public static bool operator !=(Cubic<T> a, Cubic<IExa> i)
        => Unit.Convert(i.m_value, Prefix.Exa, Prefix) != a.m_value;

    public static bool operator >(Cubic<T> a, Cubic<IExa> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator <(Cubic<T> a, Cubic<IExa> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator >=(Cubic<T> a, Cubic<IExa> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator <=(Cubic<T> a, Cubic<IExa> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator ==(Cubic<T> a, Cubic<IPeta> i)
        => Unit.Convert(i.m_value, Prefix.Peta, Prefix) == a.m_value;

    public static bool operator !=(Cubic<T> a, Cubic<IPeta> i)
        => Unit.Convert(i.m_value, Prefix.Peta, Prefix) != a.m_value;

    public static bool operator >(Cubic<T> a, Cubic<IPeta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static bool operator <(Cubic<T> a, Cubic<IPeta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static bool operator >=(Cubic<T> a, Cubic<IPeta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static bool operator <=(Cubic<T> a, Cubic<IPeta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static bool operator ==(Cubic<T> a, Cubic<ITera> i)
        => Unit.Convert(i.m_value, Prefix.Tera, Prefix) == a.m_value;

    public static bool operator !=(Cubic<T> a, Cubic<ITera> i)
        => Unit.Convert(i.m_value, Prefix.Tera, Prefix) != a.m_value;

    public static bool operator >(Cubic<T> a, Cubic<ITera> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static bool operator <(Cubic<T> a, Cubic<ITera> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static bool operator >=(Cubic<T> a, Cubic<ITera> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static bool operator <=(Cubic<T> a, Cubic<ITera> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static bool operator ==(Cubic<T> a, Cubic<IGiga> i)
        => Unit.Convert(i.m_value, Prefix.Giga, Prefix) == a.m_value;

    public static bool operator !=(Cubic<T> a, Cubic<IGiga> i)
        => Unit.Convert(i.m_value, Prefix.Giga, Prefix) != a.m_value;

    public static bool operator >(Cubic<T> a, Cubic<IGiga> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static bool operator <(Cubic<T> a, Cubic<IGiga> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static bool operator >=(Cubic<T> a, Cubic<IGiga> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static bool operator <=(Cubic<T> a, Cubic<IGiga> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static bool operator ==(Cubic<T> a, Cubic<IMega> i)
        => Unit.Convert(i.m_value, Prefix.Mega, Prefix) == a.m_value;

    public static bool operator !=(Cubic<T> a, Cubic<IMega> i)
        => Unit.Convert(i.m_value, Prefix.Mega, Prefix) != a.m_value;

    public static bool operator >(Cubic<T> a, Cubic<IMega> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static bool operator <(Cubic<T> a, Cubic<IMega> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static bool operator >=(Cubic<T> a, Cubic<IMega> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static bool operator <=(Cubic<T> a, Cubic<IMega> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static bool operator ==(Cubic<T> a, Cubic<IKilo> i)
        => Unit.Convert(i.m_value, Prefix.Kilo, Prefix) == a.m_value;

    public static bool operator !=(Cubic<T> a, Cubic<IKilo> i)
        => Unit.Convert(i.m_value, Prefix.Kilo, Prefix) != a.m_value;

    public static bool operator >(Cubic<T> a, Cubic<IKilo> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static bool operator <(Cubic<T> a, Cubic<IKilo> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static bool operator >=(Cubic<T> a, Cubic<IKilo> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static bool operator <=(Cubic<T> a, Cubic<IKilo> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static bool operator ==(Cubic<T> a, Cubic<IHecto> i)
        => Unit.Convert(i.m_value, Prefix.Hecto, Prefix) == a.m_value;

    public static bool operator !=(Cubic<T> a, Cubic<IHecto> i)
        => Unit.Convert(i.m_value, Prefix.Hecto, Prefix) != a.m_value;

    public static bool operator >(Cubic<T> a, Cubic<IHecto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static bool operator <(Cubic<T> a, Cubic<IHecto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static bool operator >=(Cubic<T> a, Cubic<IHecto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static bool operator <=(Cubic<T> a, Cubic<IHecto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static bool operator ==(Cubic<T> a, Cubic<IDeca> i)
        => Unit.Convert(i.m_value, Prefix.Deca, Prefix) == a.m_value;

    public static bool operator !=(Cubic<T> a, Cubic<IDeca> i)
        => Unit.Convert(i.m_value, Prefix.Deca, Prefix) != a.m_value;

    public static bool operator >(Cubic<T> a, Cubic<IDeca> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static bool operator <(Cubic<T> a, Cubic<IDeca> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static bool operator >=(Cubic<T> a, Cubic<IDeca> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static bool operator <=(Cubic<T> a, Cubic<IDeca> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static bool operator ==(Cubic<T> a, Cubic<INone> i)
        => Unit.Convert(i.m_value, Prefix.None, Prefix) == a.m_value;

    public static bool operator !=(Cubic<T> a, Cubic<INone> i)
        => Unit.Convert(i.m_value, Prefix.Nano, Prefix) != a.m_value;

    public static bool operator >(Cubic<T> a, Cubic<INone> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static bool operator <(Cubic<T> a, Cubic<INone> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static bool operator >=(Cubic<T> a, Cubic<INone> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static bool operator <=(Cubic<T> a, Cubic<INone> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static bool operator ==(Cubic<T> a, Cubic<IDeci> i)
        => Unit.Convert(i.m_value, Prefix.Deci, Prefix) == a.m_value;

    public static bool operator !=(Cubic<T> a, Cubic<IDeci> i)
        => Unit.Convert(i.m_value, Prefix.Deci, Prefix) != a.m_value;

    public static bool operator >(Cubic<T> a, Cubic<IDeci> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static bool operator <(Cubic<T> a, Cubic<IDeci> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static bool operator >=(Cubic<T> a, Cubic<IDeci> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static bool operator <=(Cubic<T> a, Cubic<IDeci> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static bool operator ==(Cubic<T> a, Cubic<ICenti> i)
        => Unit.Convert(i.m_value, Prefix.Centi, Prefix) == a.m_value;

    public static bool operator !=(Cubic<T> a, Cubic<ICenti> i)
        => Unit.Convert(i.m_value, Prefix.Centi, Prefix) != a.m_value;

    public static bool operator >(Cubic<T> a, Cubic<ICenti> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static bool operator <(Cubic<T> a, Cubic<ICenti> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static bool operator >=(Cubic<T> a, Cubic<ICenti> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static bool operator <=(Cubic<T> a, Cubic<ICenti> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static bool operator ==(Cubic<T> a, Cubic<IMilli> i)
        => Unit.Convert(i.m_value, Prefix.Milli, Prefix) == a.m_value;

    public static bool operator !=(Cubic<T> a, Cubic<IMilli> i)
        => Unit.Convert(i.m_value, Prefix.Milli, Prefix) != a.m_value;

    public static bool operator >(Cubic<T> a, Cubic<IMilli> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static bool operator <(Cubic<T> a, Cubic<IMilli> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static bool operator >=(Cubic<T> a, Cubic<IMilli> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static bool operator <=(Cubic<T> a, Cubic<IMilli> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static bool operator ==(Cubic<T> a, Cubic<IMicro> i)
        => Unit.Convert(i.m_value, Prefix.Micro, Prefix) == a.m_value;

    public static bool operator !=(Cubic<T> a, Cubic<IMicro> i)
        => Unit.Convert(i.m_value, Prefix.Micro, Prefix) != a.m_value;

    public static bool operator >(Cubic<T> a, Cubic<IMicro> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static bool operator <(Cubic<T> a, Cubic<IMicro> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static bool operator >=(Cubic<T> a, Cubic<IMicro> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static bool operator <=(Cubic<T> a, Cubic<IMicro> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static bool operator ==(Cubic<T> a, Cubic<INano> i)
        => Unit.Convert(i.m_value, Prefix.Nano, Prefix) == a.m_value;

    public static bool operator !=(Cubic<T> a, Cubic<INano> i)
        => Unit.Convert(i.m_value, Prefix.Nano, Prefix) != a.m_value;

    public static bool operator >(Cubic<T> a, Cubic<INano> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static bool operator <(Cubic<T> a, Cubic<INano> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static bool operator >=(Cubic<T> a, Cubic<INano> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static bool operator <=(Cubic<T> a, Cubic<INano> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static bool operator ==(Cubic<T> a, Cubic<IPico> i)
        => Unit.Convert(i.m_value, Prefix.Pico, Prefix) == a.m_value;

    public static bool operator !=(Cubic<T> a, Cubic<IPico> i)
        => Unit.Convert(i.m_value, Prefix.Pico, Prefix) != a.m_value;

    public static bool operator >(Cubic<T> a, Cubic<IPico> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static bool operator <(Cubic<T> a, Cubic<IPico> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static bool operator >=(Cubic<T> a, Cubic<IPico> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static bool operator <=(Cubic<T> a, Cubic<IPico> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static bool operator ==(Cubic<T> a, Cubic<IFemto> i)
        => Unit.Convert(i.m_value, Prefix.Femto, Prefix) == a.m_value;

    public static bool operator !=(Cubic<T> a, Cubic<IFemto> i)
        => Unit.Convert(i.m_value, Prefix.Femto, Prefix) != a.m_value;

    public static bool operator >(Cubic<T> a, Cubic<IFemto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static bool operator <(Cubic<T> a, Cubic<IFemto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static bool operator >=(Cubic<T> a, Cubic<IFemto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static bool operator <=(Cubic<T> a, Cubic<IFemto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static bool operator ==(Cubic<T> a, Cubic<IAtto> i)
        => Unit.Convert(i.m_value, Prefix.Atto, Prefix) == a.m_value;

    public static bool operator !=(Cubic<T> a, Cubic<IAtto> i)
        => Unit.Convert(i.m_value, Prefix.Atto, Prefix) != a.m_value;

    public static bool operator >(Cubic<T> a, Cubic<IAtto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static bool operator <(Cubic<T> a, Cubic<IAtto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static bool operator >=(Cubic<T> a, Cubic<IAtto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static bool operator <=(Cubic<T> a, Cubic<IAtto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static bool operator ==(Cubic<T> a, Cubic<IZepto> i)
        => Unit.Convert(i.m_value, Prefix.Zepto, Prefix) == a.m_value;

    public static bool operator !=(Cubic<T> a, Cubic<IZepto> i)
        => Unit.Convert(i.m_value, Prefix.Zepto, Prefix) != a.m_value;

    public static bool operator >(Cubic<T> a, Cubic<IZepto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    public static bool operator <(Cubic<T> a, Cubic<IZepto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    public static bool operator >=(Cubic<T> a, Cubic<IZepto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    public static bool operator <=(Cubic<T> a, Cubic<IZepto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    public override bool Equals(object? obj) => m_value.Equals(obj) && Prefix.Equals(obj);

    public override int GetHashCode() => HashCode.Combine(m_value, Prefix);

    public override string ToString() => m_value.ToString(NumberFormatInfo.CurrentInfo) +
                                         Prefix.Attribute<SuffixAttribute>()!.Suffix + "m";
}