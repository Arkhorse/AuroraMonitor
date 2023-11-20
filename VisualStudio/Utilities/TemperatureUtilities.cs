using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AuroraMonitor.Utilities
{
    public class TemperatureUtilities
    {
        public static int GetNormalizedTemperature(float temperature)
        {
            return Mathf.CeilToInt(temperature);
        }

        public static float ConvertCelsiusToFahrenheit(float temperature)
        {
            return temperature * 1.8000f + 32.00f;
        }

        public static float ConvertCelsiusToKelvin(float temperature)
        {
            return temperature + 273.15f;
        }

        public static float GetTemperature(TemperatureUnits units, float temperature)
        {
            return units switch
            {
                TemperatureUnits.Celsius        => GetNormalizedTemperature(temperature),
                TemperatureUnits.Kelvin         => GetNormalizedTemperature(ConvertCelsiusToKelvin(temperature)),
                TemperatureUnits.Fahrenheit     => GetNormalizedTemperature(ConvertCelsiusToFahrenheit(temperature)),
                _ => throw new NotImplementedException()
            };
        }

        public static string GetTemperatureUnits(TemperatureUnits units)
        {
            return units switch
            {
                TemperatureUnits.Celsius        => "ºC",
                TemperatureUnits.Kelvin         => "ºK",
                TemperatureUnits.Fahrenheit     => "ºF",
                _ => throw new NotImplementedException()
            };
        }
    }
}
