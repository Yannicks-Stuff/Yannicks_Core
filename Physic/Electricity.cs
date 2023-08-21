namespace Yannick.Physic
{
    /// <summary>
    /// Provides methods for performing electrical calculations.
    /// </summary>
    public static class Electricity
    {
        /// <summary>
        /// Calculates the electrical resistance using Ohm's law.
        /// </summary>
        /// <param name="voltage">The voltage across the resistor in volts.</param>
        /// <param name="current">The current flowing through the resistor in amperes.</param>
        /// <returns>The electrical resistance in ohms.</returns>
        public static decimal CalculateResistance(decimal voltage, decimal current)
        {
            return voltage / current;
        }


        /// <summary>
        /// Calculates the electrical power.
        /// </summary>
        /// <param name="voltage">The voltage across the component in volts.</param>
        /// <param name="current">The current flowing through the component in amperes.</param>
        /// <returns>The electrical power in watts.</returns>
        public static decimal CalculatePower(decimal voltage, decimal current)
        {
            return voltage * current;
        }

        /// <summary>
        /// Calculates the electrical work.
        /// </summary>
        /// <param name="power">The electrical power in watts.</param>
        /// <param name="time">The time period in seconds.</param>
        /// <returns>The electrical work in joules.</returns>
        public static decimal CalculateWork(decimal power, decimal time)
        {
            return power * time;
        }

        /// <summary>
        /// Calculates the electrical capacitance.
        /// </summary>
        /// <param name="charge">The charge stored in the capacitor in coulombs.</param>
        /// <param name="voltage">The voltage across the capacitor in volts.</param>
        /// <returns>The electrical capacitance in farads.</returns>
        public static decimal CalculateCapacitance(decimal charge, decimal voltage)
        {
            return charge / voltage;
        }

        /// <summary>
        /// Calculates the electrical energy.
        /// </summary>
        /// <param name="power">The electrical power in watts.</param>
        /// <param name="time">The time period in hours.</param>
        /// <returns>The electrical energy in kilowatt-hours.</returns>
        public static decimal CalculateEnergy(decimal power, decimal time)
        {
            return power * time / 1000;
        }

        /// <summary>
        /// Calculates the electrical charge.
        /// </summary>
        /// <param name="current">The current flowing through the component in amperes.</param>
        /// <param name="time">The time period in seconds.</param>
        /// <returns>The electrical charge in coulombs.</returns>
        public static decimal CalculateCharge(decimal current, decimal time)
        {
            return current * time;
        }

        /// <summary>
        /// Calculates the electric field strength.
        /// </summary>
        /// <param name="force">The force experienced by a charge in newtons.</param>
        /// <param name="charge">The charge in coulombs.</param>
        /// <returns>The electric field strength in newtons per coulomb.</returns>
        public static decimal CalculateFieldStrength(decimal force, decimal charge)
        {
            return force / charge;
        }

        /// <summary>
        /// Calculates the electric voltage.
        /// </summary>
        /// <param name="energy">The energy in joules.</param>
        /// <param name="charge">The charge in coulombs.</param>
        /// <returns>The electric voltage in volts.</returns>
        public static decimal CalculateVoltage(decimal energy, decimal charge)
        {
            return energy / charge;
        }

        /// <summary>
        /// Calculates the inductance.
        /// </summary>
        /// <param name="flux">The magnetic flux in webers.</param>
        /// <param name="current">The current in amperes.</param>
        /// <returns>The inductance in henrys.</returns>
        public static decimal CalculateInductance(decimal flux, decimal current)
        {
            return flux / current;
        }

        /// <summary>
        /// Calculates the impedance.
        /// </summary>
        /// <param name="resistance">The resistance in ohms.</param>
        /// <param name="reactance">The reactance in ohms.</param>
        /// <returns>The impedance in ohms.</returns>
        public static decimal CalculateImpedance(decimal resistance, decimal reactance)
        {
            return (decimal)Math.Sqrt((double)(resistance * resistance + reactance * reactance));
        }

        /// <summary>
        /// Calculates the reactance.
        /// </summary>
        /// <param name="inductance">The inductance in henrys.</param>
        /// <param name="frequency">The frequency in hertz.</param>
        /// <returns>The reactance in ohms.</returns>
        public static decimal CalculateReactance(decimal inductance, decimal frequency)
        {
            return 2 * (decimal)Math.PI * frequency * inductance;
        }

        /// <summary>
        /// Calculates the electromagnetic wavelength.
        /// </summary>
        /// <param name="speed">The speed of light in meters per second.</param>
        /// <param name="frequency">The frequency in hertz.</param>
        /// <returns>The electromagnetic wavelength in meters.</returns>
        public static decimal CalculateWavelength(decimal speed, decimal frequency)
        {
            return speed / frequency;
        }

        /// <summary>
        /// Calculates the electromagnetic frequency.
        /// </summary>
        /// <param name="speed">The speed of light in meters per second.</param>
        /// <param name="wavelength">The wavelength in meters.</param>
        /// <returns>The electromagnetic frequency in hertz.</returns>
        public static decimal CalculateFrequency(decimal speed, decimal wavelength)
        {
            return speed / wavelength;
        }

        /// <summary>
        /// Calculates the magnetic field strength.
        /// </summary>
        /// <param name="force">The force experienced by a charge in newtons.</param>
        /// <param name="charge">The charge in coulombs.</param>
        /// <param name="velocity">The velocity in meters per second.</param>
        /// <returns>The magnetic field strength in teslas.</returns>
        public static decimal CalculateMagneticFieldStrength(decimal force, decimal charge, decimal velocity)
        {
            return force / (charge * velocity);
        }

        /// <summary>
        /// Calculates the magnetic flux density.
        /// </summary>
        /// <param name="flux">The magnetic flux in webers.</param>
        /// <param name="area">The area in square meters.</param>
        /// <returns>The magnetic flux density in teslas.</returns>
        public static decimal CalculateMagneticFluxDensity(decimal flux, decimal area)
        {
            return flux / area;
        }
    }
}