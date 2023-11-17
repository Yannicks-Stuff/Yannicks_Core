using System.Globalization;
using System.Runtime.InteropServices;
using Yannick.Extensions.EnumExtensions;
using Yannick.Lang.Attribute;
using Unit = Yannick.Physic.SI.Converter;

namespace Yannick.Physic.SI.Pressure;

/// <summary>
/// Represent the si unit for amount of substance
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Pascal : INone
{
    internal readonly decimal m_value; // Do not rename (binary serialization)

    public Pascal(decimal val, Prefix from = Prefix.None)
    {
        m_value = from == Prefix.None ? val : Unit.BaseValue(val, from);
    }

    public decimal Convert(Prefix target) => Unit.Convert(m_value, INone.Prefix, target);

    public static implicit operator Pascal(decimal val) => new(val);

    public static implicit operator Pascal<IYotta>(Pascal m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Yotta));
    public static implicit operator Pascal<IZetta>(Pascal m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Zetta));
    public static implicit operator Pascal<IExa>(Pascal m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Exa));
    public static implicit operator Pascal<IPeta>(Pascal m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Peta));
    public static implicit operator Pascal<ITera>(Pascal m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Tera));
    public static implicit operator Pascal<IGiga>(Pascal m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Giga));
    public static implicit operator Pascal<IMega>(Pascal m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Mega));
    public static implicit operator Pascal<IKilo>(Pascal m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Kilo));
    public static implicit operator Pascal<IHecto>(Pascal m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Hecto));
    public static implicit operator Pascal<IDeca>(Pascal m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Deca));
    public static implicit operator Pascal<INone>(Pascal m) => new(m.m_value);
    public static implicit operator Pascal<IDeci>(Pascal m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Deci));
    public static implicit operator Pascal<ICenti>(Pascal m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Centi));
    public static implicit operator Pascal<IMilli>(Pascal m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Milli));
    public static implicit operator Pascal<IMicro>(Pascal m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Micro));
    public static implicit operator Pascal<INano>(Pascal m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Nano));
    public static implicit operator Pascal<IPico>(Pascal m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Pico));
    public static implicit operator Pascal<IFemto>(Pascal m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Femto));
    public static implicit operator Pascal<IAtto>(Pascal m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Atto));
    public static implicit operator Pascal<IZepto>(Pascal m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Zepto));
    public static implicit operator Pascal<IYocto>(Pascal m) => new(Unit.Convert(m.m_value, Prefix.None, Prefix.Yocto));


    #region PLUS

    public static Pascal operator +(Pascal a, Pascal<IYotta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Pascal operator +(Pascal a, Pascal<IZetta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Pascal operator +(Pascal a, Pascal<IExa> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Pascal operator +(Pascal a, Pascal<IPeta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Pascal operator +(Pascal a, Pascal<ITera> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Pascal operator +(Pascal a, Pascal<IGiga> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Pascal operator +(Pascal a, Pascal<IMega> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Pascal operator +(Pascal a, Pascal<IKilo> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Pascal operator +(Pascal a, Pascal<IHecto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Pascal operator +(Pascal a, Pascal<IDeca> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Pascal operator +(Pascal a, Pascal<INone> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Pascal operator +(Pascal a, Pascal<IDeci> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Pascal operator +(Pascal a, Pascal<ICenti> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Pascal operator +(Pascal a, Pascal<IMilli> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Pascal operator +(Pascal a, Pascal<IMicro> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Pascal operator +(Pascal a, Pascal<INano> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Pascal operator +(Pascal a, Pascal<IPico> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Pascal operator +(Pascal a, Pascal<IFemto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Pascal operator +(Pascal a, Pascal<IAtto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Pascal operator +(Pascal a, Pascal<IZepto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region MINUS

    public static Pascal operator -(Pascal a, Pascal<IYotta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Pascal operator -(Pascal a, Pascal<IZetta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Pascal operator -(Pascal a, Pascal<IExa> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Pascal operator -(Pascal a, Pascal<IPeta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Pascal operator -(Pascal a, Pascal<ITera> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Pascal operator -(Pascal a, Pascal<IGiga> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Pascal operator -(Pascal a, Pascal<IMega> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Pascal operator -(Pascal a, Pascal<IKilo> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Pascal operator -(Pascal a, Pascal<IHecto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Pascal operator -(Pascal a, Pascal<IDeca> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Pascal operator -(Pascal a, Pascal<INone> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Pascal operator -(Pascal a, Pascal<IDeci> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Pascal operator -(Pascal a, Pascal<ICenti> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Pascal operator -(Pascal a, Pascal<IMilli> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Pascal operator -(Pascal a, Pascal<IMicro> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Pascal operator -(Pascal a, Pascal<INano> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Pascal operator -(Pascal a, Pascal<IPico> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Pascal operator -(Pascal a, Pascal<IFemto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Pascal operator -(Pascal a, Pascal<IAtto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Pascal operator -(Pascal a, Pascal<IZepto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region MULTI

    public static Pascal operator *(Pascal a, Pascal<IYotta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Pascal operator *(Pascal a, Pascal<IZetta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Pascal operator *(Pascal a, Pascal<IExa> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Pascal operator *(Pascal a, Pascal<IPeta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Pascal operator *(Pascal a, Pascal<ITera> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Pascal operator *(Pascal a, Pascal<IGiga> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Pascal operator *(Pascal a, Pascal<IMega> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Pascal operator *(Pascal a, Pascal<IKilo> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Pascal operator *(Pascal a, Pascal<IHecto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Pascal operator *(Pascal a, Pascal<IDeca> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Pascal operator *(Pascal a, Pascal<INone> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Pascal operator *(Pascal a, Pascal<IDeci> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Pascal operator *(Pascal a, Pascal<ICenti> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Pascal operator *(Pascal a, Pascal<IMilli> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Pascal operator *(Pascal a, Pascal<IMicro> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Pascal operator *(Pascal a, Pascal<INano> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Pascal operator *(Pascal a, Pascal<IPico> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Pascal operator *(Pascal a, Pascal<IFemto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Pascal operator *(Pascal a, Pascal<IAtto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Pascal operator *(Pascal a, Pascal<IZepto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region DIV

    public static Pascal operator /(Pascal a, Pascal<IYotta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Pascal operator /(Pascal a, Pascal<IZetta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Pascal operator /(Pascal a, Pascal<IExa> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Pascal operator /(Pascal a, Pascal<IPeta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Pascal operator /(Pascal a, Pascal<ITera> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Pascal operator /(Pascal a, Pascal<IGiga> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Pascal operator /(Pascal a, Pascal<IMega> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Pascal operator /(Pascal a, Pascal<IKilo> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Pascal operator /(Pascal a, Pascal<IHecto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Pascal operator /(Pascal a, Pascal<IDeca> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Pascal operator /(Pascal a, Pascal<INone> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Pascal operator /(Pascal a, Pascal<IDeci> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Pascal operator /(Pascal a, Pascal<ICenti> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Pascal operator /(Pascal a, Pascal<IMilli> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Pascal operator /(Pascal a, Pascal<IMicro> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Pascal operator /(Pascal a, Pascal<INano> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Pascal operator /(Pascal a, Pascal<IPico> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Pascal operator /(Pascal a, Pascal<IFemto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Pascal operator /(Pascal a, Pascal<IAtto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Pascal operator /(Pascal a, Pascal<IZepto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region PERCENT

    public static Pascal operator %(Pascal a, Pascal<IYotta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Yotta, INone.Prefix);

    public static Pascal operator %(Pascal a, Pascal<IZetta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static Pascal operator %(Pascal a, Pascal<IExa> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static Pascal operator %(Pascal a, Pascal<IPeta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static Pascal operator %(Pascal a, Pascal<ITera> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static Pascal operator %(Pascal a, Pascal<IGiga> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static Pascal operator %(Pascal a, Pascal<IMega> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static Pascal operator %(Pascal a, Pascal<IKilo> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static Pascal operator %(Pascal a, Pascal<IHecto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static Pascal operator %(Pascal a, Pascal<IDeca> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static Pascal operator %(Pascal a, Pascal<INone> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static Pascal operator %(Pascal a, Pascal<IDeci> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static Pascal operator %(Pascal a, Pascal<ICenti> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static Pascal operator %(Pascal a, Pascal<IMilli> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static Pascal operator %(Pascal a, Pascal<IMicro> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static Pascal operator %(Pascal a, Pascal<INano> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static Pascal operator %(Pascal a, Pascal<IPico> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static Pascal operator %(Pascal a, Pascal<IFemto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static Pascal operator %(Pascal a, Pascal<IAtto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static Pascal operator %(Pascal a, Pascal<IZepto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    #region EQUAL

    public static bool operator ==(Pascal a, Pascal<IYotta> i)
        => Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix) == a.m_value;

    public static bool operator !=(Pascal a, Pascal<IYotta> i)
        => Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix) != a.m_value;

    public static bool operator >(Pascal a, Pascal<IYotta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator <(Pascal a, Pascal<IYotta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator >=(Pascal a, Pascal<IYotta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator <=(Pascal a, Pascal<IYotta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator ==(Pascal a, Pascal<IZetta> i)
        => Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix) == a.m_value;

    public static bool operator !=(Pascal a, Pascal<IZetta> i)
        => Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix) != a.m_value;

    public static bool operator >(Pascal a, Pascal<IZetta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static bool operator <(Pascal a, Pascal<IZetta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static bool operator >=(Pascal a, Pascal<IZetta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static bool operator <=(Pascal a, Pascal<IZetta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Zetta, INone.Prefix);

    public static bool operator ==(Pascal a, Pascal<IExa> i)
        => Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix) == a.m_value;

    public static bool operator !=(Pascal a, Pascal<IExa> i)
        => Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix) != a.m_value;

    public static bool operator >(Pascal a, Pascal<IExa> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator <(Pascal a, Pascal<IExa> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator >=(Pascal a, Pascal<IExa> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator <=(Pascal a, Pascal<IExa> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Exa, INone.Prefix);

    public static bool operator ==(Pascal a, Pascal<IPeta> i)
        => Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix) == a.m_value;

    public static bool operator !=(Pascal a, Pascal<IPeta> i)
        => Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix) != a.m_value;

    public static bool operator >(Pascal a, Pascal<IPeta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static bool operator <(Pascal a, Pascal<IPeta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static bool operator >=(Pascal a, Pascal<IPeta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static bool operator <=(Pascal a, Pascal<IPeta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Peta, INone.Prefix);

    public static bool operator ==(Pascal a, Pascal<ITera> i)
        => Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix) == a.m_value;

    public static bool operator !=(Pascal a, Pascal<ITera> i)
        => Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix) != a.m_value;

    public static bool operator >(Pascal a, Pascal<ITera> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static bool operator <(Pascal a, Pascal<ITera> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static bool operator >=(Pascal a, Pascal<ITera> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static bool operator <=(Pascal a, Pascal<ITera> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Tera, INone.Prefix);

    public static bool operator ==(Pascal a, Pascal<IGiga> i)
        => Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix) == a.m_value;

    public static bool operator !=(Pascal a, Pascal<IGiga> i)
        => Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix) != a.m_value;

    public static bool operator >(Pascal a, Pascal<IGiga> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static bool operator <(Pascal a, Pascal<IGiga> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static bool operator >=(Pascal a, Pascal<IGiga> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static bool operator <=(Pascal a, Pascal<IGiga> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Giga, INone.Prefix);

    public static bool operator ==(Pascal a, Pascal<IMega> i)
        => Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix) == a.m_value;

    public static bool operator !=(Pascal a, Pascal<IMega> i)
        => Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix) != a.m_value;

    public static bool operator >(Pascal a, Pascal<IMega> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static bool operator <(Pascal a, Pascal<IMega> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static bool operator >=(Pascal a, Pascal<IMega> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static bool operator <=(Pascal a, Pascal<IMega> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Mega, INone.Prefix);

    public static bool operator ==(Pascal a, Pascal<IKilo> i)
        => Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix) == a.m_value;

    public static bool operator !=(Pascal a, Pascal<IKilo> i)
        => Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix) != a.m_value;

    public static bool operator >(Pascal a, Pascal<IKilo> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static bool operator <(Pascal a, Pascal<IKilo> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static bool operator >=(Pascal a, Pascal<IKilo> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static bool operator <=(Pascal a, Pascal<IKilo> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Kilo, INone.Prefix);

    public static bool operator ==(Pascal a, Pascal<IHecto> i)
        => Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix) == a.m_value;

    public static bool operator !=(Pascal a, Pascal<IHecto> i)
        => Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix) != a.m_value;

    public static bool operator >(Pascal a, Pascal<IHecto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static bool operator <(Pascal a, Pascal<IHecto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static bool operator >=(Pascal a, Pascal<IHecto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static bool operator <=(Pascal a, Pascal<IHecto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Hecto, INone.Prefix);

    public static bool operator ==(Pascal a, Pascal<IDeca> i)
        => Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix) == a.m_value;

    public static bool operator !=(Pascal a, Pascal<IDeca> i)
        => Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix) != a.m_value;

    public static bool operator >(Pascal a, Pascal<IDeca> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static bool operator <(Pascal a, Pascal<IDeca> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static bool operator >=(Pascal a, Pascal<IDeca> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static bool operator <=(Pascal a, Pascal<IDeca> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Deca, INone.Prefix);

    public static bool operator ==(Pascal a, Pascal<INone> i)
        => Unit.Convert(i.m_value, Prefix.None, INone.Prefix) == a.m_value;

    public static bool operator !=(Pascal a, Pascal<INone> i)
        => Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix) != a.m_value;

    public static bool operator >(Pascal a, Pascal<INone> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static bool operator <(Pascal a, Pascal<INone> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static bool operator >=(Pascal a, Pascal<INone> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static bool operator <=(Pascal a, Pascal<INone> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.None, INone.Prefix);

    public static bool operator ==(Pascal a, Pascal<IDeci> i)
        => Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix) == a.m_value;

    public static bool operator !=(Pascal a, Pascal<IDeci> i)
        => Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix) != a.m_value;

    public static bool operator >(Pascal a, Pascal<IDeci> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static bool operator <(Pascal a, Pascal<IDeci> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static bool operator >=(Pascal a, Pascal<IDeci> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static bool operator <=(Pascal a, Pascal<IDeci> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Deci, INone.Prefix);

    public static bool operator ==(Pascal a, Pascal<ICenti> i)
        => Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix) == a.m_value;

    public static bool operator !=(Pascal a, Pascal<ICenti> i)
        => Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix) != a.m_value;

    public static bool operator >(Pascal a, Pascal<ICenti> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static bool operator <(Pascal a, Pascal<ICenti> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static bool operator >=(Pascal a, Pascal<ICenti> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static bool operator <=(Pascal a, Pascal<ICenti> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Centi, INone.Prefix);

    public static bool operator ==(Pascal a, Pascal<IMilli> i)
        => Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix) == a.m_value;

    public static bool operator !=(Pascal a, Pascal<IMilli> i)
        => Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix) != a.m_value;

    public static bool operator >(Pascal a, Pascal<IMilli> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static bool operator <(Pascal a, Pascal<IMilli> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static bool operator >=(Pascal a, Pascal<IMilli> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static bool operator <=(Pascal a, Pascal<IMilli> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Milli, INone.Prefix);

    public static bool operator ==(Pascal a, Pascal<IMicro> i)
        => Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix) == a.m_value;

    public static bool operator !=(Pascal a, Pascal<IMicro> i)
        => Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix) != a.m_value;

    public static bool operator >(Pascal a, Pascal<IMicro> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static bool operator <(Pascal a, Pascal<IMicro> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static bool operator >=(Pascal a, Pascal<IMicro> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static bool operator <=(Pascal a, Pascal<IMicro> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Micro, INone.Prefix);

    public static bool operator ==(Pascal a, Pascal<INano> i)
        => Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix) == a.m_value;

    public static bool operator !=(Pascal a, Pascal<INano> i)
        => Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix) != a.m_value;

    public static bool operator >(Pascal a, Pascal<INano> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static bool operator <(Pascal a, Pascal<INano> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static bool operator >=(Pascal a, Pascal<INano> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static bool operator <=(Pascal a, Pascal<INano> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Nano, INone.Prefix);

    public static bool operator ==(Pascal a, Pascal<IPico> i)
        => Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix) == a.m_value;

    public static bool operator !=(Pascal a, Pascal<IPico> i)
        => Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix) != a.m_value;

    public static bool operator >(Pascal a, Pascal<IPico> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static bool operator <(Pascal a, Pascal<IPico> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static bool operator >=(Pascal a, Pascal<IPico> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static bool operator <=(Pascal a, Pascal<IPico> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Pico, INone.Prefix);

    public static bool operator ==(Pascal a, Pascal<IFemto> i)
        => Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix) == a.m_value;

    public static bool operator !=(Pascal a, Pascal<IFemto> i)
        => Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix) != a.m_value;

    public static bool operator >(Pascal a, Pascal<IFemto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static bool operator <(Pascal a, Pascal<IFemto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static bool operator >=(Pascal a, Pascal<IFemto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static bool operator <=(Pascal a, Pascal<IFemto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Femto, INone.Prefix);

    public static bool operator ==(Pascal a, Pascal<IAtto> i)
        => Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix) == a.m_value;

    public static bool operator !=(Pascal a, Pascal<IAtto> i)
        => Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix) != a.m_value;

    public static bool operator >(Pascal a, Pascal<IAtto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static bool operator <(Pascal a, Pascal<IAtto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static bool operator >=(Pascal a, Pascal<IAtto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static bool operator <=(Pascal a, Pascal<IAtto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Atto, INone.Prefix);

    public static bool operator ==(Pascal a, Pascal<IZepto> i)
        => Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix) == a.m_value;

    public static bool operator !=(Pascal a, Pascal<IZepto> i)
        => Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix) != a.m_value;

    public static bool operator >(Pascal a, Pascal<IZepto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    public static bool operator <(Pascal a, Pascal<IZepto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    public static bool operator >=(Pascal a, Pascal<IZepto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    public static bool operator <=(Pascal a, Pascal<IZepto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Zepto, INone.Prefix);

    #endregion

    public override bool Equals(object? obj) => m_value.Equals(obj) && INone.Prefix.Equals(obj);

    public override int GetHashCode() => HashCode.Combine(m_value, INone.Prefix);

    public override string ToString() => m_value.ToString(NumberFormatInfo.CurrentInfo) + "Pa";
}

/// <summary>
/// Represent the si unit for amount of substance
/// </summary>
/// <typeparam name="T">The SI Prefix</typeparam>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Pascal<T> where T : SI
{
    internal readonly decimal m_value; // Do not rename (binary serialization)

    public Pascal(decimal val)
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

    public static implicit operator Pascal<T>(decimal val) => new(val);

    #region IMPL_Pascal

    public static implicit operator Pascal<T>(Pascal<IYotta> i)
    {
        return Prefix switch
        {
            Prefix.Yotta => new Pascal<T>(i.m_value),
            Prefix.None => new Pascal<T>(Unit.BaseValue(i.m_value, Prefix.Yotta)),
            _ => new Pascal<T>(Unit.Convert(i.m_value, Prefix.Yotta, Prefix))
        };
    }

    public static implicit operator Pascal<T>(Pascal<IZetta> i)
    {
        return Prefix switch
        {
            Prefix.Zetta => new Pascal<T>(i.m_value),
            Prefix.None => new Pascal<T>(Unit.BaseValue(i.m_value, Prefix.Zetta)),
            _ => new Pascal<T>(Unit.Convert(i.m_value, Prefix.Zetta, Prefix))
        };
    }

    public static implicit operator Pascal<T>(Pascal<IExa> i)
    {
        return Prefix switch
        {
            Prefix.Exa => new Pascal<T>(i.m_value),
            Prefix.None => new Pascal<T>(Unit.BaseValue(i.m_value, Prefix.Exa)),
            _ => new Pascal<T>(Unit.Convert(i.m_value, Prefix.Exa, Prefix))
        };
    }

    public static implicit operator Pascal<T>(Pascal<IPeta> i)
    {
        return Prefix switch
        {
            Prefix.Peta => new Pascal<T>(i.m_value),
            Prefix.None => new Pascal<T>(Unit.BaseValue(i.m_value, Prefix.Peta)),
            _ => new Pascal<T>(Unit.Convert(i.m_value, Prefix.Peta, Prefix))
        };
    }

    public static implicit operator Pascal<T>(Pascal<ITera> i)
    {
        return Prefix switch
        {
            Prefix.Tera => new Pascal<T>(i.m_value),
            Prefix.None => new Pascal<T>(Unit.BaseValue(i.m_value, Prefix.Tera)),
            _ => new Pascal<T>(Unit.Convert(i.m_value, Prefix.Tera, Prefix))
        };
    }

    public static implicit operator Pascal<T>(Pascal<IGiga> i)
    {
        return Prefix switch
        {
            Prefix.Giga => new Pascal<T>(i.m_value),
            Prefix.None => new Pascal<T>(Unit.BaseValue(i.m_value, Prefix.Giga)),
            _ => new Pascal<T>(Unit.Convert(i.m_value, Prefix.Giga, Prefix))
        };
    }

    public static implicit operator Pascal<T>(Pascal<IMega> i)
    {
        return Prefix switch
        {
            Prefix.Mega => new Pascal<T>(i.m_value),
            Prefix.None => new Pascal<T>(Unit.BaseValue(i.m_value, Prefix.Mega)),
            _ => new Pascal<T>(Unit.Convert(i.m_value, Prefix.Mega, Prefix))
        };
    }

    public static implicit operator Pascal<T>(Pascal<IKilo> i)
    {
        return Prefix switch
        {
            Prefix.Kilo => new Pascal<T>(i.m_value),
            Prefix.None => new Pascal<T>(Unit.BaseValue(i.m_value, Prefix.Kilo)),
            _ => new Pascal<T>(Unit.Convert(i.m_value, Prefix.Kilo, Prefix))
        };
    }

    public static implicit operator Pascal<T>(Pascal<IHecto> i)
    {
        return Prefix switch
        {
            Prefix.Hecto => new Pascal<T>(i.m_value),
            Prefix.None => new Pascal<T>(Unit.BaseValue(i.m_value, Prefix.Hecto)),
            _ => new Pascal<T>(Unit.Convert(i.m_value, Prefix.Hecto, Prefix))
        };
    }

    public static implicit operator Pascal<T>(Pascal<IDeca> i)
    {
        return Prefix switch
        {
            Prefix.Deca => new Pascal<T>(i.m_value),
            Prefix.None => new Pascal<T>(Unit.BaseValue(i.m_value, Prefix.Deca)),
            _ => new Pascal<T>(Unit.Convert(i.m_value, Prefix.Deca, Prefix))
        };
    }

    public static implicit operator Pascal<T>(Pascal<INone> i) => new Pascal<T>(i.m_value);

    public static implicit operator Pascal<T>(Pascal<IDeci> i)
    {
        return Prefix switch
        {
            Prefix.Deci => new Pascal<T>(i.m_value),
            Prefix.None => new Pascal<T>(Unit.BaseValue(i.m_value, Prefix.Deci)),
            _ => new Pascal<T>(Unit.Convert(i.m_value, Prefix.Deci, Prefix))
        };
    }

    public static implicit operator Pascal<T>(Pascal<ICenti> i)
    {
        return Prefix switch
        {
            Prefix.Centi => new Pascal<T>(i.m_value),
            Prefix.None => new Pascal<T>(Unit.BaseValue(i.m_value, Prefix.Centi)),
            _ => new Pascal<T>(Unit.Convert(i.m_value, Prefix.Centi, Prefix))
        };
    }

    public static implicit operator Pascal<T>(Pascal<IMilli> i)
    {
        return Prefix switch
        {
            Prefix.Milli => new Pascal<T>(i.m_value),
            Prefix.None => new Pascal<T>(Unit.BaseValue(i.m_value, Prefix.Milli)),
            _ => new Pascal<T>(Unit.Convert(i.m_value, Prefix.Milli, Prefix))
        };
    }

    public static implicit operator Pascal<T>(Pascal<IMicro> i)
    {
        return Prefix switch
        {
            Prefix.Micro => new Pascal<T>(i.m_value),
            Prefix.None => new Pascal<T>(Unit.BaseValue(i.m_value, Prefix.Micro)),
            _ => new Pascal<T>(Unit.Convert(i.m_value, Prefix.Micro, Prefix))
        };
    }

    public static implicit operator Pascal<T>(Pascal<INano> i)
    {
        return Prefix switch
        {
            Prefix.Nano => new Pascal<T>(i.m_value),
            Prefix.None => new Pascal<T>(Unit.BaseValue(i.m_value, Prefix.Nano)),
            _ => new Pascal<T>(Unit.Convert(i.m_value, Prefix.Nano, Prefix))
        };
    }

    public static implicit operator Pascal<T>(Pascal<IPico> i)
    {
        return Prefix switch
        {
            Prefix.Pico => new Pascal<T>(i.m_value),
            Prefix.None => new Pascal<T>(Unit.BaseValue(i.m_value, Prefix.Pico)),
            _ => new Pascal<T>(Unit.Convert(i.m_value, Prefix.Pico, Prefix))
        };
    }

    public static implicit operator Pascal<T>(Pascal<IFemto> i)
    {
        return Prefix switch
        {
            Prefix.Femto => new Pascal<T>(i.m_value),
            Prefix.None => new Pascal<T>(Unit.BaseValue(i.m_value, Prefix.Femto)),
            _ => new Pascal<T>(Unit.Convert(i.m_value, Prefix.Femto, Prefix))
        };
    }

    public static implicit operator Pascal<T>(Pascal<IAtto> i)
    {
        return Prefix switch
        {
            Prefix.Atto => new Pascal<T>(i.m_value),
            Prefix.None => new Pascal<T>(Unit.BaseValue(i.m_value, Prefix.Atto)),
            _ => new Pascal<T>(Unit.Convert(i.m_value, Prefix.Atto, Prefix))
        };
    }

    public static implicit operator Pascal<T>(Pascal<IZepto> i)
    {
        return Prefix switch
        {
            Prefix.Zepto => new Pascal<T>(i.m_value),
            Prefix.None => new Pascal<T>(Unit.BaseValue(i.m_value, Prefix.Zepto)),
            _ => new Pascal<T>(Unit.Convert(i.m_value, Prefix.Zepto, Prefix))
        };
    }

    #endregion

    #region PLUS

    public static Pascal<T> operator +(Pascal<T> a, Pascal<IYotta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Pascal<T> operator +(Pascal<T> a, Pascal<IZetta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Pascal<T> operator +(Pascal<T> a, Pascal<IExa> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Pascal<T> operator +(Pascal<T> a, Pascal<IPeta> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Pascal<T> operator +(Pascal<T> a, Pascal<ITera> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Pascal<T> operator +(Pascal<T> a, Pascal<IGiga> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Pascal<T> operator +(Pascal<T> a, Pascal<IMega> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Pascal<T> operator +(Pascal<T> a, Pascal<IKilo> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Pascal<T> operator +(Pascal<T> a, Pascal<IHecto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Pascal<T> operator +(Pascal<T> a, Pascal<IDeca> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Pascal<T> operator +(Pascal<T> a, Pascal<INone> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Pascal<T> operator +(Pascal<T> a, Pascal<IDeci> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Pascal<T> operator +(Pascal<T> a, Pascal<ICenti> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Pascal<T> operator +(Pascal<T> a, Pascal<IMilli> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Pascal<T> operator +(Pascal<T> a, Pascal<IMicro> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Pascal<T> operator +(Pascal<T> a, Pascal<INano> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Pascal<T> operator +(Pascal<T> a, Pascal<IPico> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Pascal<T> operator +(Pascal<T> a, Pascal<IFemto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Pascal<T> operator +(Pascal<T> a, Pascal<IAtto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Pascal<T> operator +(Pascal<T> a, Pascal<IZepto> i)
        => a.m_value + Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region MINUS

    public static Pascal<T> operator -(Pascal<T> a, Pascal<IYotta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Pascal<T> operator -(Pascal<T> a, Pascal<IZetta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Pascal<T> operator -(Pascal<T> a, Pascal<IExa> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Pascal<T> operator -(Pascal<T> a, Pascal<IPeta> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Pascal<T> operator -(Pascal<T> a, Pascal<ITera> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Pascal<T> operator -(Pascal<T> a, Pascal<IGiga> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Pascal<T> operator -(Pascal<T> a, Pascal<IMega> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Pascal<T> operator -(Pascal<T> a, Pascal<IKilo> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Pascal<T> operator -(Pascal<T> a, Pascal<IHecto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Pascal<T> operator -(Pascal<T> a, Pascal<IDeca> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Pascal<T> operator -(Pascal<T> a, Pascal<INone> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Pascal<T> operator -(Pascal<T> a, Pascal<IDeci> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Pascal<T> operator -(Pascal<T> a, Pascal<ICenti> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Pascal<T> operator -(Pascal<T> a, Pascal<IMilli> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Pascal<T> operator -(Pascal<T> a, Pascal<IMicro> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Pascal<T> operator -(Pascal<T> a, Pascal<INano> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Pascal<T> operator -(Pascal<T> a, Pascal<IPico> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Pascal<T> operator -(Pascal<T> a, Pascal<IFemto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Pascal<T> operator -(Pascal<T> a, Pascal<IAtto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Pascal<T> operator -(Pascal<T> a, Pascal<IZepto> i)
        => a.m_value - Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region MULTI

    public static Pascal<T> operator *(Pascal<T> a, Pascal<IYotta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Pascal<T> operator *(Pascal<T> a, Pascal<IZetta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Pascal<T> operator *(Pascal<T> a, Pascal<IExa> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Pascal<T> operator *(Pascal<T> a, Pascal<IPeta> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Pascal<T> operator *(Pascal<T> a, Pascal<ITera> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Pascal<T> operator *(Pascal<T> a, Pascal<IGiga> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Pascal<T> operator *(Pascal<T> a, Pascal<IMega> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Pascal<T> operator *(Pascal<T> a, Pascal<IKilo> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Pascal<T> operator *(Pascal<T> a, Pascal<IHecto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Pascal<T> operator *(Pascal<T> a, Pascal<IDeca> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Pascal<T> operator *(Pascal<T> a, Pascal<INone> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Pascal<T> operator *(Pascal<T> a, Pascal<IDeci> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Pascal<T> operator *(Pascal<T> a, Pascal<ICenti> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Pascal<T> operator *(Pascal<T> a, Pascal<IMilli> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Pascal<T> operator *(Pascal<T> a, Pascal<IMicro> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Pascal<T> operator *(Pascal<T> a, Pascal<INano> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Pascal<T> operator *(Pascal<T> a, Pascal<IPico> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Pascal<T> operator *(Pascal<T> a, Pascal<IFemto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Pascal<T> operator *(Pascal<T> a, Pascal<IAtto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Pascal<T> operator *(Pascal<T> a, Pascal<IZepto> i)
        => a.m_value * Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region DIV

    public static Pascal<T> operator /(Pascal<T> a, Pascal<IYotta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Pascal<T> operator /(Pascal<T> a, Pascal<IZetta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Pascal<T> operator /(Pascal<T> a, Pascal<IExa> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Pascal<T> operator /(Pascal<T> a, Pascal<IPeta> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Pascal<T> operator /(Pascal<T> a, Pascal<ITera> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Pascal<T> operator /(Pascal<T> a, Pascal<IGiga> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Pascal<T> operator /(Pascal<T> a, Pascal<IMega> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Pascal<T> operator /(Pascal<T> a, Pascal<IKilo> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Pascal<T> operator /(Pascal<T> a, Pascal<IHecto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Pascal<T> operator /(Pascal<T> a, Pascal<IDeca> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Pascal<T> operator /(Pascal<T> a, Pascal<INone> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Pascal<T> operator /(Pascal<T> a, Pascal<IDeci> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Pascal<T> operator /(Pascal<T> a, Pascal<ICenti> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Pascal<T> operator /(Pascal<T> a, Pascal<IMilli> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Pascal<T> operator /(Pascal<T> a, Pascal<IMicro> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Pascal<T> operator /(Pascal<T> a, Pascal<INano> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Pascal<T> operator /(Pascal<T> a, Pascal<IPico> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Pascal<T> operator /(Pascal<T> a, Pascal<IFemto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Pascal<T> operator /(Pascal<T> a, Pascal<IAtto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Pascal<T> operator /(Pascal<T> a, Pascal<IZepto> i)
        => a.m_value / Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region PERCENT

    public static Pascal<T> operator %(Pascal<T> a, Pascal<IYotta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Yotta, Prefix);

    public static Pascal<T> operator %(Pascal<T> a, Pascal<IZetta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static Pascal<T> operator %(Pascal<T> a, Pascal<IExa> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static Pascal<T> operator %(Pascal<T> a, Pascal<IPeta> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static Pascal<T> operator %(Pascal<T> a, Pascal<ITera> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static Pascal<T> operator %(Pascal<T> a, Pascal<IGiga> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static Pascal<T> operator %(Pascal<T> a, Pascal<IMega> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static Pascal<T> operator %(Pascal<T> a, Pascal<IKilo> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static Pascal<T> operator %(Pascal<T> a, Pascal<IHecto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static Pascal<T> operator %(Pascal<T> a, Pascal<IDeca> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static Pascal<T> operator %(Pascal<T> a, Pascal<INone> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static Pascal<T> operator %(Pascal<T> a, Pascal<IDeci> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static Pascal<T> operator %(Pascal<T> a, Pascal<ICenti> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static Pascal<T> operator %(Pascal<T> a, Pascal<IMilli> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static Pascal<T> operator %(Pascal<T> a, Pascal<IMicro> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static Pascal<T> operator %(Pascal<T> a, Pascal<INano> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static Pascal<T> operator %(Pascal<T> a, Pascal<IPico> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static Pascal<T> operator %(Pascal<T> a, Pascal<IFemto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static Pascal<T> operator %(Pascal<T> a, Pascal<IAtto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static Pascal<T> operator %(Pascal<T> a, Pascal<IZepto> i)
        => a.m_value % Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    #region EQUAL

    public static bool operator ==(Pascal<T> a, Pascal<IYotta> i)
        => Unit.Convert(i.m_value, Prefix.Exa, Prefix) == a.m_value;

    public static bool operator !=(Pascal<T> a, Pascal<IYotta> i)
        => Unit.Convert(i.m_value, Prefix.Exa, Prefix) != a.m_value;

    public static bool operator >(Pascal<T> a, Pascal<IYotta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator <(Pascal<T> a, Pascal<IYotta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator >=(Pascal<T> a, Pascal<IYotta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator <=(Pascal<T> a, Pascal<IYotta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator ==(Pascal<T> a, Pascal<IZetta> i)
        => Unit.Convert(i.m_value, Prefix.Zetta, Prefix) == a.m_value;

    public static bool operator !=(Pascal<T> a, Pascal<IZetta> i)
        => Unit.Convert(i.m_value, Prefix.Zetta, Prefix) != a.m_value;

    public static bool operator >(Pascal<T> a, Pascal<IZetta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static bool operator <(Pascal<T> a, Pascal<IZetta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static bool operator >=(Pascal<T> a, Pascal<IZetta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static bool operator <=(Pascal<T> a, Pascal<IZetta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Zetta, Prefix);

    public static bool operator ==(Pascal<T> a, Pascal<IExa> i)
        => Unit.Convert(i.m_value, Prefix.Exa, Prefix) == a.m_value;

    public static bool operator !=(Pascal<T> a, Pascal<IExa> i)
        => Unit.Convert(i.m_value, Prefix.Exa, Prefix) != a.m_value;

    public static bool operator >(Pascal<T> a, Pascal<IExa> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator <(Pascal<T> a, Pascal<IExa> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator >=(Pascal<T> a, Pascal<IExa> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator <=(Pascal<T> a, Pascal<IExa> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Exa, Prefix);

    public static bool operator ==(Pascal<T> a, Pascal<IPeta> i)
        => Unit.Convert(i.m_value, Prefix.Peta, Prefix) == a.m_value;

    public static bool operator !=(Pascal<T> a, Pascal<IPeta> i)
        => Unit.Convert(i.m_value, Prefix.Peta, Prefix) != a.m_value;

    public static bool operator >(Pascal<T> a, Pascal<IPeta> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static bool operator <(Pascal<T> a, Pascal<IPeta> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static bool operator >=(Pascal<T> a, Pascal<IPeta> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static bool operator <=(Pascal<T> a, Pascal<IPeta> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Peta, Prefix);

    public static bool operator ==(Pascal<T> a, Pascal<ITera> i)
        => Unit.Convert(i.m_value, Prefix.Tera, Prefix) == a.m_value;

    public static bool operator !=(Pascal<T> a, Pascal<ITera> i)
        => Unit.Convert(i.m_value, Prefix.Tera, Prefix) != a.m_value;

    public static bool operator >(Pascal<T> a, Pascal<ITera> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static bool operator <(Pascal<T> a, Pascal<ITera> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static bool operator >=(Pascal<T> a, Pascal<ITera> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static bool operator <=(Pascal<T> a, Pascal<ITera> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Tera, Prefix);

    public static bool operator ==(Pascal<T> a, Pascal<IGiga> i)
        => Unit.Convert(i.m_value, Prefix.Giga, Prefix) == a.m_value;

    public static bool operator !=(Pascal<T> a, Pascal<IGiga> i)
        => Unit.Convert(i.m_value, Prefix.Giga, Prefix) != a.m_value;

    public static bool operator >(Pascal<T> a, Pascal<IGiga> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static bool operator <(Pascal<T> a, Pascal<IGiga> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static bool operator >=(Pascal<T> a, Pascal<IGiga> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static bool operator <=(Pascal<T> a, Pascal<IGiga> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Giga, Prefix);

    public static bool operator ==(Pascal<T> a, Pascal<IMega> i)
        => Unit.Convert(i.m_value, Prefix.Mega, Prefix) == a.m_value;

    public static bool operator !=(Pascal<T> a, Pascal<IMega> i)
        => Unit.Convert(i.m_value, Prefix.Mega, Prefix) != a.m_value;

    public static bool operator >(Pascal<T> a, Pascal<IMega> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static bool operator <(Pascal<T> a, Pascal<IMega> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static bool operator >=(Pascal<T> a, Pascal<IMega> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static bool operator <=(Pascal<T> a, Pascal<IMega> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Mega, Prefix);

    public static bool operator ==(Pascal<T> a, Pascal<IKilo> i)
        => Unit.Convert(i.m_value, Prefix.Kilo, Prefix) == a.m_value;

    public static bool operator !=(Pascal<T> a, Pascal<IKilo> i)
        => Unit.Convert(i.m_value, Prefix.Kilo, Prefix) != a.m_value;

    public static bool operator >(Pascal<T> a, Pascal<IKilo> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static bool operator <(Pascal<T> a, Pascal<IKilo> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static bool operator >=(Pascal<T> a, Pascal<IKilo> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static bool operator <=(Pascal<T> a, Pascal<IKilo> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Kilo, Prefix);

    public static bool operator ==(Pascal<T> a, Pascal<IHecto> i)
        => Unit.Convert(i.m_value, Prefix.Hecto, Prefix) == a.m_value;

    public static bool operator !=(Pascal<T> a, Pascal<IHecto> i)
        => Unit.Convert(i.m_value, Prefix.Hecto, Prefix) != a.m_value;

    public static bool operator >(Pascal<T> a, Pascal<IHecto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static bool operator <(Pascal<T> a, Pascal<IHecto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static bool operator >=(Pascal<T> a, Pascal<IHecto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static bool operator <=(Pascal<T> a, Pascal<IHecto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Hecto, Prefix);

    public static bool operator ==(Pascal<T> a, Pascal<IDeca> i)
        => Unit.Convert(i.m_value, Prefix.Deca, Prefix) == a.m_value;

    public static bool operator !=(Pascal<T> a, Pascal<IDeca> i)
        => Unit.Convert(i.m_value, Prefix.Deca, Prefix) != a.m_value;

    public static bool operator >(Pascal<T> a, Pascal<IDeca> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static bool operator <(Pascal<T> a, Pascal<IDeca> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static bool operator >=(Pascal<T> a, Pascal<IDeca> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static bool operator <=(Pascal<T> a, Pascal<IDeca> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Deca, Prefix);

    public static bool operator ==(Pascal<T> a, Pascal<INone> i)
        => Unit.Convert(i.m_value, Prefix.None, Prefix) == a.m_value;

    public static bool operator !=(Pascal<T> a, Pascal<INone> i)
        => Unit.Convert(i.m_value, Prefix.Nano, Prefix) != a.m_value;

    public static bool operator >(Pascal<T> a, Pascal<INone> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static bool operator <(Pascal<T> a, Pascal<INone> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static bool operator >=(Pascal<T> a, Pascal<INone> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static bool operator <=(Pascal<T> a, Pascal<INone> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.None, Prefix);

    public static bool operator ==(Pascal<T> a, Pascal<IDeci> i)
        => Unit.Convert(i.m_value, Prefix.Deci, Prefix) == a.m_value;

    public static bool operator !=(Pascal<T> a, Pascal<IDeci> i)
        => Unit.Convert(i.m_value, Prefix.Deci, Prefix) != a.m_value;

    public static bool operator >(Pascal<T> a, Pascal<IDeci> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static bool operator <(Pascal<T> a, Pascal<IDeci> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static bool operator >=(Pascal<T> a, Pascal<IDeci> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static bool operator <=(Pascal<T> a, Pascal<IDeci> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Deci, Prefix);

    public static bool operator ==(Pascal<T> a, Pascal<ICenti> i)
        => Unit.Convert(i.m_value, Prefix.Centi, Prefix) == a.m_value;

    public static bool operator !=(Pascal<T> a, Pascal<ICenti> i)
        => Unit.Convert(i.m_value, Prefix.Centi, Prefix) != a.m_value;

    public static bool operator >(Pascal<T> a, Pascal<ICenti> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static bool operator <(Pascal<T> a, Pascal<ICenti> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static bool operator >=(Pascal<T> a, Pascal<ICenti> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static bool operator <=(Pascal<T> a, Pascal<ICenti> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Centi, Prefix);

    public static bool operator ==(Pascal<T> a, Pascal<IMilli> i)
        => Unit.Convert(i.m_value, Prefix.Milli, Prefix) == a.m_value;

    public static bool operator !=(Pascal<T> a, Pascal<IMilli> i)
        => Unit.Convert(i.m_value, Prefix.Milli, Prefix) != a.m_value;

    public static bool operator >(Pascal<T> a, Pascal<IMilli> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static bool operator <(Pascal<T> a, Pascal<IMilli> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static bool operator >=(Pascal<T> a, Pascal<IMilli> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static bool operator <=(Pascal<T> a, Pascal<IMilli> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Milli, Prefix);

    public static bool operator ==(Pascal<T> a, Pascal<IMicro> i)
        => Unit.Convert(i.m_value, Prefix.Micro, Prefix) == a.m_value;

    public static bool operator !=(Pascal<T> a, Pascal<IMicro> i)
        => Unit.Convert(i.m_value, Prefix.Micro, Prefix) != a.m_value;

    public static bool operator >(Pascal<T> a, Pascal<IMicro> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static bool operator <(Pascal<T> a, Pascal<IMicro> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static bool operator >=(Pascal<T> a, Pascal<IMicro> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static bool operator <=(Pascal<T> a, Pascal<IMicro> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Micro, Prefix);

    public static bool operator ==(Pascal<T> a, Pascal<INano> i)
        => Unit.Convert(i.m_value, Prefix.Nano, Prefix) == a.m_value;

    public static bool operator !=(Pascal<T> a, Pascal<INano> i)
        => Unit.Convert(i.m_value, Prefix.Nano, Prefix) != a.m_value;

    public static bool operator >(Pascal<T> a, Pascal<INano> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static bool operator <(Pascal<T> a, Pascal<INano> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static bool operator >=(Pascal<T> a, Pascal<INano> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static bool operator <=(Pascal<T> a, Pascal<INano> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Nano, Prefix);

    public static bool operator ==(Pascal<T> a, Pascal<IPico> i)
        => Unit.Convert(i.m_value, Prefix.Pico, Prefix) == a.m_value;

    public static bool operator !=(Pascal<T> a, Pascal<IPico> i)
        => Unit.Convert(i.m_value, Prefix.Pico, Prefix) != a.m_value;

    public static bool operator >(Pascal<T> a, Pascal<IPico> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static bool operator <(Pascal<T> a, Pascal<IPico> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static bool operator >=(Pascal<T> a, Pascal<IPico> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static bool operator <=(Pascal<T> a, Pascal<IPico> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Pico, Prefix);

    public static bool operator ==(Pascal<T> a, Pascal<IFemto> i)
        => Unit.Convert(i.m_value, Prefix.Femto, Prefix) == a.m_value;

    public static bool operator !=(Pascal<T> a, Pascal<IFemto> i)
        => Unit.Convert(i.m_value, Prefix.Femto, Prefix) != a.m_value;

    public static bool operator >(Pascal<T> a, Pascal<IFemto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static bool operator <(Pascal<T> a, Pascal<IFemto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static bool operator >=(Pascal<T> a, Pascal<IFemto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static bool operator <=(Pascal<T> a, Pascal<IFemto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Femto, Prefix);

    public static bool operator ==(Pascal<T> a, Pascal<IAtto> i)
        => Unit.Convert(i.m_value, Prefix.Atto, Prefix) == a.m_value;

    public static bool operator !=(Pascal<T> a, Pascal<IAtto> i)
        => Unit.Convert(i.m_value, Prefix.Atto, Prefix) != a.m_value;

    public static bool operator >(Pascal<T> a, Pascal<IAtto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static bool operator <(Pascal<T> a, Pascal<IAtto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static bool operator >=(Pascal<T> a, Pascal<IAtto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static bool operator <=(Pascal<T> a, Pascal<IAtto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Atto, Prefix);

    public static bool operator ==(Pascal<T> a, Pascal<IZepto> i)
        => Unit.Convert(i.m_value, Prefix.Zepto, Prefix) == a.m_value;

    public static bool operator !=(Pascal<T> a, Pascal<IZepto> i)
        => Unit.Convert(i.m_value, Prefix.Zepto, Prefix) != a.m_value;

    public static bool operator >(Pascal<T> a, Pascal<IZepto> i)
        => a.m_value > Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    public static bool operator <(Pascal<T> a, Pascal<IZepto> i)
        => a.m_value < Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    public static bool operator >=(Pascal<T> a, Pascal<IZepto> i)
        => a.m_value >= Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    public static bool operator <=(Pascal<T> a, Pascal<IZepto> i)
        => a.m_value <= Unit.Convert(i.m_value, Prefix.Zepto, Prefix);

    #endregion

    public override bool Equals(object? obj) => m_value.Equals(obj) && Prefix.Equals(obj);

    public override int GetHashCode() => HashCode.Combine(m_value, Prefix);

    public override string ToString() => m_value.ToString(NumberFormatInfo.CurrentInfo) +
                                         Prefix.Attribute<SuffixAttribute>()!.Suffix + "Pa";
}