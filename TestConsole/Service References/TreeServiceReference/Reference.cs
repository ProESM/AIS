﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestConsole.TreeServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BaseDto", Namespace="http://schemas.datacontract.org/2004/07/DTO")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(TestConsole.TreeServiceReference.BaseTreeDto))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(TestConsole.TreeServiceReference.TreeDto))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(TestConsole.TreeServiceReference.VirtualTreeDto))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(TestConsole.TreeServiceReference.UserDto))]
    public partial class BaseDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BaseTreeDto", Namespace="http://schemas.datacontract.org/2004/07/DTO")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(TestConsole.TreeServiceReference.TreeDto))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(TestConsole.TreeServiceReference.VirtualTreeDto))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(TestConsole.TreeServiceReference.UserDto))]
    public partial class BaseTreeDto : TestConsole.TreeServiceReference.BaseDto {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CreateDateTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.Guid> ParentIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ShortNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid StateIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid TypeIdField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CreateDateTime {
            get {
                return this.CreateDateTimeField;
            }
            set {
                if ((this.CreateDateTimeField.Equals(value) != true)) {
                    this.CreateDateTimeField = value;
                    this.RaisePropertyChanged("CreateDateTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.Guid> ParentId {
            get {
                return this.ParentIdField;
            }
            set {
                if ((this.ParentIdField.Equals(value) != true)) {
                    this.ParentIdField = value;
                    this.RaisePropertyChanged("ParentId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ShortName {
            get {
                return this.ShortNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ShortNameField, value) != true)) {
                    this.ShortNameField = value;
                    this.RaisePropertyChanged("ShortName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid StateId {
            get {
                return this.StateIdField;
            }
            set {
                if ((this.StateIdField.Equals(value) != true)) {
                    this.StateIdField = value;
                    this.RaisePropertyChanged("StateId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid TypeId {
            get {
                return this.TypeIdField;
            }
            set {
                if ((this.TypeIdField.Equals(value) != true)) {
                    this.TypeIdField = value;
                    this.RaisePropertyChanged("TypeId");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TreeDto", Namespace="http://schemas.datacontract.org/2004/07/DTO")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(TestConsole.TreeServiceReference.VirtualTreeDto))]
    public partial class TreeDto : TestConsole.TreeServiceReference.BaseTreeDto {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="VirtualTreeDto", Namespace="http://schemas.datacontract.org/2004/07/DTO")]
    [System.SerializableAttribute()]
    public partial class VirtualTreeDto : TestConsole.TreeServiceReference.TreeDto {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool HasChildrenField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool HasChildren {
            get {
                return this.HasChildrenField;
            }
            set {
                if ((this.HasChildrenField.Equals(value) != true)) {
                    this.HasChildrenField = value;
                    this.RaisePropertyChanged("HasChildren");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserDto", Namespace="http://schemas.datacontract.org/2004/07/DTO")]
    [System.SerializableAttribute()]
    public partial class UserDto : TestConsole.TreeServiceReference.BaseTreeDto {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LoginField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SaltField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid UserGroupIdField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Login {
            get {
                return this.LoginField;
            }
            set {
                if ((object.ReferenceEquals(this.LoginField, value) != true)) {
                    this.LoginField = value;
                    this.RaisePropertyChanged("Login");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Salt {
            get {
                return this.SaltField;
            }
            set {
                if ((object.ReferenceEquals(this.SaltField, value) != true)) {
                    this.SaltField = value;
                    this.RaisePropertyChanged("Salt");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid UserGroupId {
            get {
                return this.UserGroupIdField;
            }
            set {
                if ((this.UserGroupIdField.Equals(value) != true)) {
                    this.UserGroupIdField = value;
                    this.RaisePropertyChanged("UserGroupId");
                }
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TreeServiceReference.ITreeService")]
    public interface ITreeService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITreeService/FindUserByLogin", ReplyAction="http://tempuri.org/ITreeService/FindUserByLoginResponse")]
        TestConsole.TreeServiceReference.UserDto FindUserByLogin(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITreeService/FindUserByLogin", ReplyAction="http://tempuri.org/ITreeService/FindUserByLoginResponse")]
        System.Threading.Tasks.Task<TestConsole.TreeServiceReference.UserDto> FindUserByLoginAsync(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITreeService/AuthenticateUser", ReplyAction="http://tempuri.org/ITreeService/AuthenticateUserResponse")]
        TestConsole.TreeServiceReference.UserDto AuthenticateUser(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITreeService/AuthenticateUser", ReplyAction="http://tempuri.org/ITreeService/AuthenticateUserResponse")]
        System.Threading.Tasks.Task<TestConsole.TreeServiceReference.UserDto> AuthenticateUserAsync(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITreeService/GetSystemObjects", ReplyAction="http://tempuri.org/ITreeService/GetSystemObjectsResponse")]
        System.Guid[] GetSystemObjects();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITreeService/GetSystemObjects", ReplyAction="http://tempuri.org/ITreeService/GetSystemObjectsResponse")]
        System.Threading.Tasks.Task<System.Guid[]> GetSystemObjectsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITreeService/GetTrees", ReplyAction="http://tempuri.org/ITreeService/GetTreesResponse")]
        TestConsole.TreeServiceReference.VirtualTreeDto[] GetTrees(System.Nullable<System.Guid> parent, System.Guid treeParentType, bool includeParent);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITreeService/GetTrees", ReplyAction="http://tempuri.org/ITreeService/GetTreesResponse")]
        System.Threading.Tasks.Task<TestConsole.TreeServiceReference.VirtualTreeDto[]> GetTreesAsync(System.Nullable<System.Guid> parent, System.Guid treeParentType, bool includeParent);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITreeService/CreateTree", ReplyAction="http://tempuri.org/ITreeService/CreateTreeResponse")]
        TestConsole.TreeServiceReference.TreeDto CreateTree(TestConsole.TreeServiceReference.TreeDto treeDto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITreeService/CreateTree", ReplyAction="http://tempuri.org/ITreeService/CreateTreeResponse")]
        System.Threading.Tasks.Task<TestConsole.TreeServiceReference.TreeDto> CreateTreeAsync(TestConsole.TreeServiceReference.TreeDto treeDto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITreeService/GetTree", ReplyAction="http://tempuri.org/ITreeService/GetTreeResponse")]
        TestConsole.TreeServiceReference.TreeDto GetTree(System.Guid treeId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITreeService/GetTree", ReplyAction="http://tempuri.org/ITreeService/GetTreeResponse")]
        System.Threading.Tasks.Task<TestConsole.TreeServiceReference.TreeDto> GetTreeAsync(System.Guid treeId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITreeService/UpdateTree", ReplyAction="http://tempuri.org/ITreeService/UpdateTreeResponse")]
        void UpdateTree(TestConsole.TreeServiceReference.TreeDto treeDto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITreeService/UpdateTree", ReplyAction="http://tempuri.org/ITreeService/UpdateTreeResponse")]
        System.Threading.Tasks.Task UpdateTreeAsync(TestConsole.TreeServiceReference.TreeDto treeDto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITreeService/DeleteTree", ReplyAction="http://tempuri.org/ITreeService/DeleteTreeResponse")]
        void DeleteTree(TestConsole.TreeServiceReference.TreeDto treeDto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITreeService/DeleteTree", ReplyAction="http://tempuri.org/ITreeService/DeleteTreeResponse")]
        System.Threading.Tasks.Task DeleteTreeAsync(TestConsole.TreeServiceReference.TreeDto treeDto);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITreeServiceChannel : TestConsole.TreeServiceReference.ITreeService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TreeServiceClient : System.ServiceModel.ClientBase<TestConsole.TreeServiceReference.ITreeService>, TestConsole.TreeServiceReference.ITreeService {
        
        public TreeServiceClient() {
        }
        
        public TreeServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TreeServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TreeServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TreeServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public TestConsole.TreeServiceReference.UserDto FindUserByLogin(string login) {
            return base.Channel.FindUserByLogin(login);
        }
        
        public System.Threading.Tasks.Task<TestConsole.TreeServiceReference.UserDto> FindUserByLoginAsync(string login) {
            return base.Channel.FindUserByLoginAsync(login);
        }
        
        public TestConsole.TreeServiceReference.UserDto AuthenticateUser(string login, string password) {
            return base.Channel.AuthenticateUser(login, password);
        }
        
        public System.Threading.Tasks.Task<TestConsole.TreeServiceReference.UserDto> AuthenticateUserAsync(string login, string password) {
            return base.Channel.AuthenticateUserAsync(login, password);
        }
        
        public System.Guid[] GetSystemObjects() {
            return base.Channel.GetSystemObjects();
        }
        
        public System.Threading.Tasks.Task<System.Guid[]> GetSystemObjectsAsync() {
            return base.Channel.GetSystemObjectsAsync();
        }
        
        public TestConsole.TreeServiceReference.VirtualTreeDto[] GetTrees(System.Nullable<System.Guid> parent, System.Guid treeParentType, bool includeParent) {
            return base.Channel.GetTrees(parent, treeParentType, includeParent);
        }
        
        public System.Threading.Tasks.Task<TestConsole.TreeServiceReference.VirtualTreeDto[]> GetTreesAsync(System.Nullable<System.Guid> parent, System.Guid treeParentType, bool includeParent) {
            return base.Channel.GetTreesAsync(parent, treeParentType, includeParent);
        }
        
        public TestConsole.TreeServiceReference.TreeDto CreateTree(TestConsole.TreeServiceReference.TreeDto treeDto) {
            return base.Channel.CreateTree(treeDto);
        }
        
        public System.Threading.Tasks.Task<TestConsole.TreeServiceReference.TreeDto> CreateTreeAsync(TestConsole.TreeServiceReference.TreeDto treeDto) {
            return base.Channel.CreateTreeAsync(treeDto);
        }
        
        public TestConsole.TreeServiceReference.TreeDto GetTree(System.Guid treeId) {
            return base.Channel.GetTree(treeId);
        }
        
        public System.Threading.Tasks.Task<TestConsole.TreeServiceReference.TreeDto> GetTreeAsync(System.Guid treeId) {
            return base.Channel.GetTreeAsync(treeId);
        }
        
        public void UpdateTree(TestConsole.TreeServiceReference.TreeDto treeDto) {
            base.Channel.UpdateTree(treeDto);
        }
        
        public System.Threading.Tasks.Task UpdateTreeAsync(TestConsole.TreeServiceReference.TreeDto treeDto) {
            return base.Channel.UpdateTreeAsync(treeDto);
        }
        
        public void DeleteTree(TestConsole.TreeServiceReference.TreeDto treeDto) {
            base.Channel.DeleteTree(treeDto);
        }
        
        public System.Threading.Tasks.Task DeleteTreeAsync(TestConsole.TreeServiceReference.TreeDto treeDto) {
            return base.Channel.DeleteTreeAsync(treeDto);
        }
    }
}
