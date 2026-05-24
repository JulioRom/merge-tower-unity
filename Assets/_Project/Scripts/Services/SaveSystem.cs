using System.IO;
using UnityEngine;

namespace MergeTower
{
    public sealed class SaveSystem
    {
        private readonly string _savePath;
        private readonly string _backupPath;

        public SaveSystem() : this(Application.persistentDataPath) { }

        public SaveSystem(string directory)
        {
            _savePath = Path.Combine(directory, "save.json");
            _backupPath = Path.Combine(directory, "save.backup.json");
        }

        public void Save(PlayerSaveData data)
        {
            if (File.Exists(_savePath))
                File.Copy(_savePath, _backupPath, overwrite: true);

            string json = JsonUtility.ToJson(data, prettyPrint: false);
            File.WriteAllText(_savePath, json);
        }

        public PlayerSaveData Load()
        {
            if (!File.Exists(_savePath))
                return new PlayerSaveData();

            try
            {
                string json = File.ReadAllText(_savePath);
                return JsonUtility.FromJson<PlayerSaveData>(json);
            }
            catch
            {
                if (!File.Exists(_backupPath))
                    return new PlayerSaveData();

                string backupJson = File.ReadAllText(_backupPath);
                return JsonUtility.FromJson<PlayerSaveData>(backupJson);
            }
        }
    }
}
