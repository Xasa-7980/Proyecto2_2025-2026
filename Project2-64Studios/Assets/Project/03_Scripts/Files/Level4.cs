using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Text.RegularExpressions;
using static UnityEngine.Rendering.DebugUI;

public class Level4 : FileManager
{

    [SerializeField] Light2D globalLight; //lvl3
    public Level4 ( string _directoryPath, string _fileName, Light2D _light ) : base(_directoryPath, _fileName) 
    { 
        globalLight = _light;
        originalContent = File.ReadAllText(filePath);
    }
    public override void LevelMechanics ( )
    {
        string[] lines = File.ReadAllLines(filePath);

        if (lines.Length == 0)
        {
            UnityEngine.Debug.LogWarning("El archivo está vacío.");
            return;
        }

        float valueFound = -1f;

        foreach (string line in lines)
        {

            Match match = Regex.Match(line, @"[-+]?[0-9]*\.?[0-9]+");
            //Sirve para buscar el primer numero entero o decimal negativo o positivo

            if (match.Success)
            {
                // Convertir el texto del número en float
                if (float.TryParse(match.Value, System.Globalization.NumberStyles.Float,
                                    System.Globalization.CultureInfo.InvariantCulture, out float val))
                {
                    valueFound = val;
                    UnityEngine.Debug.Log($"Valor numérico encontrado: {valueFound}");
                    break;
                }
            }
        }

        valueFound = Mathf.Clamp(valueFound, 0f, 2);
        if (valueFound >= 1)
        {
            isCompleted = true;
        }
        ChangeLightIntensity(valueFound);
        fileChanged = false;
    }
    void ChangeLightIntensity(float value )
    {
        globalLight.intensity = value;
    }
}

