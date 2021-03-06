﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18052
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppProj.Service.StaffInfoServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="StaffInfoServiceReference.StaffInfoSoap")]
    public interface StaffInfoSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/BRACStaffs", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string BRACStaffs();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/BRACStaffs", ReplyAction="*")]
        System.Threading.Tasks.Task<string> BRACStaffsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/StaffInfoByPIN", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string StaffInfoByPIN(string strStaffPIN);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/StaffInfoByPIN", ReplyAction="*")]
        System.Threading.Tasks.Task<string> StaffInfoByPINAsync(string strStaffPIN);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/StaffInfoByDepartment", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string StaffInfoByDepartment(AppProj.Service.StaffInfoServiceReference.project project);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/StaffInfoByDepartment", ReplyAction="*")]
        System.Threading.Tasks.Task<string> StaffInfoByDepartmentAsync(AppProj.Service.StaffInfoServiceReference.project project);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/StaffInfoByDepartmentAndStatus", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string StaffInfoByDepartmentAndStatus(AppProj.Service.StaffInfoServiceReference.project project, string StaffStatu);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/StaffInfoByDepartmentAndStatus", ReplyAction="*")]
        System.Threading.Tasks.Task<string> StaffInfoByDepartmentAndStatusAsync(AppProj.Service.StaffInfoServiceReference.project project, string StaffStatu);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getStaffInfoForMyBRAC", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string getStaffInfoForMyBRAC(string strStaffPIN);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getStaffInfoForMyBRAC", ReplyAction="*")]
        System.Threading.Tasks.Task<string> getStaffInfoForMyBRACAsync(string strStaffPIN);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetStaffInfoByEmail", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string GetStaffInfoByEmail(string EmailID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetStaffInfoByEmail", ReplyAction="*")]
        System.Threading.Tasks.Task<string> GetStaffInfoByEmailAsync(string EmailID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetStaffListByDesignation", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string GetStaffListByDesignation(string DesGrpID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetStaffListByDesignation", ReplyAction="*")]
        System.Threading.Tasks.Task<string> GetStaffListByDesignationAsync(string DesGrpID);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18060")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class project : object, System.ComponentModel.INotifyPropertyChanged {
        
        private projectKeys keysField;
        
        private string projectIdField;
        
        private string programIDField;
        
        private string projectNameField;
        
        private byte[] projectDescField;
        
        private bool projectYNField;
        
        private System.DateTime setDateField;
        
        private string userNameField;
        
        private string projectStatusTypeIDField;
        
        private System.DateTime effectiveDateField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public projectKeys Keys {
            get {
                return this.keysField;
            }
            set {
                this.keysField = value;
                this.RaisePropertyChanged("Keys");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ProjectId {
            get {
                return this.projectIdField;
            }
            set {
                this.projectIdField = value;
                this.RaisePropertyChanged("ProjectId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string ProgramID {
            get {
                return this.programIDField;
            }
            set {
                this.programIDField = value;
                this.RaisePropertyChanged("ProgramID");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string ProjectName {
            get {
                return this.projectNameField;
            }
            set {
                this.projectNameField = value;
                this.RaisePropertyChanged("ProjectName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", Order=4)]
        public byte[] ProjectDesc {
            get {
                return this.projectDescField;
            }
            set {
                this.projectDescField = value;
                this.RaisePropertyChanged("ProjectDesc");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public bool ProjectYN {
            get {
                return this.projectYNField;
            }
            set {
                this.projectYNField = value;
                this.RaisePropertyChanged("ProjectYN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public System.DateTime SetDate {
            get {
                return this.setDateField;
            }
            set {
                this.setDateField = value;
                this.RaisePropertyChanged("SetDate");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public string UserName {
            get {
                return this.userNameField;
            }
            set {
                this.userNameField = value;
                this.RaisePropertyChanged("UserName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public string ProjectStatusTypeID {
            get {
                return this.projectStatusTypeIDField;
            }
            set {
                this.projectStatusTypeIDField = value;
                this.RaisePropertyChanged("ProjectStatusTypeID");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public System.DateTime EffectiveDate {
            get {
                return this.effectiveDateField;
            }
            set {
                this.effectiveDateField = value;
                this.RaisePropertyChanged("EffectiveDate");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18060")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class projectKeys : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string projectIdField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string ProjectId {
            get {
                return this.projectIdField;
            }
            set {
                this.projectIdField = value;
                this.RaisePropertyChanged("ProjectId");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface StaffInfoSoapChannel : AppProj.Service.StaffInfoServiceReference.StaffInfoSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class StaffInfoSoapClient : System.ServiceModel.ClientBase<AppProj.Service.StaffInfoServiceReference.StaffInfoSoap>, AppProj.Service.StaffInfoServiceReference.StaffInfoSoap {
        
        public StaffInfoSoapClient() {
        }
        
        public StaffInfoSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public StaffInfoSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StaffInfoSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StaffInfoSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string BRACStaffs() {
            return base.Channel.BRACStaffs();
        }
        
        public System.Threading.Tasks.Task<string> BRACStaffsAsync() {
            return base.Channel.BRACStaffsAsync();
        }
        
        public string StaffInfoByPIN(string strStaffPIN) {
            return base.Channel.StaffInfoByPIN(strStaffPIN);
        }
        
        public System.Threading.Tasks.Task<string> StaffInfoByPINAsync(string strStaffPIN) {
            return base.Channel.StaffInfoByPINAsync(strStaffPIN);
        }
        
        public string StaffInfoByDepartment(AppProj.Service.StaffInfoServiceReference.project project) {
            return base.Channel.StaffInfoByDepartment(project);
        }
        
        public System.Threading.Tasks.Task<string> StaffInfoByDepartmentAsync(AppProj.Service.StaffInfoServiceReference.project project) {
            return base.Channel.StaffInfoByDepartmentAsync(project);
        }
        
        public string StaffInfoByDepartmentAndStatus(AppProj.Service.StaffInfoServiceReference.project project, string StaffStatu) {
            return base.Channel.StaffInfoByDepartmentAndStatus(project, StaffStatu);
        }
        
        public System.Threading.Tasks.Task<string> StaffInfoByDepartmentAndStatusAsync(AppProj.Service.StaffInfoServiceReference.project project, string StaffStatu) {
            return base.Channel.StaffInfoByDepartmentAndStatusAsync(project, StaffStatu);
        }
        
        public string getStaffInfoForMyBRAC(string strStaffPIN) {
            return base.Channel.getStaffInfoForMyBRAC(strStaffPIN);
        }
        
        public System.Threading.Tasks.Task<string> getStaffInfoForMyBRACAsync(string strStaffPIN) {
            return base.Channel.getStaffInfoForMyBRACAsync(strStaffPIN);
        }
        
        public string GetStaffInfoByEmail(string EmailID) {
            return base.Channel.GetStaffInfoByEmail(EmailID);
        }
        
        public System.Threading.Tasks.Task<string> GetStaffInfoByEmailAsync(string EmailID) {
            return base.Channel.GetStaffInfoByEmailAsync(EmailID);
        }
        
        public string GetStaffListByDesignation(string DesGrpID) {
            return base.Channel.GetStaffListByDesignation(DesGrpID);
        }
        
        public System.Threading.Tasks.Task<string> GetStaffListByDesignationAsync(string DesGrpID) {
            return base.Channel.GetStaffListByDesignationAsync(DesGrpID);
        }
    }
}
