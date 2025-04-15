using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using System.Windows.Input;

namespace WpfApp28
{
   public class MainWindowModel : INotifyPropertyChanged
    {
        public DynamicConfig Config { get; }

        public MainWindowModel()
        {
            Config = new DynamicConfig("config.json");
            Config.LoadFromDictionary();
            Timer timer = new Timer(aaaa);
            timer.Change(1000, 1000);
            // 订阅属性更改事件  方法一
            Config.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "UserName")
                {
                    // 执行你的方法
                    YourCustomMethod();
                }
            };
        }
        public void YourCustomMethod()
        {
            MessageBox.Show("属性值已更改！");
        }
        int num = 0;
        private void aaaa(object? state)
        {
            //MessageBox.Show(Config.GetValue("hobbies").ToString());
            //foreach (var item in Config.GetDynamicMemberNames())
            //{
            //   MessageBox.Show(Config.GetValue(item.ToString()).ToString());
            //}
            //num++;
            //UpdateConfig("Name", num);
            //if(num%2==0)
            //UpdateConfig("EnableFeature", true);
            //else UpdateConfig("EnableFeature", false);
            Config.SetValue("ThemeColor", "#00FF00");
            Config.SetValue("hobbies", "abcd");
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
    
    public class DynamicConfig : DynamicObject, INotifyPropertyChanged
    {
        private class DataModel
        {
            public string Name  { get; set; }="";
            public double ScaleFactor { get; set; } = 1;
            public int UpperLimit { get; set; } = int.MinValue;
            public int LowerLimit { get; set; } = int.MaxValue;
        }
        private readonly Dictionary<string, object> _properties = new();
        public DynamicConfig(string path)
        {
            _properties = LoadJsonIntoDictionary(path);
        }
        public DynamicConfig(ApplicationSettingsBase settings)
        {
            _properties = LoadSettingsIntoDictionary(settings);
        }
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
            try
            {
                foreach (SettingsProperty prop in settings.Properties)
                {
                    object value = settings[prop.Name];
                    _properties.Add(prop.Name, value);
                }
            }
            catch (Exception)
            {
                throw new Exception("配置文件读取失败");
            }
            return _properties;
        }
        public Dictionary<string, object> LoadJsonIntoDictionary(string path)
        {
            try
            {
                var json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<Dictionary<string, object>>(json)!;
            }
            catch (Exception)
            {
                throw new Exception("配置文件读取失败");
            }  
        }
        public void LoadFromDictionary()
        {
            try
            {
                foreach (var kvp in _properties)
                {
                    _properties[kvp.Key] = kvp.Value;
                    OnPropertyChanged(kvp.Key);
                }
            }
            catch (Exception)
            {
                throw new Exception("内容为空");
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
