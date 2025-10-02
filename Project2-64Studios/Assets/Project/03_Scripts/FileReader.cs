using UnityEngine;
using System.Diagnostics;
using System.IO;

public class FileReader : MonoBehaviour
{
    string filePath;
    int pLine = -1;
    int pColumn = -1;

    void Start()
    {
        string persistentDir = Path.Combine(Application.persistentDataPath, "Level-3");
        if (!Directory.Exists(persistentDir))
            Directory.CreateDirectory(persistentDir);

        filePath = Path.Combine(persistentDir, "Puzzle.txt");

        // Copiar archivo desde StreamingAssets si no existe
        if (!File.Exists(filePath))
        {
            string sourceFile = Path.Combine(Application.streamingAssetsPath, "Level-3/Puzzle.txt");
            if (File.Exists(sourceFile))
            {
                File.Copy(sourceFile, filePath);
                UnityEngine.Debug.Log("Archivo copiado a persistentDataPath");
            }
            else
            {
                UnityEngine.Debug.LogError("No se encontró el archivo original en StreamingAssets: " + sourceFile);
                return;
            }
        }

        // Buscar la posición original de la 'p'
        string[] lines = File.ReadAllLines(filePath);
        bool found = false;
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == 'p')
                {
                    pLine = i;
                    pColumn = j;
                    found = true;
                    break;
                }
            }
            if (found) break;
        }

        if (!found)
        {
            UnityEngine.Debug.LogError("No se encontró la 'p' en el archivo al inicio.");
        }
        else
        {
            UnityEngine.Debug.Log($"Posición original de 'p': línea {pLine}, columna {pColumn}");
        }
    }

    void Update()
    {
        ProtectP(filePath);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (File.Exists(filePath))
            {
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
            else
            {
                UnityEngine.Debug.LogError("No se encontró el archivo: " + filePath);
            }
        }
    }

    void ProtectP(string fileName)
    {
        if (!File.Exists(fileName)) return;

        string[] lines = File.ReadAllLines(fileName);

        if (pLine < 0 || pColumn < 0 || pLine >= lines.Length)
            return;

        char[] chars = lines[pLine].ToCharArray();

        // Si la columna es mayor que la longitud de la línea, expandimos con espacios
        if (pColumn >= chars.Length)
        {
            System.Array.Resize(ref chars, pColumn + 1);
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == '\0')
                    chars[i] = ' ';
            }
        }

        // Restaurar la 'p' si fue borrada
        if (chars[pColumn] != 'p')
        {
            chars[pColumn] = 'p';
            lines[pLine] = new string(chars);
            File.WriteAllLines(fileName, lines);
            UnityEngine.Debug.Log("Se restauró la 'p' en su posición original!");
        }
    }
}