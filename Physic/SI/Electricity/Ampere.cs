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
public readonly struct Ampere : INone
{
    internal readonly decimal m_value; // Do not rename (binary serialization)

    public Ampere(decimal val, Prefix from = Prefix.None)
    {
        m_value = from == Prefix.None ? val : Unit.BaseValue(val, from);
    }

    public decimal Convert(Prefix target) => Unit.Convert(m_value, INone.Prefix, target);

    public static implicit operator Ampere(decimal val) => new(val);

    public static implicit operator Ampere<IYotta>(Ampere m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Yotta));
    public static implicit operator Ampere<IZetta>(Ampere m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Zetta));
    public static implicit operator Ampere<IExa>(Ampere m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Exa));
    public static implicit operator Ampere<IPeta>(Ampere m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Peta));
    public static implicit operator Ampere<ITera>(Ampere m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Tera));
    public static implicit operator Ampere<IGiga>(Ampere m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Giga));
    public static implicit operator Ampere<IMega>(Ampere m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Mega));
    public static implicit operator Ampere<IKilo>(Ampere m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Kilo));
    public static implicit operator Ampere<IHecto>(Ampere m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Hecto));
    public static implicit operator Ampere<IDeca>(Ampere m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Deca));
    public static implicit operator Ampere<INone>(Ampere m) => new(m.m_value);
    public static implicit operator Ampere<IDeci>(Ampere m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Deci));
    public static implicit operator Ampere<ICenti>(Ampere m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Centi));
    public static implicit operator Ampere<IMilli>(Ampere m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Milli));
    public static implicit operator Ampere<IMicro>(Ampere m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Micro));
    public static implicit operator Ampere<INano>(Ampere m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Nano));
    public static implicit operator Ampere<IPico>(Ampere m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Pico));
    public static implicit operator Ampere<IFemto>(Ampere m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Femto));
    public static implicit operator Ampere<IAtto>(Ampere m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Atto));
    public static implicit operator Ampere<IZepto>(Ampere m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Zepto));
    public static implicit operator Ampere<IYocto>(Ampere m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Yocto));


    #region PLUS

    public static Ampere operator +(Ampere a, Ampere<IYotta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Ampere operator +(Ampere a, Ampere<IZetta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Ampere operator +(Ampere a, Ampere<IExa> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Ampere operator +(Ampere a, Ampere<IPeta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Ampere operator +(Ampere a, Ampere<ITera> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Ampere operator +(Ampere a, Ampere<IGiga> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Ampere operator +(Ampere a, Ampere<IMega> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Ampere operator +(Ampere a, Ampere<IKilo> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Ampere operator +(Ampere a, Ampere<IHecto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Ampere operator +(Ampere a, Ampere<IDeca> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Ampere operator +(Ampere a, Ampere<INone> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Ampere operator +(Ampere a, Ampere<IDeci> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Ampere operator +(Ampere a, Ampere<ICenti> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Ampere operator +(Ampere a, Ampere<IMilli> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Ampere operator +(Ampere a, Ampere<IMicro> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Ampere operator +(Ampere a, Ampere<INano> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Ampere operator +(Ampere a, Ampere<IPico> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Ampere operator +(Ampere a, Ampere<IFemto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Ampere operator +(Ampere a, Ampere<IAtto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Ampere operator +(Ampere a, Ampere<IZepto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region MINUS

    public static Ampere operator -(Ampere a, Ampere<IYotta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Ampere operator -(Ampere a, Ampere<IZetta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Ampere operator -(Ampere a, Ampere<IExa> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Ampere operator -(Ampere a, Ampere<IPeta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Ampere operator -(Ampere a, Ampere<ITera> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Ampere operator -(Ampere a, Ampere<IGiga> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Ampere operator -(Ampere a, Ampere<IMega> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Ampere operator -(Ampere a, Ampere<IKilo> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Ampere operator -(Ampere a, Ampere<IHecto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Ampere operator -(Ampere a, Ampere<IDeca> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Ampere operator -(Ampere a, Ampere<INone> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Ampere operator -(Ampere a, Ampere<IDeci> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Ampere operator -(Ampere a, Ampere<ICenti> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Ampere operator -(Ampere a, Ampere<IMilli> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Ampere operator -(Ampere a, Ampere<IMicro> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Ampere operator -(Ampere a, Ampere<INano> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Ampere operator -(Ampere a, Ampere<IPico> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Ampere operator -(Ampere a, Ampere<IFemto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Ampere operator -(Ampere a, Ampere<IAtto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Ampere operator -(Ampere a, Ampere<IZepto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region MULTI

    public static Ampere operator *(Ampere a, Ampere<IYotta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Ampere operator *(Ampere a, Ampere<IZetta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Ampere operator *(Ampere a, Ampere<IExa> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Ampere operator *(Ampere a, Ampere<IPeta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Ampere operator *(Ampere a, Ampere<ITera> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Ampere operator *(Ampere a, Ampere<IGiga> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Ampere operator *(Ampere a, Ampere<IMega> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Ampere operator *(Ampere a, Ampere<IKilo> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Ampere operator *(Ampere a, Ampere<IHecto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Ampere operator *(Ampere a, Ampere<IDeca> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Ampere operator *(Ampere a, Ampere<INone> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Ampere operator *(Ampere a, Ampere<IDeci> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Ampere operator *(Ampere a, Ampere<ICenti> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Ampere operator *(Ampere a, Ampere<IMilli> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Ampere operator *(Ampere a, Ampere<IMicro> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Ampere operator *(Ampere a, Ampere<INano> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Ampere operator *(Ampere a, Ampere<IPico> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Ampere operator *(Ampere a, Ampere<IFemto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Ampere operator *(Ampere a, Ampere<IAtto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Ampere operator *(Ampere a, Ampere<IZepto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region DIV

    public static Ampere operator /(Ampere a, Ampere<IYotta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Ampere operator /(Ampere a, Ampere<IZetta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Ampere operator /(Ampere a, Ampere<IExa> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Ampere operator /(Ampere a, Ampere<IPeta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Ampere operator /(Ampere a, Ampere<ITera> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Ampere operator /(Ampere a, Ampere<IGiga> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Ampere operator /(Ampere a, Ampere<IMega> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Ampere operator /(Ampere a, Ampere<IKilo> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Ampere operator /(Ampere a, Ampere<IHecto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Ampere operator /(Ampere a, Ampere<IDeca> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Ampere operator /(Ampere a, Ampere<INone> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Ampere operator /(Ampere a, Ampere<IDeci> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Ampere operator /(Ampere a, Ampere<ICenti> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Ampere operator /(Ampere a, Ampere<IMilli> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Ampere operator /(Ampere a, Ampere<IMicro> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Ampere operator /(Ampere a, Ampere<INano> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Ampere operator /(Ampere a, Ampere<IPico> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Ampere operator /(Ampere a, Ampere<IFemto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Ampere operator /(Ampere a, Ampere<IAtto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Ampere operator /(Ampere a, Ampere<IZepto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region PERCENT

    public static Ampere operator %(Ampere a, Ampere<IYotta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Ampere operator %(Ampere a, Ampere<IZetta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Ampere operator %(Ampere a, Ampere<IExa> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Ampere operator %(Ampere a, Ampere<IPeta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Ampere operator %(Ampere a, Ampere<ITera> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Ampere operator %(Ampere a, Ampere<IGiga> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Ampere operator %(Ampere a, Ampere<IMega> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Ampere operator %(Ampere a, Ampere<IKilo> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Ampere operator %(Ampere a, Ampere<IHecto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Ampere operator %(Ampere a, Ampere<IDeca> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Ampere operator %(Ampere a, Ampere<INone> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Ampere operator %(Ampere a, Ampere<IDeci> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Ampere operator %(Ampere a, Ampere<ICenti> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Ampere operator %(Ampere a, Ampere<IMilli> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Ampere operator %(Ampere a, Ampere<IMicro> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Ampere operator %(Ampere a, Ampere<INano> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Ampere operator %(Ampere a, Ampere<IPico> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Ampere operator %(Ampere a, Ampere<IFemto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Ampere operator %(Ampere a, Ampere<IAtto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Ampere operator %(Ampere a, Ampere<IZepto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region EQUAL

    public static bool operator ==(Ampere a, Ampere<IYotta> i)
        => Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix) == a.m_value;

    public static bool operator !=(Ampere a, Ampere<IYotta> i)
        => Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix) != a.m_value;

    public static bool operator >(Ampere a, Ampere<IYotta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator <(Ampere a, Ampere<IYotta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator >=(Ampere a, Ampere<IYotta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator <=(Ampere a, Ampere<IYotta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator ==(Ampere a, Ampere<IZetta> i)
        => Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix) == a.m_value;

    public static bool operator !=(Ampere a, Ampere<IZetta> i)
        => Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix) != a.m_value;

    public static bool operator >(Ampere a, Ampere<IZetta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static bool operator <(Ampere a, Ampere<IZetta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static bool operator >=(Ampere a, Ampere<IZetta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static bool operator <=(Ampere a, Ampere<IZetta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static bool operator ==(Ampere a, Ampere<IExa> i)
        => Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix) == a.m_value;

    public static bool operator !=(Ampere a, Ampere<IExa> i)
        => Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix) != a.m_value;

    public static bool operator >(Ampere a, Ampere<IExa> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator <(Ampere a, Ampere<IExa> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator >=(Ampere a, Ampere<IExa> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator <=(Ampere a, Ampere<IExa> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator ==(Ampere a, Ampere<IPeta> i)
        => Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix) == a.m_value;

    public static bool operator !=(Ampere a, Ampere<IPeta> i)
        => Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix) != a.m_value;

    public static bool operator >(Ampere a, Ampere<IPeta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static bool operator <(Ampere a, Ampere<IPeta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static bool operator >=(Ampere a, Ampere<IPeta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static bool operator <=(Ampere a, Ampere<IPeta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static bool operator ==(Ampere a, Ampere<ITera> i)
        => Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix) == a.m_value;

    public static bool operator !=(Ampere a, Ampere<ITera> i)
        => Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix) != a.m_value;

    public static bool operator >(Ampere a, Ampere<ITera> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static bool operator <(Ampere a, Ampere<ITera> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static bool operator >=(Ampere a, Ampere<ITera> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static bool operator <=(Ampere a, Ampere<ITera> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static bool operator ==(Ampere a, Ampere<IGiga> i)
        => Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix) == a.m_value;

    public static bool operator !=(Ampere a, Ampere<IGiga> i)
        => Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix) != a.m_value;

    public static bool operator >(Ampere a, Ampere<IGiga> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static bool operator <(Ampere a, Ampere<IGiga> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static bool operator >=(Ampere a, Ampere<IGiga> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static bool operator <=(Ampere a, Ampere<IGiga> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static bool operator ==(Ampere a, Ampere<IMega> i)
        => Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix) == a.m_value;

    public static bool operator !=(Ampere a, Ampere<IMega> i)
        => Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix) != a.m_value;

    public static bool operator >(Ampere a, Ampere<IMega> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static bool operator <(Ampere a, Ampere<IMega> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static bool operator >=(Ampere a, Ampere<IMega> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static bool operator <=(Ampere a, Ampere<IMega> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static bool operator ==(Ampere a, Ampere<IKilo> i)
        => Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix) == a.m_value;

    public static bool operator !=(Ampere a, Ampere<IKilo> i)
        => Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix) != a.m_value;

    public static bool operator >(Ampere a, Ampere<IKilo> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static bool operator <(Ampere a, Ampere<IKilo> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static bool operator >=(Ampere a, Ampere<IKilo> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static bool operator <=(Ampere a, Ampere<IKilo> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static bool operator ==(Ampere a, Ampere<IHecto> i)
        => Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix) == a.m_value;

    public static bool operator !=(Ampere a, Ampere<IHecto> i)
        => Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix) != a.m_value;

    public static bool operator >(Ampere a, Ampere<IHecto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static bool operator <(Ampere a, Ampere<IHecto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static bool operator >=(Ampere a, Ampere<IHecto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static bool operator <=(Ampere a, Ampere<IHecto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static bool operator ==(Ampere a, Ampere<IDeca> i)
        => Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix) == a.m_value;

    public static bool operator !=(Ampere a, Ampere<IDeca> i)
        => Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix) != a.m_value;

    public static bool operator >(Ampere a, Ampere<IDeca> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static bool operator <(Ampere a, Ampere<IDeca> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static bool operator >=(Ampere a, Ampere<IDeca> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static bool operator <=(Ampere a, Ampere<IDeca> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static bool operator ==(Ampere a, Ampere<INone> i)
        => Unit.Convert(i.m_value, Prefix.None, INone.Prefix) == a.m_value;

    public static bool operator !=(Ampere a, Ampere<INone> i)
        => Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix) != a.m_value;

    public static bool operator >(Ampere a, Ampere<INone> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static bool operator <(Ampere a, Ampere<INone> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static bool operator >=(Ampere a, Ampere<INone> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static bool operator <=(Ampere a, Ampere<INone> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static bool operator ==(Ampere a, Ampere<IDeci> i)
        => Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix) == a.m_value;

    public static bool operator !=(Ampere a, Ampere<IDeci> i)
        => Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix) != a.m_value;

    public static bool operator >(Ampere a, Ampere<IDeci> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static bool operator <(Ampere a, Ampere<IDeci> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static bool operator >=(Ampere a, Ampere<IDeci> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static bool operator <=(Ampere a, Ampere<IDeci> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static bool operator ==(Ampere a, Ampere<ICenti> i)
        => Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix) == a.m_value;

    public static bool operator !=(Ampere a, Ampere<ICenti> i)
        => Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix) != a.m_value;

    public static bool operator >(Ampere a, Ampere<ICenti> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static bool operator <(Ampere a, Ampere<ICenti> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static bool operator >=(Ampere a, Ampere<ICenti> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static bool operator <=(Ampere a, Ampere<ICenti> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static bool operator ==(Ampere a, Ampere<IMilli> i)
        => Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix) == a.m_value;

    public static bool operator !=(Ampere a, Ampere<IMilli> i)
        => Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix) != a.m_value;

    public static bool operator >(Ampere a, Ampere<IMilli> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static bool operator <(Ampere a, Ampere<IMilli> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static bool operator >=(Ampere a, Ampere<IMilli> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static bool operator <=(Ampere a, Ampere<IMilli> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static bool operator ==(Ampere a, Ampere<IMicro> i)
        => Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix) == a.m_value;

    public static bool operator !=(Ampere a, Ampere<IMicro> i)
        => Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix) != a.m_value;

    public static bool operator >(Ampere a, Ampere<IMicro> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static bool operator <(Ampere a, Ampere<IMicro> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static bool operator >=(Ampere a, Ampere<IMicro> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static bool operator <=(Ampere a, Ampere<IMicro> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static bool operator ==(Ampere a, Ampere<INano> i)
        => Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix) == a.m_value;

    public static bool operator !=(Ampere a, Ampere<INano> i)
        => Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix) != a.m_value;

    public static bool operator >(Ampere a, Ampere<INano> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static bool operator <(Ampere a, Ampere<INano> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static bool operator >=(Ampere a, Ampere<INano> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static bool operator <=(Ampere a, Ampere<INano> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static bool operator ==(Ampere a, Ampere<IPico> i)
        => Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix) == a.m_value;

    public static bool operator !=(Ampere a, Ampere<IPico> i)
        => Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix) != a.m_value;

    public static bool operator >(Ampere a, Ampere<IPico> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static bool operator <(Ampere a, Ampere<IPico> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static bool operator >=(Ampere a, Ampere<IPico> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static bool operator <=(Ampere a, Ampere<IPico> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static bool operator ==(Ampere a, Ampere<IFemto> i)
        => Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix) == a.m_value;

    public static bool operator !=(Ampere a, Ampere<IFemto> i)
        => Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix) != a.m_value;

    public static bool operator >(Ampere a, Ampere<IFemto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static bool operator <(Ampere a, Ampere<IFemto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static bool operator >=(Ampere a, Ampere<IFemto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static bool operator <=(Ampere a, Ampere<IFemto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static bool operator ==(Ampere a, Ampere<IAtto> i)
        => Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix) == a.m_value;

    public static bool operator !=(Ampere a, Ampere<IAtto> i)
        => Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix) != a.m_value;

    public static bool operator >(Ampere a, Ampere<IAtto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static bool operator <(Ampere a, Ampere<IAtto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static bool operator >=(Ampere a, Ampere<IAtto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static bool operator <=(Ampere a, Ampere<IAtto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static bool operator ==(Ampere a, Ampere<IZepto> i)
        => Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix) == a.m_value;

    public static bool operator !=(Ampere a, Ampere<IZepto> i)
        => Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix) != a.m_value;

    public static bool operator >(Ampere a, Ampere<IZepto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    public static bool operator <(Ampere a, Ampere<IZepto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    public static bool operator >=(Ampere a, Ampere<IZepto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    public static bool operator <=(Ampere a, Ampere<IZepto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    public override bool Equals(object? obj) => m_value.Equals(obj) && INone.Prefix.Equals(obj);

    public override int GetHashCode() => HashCode.Combine(m_value, INone.Prefix);

    public override string ToString() => m_value.ToString(NumberFormatInfo.CurrentInfo) + "a";
}

/// <summary>
/// Represent the si unit for electricity
/// </summary>
/// <typeparam name="T">The SI Prefix</typeparam>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Ampere<T> where T : SI
{
    internal readonly decimal m_value; // Do not rename (binary serialization)

    public Ampere(decimal val)
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

    public static implicit operator Ampere<T>(decimal val) => new(val);

    #region IMPL_Ampere

    public static implicit operator Ampere<T>(Ampere<IYotta> i)
    {
        return Prefix switch
        {
            Prefix.Yotta => new Ampere<T>(i.m_value),
            Prefix.None => new Ampere<T>(Unit.BaseValue(i.m_value, Prefix.Yotta)),
            _ => new Ampere<T>(Unit.Convert(i.m_value, Prefix.Yotta, Prefix))
        };
    }

    public static implicit operator Ampere<T>(Ampere<IZetta> i)
    {
        return Prefix switch
        {
            Prefix.Zetta => new Ampere<T>(i.m_value),
            Prefix.None => new Ampere<T>(Unit.BaseValue(i.m_value, Prefix.Zetta)),
            _ => new Ampere<T>(Unit.Convert(i.m_value, Prefix.Zetta, Prefix))
        };
    }

    public static implicit operator Ampere<T>(Ampere<IExa> i)
    {
        return Prefix switch
        {
            Prefix.Exa => new Ampere<T>(i.m_value),
            Prefix.None => new Ampere<T>(Unit.BaseValue(i.m_value, Prefix.Exa)),
            _ => new Ampere<T>(Unit.Convert(i.m_value, Prefix.Exa, Prefix))
        };
    }

    public static implicit operator Ampere<T>(Ampere<IPeta> i)
    {
        return Prefix switch
        {
            Prefix.Peta => new Ampere<T>(i.m_value),
            Prefix.None => new Ampere<T>(Unit.BaseValue(i.m_value, Prefix.Peta)),
            _ => new Ampere<T>(Unit.Convert(i.m_value, Prefix.Peta, Prefix))
        };
    }

    public static implicit operator Ampere<T>(Ampere<ITera> i)
    {
        return Prefix switch
        {
            Prefix.Tera => new Ampere<T>(i.m_value),
            Prefix.None => new Ampere<T>(Unit.BaseValue(i.m_value, Prefix.Tera)),
            _ => new Ampere<T>(Unit.Convert(i.m_value, Prefix.Tera, Prefix))
        };
    }

    public static implicit operator Ampere<T>(Ampere<IGiga> i)
    {
        return Prefix switch
        {
            Prefix.Giga => new Ampere<T>(i.m_value),
            Prefix.None => new Ampere<T>(Unit.BaseValue(i.m_value, Prefix.Giga)),
            _ => new Ampere<T>(Unit.Convert(i.m_value, Prefix.Giga, Prefix))
        };
    }

    public static implicit operator Ampere<T>(Ampere<IMega> i)
    {
        return Prefix switch
        {
            Prefix.Mega => new Ampere<T>(i.m_value),
            Prefix.None => new Ampere<T>(Unit.BaseValue(i.m_value, Prefix.Mega)),
            _ => new Ampere<T>(Unit.Convert(i.m_value, Prefix.Mega, Prefix))
        };
    }

    public static implicit operator Ampere<T>(Ampere<IKilo> i)
    {
        return Prefix switch
        {
            Prefix.Kilo => new Ampere<T>(i.m_value),
            Prefix.None => new Ampere<T>(Unit.BaseValue(i.m_value, Prefix.Kilo)),
            _ => new Ampere<T>(Unit.Convert(i.m_value, Prefix.Kilo, Prefix))
        };
    }

    public static implicit operator Ampere<T>(Ampere<IHecto> i)
    {
        return Prefix switch
        {
            Prefix.Hecto => new Ampere<T>(i.m_value),
            Prefix.None => new Ampere<T>(Unit.BaseValue(i.m_value, Prefix.Hecto)),
            _ => new Ampere<T>(Unit.Convert(i.m_value, Prefix.Hecto, Prefix))
        };
    }

    public static implicit operator Ampere<T>(Ampere<IDeca> i)
    {
        return Prefix switch
        {
            Prefix.Deca => new Ampere<T>(i.m_value),
            Prefix.None => new Ampere<T>(Unit.BaseValue(i.m_value, Prefix.Deca)),
            _ => new Ampere<T>(Unit.Convert(i.m_value, Prefix.Deca, Prefix))
        };
    }

    public static implicit operator Ampere<T>(Ampere<INone> i) => new Ampere<T>(i.m_value);

    public static implicit operator Ampere<T>(Ampere<IDeci> i)
    {
        return Prefix switch
        {
            Prefix.Deci => new Ampere<T>(i.m_value),
            Prefix.None => new Ampere<T>(Unit.BaseValue(i.m_value, Prefix.Deci)),
            _ => new Ampere<T>(Unit.Convert(i.m_value, Prefix.Deci, Prefix))
        };
    }

    public static implicit operator Ampere<T>(Ampere<ICenti> i)
    {
        return Prefix switch
        {
            Prefix.Centi => new Ampere<T>(i.m_value),
            Prefix.None => new Ampere<T>(Unit.BaseValue(i.m_value, Prefix.Centi)),
            _ => new Ampere<T>(Unit.Convert(i.m_value, Prefix.Centi, Prefix))
        };
    }

    public static implicit operator Ampere<T>(Ampere<IMilli> i)
    {
        return Prefix switch
        {
            Prefix.Milli => new Ampere<T>(i.m_value),
            Prefix.None => new Ampere<T>(Unit.BaseValue(i.m_value, Prefix.Milli)),
            _ => new Ampere<T>(Unit.Convert(i.m_value, Prefix.Milli, Prefix))
        };
    }

    public static implicit operator Ampere<T>(Ampere<IMicro> i)
    {
        return Prefix switch
        {
            Prefix.Micro => new Ampere<T>(i.m_value),
            Prefix.None => new Ampere<T>(Unit.BaseValue(i.m_value, Prefix.Micro)),
            _ => new Ampere<T>(Unit.Convert(i.m_value, Prefix.Micro, Prefix))
        };
    }

    public static implicit operator Ampere<T>(Ampere<INano> i)
    {
        return Prefix switch
        {
            Prefix.Nano => new Ampere<T>(i.m_value),
            Prefix.None => new Ampere<T>(Unit.BaseValue(i.m_value, Prefix.Nano)),
            _ => new Ampere<T>(Unit.Convert(i.m_value, Prefix.Nano, Prefix))
        };
    }

    public static implicit operator Ampere<T>(Ampere<IPico> i)
    {
        return Prefix switch
        {
            Prefix.Pico => new Ampere<T>(i.m_value),
            Prefix.None => new Ampere<T>(Unit.BaseValue(i.m_value, Prefix.Pico)),
            _ => new Ampere<T>(Unit.Convert(i.m_value, Prefix.Pico, Prefix))
        };
    }

    public static implicit operator Ampere<T>(Ampere<IFemto> i)
    {
        return Prefix switch
        {
            Prefix.Femto => new Ampere<T>(i.m_value),
            Prefix.None => new Ampere<T>(Unit.BaseValue(i.m_value, Prefix.Femto)),
            _ => new Ampere<T>(Unit.Convert(i.m_value, Prefix.Femto, Prefix))
        };
    }

    public static implicit operator Ampere<T>(Ampere<IAtto> i)
    {
        return Prefix switch
        {
            Prefix.Atto => new Ampere<T>(i.m_value),
            Prefix.None => new Ampere<T>(Unit.BaseValue(i.m_value, Prefix.Atto)),
            _ => new Ampere<T>(Unit.Convert(i.m_value, Prefix.Atto, Prefix))
        };
    }

    public static implicit operator Ampere<T>(Ampere<IZepto> i)
    {
        return Prefix switch
        {
            Prefix.Zepto => new Ampere<T>(i.m_value),
            Prefix.None => new Ampere<T>(Unit.BaseValue(i.m_value, Prefix.Zepto)),
            _ => new Ampere<T>(Unit.Convert(i.m_value, Prefix.Zepto, Prefix))
        };
    }

    #endregion

    #region PLUS

    public static Ampere<T> operator +(Ampere<T> a, Ampere<IYotta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Ampere<T> operator +(Ampere<T> a, Ampere<IZetta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Ampere<T> operator +(Ampere<T> a, Ampere<IExa> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Ampere<T> operator +(Ampere<T> a, Ampere<IPeta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Ampere<T> operator +(Ampere<T> a, Ampere<ITera> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Ampere<T> operator +(Ampere<T> a, Ampere<IGiga> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Ampere<T> operator +(Ampere<T> a, Ampere<IMega> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Ampere<T> operator +(Ampere<T> a, Ampere<IKilo> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Ampere<T> operator +(Ampere<T> a, Ampere<IHecto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Ampere<T> operator +(Ampere<T> a, Ampere<IDeca> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Ampere<T> operator +(Ampere<T> a, Ampere<INone> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Ampere<T> operator +(Ampere<T> a, Ampere<IDeci> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Ampere<T> operator +(Ampere<T> a, Ampere<ICenti> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Ampere<T> operator +(Ampere<T> a, Ampere<IMilli> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Ampere<T> operator +(Ampere<T> a, Ampere<IMicro> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Ampere<T> operator +(Ampere<T> a, Ampere<INano> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Ampere<T> operator +(Ampere<T> a, Ampere<IPico> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Ampere<T> operator +(Ampere<T> a, Ampere<IFemto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Ampere<T> operator +(Ampere<T> a, Ampere<IAtto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Ampere<T> operator +(Ampere<T> a, Ampere<IZepto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region MINUS

    public static Ampere<T> operator -(Ampere<T> a, Ampere<IYotta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Ampere<T> operator -(Ampere<T> a, Ampere<IZetta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Ampere<T> operator -(Ampere<T> a, Ampere<IExa> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Ampere<T> operator -(Ampere<T> a, Ampere<IPeta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Ampere<T> operator -(Ampere<T> a, Ampere<ITera> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Ampere<T> operator -(Ampere<T> a, Ampere<IGiga> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Ampere<T> operator -(Ampere<T> a, Ampere<IMega> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Ampere<T> operator -(Ampere<T> a, Ampere<IKilo> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Ampere<T> operator -(Ampere<T> a, Ampere<IHecto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Ampere<T> operator -(Ampere<T> a, Ampere<IDeca> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Ampere<T> operator -(Ampere<T> a, Ampere<INone> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Ampere<T> operator -(Ampere<T> a, Ampere<IDeci> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Ampere<T> operator -(Ampere<T> a, Ampere<ICenti> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Ampere<T> operator -(Ampere<T> a, Ampere<IMilli> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Ampere<T> operator -(Ampere<T> a, Ampere<IMicro> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Ampere<T> operator -(Ampere<T> a, Ampere<INano> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Ampere<T> operator -(Ampere<T> a, Ampere<IPico> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Ampere<T> operator -(Ampere<T> a, Ampere<IFemto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Ampere<T> operator -(Ampere<T> a, Ampere<IAtto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Ampere<T> operator -(Ampere<T> a, Ampere<IZepto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region MULTI

    public static Ampere<T> operator *(Ampere<T> a, Ampere<IYotta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Ampere<T> operator *(Ampere<T> a, Ampere<IZetta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Ampere<T> operator *(Ampere<T> a, Ampere<IExa> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Ampere<T> operator *(Ampere<T> a, Ampere<IPeta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Ampere<T> operator *(Ampere<T> a, Ampere<ITera> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Ampere<T> operator *(Ampere<T> a, Ampere<IGiga> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Ampere<T> operator *(Ampere<T> a, Ampere<IMega> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Ampere<T> operator *(Ampere<T> a, Ampere<IKilo> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Ampere<T> operator *(Ampere<T> a, Ampere<IHecto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Ampere<T> operator *(Ampere<T> a, Ampere<IDeca> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Ampere<T> operator *(Ampere<T> a, Ampere<INone> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Ampere<T> operator *(Ampere<T> a, Ampere<IDeci> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Ampere<T> operator *(Ampere<T> a, Ampere<ICenti> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Ampere<T> operator *(Ampere<T> a, Ampere<IMilli> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Ampere<T> operator *(Ampere<T> a, Ampere<IMicro> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Ampere<T> operator *(Ampere<T> a, Ampere<INano> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Ampere<T> operator *(Ampere<T> a, Ampere<IPico> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Ampere<T> operator *(Ampere<T> a, Ampere<IFemto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Ampere<T> operator *(Ampere<T> a, Ampere<IAtto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Ampere<T> operator *(Ampere<T> a, Ampere<IZepto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region DIV

    public static Ampere<T> operator /(Ampere<T> a, Ampere<IYotta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Ampere<T> operator /(Ampere<T> a, Ampere<IZetta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Ampere<T> operator /(Ampere<T> a, Ampere<IExa> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Ampere<T> operator /(Ampere<T> a, Ampere<IPeta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Ampere<T> operator /(Ampere<T> a, Ampere<ITera> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Ampere<T> operator /(Ampere<T> a, Ampere<IGiga> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Ampere<T> operator /(Ampere<T> a, Ampere<IMega> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Ampere<T> operator /(Ampere<T> a, Ampere<IKilo> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Ampere<T> operator /(Ampere<T> a, Ampere<IHecto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Ampere<T> operator /(Ampere<T> a, Ampere<IDeca> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Ampere<T> operator /(Ampere<T> a, Ampere<INone> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Ampere<T> operator /(Ampere<T> a, Ampere<IDeci> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Ampere<T> operator /(Ampere<T> a, Ampere<ICenti> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Ampere<T> operator /(Ampere<T> a, Ampere<IMilli> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Ampere<T> operator /(Ampere<T> a, Ampere<IMicro> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Ampere<T> operator /(Ampere<T> a, Ampere<INano> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Ampere<T> operator /(Ampere<T> a, Ampere<IPico> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Ampere<T> operator /(Ampere<T> a, Ampere<IFemto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Ampere<T> operator /(Ampere<T> a, Ampere<IAtto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Ampere<T> operator /(Ampere<T> a, Ampere<IZepto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region PERCENT

    public static Ampere<T> operator %(Ampere<T> a, Ampere<IYotta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Ampere<T> operator %(Ampere<T> a, Ampere<IZetta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Ampere<T> operator %(Ampere<T> a, Ampere<IExa> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Ampere<T> operator %(Ampere<T> a, Ampere<IPeta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Ampere<T> operator %(Ampere<T> a, Ampere<ITera> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Ampere<T> operator %(Ampere<T> a, Ampere<IGiga> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Ampere<T> operator %(Ampere<T> a, Ampere<IMega> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Ampere<T> operator %(Ampere<T> a, Ampere<IKilo> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Ampere<T> operator %(Ampere<T> a, Ampere<IHecto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Ampere<T> operator %(Ampere<T> a, Ampere<IDeca> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Ampere<T> operator %(Ampere<T> a, Ampere<INone> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Ampere<T> operator %(Ampere<T> a, Ampere<IDeci> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Ampere<T> operator %(Ampere<T> a, Ampere<ICenti> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Ampere<T> operator %(Ampere<T> a, Ampere<IMilli> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Ampere<T> operator %(Ampere<T> a, Ampere<IMicro> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Ampere<T> operator %(Ampere<T> a, Ampere<INano> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Ampere<T> operator %(Ampere<T> a, Ampere<IPico> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Ampere<T> operator %(Ampere<T> a, Ampere<IFemto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Ampere<T> operator %(Ampere<T> a, Ampere<IAtto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Ampere<T> operator %(Ampere<T> a, Ampere<IZepto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region EQUAL

    public static bool operator ==(Ampere<T> a, Ampere<IYotta> i)
        => Unit.Convert(i.m_value, Prefix.Exa, Prefix) == a.m_value;

    public static bool operator !=(Ampere<T> a, Ampere<IYotta> i)
        => Unit.Convert(i.m_value, Prefix.Exa, Prefix) != a.m_value;

    public static bool operator >(Ampere<T> a, Ampere<IYotta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator <(Ampere<T> a, Ampere<IYotta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator >=(Ampere<T> a, Ampere<IYotta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator <=(Ampere<T> a, Ampere<IYotta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator ==(Ampere<T> a, Ampere<IZetta> i)
        => Unit.Convert(i.m_value, Prefix.Zetta, Prefix) == a.m_value;

    public static bool operator !=(Ampere<T> a, Ampere<IZetta> i)
        => Unit.Convert(i.m_value, Prefix.Zetta, Prefix) != a.m_value;

    public static bool operator >(Ampere<T> a, Ampere<IZetta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static bool operator <(Ampere<T> a, Ampere<IZetta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static bool operator >=(Ampere<T> a, Ampere<IZetta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static bool operator <=(Ampere<T> a, Ampere<IZetta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static bool operator ==(Ampere<T> a, Ampere<IExa> i)
        => Unit.Convert(i.m_value, Prefix.Exa, Prefix) == a.m_value;

    public static bool operator !=(Ampere<T> a, Ampere<IExa> i)
        => Unit.Convert(i.m_value, Prefix.Exa, Prefix) != a.m_value;

    public static bool operator >(Ampere<T> a, Ampere<IExa> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator <(Ampere<T> a, Ampere<IExa> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator >=(Ampere<T> a, Ampere<IExa> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator <=(Ampere<T> a, Ampere<IExa> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator ==(Ampere<T> a, Ampere<IPeta> i)
        => Unit.Convert(i.m_value, Prefix.Peta, Prefix) == a.m_value;

    public static bool operator !=(Ampere<T> a, Ampere<IPeta> i)
        => Unit.Convert(i.m_value, Prefix.Peta, Prefix) != a.m_value;

    public static bool operator >(Ampere<T> a, Ampere<IPeta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static bool operator <(Ampere<T> a, Ampere<IPeta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static bool operator >=(Ampere<T> a, Ampere<IPeta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static bool operator <=(Ampere<T> a, Ampere<IPeta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static bool operator ==(Ampere<T> a, Ampere<ITera> i)
        => Unit.Convert(i.m_value, Prefix.Tera, Prefix) == a.m_value;

    public static bool operator !=(Ampere<T> a, Ampere<ITera> i)
        => Unit.Convert(i.m_value, Prefix.Tera, Prefix) != a.m_value;

    public static bool operator >(Ampere<T> a, Ampere<ITera> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static bool operator <(Ampere<T> a, Ampere<ITera> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static bool operator >=(Ampere<T> a, Ampere<ITera> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static bool operator <=(Ampere<T> a, Ampere<ITera> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static bool operator ==(Ampere<T> a, Ampere<IGiga> i)
        => Unit.Convert(i.m_value, Prefix.Giga, Prefix) == a.m_value;

    public static bool operator !=(Ampere<T> a, Ampere<IGiga> i)
        => Unit.Convert(i.m_value, Prefix.Giga, Prefix) != a.m_value;

    public static bool operator >(Ampere<T> a, Ampere<IGiga> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static bool operator <(Ampere<T> a, Ampere<IGiga> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static bool operator >=(Ampere<T> a, Ampere<IGiga> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static bool operator <=(Ampere<T> a, Ampere<IGiga> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static bool operator ==(Ampere<T> a, Ampere<IMega> i)
        => Unit.Convert(i.m_value, Prefix.Mega, Prefix) == a.m_value;

    public static bool operator !=(Ampere<T> a, Ampere<IMega> i)
        => Unit.Convert(i.m_value, Prefix.Mega, Prefix) != a.m_value;

    public static bool operator >(Ampere<T> a, Ampere<IMega> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static bool operator <(Ampere<T> a, Ampere<IMega> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static bool operator >=(Ampere<T> a, Ampere<IMega> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static bool operator <=(Ampere<T> a, Ampere<IMega> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static bool operator ==(Ampere<T> a, Ampere<IKilo> i)
        => Unit.Convert(i.m_value, Prefix.Kilo, Prefix) == a.m_value;

    public static bool operator !=(Ampere<T> a, Ampere<IKilo> i)
        => Unit.Convert(i.m_value, Prefix.Kilo, Prefix) != a.m_value;

    public static bool operator >(Ampere<T> a, Ampere<IKilo> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static bool operator <(Ampere<T> a, Ampere<IKilo> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static bool operator >=(Ampere<T> a, Ampere<IKilo> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static bool operator <=(Ampere<T> a, Ampere<IKilo> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static bool operator ==(Ampere<T> a, Ampere<IHecto> i)
        => Unit.Convert(i.m_value, Prefix.Hecto, Prefix) == a.m_value;

    public static bool operator !=(Ampere<T> a, Ampere<IHecto> i)
        => Unit.Convert(i.m_value, Prefix.Hecto, Prefix) != a.m_value;

    public static bool operator >(Ampere<T> a, Ampere<IHecto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static bool operator <(Ampere<T> a, Ampere<IHecto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static bool operator >=(Ampere<T> a, Ampere<IHecto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static bool operator <=(Ampere<T> a, Ampere<IHecto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static bool operator ==(Ampere<T> a, Ampere<IDeca> i)
        => Unit.Convert(i.m_value, Prefix.Deca, Prefix) == a.m_value;

    public static bool operator !=(Ampere<T> a, Ampere<IDeca> i)
        => Unit.Convert(i.m_value, Prefix.Deca, Prefix) != a.m_value;

    public static bool operator >(Ampere<T> a, Ampere<IDeca> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static bool operator <(Ampere<T> a, Ampere<IDeca> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static bool operator >=(Ampere<T> a, Ampere<IDeca> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static bool operator <=(Ampere<T> a, Ampere<IDeca> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static bool operator ==(Ampere<T> a, Ampere<INone> i)
        => Unit.Convert(i.m_value, Prefix.None, Prefix) == a.m_value;

    public static bool operator !=(Ampere<T> a, Ampere<INone> i)
        => Unit.Convert(i.m_value, Prefix.Nano, Prefix) != a.m_value;

    public static bool operator >(Ampere<T> a, Ampere<INone> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static bool operator <(Ampere<T> a, Ampere<INone> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static bool operator >=(Ampere<T> a, Ampere<INone> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static bool operator <=(Ampere<T> a, Ampere<INone> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static bool operator ==(Ampere<T> a, Ampere<IDeci> i)
        => Unit.Convert(i.m_value, Prefix.Deci, Prefix) == a.m_value;

    public static bool operator !=(Ampere<T> a, Ampere<IDeci> i)
        => Unit.Convert(i.m_value, Prefix.Deci, Prefix) != a.m_value;

    public static bool operator >(Ampere<T> a, Ampere<IDeci> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static bool operator <(Ampere<T> a, Ampere<IDeci> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static bool operator >=(Ampere<T> a, Ampere<IDeci> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static bool operator <=(Ampere<T> a, Ampere<IDeci> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static bool operator ==(Ampere<T> a, Ampere<ICenti> i)
        => Unit.Convert(i.m_value, Prefix.Centi, Prefix) == a.m_value;

    public static bool operator !=(Ampere<T> a, Ampere<ICenti> i)
        => Unit.Convert(i.m_value, Prefix.Centi, Prefix) != a.m_value;

    public static bool operator >(Ampere<T> a, Ampere<ICenti> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static bool operator <(Ampere<T> a, Ampere<ICenti> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static bool operator >=(Ampere<T> a, Ampere<ICenti> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static bool operator <=(Ampere<T> a, Ampere<ICenti> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static bool operator ==(Ampere<T> a, Ampere<IMilli> i)
        => Unit.Convert(i.m_value, Prefix.Milli, Prefix) == a.m_value;

    public static bool operator !=(Ampere<T> a, Ampere<IMilli> i)
        => Unit.Convert(i.m_value, Prefix.Milli, Prefix) != a.m_value;

    public static bool operator >(Ampere<T> a, Ampere<IMilli> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static bool operator <(Ampere<T> a, Ampere<IMilli> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static bool operator >=(Ampere<T> a, Ampere<IMilli> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static bool operator <=(Ampere<T> a, Ampere<IMilli> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static bool operator ==(Ampere<T> a, Ampere<IMicro> i)
        => Unit.Convert(i.m_value, Prefix.Micro, Prefix) == a.m_value;

    public static bool operator !=(Ampere<T> a, Ampere<IMicro> i)
        => Unit.Convert(i.m_value, Prefix.Micro, Prefix) != a.m_value;

    public static bool operator >(Ampere<T> a, Ampere<IMicro> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static bool operator <(Ampere<T> a, Ampere<IMicro> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static bool operator >=(Ampere<T> a, Ampere<IMicro> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static bool operator <=(Ampere<T> a, Ampere<IMicro> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static bool operator ==(Ampere<T> a, Ampere<INano> i)
        => Unit.Convert(i.m_value, Prefix.Nano, Prefix) == a.m_value;

    public static bool operator !=(Ampere<T> a, Ampere<INano> i)
        => Unit.Convert(i.m_value, Prefix.Nano, Prefix) != a.m_value;

    public static bool operator >(Ampere<T> a, Ampere<INano> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static bool operator <(Ampere<T> a, Ampere<INano> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static bool operator >=(Ampere<T> a, Ampere<INano> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static bool operator <=(Ampere<T> a, Ampere<INano> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static bool operator ==(Ampere<T> a, Ampere<IPico> i)
        => Unit.Convert(i.m_value, Prefix.Pico, Prefix) == a.m_value;

    public static bool operator !=(Ampere<T> a, Ampere<IPico> i)
        => Unit.Convert(i.m_value, Prefix.Pico, Prefix) != a.m_value;

    public static bool operator >(Ampere<T> a, Ampere<IPico> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static bool operator <(Ampere<T> a, Ampere<IPico> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static bool operator >=(Ampere<T> a, Ampere<IPico> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static bool operator <=(Ampere<T> a, Ampere<IPico> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static bool operator ==(Ampere<T> a, Ampere<IFemto> i)
        => Unit.Convert(i.m_value, Prefix.Femto, Prefix) == a.m_value;

    public static bool operator !=(Ampere<T> a, Ampere<IFemto> i)
        => Unit.Convert(i.m_value, Prefix.Femto, Prefix) != a.m_value;

    public static bool operator >(Ampere<T> a, Ampere<IFemto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static bool operator <(Ampere<T> a, Ampere<IFemto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static bool operator >=(Ampere<T> a, Ampere<IFemto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static bool operator <=(Ampere<T> a, Ampere<IFemto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static bool operator ==(Ampere<T> a, Ampere<IAtto> i)
        => Unit.Convert(i.m_value, Prefix.Atto, Prefix) == a.m_value;

    public static bool operator !=(Ampere<T> a, Ampere<IAtto> i)
        => Unit.Convert(i.m_value, Prefix.Atto, Prefix) != a.m_value;

    public static bool operator >(Ampere<T> a, Ampere<IAtto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static bool operator <(Ampere<T> a, Ampere<IAtto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static bool operator >=(Ampere<T> a, Ampere<IAtto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static bool operator <=(Ampere<T> a, Ampere<IAtto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static bool operator ==(Ampere<T> a, Ampere<IZepto> i)
        => Unit.Convert(i.m_value, Prefix.Zepto, Prefix) == a.m_value;

    public static bool operator !=(Ampere<T> a, Ampere<IZepto> i)
        => Unit.Convert(i.m_value, Prefix.Zepto, Prefix) != a.m_value;

    public static bool operator >(Ampere<T> a, Ampere<IZepto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    public static bool operator <(Ampere<T> a, Ampere<IZepto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    public static bool operator >=(Ampere<T> a, Ampere<IZepto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    public static bool operator <=(Ampere<T> a, Ampere<IZepto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    public override bool Equals(object? obj) => m_value.Equals(obj) && Prefix.Equals(obj);

    public override int GetHashCode() => HashCode.Combine(m_value, Prefix);

    public override string ToString() => m_value.ToString(NumberFormatInfo.CurrentInfo) +
                                         Prefix.Attribute<SuffixAttribute>()!.Suffix + "a";
}