using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Shared.Utilities {
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
  public class DataPath : Attribute {
    public readonly string Path;

    public DataPath(string path) {
      Path = path;
    }
  }

  [Serializable]
  public abstract class PersistentDataManager<T> where T : PersistentDataManager<T>, new() {
    private static string GetPath() {
      var attrs = Attribute.GetCustomAttributes(typeof(T));
      var path = "";

      foreach (var attribute in attrs) {
        if (attribute is DataPath pathAttribute) {
          path = pathAttribute.Path;

          break;
        }
      }

      if (path == "") {
        throw new Exception("Provide data path");
      }

      return $"{Application.persistentDataPath}/data{path}";
    }

    public static T Load() {
      var path = GetPath();

      if (!File.Exists(path)) {
        return new T();
      }

      var formatter = new BinaryFormatter();
      var file = File.Open(path, FileMode.Open);
      var data = (T)formatter.Deserialize(file);

      file.Close();

      return data;
    }

    public void Save() {
      var path = GetPath();

      new FileInfo(path).Directory?.Create();

      var formatter = new BinaryFormatter();
      var file = File.Create(path);

      formatter.Serialize(file, this);
      file.Close();
    }
  }
}
