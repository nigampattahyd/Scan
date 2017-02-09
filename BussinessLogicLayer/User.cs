using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessLogicLayer
{
    public class User
    {
        Int64 _userId;
        string _userName = string.Empty;
        string _password = string.Empty;
        string _emailId = string.Empty;
        string _pNumber = string.Empty;
        int _userType;
        string _fullName = string.Empty;
        string _initial = string.Empty;
        string _address1 = string.Empty;
        string _address2 = string.Empty;
        string _address3 = string.Empty;
        string _city = string.Empty;
        int _state;
        int _country;
        string _zip = string.Empty;
        string _FNumber = string.Empty;

        public Int64 UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string EmailId
        {
            get { return _emailId; }
            set { _emailId = value; }
        }

        public string PhoneNumber
        {
            get { return _pNumber; }
            set { _pNumber = value; }
        }

        public int UserType
        {
            get { return _userType; }
            set { _userType = value; }
        }

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }
        public string Initials
        {
            get { return _initial; }
            set { _initial = value; }
        }
        public string Address1
        {
            get { return _address1; }
            set { _address1 = value; }
        }
        public string Address2
        {
            get { return _address2; }
            set { _address2 = value; }
        }
        public string Address3
        {
            get { return _address3; }
            set { _address3 = value; }
        }
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }
        public int Country
        {
            get { return _country; }
            set { _country = value; }
        }
        public string ZIP
        {
            get { return _zip; }
            set { _zip = value; }
        }
        public string FaxNumber
        {
            get { return _FNumber; }
            set { _FNumber = value; }
        }
    }

    public class MedicalFcility
    {
        Int64 _mdicalFacilityRefNo;
        string _name = string.Empty;
        string _nickname = string.Empty;
        string _address1 = string.Empty;
        string _address2 = string.Empty;
        string _address3 = string.Empty;
        string _city = string.Empty;
        int _state;
        int _country;
        string _zip = string.Empty;
        string _phoneNumber1 = string.Empty;
        string _phoneNumber2 = string.Empty;
        string _phoneNumber3 = string.Empty;
        string _phoneNumber4 = string.Empty;
        string _phoneNumber5 = string.Empty;
        string _fax1 = string.Empty;
        string _fax2 = string.Empty;
        string _fax3 = string.Empty;
        string _emailAddress1 = string.Empty;
        string _emailAddress2 = string.Empty;
        string _emailAddress3 = string.Empty;
        string _emailAddress4 = string.Empty;
        string _emailAddress5 = string.Empty;
        DateTime _createdOn;
        DateTime _createdBy;
        string _ServicedFrom = string.Empty;
        string _MRContactName = string.Empty;
        string _MRContactPhone = string.Empty;
        string _MRContactEmailAddress = string.Empty;
        string _PracticeManagerContactName = string.Empty;
        string _PracticeManagerContactPhoneNumber = string.Empty;
        string _PracticeManagerContactEmailAddress = string.Empty;
        string _AreaManagerName = string.Empty;
        string _AreaManagerAddress1 = string.Empty;
        string _AreaManagerAddress2 = string.Empty;
        string _AreaManagerCity = string.Empty;
        int _AreaManagerState;
        string _AreaManagerZIP = string.Empty;
        string _AreaManagerPhoneNumber = string.Empty;
        string _AreaManagerFaxNumber = string.Empty;
        string _AreaManagerMobileNumber = string.Empty;
        string _AreaManagerEmailAddress = string.Empty;
        int _EstNumberOfRequestPerMonth;
        int _PhysicianCounts;
        string _Speciality = string.Empty;
        double _EstMonthlyRevenue;
        Int64 _AddedBy;

        public Int64 MedicalReferenceNumber
        {
            get { return _mdicalFacilityRefNo; }
            set { _mdicalFacilityRefNo = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Nickname
        {
            get { return _nickname; }
            set { _nickname = value; }
        }
        public string Address1
        {
            get { return _address1; }
            set { _address1 = value; }
        }
        public string Address2
        {
            get { return _address2; }
            set { _address2 = value; }
        }
        public string Address3
        {
            get { return _address3; }
            set { _address3 = value; }
        }
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }
        public int Country
        {
            get { return _country; }
            set { _country = value; }
        }
        public string ZIP
        {
            get { return _zip; }
            set { _zip = value; }
        }
        public string PhoneNumber1
        {
            get { return _phoneNumber1; }
            set { _phoneNumber1 = value; }
        }
        public string PhoneNumber2
        {
            get { return _phoneNumber2; }
            set { _phoneNumber2 = value; }
        }
        public string PhoneNumber3
        {
            get { return _phoneNumber3; }
            set { _phoneNumber3 = value; }
        }
        public string PhoneNumber4
        {
            get { return _phoneNumber4; }
            set { _phoneNumber4 = value; }
        }
        public string PhoneNumber5
        {
            get { return _phoneNumber5; }
            set { _phoneNumber5 = value; }
        }
        public string Fax1
        {
            get { return _fax1; }
            set { _fax1 = value; }
        }
        public string Fax2
        {
            get { return _fax2; }
            set { _fax2 = value; }
        }
        public string Fax3
        {
            get { return _fax3; }
            set { _fax3 = value; }
        }
        public string EmailAddress1
        {
            get { return _emailAddress1; }
            set { _emailAddress1 = value; }
        }
        public string EmailAddress2
        {
            get { return _emailAddress2; }
            set { _emailAddress2 = value; }
        }
        public string EmailAddress3
        {
            get { return _emailAddress3; }
            set { _emailAddress3 = value; }
        }
        public string EmailAddress4
        {
            get { return _emailAddress4; }
            set { _emailAddress4 = value; }
        }
        public string EmailAddress5
        {
            get { return _emailAddress5; }
            set { _emailAddress5 = value; }
        }
        public DateTime CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }
        public DateTime CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        public string ServicedFrom
        {
            get { return _ServicedFrom; }
            set { _ServicedFrom = value; }
        }
        public string MRContactName
        {
            get { return _MRContactName; }
            set { _MRContactName = value; }
        }
        public string MRContactPhone
        {
            get { return _MRContactPhone; }
            set { _MRContactPhone = value; }
        }
        public string MRContactEmailAddress
        {
            get { return _MRContactEmailAddress; }
            set { _MRContactEmailAddress = value; }
        }
        public string PracticeManagerContactName
        {
            get { return _PracticeManagerContactName; }
            set { _PracticeManagerContactName = value; }
        }
        public string PracticeManagerContactPhoneNumber
        {
            get { return _PracticeManagerContactPhoneNumber; }
            set { _PracticeManagerContactPhoneNumber = value; }
        }
        public string PracticeManagerContactEmailAddress
        {
            get { return _PracticeManagerContactEmailAddress; }
            set { _PracticeManagerContactEmailAddress = value; }
        }
        public string AreaManagerName
        {
            get { return _AreaManagerName; }
            set { _AreaManagerName = value; }
        }
        public string AreaManagerAddress1
        {
            get { return _AreaManagerAddress1; }
            set { _AreaManagerAddress1 = value; }
        }
        public string AreaManagerAddress2
        {
            get { return _AreaManagerAddress2; }
            set { _AreaManagerAddress2 = value; }
        }
        public string AreaManagerCity
        {
            get { return _AreaManagerCity; }
            set { _AreaManagerCity = value; }
        }
        public int AreaManagerState
        {
            get { return _AreaManagerState; }
            set { _AreaManagerState = value; }
        }
        public string AreaManagerZIP
        {
            get { return _AreaManagerZIP; }
            set { _AreaManagerZIP = value; }
        }
        public string AreaManagerPhoneNumber
        {
            get { return _AreaManagerPhoneNumber; }
            set { _AreaManagerPhoneNumber = value; }
        }
        public string AreaManagerFaxNumber
        {
            get { return _AreaManagerFaxNumber; }
            set { _AreaManagerFaxNumber = value; }
        }
        public string AreaManagerMobileNumber
        {
            get { return _AreaManagerMobileNumber; }
            set { _AreaManagerMobileNumber = value; }
        }
        public string AreaManagerEmailAddress
        {
            get { return _AreaManagerEmailAddress; }
            set { _AreaManagerEmailAddress = value; }
        }
        public int EstNumberOfRequestPerMonth
        {
            get { return _EstNumberOfRequestPerMonth; }
            set { _EstNumberOfRequestPerMonth = value; }
        }
        public int PhysicianCounts
        {
            get { return _PhysicianCounts; }
            set { _PhysicianCounts = value; }
        }
        public string Speciality
        {
            get { return _Speciality; }
            set { _Speciality = value; }
        }
        public double EstMonthlyRevenue
        {
            get { return _EstMonthlyRevenue; }
            set { _EstMonthlyRevenue = value; }
        }        
        public Int64 AddedBy
        {
            get { return _AddedBy; }
            set { _AddedBy = value; }
        }
    }

    public struct ShippingType
    {
        Int64 _mdicalFacilityRefNo;
        int _shipToTypeId;
        string _shipToText;
        bool _active;
        bool _defaultValue;
        DateTime _createdOn;

        public Int64 MedicalReferenceNumber
        {
            get { return _mdicalFacilityRefNo; }
            set { _mdicalFacilityRefNo = value; }
        }

        public int ShipToTypeId
        {
            get
            {
                return _shipToTypeId;
            }
            set
            {
                _shipToTypeId = value;
            }
        }

        public string ShipToText
        {
            get { return _shipToText; }
            set { _shipToText = value; }
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public bool Default
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

        public DateTime CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }
    }

    public struct RequestType
    {
        Int64 _mdicalFacilityRefNo;
        int _requestTypeId;
        string _requestType;
        bool _active;
        bool _defaultValue;
        DateTime _createdOn;

        public Int64 MedicalReferenceNumber
        {
            get { return _mdicalFacilityRefNo; }
            set { _mdicalFacilityRefNo = value; }
        }

        public int RequestTypeId
        {
            get
            {
                return _requestTypeId;
            }
            set
            {
                _requestTypeId = value;
            }
        }

        public string RequestTypeText
        {
            get { return _requestType; }
            set { _requestType = value; }
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public bool Default
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

        public DateTime CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }
    }

    public struct RequestorType
    {
        Int64 _mdicalFacilityRefNo;
        int _requestorTypeId;
        string _requestorType;
        bool _active;
        bool _defaultValue;
        DateTime _createdOn;

        public Int64 MedicalReferenceNumber
        {
            get { return _mdicalFacilityRefNo; }
            set { _mdicalFacilityRefNo = value; }
        }

        public int RequestorTypeId
        {
            get
            {
                return _requestorTypeId;
            }
            set
            {
                _requestorTypeId = value;
            }
        }

        public string RequestorTypeText
        {
            get { return _requestorType; }
            set { _requestorType = value; }
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public bool Default
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

        public DateTime CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }
    }

    public struct DeliveryTypes
    {
        Int64 _mdicalFacilityRefNo;
        int _deliveryTypeId;
        string _deliveryType;
        bool _active;
        bool _defaultValue;
        DateTime _createdOn;

        public Int64 MedicalReferenceNumber
        {
            get { return _mdicalFacilityRefNo; }
            set { _mdicalFacilityRefNo = value; }
        }

        public int DeliveryTypeId
        {
            get
            {
                return _deliveryTypeId;
            }
            set
            {
                _deliveryTypeId = value;
            }
        }

        public string DeliveryType
        {
            get { return _deliveryType; }
            set { _deliveryType = value; }
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public bool Default
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

        public DateTime CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }
    }

    public struct BillerTypes
    {
        Int64 _mdicalFacilityRefNo;
        int _billerTypeId;
        string _biller;
        bool _active;
        bool _defaultValue;
        DateTime _createdOn;

        public Int64 MedicalReferenceNumber
        {
            get { return _mdicalFacilityRefNo; }
            set { _mdicalFacilityRefNo = value; }
        }

        public int BillerTypeId
        {
            get
            {
                return _billerTypeId;
            }
            set
            {
                _billerTypeId = value;
            }
        }

        public string Biller
        {
            get { return _biller; }
            set { _biller = value; }
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public bool Default
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

        public DateTime CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }
    }

    public struct ReleaseReasonStuff
    {
        Int64 _mdicalFacilityRefNo;
        int _releaseReasonId;
        string _releaseReason;
        int _requestorType;
        bool _active;
        bool _defaultValue;
        DateTime _createdOn;

        public Int64 MedicalReferenceNumber
        {
            get { return _mdicalFacilityRefNo; }
            set { _mdicalFacilityRefNo = value; }
        }

        public int ReleaseReasonId
        {
            get
            {
                return _releaseReasonId;
            }
            set
            {
                _releaseReasonId = value;
            }
        }

        public string ReleaseReason
        {
            get { return _releaseReason; }
            set { _releaseReason = value; }
        }

        public int RequestorType
        {
            get { return _requestorType; }
            set { _requestorType = value; }
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public bool Default
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

        public DateTime CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }
    }

    public struct PatientDetails
    {
        Int64 _patientId;
        string _lastName;
        string _firstName;
        string _emailId;
        string _pNumber;
        string _ssn;
        int _userType;
        string _guardian;
        string _initial;
        string _address1;
        string _address2;
        string _city;
        int _state;
        int _country;
        string _zip;
        private DateTime _dob;
        private DateTime _dateOfService;
        private string _accountNumber;
        private string _medicalRecordNumber;
        private DateTime _createdOn;
        private string _notes;
        public Int64 PatientId
        {
            get { return _patientId; }
            set { _patientId = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string EmailId
        {
            get { return _emailId; }
            set { _emailId = value; }
        }

        public string PhoneNumber
        {
            get { return _pNumber; }
            set { _pNumber = value; }
        }

        public int UserType
        {
            get { return _userType; }
            set { _userType = value; }
        }

        public string Initials
        {
            get { return _initial; }
            set { _initial = value; }
        }
        public string Address1
        {
            get { return _address1; }
            set { _address1 = value; }
        }
        public string Address2
        {
            get { return _address2; }
            set { _address2 = value; }
        }

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }
        public int Country
        {
            get { return _country; }
            set { _country = value; }
        }
        public string ZIP
        {
            get { return _zip; }
            set { _zip = value; }
        }

        public string Guardian
        {
            get { return _guardian; }
            set { _guardian = value; }

        }

        public string SSN
        {
            get { return _ssn; }
            set { _ssn = value; }
        }

        public DateTime DOB
        {
            get { return _dob; }
            set
            {
                _dob = value;
            }
        }

        public DateTime DateOfService
        {
            get { return _dateOfService; }
            set { _dateOfService = value; }
        }
        public string AccountNumber
        {
            get { return _accountNumber; }
            set { _accountNumber = value; }
        }

        public string MedicalRecordNumber
        {
            get { return _medicalRecordNumber; }
            set { _medicalRecordNumber = value; }
        }

        public DateTime CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }
        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
            }

        }

    }

    public struct RequestorStuff
    {
        Int64 _requestorId;
        string _requestorName;
        string _attentionTo;
        string _emailAddress;
        string _pNumber;
        string _fax;
        string _address1;
        string _address2;
        string _city;
        int _state;
        int _country;
        int _requestorType;
        string _zip;
        private DateTime _createdOn;

        public Int64 RequestorId
        {
            get { return _requestorId; }
            set { _requestorId = value; }
        }

        public string RequestorName
        {
            get { return _requestorName; }
            set { _requestorName = value; }
        }

        public string AttentionTo
        {
            get { return _attentionTo; }
            set { _attentionTo = value; }
        }

        public string EmailAddress
        {
            get { return _emailAddress; }
            set { _emailAddress = value; }
        }

        public string PhoneNumber
        {
            get { return _pNumber; }
            set { _pNumber = value; }
        }


        public string Address1
        {
            get { return _address1; }
            set { _address1 = value; }
        }
        public string Address2
        {
            get { return _address2; }
            set { _address2 = value; }
        }

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }
        public int Country
        {
            get { return _country; }
            set { _country = value; }
        }
        public string ZIP
        {
            get { return _zip; }
            set { _zip = value; }
        }

        public string FAX
        {
            get { return _fax; }
            set { _fax = value; }
        }
        public int RequestorType
        {
            get { return _requestorType; }
            set { _requestorType = value; }
        }

        public DateTime CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }

    }

    public struct feedDetails
    {
        Int64 _useId;
        string _feed;
        bool _sendToList;
        string _sendTo;


        public Int64 UserId
        {
            get { return _useId; }
            set { _useId = value; }
        }

        public string Feed
        {
            get { return _feed; }
            set { _feed = value; }
        }

        public bool SendToList
        {
            get { return _sendToList; }
            set { _sendToList = value; }
        }

        public string SendTo
        {
            get { return _sendTo; }
            set { _sendTo = value; }
        }

    }

    public class bulkData
    {
        string plastName=string.Empty;
        string pfirstName = string.Empty;
        DateTime pdob;
        string pssn = string.Empty;
        string pmedicalrecord=string.Empty;
        string paccount = string.Empty;
        DateTime pdos;
        string pphone = string.Empty;
        string pmobile = string.Empty;
        string pfax = string.Empty;
        string pemailaddress = string.Empty;
        string pnotes = string.Empty;

        string rName = string.Empty;
        string rattentionto = string.Empty;
        string raddress1 = string.Empty;
        string raddress2 = string.Empty;
        string rcity = string.Empty;
        string rcitystate = string.Empty;
        int rstate = 0;
        string rzip = string.Empty;
        string rfax = string.Empty;
        string rphone = string.Empty;
        string rmobile = string.Empty;
        string remailadds = string.Empty;
        int rreqtype = 0;
        int rreqreason = 0;
        int rbillto = 0;
        Int64 fileid = 0;
        Int64 userid = 0;
        Int64 medicalfacilityrefno = 0;
        string eeleasecode = string.Empty;
        string shippingInfo1 = string.Empty;
        string shippingInfo2 = string.Empty;
        string shippingInfo3 = string.Empty;
        string shippingInfo4 = string.Empty;
        string shippingInfo5 = string.Empty;
        string shippingInfo6 = string.Empty;
        string shippingInfo7 = string.Empty;
        string shippingInfo8 = string.Empty;
        string shippingInfo9 = string.Empty;
        string shippingInfo10 = string.Empty;
        Int64 shippingStateId = 0;
        string reqid = string.Empty;
        Int64 reqIdForEdit = 0;
        string caseNumber = string.Empty;
        string requesterTypeName = string.Empty;
        string releaseReasonName = string.Empty;
        string billToName = string.Empty;
        string medicalRecordNumber = string.Empty;
        DateTime processDate;
        DateTime uploadDate;
        string enteredBy = string.Empty;
        string status = string.Empty;
        int mrpageCount = 0;
        Int64 patientId = 0;
       
        public string PLastName
        {
            get { return plastName; }
            set { plastName = value; }
        }

        public string PFirstName
        {
            get { return pfirstName; }
            set { pfirstName = value; }
        }

        public DateTime PDOB
        {
            get { return pdob; }
            set { pdob = value; }
        }

        public string PSSN
        {
            get { return pssn; }
            set { pssn = value; }
        }

        public string PmedicalRecord
        {
            get { return pmedicalrecord; }
            set { pmedicalrecord = value; }
        }

        public string PAccount
        {
            get { return paccount; }
            set { paccount = value; }
        }
        public DateTime PDOS
        {
            get { return pdos; }
            set { pdos = value; }
        }
        public string PPhone
        {
            get { return pphone; }
            set { pphone = value; }
        }

        public string PMobile
        {
            get { return pmobile; }
            set { pmobile = value; }
        }
        public string PFax
        {
            get { return pfax; }
            set { pfax = value; }
        }
        public string PEmailAdds
        {
            get { return pemailaddress; }
            set { pemailaddress = value; }
        }

        public string PNotes
        {
            get { return pnotes; }
            set { pnotes = value; }
        }

        public string ReqName
        {
            get { return rName; }
            set { rName = value; }

        }

        public string rAttentionTo
        {
            get { return rattentionto; }
            set { rattentionto = value; }
        }

        public string rAddress1
        {
            get { return raddress1; }
            set { raddress1 = value; }
        }

        public string rAddress2
        {
            get { return raddress2; }
            set { raddress2 = value; }
        }
        public string rCity
        {
            get { return rcity; }
            set { rcity = value; }
        }
        public string rCityState
        {
            get { return rcitystate; }
            set { rcitystate = value; }
        }
        
        public int rState
        {
            get { return rstate; }
            set { rstate = value; }
        }

        public string rZip
        {
            get { return rzip; }
            set { rzip = value; }
        }
        public string rFax
        {
            get { return rfax; }
            set { rfax = value;}
        }
        public string rPhone
        {
            get { return rphone; }
            set { rphone = value; }
        }
        public string rMobile
        {
            get { return rmobile; }
            set { rmobile = value; }
        }

        public string rEmailAdds
        {
            get { return remailadds; }
            set { remailadds = value; }
        }

        public int rRequestType
        {
            get { return rreqtype; }
            set { rreqtype = value; }
        }
        public int rReleaseReason 
        {
            get { return rreqreason; }
            set { rreqreason = value; }
        }
        public Int64 FieldId 
        {
            get { return fileid; }
            set { fileid = value; }
        }
        public Int64 UserId 
        {
            get { return userid; }
            set { userid = value; }
        }
        public Int64 MedicalFacilityRefNo 
        {
            get { return medicalfacilityrefno; }
            set { medicalfacilityrefno = value; }
        }
         public Int32 RBillTo 
        {
            get { return rbillto; }
            set { rbillto = value; }
        }
         public string ReleaseCode 
        {
            get { return eeleasecode; }
            set { eeleasecode = value; }
        }
        public string ShippingInfo1
         {
             get { return shippingInfo1; }
             set { shippingInfo1 = value; }
         }
        public string ShippingInfo2
        {
            get { return shippingInfo2; }
            set { shippingInfo2 = value; }
        }
        public string ShippingInfo3
        {
            get { return shippingInfo3; }
            set { shippingInfo3 = value; }
        }
        public string ShippingInfo4
        {
            get { return shippingInfo4; }
            set { shippingInfo4 = value; }
        }
        public string ShippingInfo5
        {
            get { return shippingInfo5; }
            set { shippingInfo5 = value; }
        }
        public string ShippingInfo6
        {
            get { return shippingInfo6; }
            set { shippingInfo6 = value; }
        }
        public string ShippingInfo7
        {
            get { return shippingInfo7; }
            set { shippingInfo7 = value; }
        }
        public string ShippingInfo8
        {
            get { return shippingInfo8; }
            set { shippingInfo8 = value; }
        }
        public string ShippingInfo9
        {
            get { return shippingInfo9; }
            set { shippingInfo9 = value; }
        }
        public string ShippingInfo10
        {
            get { return shippingInfo10; }
            set { shippingInfo10 = value; }
        }
        public Int64 ShippingStateId
        {
            get { return shippingStateId; }
            set { shippingStateId = value; }
        }
        public string rReqID
        {
            get { return reqid; }
            set { reqid = value; }
        }
        public Int64 ReqIdForEdit
        {
            get { return reqIdForEdit; }
            set { reqIdForEdit = value; }
        }
        public string CaseNumber
        {
            get { return caseNumber; }
            set { caseNumber = value; }
        }
        public string RequestTypeName
        {
            get { return requesterTypeName; }
            set { requesterTypeName = value; }
        }
        public string ReleaseReasonName
        {
            get { return releaseReasonName; }
            set { releaseReasonName = value; }
        }
        public string BillToName
        {
            get { return billToName; }
            set { billToName = value; }
        }
        public string MedicalRecordNumber
        {
            get { return medicalRecordNumber; }
            set { medicalRecordNumber = value; }
        }
        public DateTime ProcessDate
        {
            get { return processDate; }
            set { processDate = value; }
        }
        public DateTime UploadDate
        {
            get { return uploadDate; }
            set { uploadDate = value; }
        }
        public string EnteredBy
        {
            get { return enteredBy; }
            set { enteredBy = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public int MRPageCount
        {
            get { return mrpageCount; }
            set { mrpageCount = value; }
        }
        public Int64 PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }
        
        #region RAC Fields
        string emsdClaimId = string.Empty;
        string emsdCaseId = string.Empty;
        string intendedRecipient = string.Empty;
        string author = string.Empty;
        string authorInstitution= string.Empty;
        string authorPerson= string.Empty;
        string contentTypeCode= string.Empty;
        string entryUUID= string.Empty;
        string targetCommunityId= string.Empty;
        string documentTitle= string.Empty;
        DateTime creationTime;
        DateTime serviceStartTime;
        DateTime serviceStopTime;
        DateTime submissionTime;
        string uniqueId= string.Empty;
        string confidentialityCode= string.Empty;
        string classCode= string.Empty;
        string formatCode= string.Empty;
        string helthcareFacility= string.Empty;
        string submissionSet= string.Empty;
        string batchProcess = string.Empty;

        public string ESMDClaimId
        {
            get { return emsdClaimId; }
            set { emsdClaimId = value; }
        }
        public string ESMDCaseId
        {
            get { return emsdCaseId; }
            set { emsdCaseId = value; }
        }
        public string IntendedRecipient
        {
            get { return intendedRecipient; }
            set { intendedRecipient = value; }
        }
        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        public string AuthorInstitution
        {
            get { return authorInstitution; }
            set { authorInstitution = value; }
        }
        public string AuthorPerson
        {
            get { return authorPerson; }
            set { authorPerson = value; }
        }
        public string ContentTypeCode
        {
            get { return contentTypeCode; }
            set { contentTypeCode = value; }
        }
        public string EntryUUID
        {
            get { return entryUUID; }
            set { entryUUID = value; }
        }
         public string TargetCommunityId
        {
            get { return targetCommunityId; }
            set { targetCommunityId = value; }
        }
        public string DocumentTitle
        {
            get { return documentTitle; }
            set { documentTitle = value; }
        }
        public DateTime CreationTime
        {
            get { return creationTime; }
            set { creationTime = value; }
        }
        public DateTime ServiceStartTime
        {
            get { return serviceStartTime; }
            set { serviceStartTime = value; }
        }
        public DateTime ServiceStopTime
        {
            get { return serviceStopTime; }
            set { serviceStopTime = value; }
        }
        public DateTime SubmissionTime
        {
            get { return submissionTime; }
            set { submissionTime = value; }
        }
        public string UniqueId
        {
            get { return uniqueId; }
            set { uniqueId = value; }
        }
        public string ConfidentialityCode
        {
            get { return confidentialityCode; }
            set { confidentialityCode = value; }
        }
        public string ClassCode
        {
            get { return classCode; }
            set { classCode = value; }
        }
        public string FormatCode
        {
            get { return formatCode; }
            set { formatCode = value; }
        }
        public string HelthcareFacility
        {
            get { return helthcareFacility; }
            set { helthcareFacility = value; }
        }
        public string SubmissionSet
        {
            get { return submissionSet; }
            set { submissionSet = value; }
        }
        public string BatchProcess
        {
            get { return batchProcess; }
            set { batchProcess = value; }
        }

        #endregion

    }

    public class RequestData
    {
        Int64 medicalfacilityRefNo = 0;
        string releaseCode = string.Empty;
        Int64 patientId=0;
        Int64 requestorId = 0;
        int requestType = 0;
        int releaseReasonVal=0;
        Int64 specialProcessInfoId=0;
        string releaseLetterRefNo = string.Empty;
        string rejectionLetterRefNo = string.Empty;
        int requestStatus=0;
        string requestAccess=string.Empty;
        Int64 createdBy=0;
        DateTime dateCompleted;
        DateTime actualDateReceived;
        Int64 invoiceId=0;
        string accountNumber=string.Empty;
        string medicalRecordNumber1=string.Empty;
        DateTime dateOfService;
        string auth_Pass_PDF_User=string.Empty;
        string auth_Pass_PDF_Owner=string.Empty;
        string auth_Pass_ZIP=string.Empty;
        Int64 invoiceNumber = 0;
        Int64 requestorLoginId = 0;
        int billtoid = 0;
        string invoiceNumberTag = string.Empty;
        public Int64 MedicalFacilityRefNo
        {
            get { return medicalfacilityRefNo; }
            set { medicalfacilityRefNo = value; }
        }

        public string ReleaseCode
        {
            get { return releaseCode; }
            set { releaseCode = value; }
        }

        public Int64 PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }

        public Int64 RequestorId
        {
            get { return requestorId; }
            set { requestorId = value; }
        }

        public int RequestType
        {
            get { return requestType; }
            set { requestType = value; }
        }

        public int ReleaseReason
        {
            get { return releaseReasonVal; }
            set { releaseReasonVal = value; }
        }
        public Int64 SpecialProcessInfoId
        {
            get { return specialProcessInfoId; }
            set { specialProcessInfoId = value; }
        }
        public string ReleaseLetterRefNo
        {
            get { return releaseLetterRefNo; }
            set { releaseLetterRefNo = value; }
        }

        public string RejectionLetterRefNo
        {
            get { return rejectionLetterRefNo; }
            set { rejectionLetterRefNo = value; }
        }
        public int RequestStatus
        {
            get { return requestStatus; }
            set { requestStatus = value; }
        }
        public string RequestAccess
        {
            get { return requestAccess; }
            set { requestAccess = value; }
        }

        public Int64 CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        public DateTime DateCompleted
        {
            get { return dateCompleted; }
            set { dateCompleted = value; }

        }
        public DateTime ActualDateReceived
        {
            get { return actualDateReceived; }
            set { actualDateReceived = value; }
        }
        public Int64 InvoiceId
        {
            get { return invoiceId; }
            set { invoiceId = value; }
        }

        public string AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value; }
        }

        public string MedicalRecordNumber1
        {
            get { return medicalRecordNumber1; }
            set { medicalRecordNumber1 = value; }
        }
        public DateTime DateOfService
        {
            get { return dateOfService; }
            set { dateOfService = value; }
        }

        public string Auth_Pass_PDF_User
        {
            get { return auth_Pass_PDF_User; }
            set { auth_Pass_PDF_User = value; }
        }
        public string Auth_Pass_PDF_Owner
        {
            get { return auth_Pass_PDF_Owner; }
            set { auth_Pass_PDF_Owner = value; }
        }
        public string Auth_Pass_ZIP
        {
            get { return auth_Pass_ZIP; }
            set { auth_Pass_ZIP = value; }
        }
        public Int64 InvoiceNumber
        {
            get { return invoiceNumber; }
            set { invoiceNumber = value; }
        }
        public Int64 RequestorLoginId
        {
            get { return requestorLoginId; }
            set { requestorLoginId = value; }
        }
        public int BillToId
        {
            get { return billtoid; }
            set { billtoid = value; }
        }
        public string InvoiceNumberTag
        {
            get { return invoiceNumberTag; }
            set { invoiceNumberTag = value; }
        }
    }
    
    public class InvoiceDetails
    {       
        string releaseCode = string.Empty;
        double amountDue=0;
        double amountPaid=0;
        DateTime dueDate;
        DateTime paidDate;
        double totalAmount = 0;
        int invoiceStatus = 0;
        int invoiceBillable = 0;
        string billingInfo1 = string.Empty;
        string billingInfo2 = string.Empty;
        string billingInfo3 = string.Empty;
        string billingInfo4 = string.Empty;
        string billingInfo5 = string.Empty;
        string billingInfo6 = string.Empty;
        string billingInfo7 = string.Empty;
        string billingInfo8 = string.Empty;
        string billingInfo9 = string.Empty;
        string billingInfo10 = string.Empty;

        string patientInfo1 = string.Empty;
        string patientInfo2 = string.Empty;
        string patientInfo3 = string.Empty;
        string patientInfo4 = string.Empty;
        string patientInfo5 = string.Empty;
        string patientInfo6 = string.Empty;
        string patientInfo7 = string.Empty;
        string patientInfo8 = string.Empty;
        string patientInfo9 = string.Empty;
        string patientInfo10 = string.Empty;

        string shippingAdds1 = string.Empty;
        string shippingAdds2 = string.Empty;
        string shippingAdds3 = string.Empty;
        string shippingAdds4 = string.Empty;
        string shippingAdds5 = string.Empty;
        string shippingAdds6 = string.Empty;
        string shippingAdds7 = string.Empty;
        string shippingAdds8 = string.Empty;
        string shippingAdds9 = string.Empty;
        string shippingAdds10 = string.Empty;

        public string ReleaseCode
        {
            get { return releaseCode; }
            set { releaseCode = value; }
        }
        public double AmountDue
        {
            get { return amountDue; }
            set { amountDue = value; }
        }
        public double AmountPaid
        {
            get { return amountPaid; }
            set { amountPaid = value; }
        }
        public double TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; }
        }
        public DateTime DueDate
        {
            get { return dueDate; }
            set { dueDate = value; }
        }
        public DateTime PaidDate
        {
            get { return paidDate; }
            set { paidDate = value; }
        }
        public int InvoiceStatus
        {
            get { return invoiceStatus; }
            set { invoiceStatus = value; }
        }
        public int InvoiceBillable
        {
            get { return invoiceBillable; }
            set { invoiceBillable = value; }
        }
        public string BillingInfo1
        {
            get { return billingInfo1; }
            set { billingInfo1 = value; }
        }
        public string BillingInfo2
        {
            get { return billingInfo2; }
            set { billingInfo2 = value; }
        }
        public string BillingInfo3
        {
            get { return billingInfo3; }
            set { billingInfo3 = value; }
        }
        public string BillingInfo4
        {
            get { return billingInfo4; }
            set { billingInfo4 = value; }
        }
        public string BillingInfo5
        {
            get { return billingInfo5; }
            set { billingInfo5 = value; }
        }
        public string BillingInfo6
        {
            get { return billingInfo6; }
            set { billingInfo6 = value; }
        }
        public string BillingInfo7
        {
            get { return billingInfo7; }
            set { billingInfo7 = value; }
        }
        public string BillingInfo8
        {
            get { return billingInfo8; }
            set { billingInfo8 = value; }
        }
        public string BillingInfo9
        {
            get { return billingInfo9; }
            set { billingInfo9 = value; }
        }
        public string BillingInfo10
        {
            get { return billingInfo10; }
            set { billingInfo10 = value; }
        }
        public string PatientInfo1
        {
            get { return patientInfo1; }
            set { patientInfo1 = value; }
        }
        public string PatientInfo2
        {
            get { return patientInfo2; }
            set { patientInfo2 = value; }
        }
        public string PatientInfo3
        {
            get { return patientInfo3; }
            set { patientInfo3 = value; }
        }
        public string PatientInfo4
        {
            get { return patientInfo4; }
            set { patientInfo4 = value; }
        }
        public string PatientInfo5
        {
            get { return patientInfo5; }
            set { patientInfo5 = value; }
        }
        public string PatientInfo6
        {
            get { return patientInfo6; }
            set { patientInfo6 = value; }
        }
        public string PatientInfo7
        {
            get { return patientInfo7; }
            set { patientInfo7 = value; }
        }
        public string PatientInfo8
        {
            get { return patientInfo8; }
            set { patientInfo8 = value; }
        }
        public string PatientInfo9
        {
            get { return patientInfo9; }
            set { patientInfo9 = value; }
        }
        public string PatientInfo10
        {
            get { return patientInfo10; }
            set { patientInfo10 = value; }
        }
        public string ShippingAdds1
        {
            get { return shippingAdds1; }
            set { shippingAdds1 = value; }
        }
        public string ShippingAdds2
        {
            get { return shippingAdds2; }
            set { shippingAdds2 = value; }
        }
        public string ShippingAdds3
        {
            get { return shippingAdds3; }
            set { shippingAdds3 = value; }
        }
        public string ShippingAdds4
        {
            get { return shippingAdds4; }
            set { shippingAdds4 = value; }
        }
        public string ShippingAdds5
        {
            get { return shippingAdds5; }
            set { shippingAdds5 = value; }
        }
        public string ShippingAdds6
        {
            get { return shippingAdds6; }
            set { shippingAdds6 = value; }
        }
        public string ShippingAdds7
        {
            get { return shippingAdds7; }
            set { shippingAdds7 = value; }
        }
        public string ShippingAdds8
        {
            get { return shippingAdds8; }
            set { shippingAdds8 = value; }
        }
        public string ShippingAdds9
        {
            get { return shippingAdds9; }
            set { shippingAdds9 = value; }
        }
        public string ShippingAdds10
        {
            get { return shippingAdds10; }
            set { shippingAdds10 = value; }
        }
    }
}
