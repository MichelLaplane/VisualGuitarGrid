using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace VisualGuitarGrid.Preset
{
  // Simple chord shape DTO used by the preset library and loader.
  public class ChordShape
  {
    public string Name { get; set; } = "";
    // -2 muted, -1 not played, 0 open, >=1 fret
    public int[] StringFrets { get; set; } = Array.Empty<int>();
    public int[] Fingers { get; set; } = Array.Empty<int>();
    public string Tuning { get; set; } = "";
  }

  public static class ChordPresetManager
  {
    public static List<ChordShape> LoadLibrary(string filePath)
    {
      if (!File.Exists(filePath)) return new List<ChordShape>();
      var json = File.ReadAllText(filePath);
      try
      {
        var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var list = JsonSerializer.Deserialize<List<ChordShape>>(json, opts);
        return list ?? new List<ChordShape>();
      }
      catch
      {
        return new List<ChordShape>();
      }
    }

    public static void SaveLibrary(string filePath, IEnumerable<ChordShape> shapes)
    {
      var opts = new JsonSerializerOptions { WriteIndented = true };
      var json = JsonSerializer.Serialize(shapes, opts);
      File.WriteAllText(filePath, json);
    }
  }
}