using System.Runtime.InteropServices;
using Unit = Yannick.Physic.SI.Converter;

namespace Yannick.Physic.SI.Length;

/// <summary>
/// Represent the si unit for length
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential)]
public readonly struct Meter
{
    internal readonly decimal m_value; // Do not rename (binary serialization)

    public Meter(decimal val, Prefix from = Prefix.None)
    {
        m_value = from == Prefix.None ? val : Unit.BaseValue(val, from);
    }

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
            else if (t == typeof(IZetta))
                return IZetta.Prefix;
            else if (t == typeof(IExa))
                return IExa.Prefix;
            else if (t == typeof(IPeta))
                return IPeta.Prefix;
            else if (t == typeof(ITera))
                return ITera.Prefix;
            else if (t == typeof(IGiga))
                return IGiga.Prefix;
            else if (t == typeof(IMega))
                return IMega.Prefix;
            else if (t == typeof(IKilo))
                return IKilo.Prefix;
            else if (t == typeof(IHecto))
                return IHecto.Prefix;
            else if (t == typeof(IDeca))
                return IDeca.Prefix;
            else if (t == typeof(INone))
                return INone.Prefix;
            else if (t == typeof(IDeci))
                return IDeci.Prefix;
            else if (t == typeof(ICenti))
                return ICenti.Prefix;
            else if (t == typeof(IMilli))
                return IMilli.Prefix;
            else if (t == typeof(IMicro))
                return IMicro.Prefix;
            else if (t == typeof(INano))
                return INano.Prefix;
            else if (t == typeof(IPico))
                return IPico.Prefix;
            else if (t == typeof(IFemto))
                return IFemto.Prefix;
            else if (t == typeof(IAtto))
                return IAtto.Prefix;
            else if (t == typeof(IZepto))
                return IZepto.Prefix;
            else
                throw new InvalidOperationException("Invalid prefix type.");
        }
    }

    public static implicit operator Meter<T>(decimal val) => new(val);

    public static implicit operator Meter(Meter<T> i) => new Meter(i.m_value, IYotta.Prefix);

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
}