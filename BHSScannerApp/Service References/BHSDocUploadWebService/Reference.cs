﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BHSScannerApp.BHSDocUploadWebService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="https://sg.bhsonline.us/bhsDocsUploadService1.asmx", ConfigurationName="BHSDocUploadWebService.bhsDocsUploadService1Soap")]
    public interface bhsDocsUploadService1Soap {
        
        // CODEGEN: Generating message contract since element name TestResult from namespace https://sg.bhsonline.us/bhsDocsUploadService1.asmx is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="https://sg.bhsonline.us/bhsDocsUploadService1.asmx/Test", ReplyAction="*")]
        BHSScannerApp.BHSDocUploadWebService.TestResponse Test(BHSScannerApp.BHSDocUploadWebService.TestRequest request);
        
        // CODEGEN: Generating message contract since element name f from namespace https://sg.bhsonline.us/bhsDocsUploadService1.asmx is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="https://sg.bhsonline.us/bhsDocsUploadService1.asmx/UploadFile", ReplyAction="*")]
        BHSScannerApp.BHSDocUploadWebService.UploadFileResponse UploadFile(BHSScannerApp.BHSDocUploadWebService.UploadFileRequest request);
        
        // CODEGEN: Generating message contract since element name reqno from namespace https://sg.bhsonline.us/bhsDocsUploadService1.asmx is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="https://sg.bhsonline.us/bhsDocsUploadService1.asmx/FinalizeAllFiles", ReplyAction="*")]
        BHSScannerApp.BHSDocUploadWebService.FinalizeAllFilesResponse FinalizeAllFiles(BHSScannerApp.BHSDocUploadWebService.FinalizeAllFilesRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class TestRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Test", Namespace="https://sg.bhsonline.us/bhsDocsUploadService1.asmx", Order=0)]
        public BHSScannerApp.BHSDocUploadWebService.TestRequestBody Body;
        
        public TestRequest() {
        }
        
        public TestRequest(BHSScannerApp.BHSDocUploadWebService.TestRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class TestRequestBody {
        
        public TestRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class TestResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="TestResponse", Namespace="https://sg.bhsonline.us/bhsDocsUploadService1.asmx", Order=0)]
        public BHSScannerApp.BHSDocUploadWebService.TestResponseBody Body;
        
        public TestResponse() {
        }
        
        public TestResponse(BHSScannerApp.BHSDocUploadWebService.TestResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="https://sg.bhsonline.us/bhsDocsUploadService1.asmx")]
    public partial class TestResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string TestResult;
        
        public TestResponseBody() {
        }
        
        public TestResponseBody(string TestResult) {
            this.TestResult = TestResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class UploadFileRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="UploadFile", Namespace="https://sg.bhsonline.us/bhsDocsUploadService1.asmx", Order=0)]
        public BHSScannerApp.BHSDocUploadWebService.UploadFileRequestBody Body;
        
        public UploadFileRequest() {
        }
        
        public UploadFileRequest(BHSScannerApp.BHSDocUploadWebService.UploadFileRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="https://sg.bhsonline.us/bhsDocsUploadService1.asmx")]
    public partial class UploadFileRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public byte[] f;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string doctype;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string fileName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string reqno;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string releasecode;
        
        public UploadFileRequestBody() {
        }
        
        public UploadFileRequestBody(byte[] f, string doctype, string fileName, string reqno, string releasecode) {
            this.f = f;
            this.doctype = doctype;
            this.fileName = fileName;
            this.reqno = reqno;
            this.releasecode = releasecode;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class UploadFileResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="UploadFileResponse", Namespace="https://sg.bhsonline.us/bhsDocsUploadService1.asmx", Order=0)]
        public BHSScannerApp.BHSDocUploadWebService.UploadFileResponseBody Body;
        
        public UploadFileResponse() {
        }
        
        public UploadFileResponse(BHSScannerApp.BHSDocUploadWebService.UploadFileResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="https://sg.bhsonline.us/bhsDocsUploadService1.asmx")]
    public partial class UploadFileResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string UploadFileResult;
        
        public UploadFileResponseBody() {
        }
        
        public UploadFileResponseBody(string UploadFileResult) {
            this.UploadFileResult = UploadFileResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FinalizeAllFilesRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FinalizeAllFiles", Namespace="https://sg.bhsonline.us/bhsDocsUploadService1.asmx", Order=0)]
        public BHSScannerApp.BHSDocUploadWebService.FinalizeAllFilesRequestBody Body;
        
        public FinalizeAllFilesRequest() {
        }
        
        public FinalizeAllFilesRequest(BHSScannerApp.BHSDocUploadWebService.FinalizeAllFilesRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="https://sg.bhsonline.us/bhsDocsUploadService1.asmx")]
    public partial class FinalizeAllFilesRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string reqno;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string releasecode;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string _type;
        
        public FinalizeAllFilesRequestBody() {
        }
        
        public FinalizeAllFilesRequestBody(string reqno, string releasecode, string _type) {
            this.reqno = reqno;
            this.releasecode = releasecode;
            this._type = _type;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class FinalizeAllFilesResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="FinalizeAllFilesResponse", Namespace="https://sg.bhsonline.us/bhsDocsUploadService1.asmx", Order=0)]
        public BHSScannerApp.BHSDocUploadWebService.FinalizeAllFilesResponseBody Body;
        
        public FinalizeAllFilesResponse() {
        }
        
        public FinalizeAllFilesResponse(BHSScannerApp.BHSDocUploadWebService.FinalizeAllFilesResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="https://sg.bhsonline.us/bhsDocsUploadService1.asmx")]
    public partial class FinalizeAllFilesResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string FinalizeAllFilesResult;
        
        public FinalizeAllFilesResponseBody() {
        }
        
        public FinalizeAllFilesResponseBody(string FinalizeAllFilesResult) {
            this.FinalizeAllFilesResult = FinalizeAllFilesResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface bhsDocsUploadService1SoapChannel : BHSScannerApp.BHSDocUploadWebService.bhsDocsUploadService1Soap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class bhsDocsUploadService1SoapClient : System.ServiceModel.ClientBase<BHSScannerApp.BHSDocUploadWebService.bhsDocsUploadService1Soap>, BHSScannerApp.BHSDocUploadWebService.bhsDocsUploadService1Soap {
        
        public bhsDocsUploadService1SoapClient() {
        }
        
        public bhsDocsUploadService1SoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public bhsDocsUploadService1SoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public bhsDocsUploadService1SoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public bhsDocsUploadService1SoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        BHSScannerApp.BHSDocUploadWebService.TestResponse BHSScannerApp.BHSDocUploadWebService.bhsDocsUploadService1Soap.Test(BHSScannerApp.BHSDocUploadWebService.TestRequest request) {
            return base.Channel.Test(request);
        }
        
        public string Test() {
            BHSScannerApp.BHSDocUploadWebService.TestRequest inValue = new BHSScannerApp.BHSDocUploadWebService.TestRequest();
            inValue.Body = new BHSScannerApp.BHSDocUploadWebService.TestRequestBody();
            BHSScannerApp.BHSDocUploadWebService.TestResponse retVal = ((BHSScannerApp.BHSDocUploadWebService.bhsDocsUploadService1Soap)(this)).Test(inValue);
            return retVal.Body.TestResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        BHSScannerApp.BHSDocUploadWebService.UploadFileResponse BHSScannerApp.BHSDocUploadWebService.bhsDocsUploadService1Soap.UploadFile(BHSScannerApp.BHSDocUploadWebService.UploadFileRequest request) {
            return base.Channel.UploadFile(request);
        }
        
        public string UploadFile(byte[] f, string doctype, string fileName, string reqno, string releasecode) {
            BHSScannerApp.BHSDocUploadWebService.UploadFileRequest inValue = new BHSScannerApp.BHSDocUploadWebService.UploadFileRequest();
            inValue.Body = new BHSScannerApp.BHSDocUploadWebService.UploadFileRequestBody();
            inValue.Body.f = f;
            inValue.Body.doctype = doctype;
            inValue.Body.fileName = fileName;
            inValue.Body.reqno = reqno;
            inValue.Body.releasecode = releasecode;
            BHSScannerApp.BHSDocUploadWebService.UploadFileResponse retVal = ((BHSScannerApp.BHSDocUploadWebService.bhsDocsUploadService1Soap)(this)).UploadFile(inValue);
            return retVal.Body.UploadFileResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        BHSScannerApp.BHSDocUploadWebService.FinalizeAllFilesResponse BHSScannerApp.BHSDocUploadWebService.bhsDocsUploadService1Soap.FinalizeAllFiles(BHSScannerApp.BHSDocUploadWebService.FinalizeAllFilesRequest request) {
            return base.Channel.FinalizeAllFiles(request);
        }
        
        public string FinalizeAllFiles(string reqno, string releasecode, string _type) {
            BHSScannerApp.BHSDocUploadWebService.FinalizeAllFilesRequest inValue = new BHSScannerApp.BHSDocUploadWebService.FinalizeAllFilesRequest();
            inValue.Body = new BHSScannerApp.BHSDocUploadWebService.FinalizeAllFilesRequestBody();
            inValue.Body.reqno = reqno;
            inValue.Body.releasecode = releasecode;
            inValue.Body._type = _type;
            BHSScannerApp.BHSDocUploadWebService.FinalizeAllFilesResponse retVal = ((BHSScannerApp.BHSDocUploadWebService.bhsDocsUploadService1Soap)(this)).FinalizeAllFiles(inValue);
            return retVal.Body.FinalizeAllFilesResult;
        }
    }
}
