using SoftCircuits.CsvParser;
using System;
using System.IO;

namespace GHHSoftware.Common.Helpers
{
  public class GHHTypedSimpleSettingsHelper
  {
    public string ErrorMessage { get; }
    private string _errorMessage;

    public T LoadSettings<T>(string settingsFile = "") where T : class, new()
    {
      object retVal = null;
      _errorMessage = string.Empty;

      if (File.Exists(settingsFile))
      {
        try
        {
          using (CsvReader<T> rdr = new CsvReader<T>(settingsFile))
          {
            rdr.ReadHeaders(true);

            retVal = rdr.Read();
          }
        }
        catch (Exception ex)
        {
          _errorMessage = $"Exception in LoadSettings(): {ex.Message}";
        }
      }
      else
      {
        _errorMessage = $"Could not locate settings file: {settingsFile}";
      }

      return retVal as T;
    }

    public bool SaveSettings<T>(string settingsFile, T settingsClass) where T : class, new() 
    {
      var retVal = false;
      _errorMessage = string.Empty;

      try
      {
        using (CsvWriter<T> wtr = new CsvWriter<T>(settingsFile))
        {
          wtr.WriteHeaders();
          wtr.Write(settingsClass);

          // if we got here, all's well!
          retVal = true;
        }
      }
      catch (Exception e)
      {
        _errorMessage = $"Exception in SaveSettings<T>(): {e.Message}";
      }

      return retVal;
    }
  }
}
