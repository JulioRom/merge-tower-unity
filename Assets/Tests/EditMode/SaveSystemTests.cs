using NUnit.Framework;
using System.IO;
using MergeTower;

namespace MergeTower.Tests
{
    public class SaveSystemTests
    {
        private string _testDir;
        private SaveSystem _saveSystem;

        [SetUp]
        public void SetUp()
        {
            _testDir = Path.Combine(Path.GetTempPath(), "merge-tower-tests");
            Directory.CreateDirectory(_testDir);
            _saveSystem = new SaveSystem(_testDir);
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(_testDir))
                Directory.Delete(_testDir, recursive: true);
        }

        [Test]
        public void Load_NoFile_ReturnsDefaultData()
        {
            var data = _saveSystem.Load();
            Assert.AreEqual(0L, data.Coins);
        }

        [Test]
        public void Save_ThenLoad_PreservesCoins()
        {
            var data = new PlayerSaveData { Coins = 9999L };
            _saveSystem.Save(data);
            var loaded = _saveSystem.Load();
            Assert.AreEqual(9999L, loaded.Coins);
        }

        [Test]
        public void Save_CreatesBackupFile()
        {
            var data = new PlayerSaveData { Coins = 1L };
            _saveSystem.Save(data);
            _saveSystem.Save(data);
            Assert.IsTrue(File.Exists(Path.Combine(_testDir, "save.backup.json")));
        }

        [Test]
        public void Load_CorruptFile_LoadsFromBackup()
        {
            var data = new PlayerSaveData { Coins = 777L };
            _saveSystem.Save(data);
            _saveSystem.Save(data);
            File.WriteAllText(Path.Combine(_testDir, "save.json"), "CORRUPTED{{");
            var loaded = _saveSystem.Load();
            Assert.AreEqual(777L, loaded.Coins);
        }
    }
}
