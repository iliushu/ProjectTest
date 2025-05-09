﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp29
{
    class MainWindowModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public DynamicCommandManager Commands { get; } 

        public MainWindowModel()
        {
            //Commands=new DynamicCommandManager(this, "CommandsConfig.json");
            
            Commands = new DynamicCommandManager(this, MainWindowModelSet.Default);
        }

        private void LoadCommandsFromConfig(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("配置文件不存在！");
                return;
            }

            var json = File.ReadAllText(filePath);
            var config = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

            if (config != null)
            {
                foreach (var kvp in config)
                {
                    var commandName = kvp.Key;
                    var methodName = kvp.Value;

                    // 通过反射找到方法并绑定
                    var method = GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    if (method != null)
                    {
                        var action = (Action)Delegate.CreateDelegate(typeof(Action), this, method);
                        Commands.AddCommand(commandName, action);
                    }
                }
            }
        }

        private void Save()
        {
            MessageBox.Show("1111111111");
            Console.WriteLine("保存操作执行！");
        }

        private void Load()
        {
            Console.WriteLine("加载操作执行！");
        }

        private void Delete()
        {
            Console.WriteLine("删除操作执行！");
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class DynamicCommandManager : DynamicObject, INotifyPropertyChanged
    {
        private readonly Dictionary<string, ICommand> _commands = new();
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly object _targetInstance;

        public DynamicCommandManager(object targetInstance, string configFilePath)
        {
            _targetInstance = targetInstance;
            LoadCommandsFromConfig(configFilePath);
        }
        public DynamicCommandManager(object targetInstance, ApplicationSettingsBase settings)
        {
            _targetInstance = targetInstance;
            try
            {
                foreach (SettingsProperty prop in settings.Properties)
                {
                    string value = (string)settings[prop.Name];
                    var method = _targetInstance.GetType().GetMethod(value, BindingFlags.NonPublic | BindingFlags.Instance);
                    if (method != null)
                    {
                        var action = (Action)Delegate.CreateDelegate(typeof(Action), _targetInstance, method);
                        AddCommand(prop.Name, action);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("配置文件读取失败");
            }
        }
        private void LoadCommandsFromConfig(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("配置文件不存在！");
                return;
            }

            var json = File.ReadAllText(filePath);
            var config = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

            if (config != null)
            {
                foreach (var kvp in config)
                {
                    var commandName = kvp.Key;
                    var methodName = kvp.Value;

                    // 通过反射找到方法并绑定
                    var method = _targetInstance.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
                    if (method != null)
                    {
                        var action = (Action)Delegate.CreateDelegate(typeof(Action), _targetInstance, method);
                        AddCommand(commandName, action);
                    }
                }
            }
        }

        // 动态获取命令
        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            if (_commands.TryGetValue(binder.Name, out var command))
            {
                result = command;
                return true;
            }
            result = null;
            return false;
        }

        // 动态设置命令
        public void AddCommand(string name, Action execute)
        {
            _commands[name] = new RelayCommand(execute);
            OnPropertyChanged(name);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;
        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute();
        }
        public void Execute(object? parameter)
        {
            _execute();
        }
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
