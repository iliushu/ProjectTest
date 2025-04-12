using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp28
{
   public class MainWindowModel : INotifyPropertyChanged
    {
        public DynamicConfig Config { get; }

        public MainWindowModel()
        {
            Config = new DynamicConfig();
            LoadConfigFromFile("config.json");
            Timer timer = new Timer(aaaa);
            timer.Change(1000, 1000);
        }
        int num = 0;
        private void aaaa(object? state)
        {
            //MessageBox.Show(Config.GetValue("hobbies").ToString());

            //foreach (var item in Config.GetDynamicMemberNames())
            //{
            //   MessageBox.Show(Config.GetValue(item.ToString()).ToString());
            //}
            ;
            //num++;
            //UpdateConfig("Name", num);
            //if(num%2==0)
            //UpdateConfig("EnableFeature", true);
            //else UpdateConfig("EnableFeature", false);
        }

        private void LoadConfigFromFile(string path)
        {
            var configDict = Config.LoadJsonIntoDictionary(path);
            var config = Config.LoadSettingsIntoDictionary(MainWindowModelSet.Default);
            Config.LoadFromDictionary(configDict);
        }
     
        // 允许手动更新配置值
        public void UpdateConfig(string key, object value)
        {
            //((dynamic)Config)[key] = value;
            Config.SetValue(key, value);
            //Config.OnPropertyChanged(key);
        }
        public Dictionary<string, object> LoadSettingsIntoDictionary()
        {
            var settingsDict = new Dictionary<string, object>();
            var settings = MainWindowModelSet.Default;

            foreach (SettingsProperty prop in settings.Properties)
            {
                object value = settings[prop.Name];
                settingsDict.Add(prop.Name, value);
            }

            return settingsDict;
        }
       
        public event PropertyChangedEventHandler? PropertyChanged;
    }

    public class DynamicConfig : DynamicObject, INotifyPropertyChanged
    {
        private readonly Dictionary<string, object> _properties = new();

        public event PropertyChangedEventHandler? PropertyChanged;
        public void SetValue(string key, object value)
        {
            _properties[key] = value; 
            OnPropertyChanged(key);
        }

        public object GetValue(string key)
        {
            return _properties.ContainsKey(key) ? _properties[key] : null;
        }
        // 动态获取属性值
        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            return _properties.TryGetValue(binder.Name, out result);
        }
        public override bool TrySetMember(SetMemberBinder binder, object? value)
        {
            _properties[binder.Name] = value!;
            OnPropertyChanged(binder.Name);
            return true;
        }
        public Dictionary<string, object> LoadSettingsIntoDictionary(ApplicationSettingsBase settings)
        {
            var settingsDict = new Dictionary<string, object>();

            foreach (SettingsProperty prop in settings.Properties)
            {
                object value = settings[prop.Name];
                settingsDict.Add(prop.Name, value);
            }

            return settingsDict;
        }
        public Dictionary<string, object> LoadJsonIntoDictionary(string path)
        {
            var json = File.ReadAllText(path);
            var configDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json)!;
            return configDict;
        }
        public void LoadFromDictionary(Dictionary<string, object> config)
        {
            foreach (var kvp in config)
            {
                _properties[kvp.Key] = kvp.Value;
                OnPropertyChanged(kvp.Key);
            }
        }
        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _properties.Keys;
        }
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
