﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.5483
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace StatisticsSummary {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    internal sealed partial class App : global::System.Configuration.ApplicationSettingsBase {
        
        private static App defaultInstance = ((App)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new App())));
        
        public static App Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string ConnectionString {
            get {
                return ((string)(this["ConnectionString"]));
            }
            set {
                this["ConnectionString"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("net.tcp://lib.kingrocket.com:8066/TransferService.svc\r\nnet.tcp://lib.kingrocket.c" +
            "om:8066/TransferService.svc")]
        public string Lines {
            get {
                return ((string)(this["Lines"]));
            }
            set {
                this["Lines"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2013/01/01\r\n2013/01/01\r\n")]
        public string Offset {
            get {
                return ((string)(this["Offset"]));
            }
            set {
                this["Offset"] = value;
            }
        }
    }
}