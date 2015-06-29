// Generated by ProtoGen, Version=2.4.1.521, Culture=neutral, PublicKeyToken=55f7125234beb589.  DO NOT EDIT!
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.ProtocolBuffers;
using pbc = global::Google.ProtocolBuffers.Collections;
using pbd = global::Google.ProtocolBuffers.Descriptors;
using scg = global::System.Collections.Generic;
namespace rfiddata {
  
  namespace Proto {
    
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public static partial class XtiveTag {
    
      #region Extension registration
      public static void RegisterAllExtensions(pb::ExtensionRegistry registry) {
      }
      #endregion
      #region Static variables
      internal static pbd::MessageDescriptor internal__static_rfiddata_XtiveTag__Descriptor;
      internal static pb::FieldAccess.FieldAccessorTable<global::rfiddata.XtiveTag, global::rfiddata.XtiveTag.Builder> internal__static_rfiddata_XtiveTag__FieldAccessorTable;
      internal static pbd::MessageDescriptor internal__static_rfiddata_XtiveTags__Descriptor;
      internal static pb::FieldAccess.FieldAccessorTable<global::rfiddata.XtiveTags, global::rfiddata.XtiveTags.Builder> internal__static_rfiddata_XtiveTags__FieldAccessorTable;
      #endregion
      #region Descriptor
      public static pbd::FileDescriptor Descriptor {
        get { return descriptor; }
      }
      private static pbd::FileDescriptor descriptor;
      
      static XtiveTag() {
        byte[] descriptorData = global::System.Convert.FromBase64String(
            "Cg5YdGl2ZVRhZy5wcm90bxIIcmZpZGRhdGEieQoIWHRpdmVUYWcSCwoDdWlk" + 
            "GAEgASgJEgwKBHJzc2kYAiABKAUSEQoJYkxvd1Bvd2VyGAMgASgIEg8KB2JF" + 
            "eGNpdGUYBCABKAgSEAoIcmVhZGVySVAYBSABKAkSCgoCYW4YBiABKAkSEAoI" + 
            "ZGF0ZVRpbWUYByABKAkiPAoJWHRpdmVUYWdzEg0KBXRva2VuGAEgASgJEiAK" + 
            "BHRhZ3MYAiADKAsyEi5yZmlkZGF0YS5YdGl2ZVRhZ0IxCiBjb20uZGV0ZWN0" + 
            "aW9ubWlkZGxld2FyZS5yZmlkZGF0YUINWHRpdmVUYWdQcm90bw==");
        pbd::FileDescriptor.InternalDescriptorAssigner assigner = delegate(pbd::FileDescriptor root) {
          descriptor = root;
          internal__static_rfiddata_XtiveTag__Descriptor = Descriptor.MessageTypes[0];
          internal__static_rfiddata_XtiveTag__FieldAccessorTable = 
              new pb::FieldAccess.FieldAccessorTable<global::rfiddata.XtiveTag, global::rfiddata.XtiveTag.Builder>(internal__static_rfiddata_XtiveTag__Descriptor,
                  new string[] { "Uid", "Rssi", "BLowPower", "BExcite", "ReaderIP", "An", "DateTime", });
          internal__static_rfiddata_XtiveTags__Descriptor = Descriptor.MessageTypes[1];
          internal__static_rfiddata_XtiveTags__FieldAccessorTable = 
              new pb::FieldAccess.FieldAccessorTable<global::rfiddata.XtiveTags, global::rfiddata.XtiveTags.Builder>(internal__static_rfiddata_XtiveTags__Descriptor,
                  new string[] { "Token", "Tags", });
          return null;
        };
        pbd::FileDescriptor.InternalBuildGeneratedFileFrom(descriptorData,
            new pbd::FileDescriptor[] {
            }, assigner);
      }
      #endregion
      
    }
  }
  #region Messages
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class XtiveTag : pb::GeneratedMessage<XtiveTag, XtiveTag.Builder> {
    private XtiveTag() { }
    private static readonly XtiveTag defaultInstance = new XtiveTag().MakeReadOnly();
    private static readonly string[] _xtiveTagFieldNames = new string[] { "an", "bExcite", "bLowPower", "dateTime", "readerIP", "rssi", "uid" };
    private static readonly uint[] _xtiveTagFieldTags = new uint[] { 50, 32, 24, 58, 42, 16, 10 };
    public static XtiveTag DefaultInstance {
      get { return defaultInstance; }
    }
    
    public override XtiveTag DefaultInstanceForType {
      get { return DefaultInstance; }
    }
    
    protected override XtiveTag ThisMessage {
      get { return this; }
    }
    
    public static pbd::MessageDescriptor Descriptor {
      get { return global::rfiddata.Proto.XtiveTag.internal__static_rfiddata_XtiveTag__Descriptor; }
    }
    
    protected override pb::FieldAccess.FieldAccessorTable<XtiveTag, XtiveTag.Builder> InternalFieldAccessors {
      get { return global::rfiddata.Proto.XtiveTag.internal__static_rfiddata_XtiveTag__FieldAccessorTable; }
    }
    
    public const int UidFieldNumber = 1;
    private bool hasUid;
    private string uid_ = "";
    public bool HasUid {
      get { return hasUid; }
    }
    public string Uid {
      get { return uid_; }
    }
    
    public const int RssiFieldNumber = 2;
    private bool hasRssi;
    private int rssi_;
    public bool HasRssi {
      get { return hasRssi; }
    }
    public int Rssi {
      get { return rssi_; }
    }
    
    public const int BLowPowerFieldNumber = 3;
    private bool hasBLowPower;
    private bool bLowPower_;
    public bool HasBLowPower {
      get { return hasBLowPower; }
    }
    public bool BLowPower {
      get { return bLowPower_; }
    }
    
    public const int BExciteFieldNumber = 4;
    private bool hasBExcite;
    private bool bExcite_;
    public bool HasBExcite {
      get { return hasBExcite; }
    }
    public bool BExcite {
      get { return bExcite_; }
    }
    
    public const int ReaderIPFieldNumber = 5;
    private bool hasReaderIP;
    private string readerIP_ = "";
    public bool HasReaderIP {
      get { return hasReaderIP; }
    }
    public string ReaderIP {
      get { return readerIP_; }
    }
    
    public const int AnFieldNumber = 6;
    private bool hasAn;
    private string an_ = "";
    public bool HasAn {
      get { return hasAn; }
    }
    public string An {
      get { return an_; }
    }
    
    public const int DateTimeFieldNumber = 7;
    private bool hasDateTime;
    private string dateTime_ = "";
    public bool HasDateTime {
      get { return hasDateTime; }
    }
    public string DateTime {
      get { return dateTime_; }
    }
    
    public override bool IsInitialized {
      get {
        return true;
      }
    }
    
    public override void WriteTo(pb::ICodedOutputStream output) {
      int size = SerializedSize;
      string[] field_names = _xtiveTagFieldNames;
      if (hasUid) {
        output.WriteString(1, field_names[6], Uid);
      }
      if (hasRssi) {
        output.WriteInt32(2, field_names[5], Rssi);
      }
      if (hasBLowPower) {
        output.WriteBool(3, field_names[2], BLowPower);
      }
      if (hasBExcite) {
        output.WriteBool(4, field_names[1], BExcite);
      }
      if (hasReaderIP) {
        output.WriteString(5, field_names[4], ReaderIP);
      }
      if (hasAn) {
        output.WriteString(6, field_names[0], An);
      }
      if (hasDateTime) {
        output.WriteString(7, field_names[3], DateTime);
      }
      UnknownFields.WriteTo(output);
    }
    
    private int memoizedSerializedSize = -1;
    public override int SerializedSize {
      get {
        int size = memoizedSerializedSize;
        if (size != -1) return size;
        
        size = 0;
        if (hasUid) {
          size += pb::CodedOutputStream.ComputeStringSize(1, Uid);
        }
        if (hasRssi) {
          size += pb::CodedOutputStream.ComputeInt32Size(2, Rssi);
        }
        if (hasBLowPower) {
          size += pb::CodedOutputStream.ComputeBoolSize(3, BLowPower);
        }
        if (hasBExcite) {
          size += pb::CodedOutputStream.ComputeBoolSize(4, BExcite);
        }
        if (hasReaderIP) {
          size += pb::CodedOutputStream.ComputeStringSize(5, ReaderIP);
        }
        if (hasAn) {
          size += pb::CodedOutputStream.ComputeStringSize(6, An);
        }
        if (hasDateTime) {
          size += pb::CodedOutputStream.ComputeStringSize(7, DateTime);
        }
        size += UnknownFields.SerializedSize;
        memoizedSerializedSize = size;
        return size;
      }
    }
    
    public static XtiveTag ParseFrom(pb::ByteString data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    public static XtiveTag ParseFrom(pb::ByteString data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    public static XtiveTag ParseFrom(byte[] data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    public static XtiveTag ParseFrom(byte[] data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    public static XtiveTag ParseFrom(global::System.IO.Stream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    public static XtiveTag ParseFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    public static XtiveTag ParseDelimitedFrom(global::System.IO.Stream input) {
      return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
    }
    public static XtiveTag ParseDelimitedFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
    }
    public static XtiveTag ParseFrom(pb::ICodedInputStream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    public static XtiveTag ParseFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    private XtiveTag MakeReadOnly() {
      return this;
    }
    
    public static Builder CreateBuilder() { return new Builder(); }
    public override Builder ToBuilder() { return CreateBuilder(this); }
    public override Builder CreateBuilderForType() { return new Builder(); }
    public static Builder CreateBuilder(XtiveTag prototype) {
      return new Builder(prototype);
    }
    
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public sealed partial class Builder : pb::GeneratedBuilder<XtiveTag, Builder> {
      protected override Builder ThisBuilder {
        get { return this; }
      }
      public Builder() {
        result = DefaultInstance;
        resultIsReadOnly = true;
      }
      internal Builder(XtiveTag cloneFrom) {
        result = cloneFrom;
        resultIsReadOnly = true;
      }
      
      private bool resultIsReadOnly;
      private XtiveTag result;
      
      private XtiveTag PrepareBuilder() {
        if (resultIsReadOnly) {
          XtiveTag original = result;
          result = new XtiveTag();
          resultIsReadOnly = false;
          MergeFrom(original);
        }
        return result;
      }
      
      public override bool IsInitialized {
        get { return result.IsInitialized; }
      }
      
      protected override XtiveTag MessageBeingBuilt {
        get { return PrepareBuilder(); }
      }
      
      public override Builder Clear() {
        result = DefaultInstance;
        resultIsReadOnly = true;
        return this;
      }
      
      public override Builder Clone() {
        if (resultIsReadOnly) {
          return new Builder(result);
        } else {
          return new Builder().MergeFrom(result);
        }
      }
      
      public override pbd::MessageDescriptor DescriptorForType {
        get { return global::rfiddata.XtiveTag.Descriptor; }
      }
      
      public override XtiveTag DefaultInstanceForType {
        get { return global::rfiddata.XtiveTag.DefaultInstance; }
      }
      
      public override XtiveTag BuildPartial() {
        if (resultIsReadOnly) {
          return result;
        }
        resultIsReadOnly = true;
        return result.MakeReadOnly();
      }
      
      public override Builder MergeFrom(pb::IMessage other) {
        if (other is XtiveTag) {
          return MergeFrom((XtiveTag) other);
        } else {
          base.MergeFrom(other);
          return this;
        }
      }
      
      public override Builder MergeFrom(XtiveTag other) {
        if (other == global::rfiddata.XtiveTag.DefaultInstance) return this;
        PrepareBuilder();
        if (other.HasUid) {
          Uid = other.Uid;
        }
        if (other.HasRssi) {
          Rssi = other.Rssi;
        }
        if (other.HasBLowPower) {
          BLowPower = other.BLowPower;
        }
        if (other.HasBExcite) {
          BExcite = other.BExcite;
        }
        if (other.HasReaderIP) {
          ReaderIP = other.ReaderIP;
        }
        if (other.HasAn) {
          An = other.An;
        }
        if (other.HasDateTime) {
          DateTime = other.DateTime;
        }
        this.MergeUnknownFields(other.UnknownFields);
        return this;
      }
      
      public override Builder MergeFrom(pb::ICodedInputStream input) {
        return MergeFrom(input, pb::ExtensionRegistry.Empty);
      }
      
      public override Builder MergeFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
        PrepareBuilder();
        pb::UnknownFieldSet.Builder unknownFields = null;
        uint tag;
        string field_name;
        while (input.ReadTag(out tag, out field_name)) {
          if(tag == 0 && field_name != null) {
            int field_ordinal = global::System.Array.BinarySearch(_xtiveTagFieldNames, field_name, global::System.StringComparer.Ordinal);
            if(field_ordinal >= 0)
              tag = _xtiveTagFieldTags[field_ordinal];
            else {
              if (unknownFields == null) {
                unknownFields = pb::UnknownFieldSet.CreateBuilder(this.UnknownFields);
              }
              ParseUnknownField(input, unknownFields, extensionRegistry, tag, field_name);
              continue;
            }
          }
          switch (tag) {
            case 0: {
              throw pb::InvalidProtocolBufferException.InvalidTag();
            }
            default: {
              if (pb::WireFormat.IsEndGroupTag(tag)) {
                if (unknownFields != null) {
                  this.UnknownFields = unknownFields.Build();
                }
                return this;
              }
              if (unknownFields == null) {
                unknownFields = pb::UnknownFieldSet.CreateBuilder(this.UnknownFields);
              }
              ParseUnknownField(input, unknownFields, extensionRegistry, tag, field_name);
              break;
            }
            case 10: {
              result.hasUid = input.ReadString(ref result.uid_);
              break;
            }
            case 16: {
              result.hasRssi = input.ReadInt32(ref result.rssi_);
              break;
            }
            case 24: {
              result.hasBLowPower = input.ReadBool(ref result.bLowPower_);
              break;
            }
            case 32: {
              result.hasBExcite = input.ReadBool(ref result.bExcite_);
              break;
            }
            case 42: {
              result.hasReaderIP = input.ReadString(ref result.readerIP_);
              break;
            }
            case 50: {
              result.hasAn = input.ReadString(ref result.an_);
              break;
            }
            case 58: {
              result.hasDateTime = input.ReadString(ref result.dateTime_);
              break;
            }
          }
        }
        
        if (unknownFields != null) {
          this.UnknownFields = unknownFields.Build();
        }
        return this;
      }
      
      
      public bool HasUid {
        get { return result.hasUid; }
      }
      public string Uid {
        get { return result.Uid; }
        set { SetUid(value); }
      }
      public Builder SetUid(string value) {
        pb::ThrowHelper.ThrowIfNull(value, "value");
        PrepareBuilder();
        result.hasUid = true;
        result.uid_ = value;
        return this;
      }
      public Builder ClearUid() {
        PrepareBuilder();
        result.hasUid = false;
        result.uid_ = "";
        return this;
      }
      
      public bool HasRssi {
        get { return result.hasRssi; }
      }
      public int Rssi {
        get { return result.Rssi; }
        set { SetRssi(value); }
      }
      public Builder SetRssi(int value) {
        PrepareBuilder();
        result.hasRssi = true;
        result.rssi_ = value;
        return this;
      }
      public Builder ClearRssi() {
        PrepareBuilder();
        result.hasRssi = false;
        result.rssi_ = 0;
        return this;
      }
      
      public bool HasBLowPower {
        get { return result.hasBLowPower; }
      }
      public bool BLowPower {
        get { return result.BLowPower; }
        set { SetBLowPower(value); }
      }
      public Builder SetBLowPower(bool value) {
        PrepareBuilder();
        result.hasBLowPower = true;
        result.bLowPower_ = value;
        return this;
      }
      public Builder ClearBLowPower() {
        PrepareBuilder();
        result.hasBLowPower = false;
        result.bLowPower_ = false;
        return this;
      }
      
      public bool HasBExcite {
        get { return result.hasBExcite; }
      }
      public bool BExcite {
        get { return result.BExcite; }
        set { SetBExcite(value); }
      }
      public Builder SetBExcite(bool value) {
        PrepareBuilder();
        result.hasBExcite = true;
        result.bExcite_ = value;
        return this;
      }
      public Builder ClearBExcite() {
        PrepareBuilder();
        result.hasBExcite = false;
        result.bExcite_ = false;
        return this;
      }
      
      public bool HasReaderIP {
        get { return result.hasReaderIP; }
      }
      public string ReaderIP {
        get { return result.ReaderIP; }
        set { SetReaderIP(value); }
      }
      public Builder SetReaderIP(string value) {
        pb::ThrowHelper.ThrowIfNull(value, "value");
        PrepareBuilder();
        result.hasReaderIP = true;
        result.readerIP_ = value;
        return this;
      }
      public Builder ClearReaderIP() {
        PrepareBuilder();
        result.hasReaderIP = false;
        result.readerIP_ = "";
        return this;
      }
      
      public bool HasAn {
        get { return result.hasAn; }
      }
      public string An {
        get { return result.An; }
        set { SetAn(value); }
      }
      public Builder SetAn(string value) {
        pb::ThrowHelper.ThrowIfNull(value, "value");
        PrepareBuilder();
        result.hasAn = true;
        result.an_ = value;
        return this;
      }
      public Builder ClearAn() {
        PrepareBuilder();
        result.hasAn = false;
        result.an_ = "";
        return this;
      }
      
      public bool HasDateTime {
        get { return result.hasDateTime; }
      }
      public string DateTime {
        get { return result.DateTime; }
        set { SetDateTime(value); }
      }
      public Builder SetDateTime(string value) {
        pb::ThrowHelper.ThrowIfNull(value, "value");
        PrepareBuilder();
        result.hasDateTime = true;
        result.dateTime_ = value;
        return this;
      }
      public Builder ClearDateTime() {
        PrepareBuilder();
        result.hasDateTime = false;
        result.dateTime_ = "";
        return this;
      }
    }
    static XtiveTag() {
      object.ReferenceEquals(global::rfiddata.Proto.XtiveTag.Descriptor, null);
    }
  }
  
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class XtiveTags : pb::GeneratedMessage<XtiveTags, XtiveTags.Builder> {
    private XtiveTags() { }
    private static readonly XtiveTags defaultInstance = new XtiveTags().MakeReadOnly();
    private static readonly string[] _xtiveTagsFieldNames = new string[] { "tags", "token" };
    private static readonly uint[] _xtiveTagsFieldTags = new uint[] { 18, 10 };
    public static XtiveTags DefaultInstance {
      get { return defaultInstance; }
    }
    
    public override XtiveTags DefaultInstanceForType {
      get { return DefaultInstance; }
    }
    
    protected override XtiveTags ThisMessage {
      get { return this; }
    }
    
    public static pbd::MessageDescriptor Descriptor {
      get { return global::rfiddata.Proto.XtiveTag.internal__static_rfiddata_XtiveTags__Descriptor; }
    }
    
    protected override pb::FieldAccess.FieldAccessorTable<XtiveTags, XtiveTags.Builder> InternalFieldAccessors {
      get { return global::rfiddata.Proto.XtiveTag.internal__static_rfiddata_XtiveTags__FieldAccessorTable; }
    }
    
    public const int TokenFieldNumber = 1;
    private bool hasToken;
    private string token_ = "";
    public bool HasToken {
      get { return hasToken; }
    }
    public string Token {
      get { return token_; }
    }
    
    public const int TagsFieldNumber = 2;
    private pbc::PopsicleList<global::rfiddata.XtiveTag> tags_ = new pbc::PopsicleList<global::rfiddata.XtiveTag>();
    public scg::IList<global::rfiddata.XtiveTag> TagsList {
      get { return tags_; }
    }
    public int TagsCount {
      get { return tags_.Count; }
    }
    public global::rfiddata.XtiveTag GetTags(int index) {
      return tags_[index];
    }
    
    public override bool IsInitialized {
      get {
        return true;
      }
    }
    
    public override void WriteTo(pb::ICodedOutputStream output) {
      int size = SerializedSize;
      string[] field_names = _xtiveTagsFieldNames;
      if (hasToken) {
        output.WriteString(1, field_names[1], Token);
      }
      if (tags_.Count > 0) {
        output.WriteMessageArray(2, field_names[0], tags_);
      }
      UnknownFields.WriteTo(output);
    }
    
    private int memoizedSerializedSize = -1;
    public override int SerializedSize {
      get {
        int size = memoizedSerializedSize;
        if (size != -1) return size;
        
        size = 0;
        if (hasToken) {
          size += pb::CodedOutputStream.ComputeStringSize(1, Token);
        }
        foreach (global::rfiddata.XtiveTag element in TagsList) {
          size += pb::CodedOutputStream.ComputeMessageSize(2, element);
        }
        size += UnknownFields.SerializedSize;
        memoizedSerializedSize = size;
        return size;
      }
    }
    
    public static XtiveTags ParseFrom(pb::ByteString data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    public static XtiveTags ParseFrom(pb::ByteString data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    public static XtiveTags ParseFrom(byte[] data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    public static XtiveTags ParseFrom(byte[] data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    public static XtiveTags ParseFrom(global::System.IO.Stream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    public static XtiveTags ParseFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    public static XtiveTags ParseDelimitedFrom(global::System.IO.Stream input) {
      return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
    }
    public static XtiveTags ParseDelimitedFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
    }
    public static XtiveTags ParseFrom(pb::ICodedInputStream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    public static XtiveTags ParseFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    private XtiveTags MakeReadOnly() {
      tags_.MakeReadOnly();
      return this;
    }
    
    public static Builder CreateBuilder() { return new Builder(); }
    public override Builder ToBuilder() { return CreateBuilder(this); }
    public override Builder CreateBuilderForType() { return new Builder(); }
    public static Builder CreateBuilder(XtiveTags prototype) {
      return new Builder(prototype);
    }
    
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public sealed partial class Builder : pb::GeneratedBuilder<XtiveTags, Builder> {
      protected override Builder ThisBuilder {
        get { return this; }
      }
      public Builder() {
        result = DefaultInstance;
        resultIsReadOnly = true;
      }
      internal Builder(XtiveTags cloneFrom) {
        result = cloneFrom;
        resultIsReadOnly = true;
      }
      
      private bool resultIsReadOnly;
      private XtiveTags result;
      
      private XtiveTags PrepareBuilder() {
        if (resultIsReadOnly) {
          XtiveTags original = result;
          result = new XtiveTags();
          resultIsReadOnly = false;
          MergeFrom(original);
        }
        return result;
      }
      
      public override bool IsInitialized {
        get { return result.IsInitialized; }
      }
      
      protected override XtiveTags MessageBeingBuilt {
        get { return PrepareBuilder(); }
      }
      
      public override Builder Clear() {
        result = DefaultInstance;
        resultIsReadOnly = true;
        return this;
      }
      
      public override Builder Clone() {
        if (resultIsReadOnly) {
          return new Builder(result);
        } else {
          return new Builder().MergeFrom(result);
        }
      }
      
      public override pbd::MessageDescriptor DescriptorForType {
        get { return global::rfiddata.XtiveTags.Descriptor; }
      }
      
      public override XtiveTags DefaultInstanceForType {
        get { return global::rfiddata.XtiveTags.DefaultInstance; }
      }
      
      public override XtiveTags BuildPartial() {
        if (resultIsReadOnly) {
          return result;
        }
        resultIsReadOnly = true;
        return result.MakeReadOnly();
      }
      
      public override Builder MergeFrom(pb::IMessage other) {
        if (other is XtiveTags) {
          return MergeFrom((XtiveTags) other);
        } else {
          base.MergeFrom(other);
          return this;
        }
      }
      
      public override Builder MergeFrom(XtiveTags other) {
        if (other == global::rfiddata.XtiveTags.DefaultInstance) return this;
        PrepareBuilder();
        if (other.HasToken) {
          Token = other.Token;
        }
        if (other.tags_.Count != 0) {
          result.tags_.Add(other.tags_);
        }
        this.MergeUnknownFields(other.UnknownFields);
        return this;
      }
      
      public override Builder MergeFrom(pb::ICodedInputStream input) {
        return MergeFrom(input, pb::ExtensionRegistry.Empty);
      }
      
      public override Builder MergeFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
        PrepareBuilder();
        pb::UnknownFieldSet.Builder unknownFields = null;
        uint tag;
        string field_name;
        while (input.ReadTag(out tag, out field_name)) {
          if(tag == 0 && field_name != null) {
            int field_ordinal = global::System.Array.BinarySearch(_xtiveTagsFieldNames, field_name, global::System.StringComparer.Ordinal);
            if(field_ordinal >= 0)
              tag = _xtiveTagsFieldTags[field_ordinal];
            else {
              if (unknownFields == null) {
                unknownFields = pb::UnknownFieldSet.CreateBuilder(this.UnknownFields);
              }
              ParseUnknownField(input, unknownFields, extensionRegistry, tag, field_name);
              continue;
            }
          }
          switch (tag) {
            case 0: {
              throw pb::InvalidProtocolBufferException.InvalidTag();
            }
            default: {
              if (pb::WireFormat.IsEndGroupTag(tag)) {
                if (unknownFields != null) {
                  this.UnknownFields = unknownFields.Build();
                }
                return this;
              }
              if (unknownFields == null) {
                unknownFields = pb::UnknownFieldSet.CreateBuilder(this.UnknownFields);
              }
              ParseUnknownField(input, unknownFields, extensionRegistry, tag, field_name);
              break;
            }
            case 10: {
              result.hasToken = input.ReadString(ref result.token_);
              break;
            }
            case 18: {
              input.ReadMessageArray(tag, field_name, result.tags_, global::rfiddata.XtiveTag.DefaultInstance, extensionRegistry);
              break;
            }
          }
        }
        
        if (unknownFields != null) {
          this.UnknownFields = unknownFields.Build();
        }
        return this;
      }
      
      
      public bool HasToken {
        get { return result.hasToken; }
      }
      public string Token {
        get { return result.Token; }
        set { SetToken(value); }
      }
      public Builder SetToken(string value) {
        pb::ThrowHelper.ThrowIfNull(value, "value");
        PrepareBuilder();
        result.hasToken = true;
        result.token_ = value;
        return this;
      }
      public Builder ClearToken() {
        PrepareBuilder();
        result.hasToken = false;
        result.token_ = "";
        return this;
      }
      
      public pbc::IPopsicleList<global::rfiddata.XtiveTag> TagsList {
        get { return PrepareBuilder().tags_; }
      }
      public int TagsCount {
        get { return result.TagsCount; }
      }
      public global::rfiddata.XtiveTag GetTags(int index) {
        return result.GetTags(index);
      }
      public Builder SetTags(int index, global::rfiddata.XtiveTag value) {
        pb::ThrowHelper.ThrowIfNull(value, "value");
        PrepareBuilder();
        result.tags_[index] = value;
        return this;
      }
      public Builder SetTags(int index, global::rfiddata.XtiveTag.Builder builderForValue) {
        pb::ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
        PrepareBuilder();
        result.tags_[index] = builderForValue.Build();
        return this;
      }
      public Builder AddTags(global::rfiddata.XtiveTag value) {
        pb::ThrowHelper.ThrowIfNull(value, "value");
        PrepareBuilder();
        result.tags_.Add(value);
        return this;
      }
      public Builder AddTags(global::rfiddata.XtiveTag.Builder builderForValue) {
        pb::ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
        PrepareBuilder();
        result.tags_.Add(builderForValue.Build());
        return this;
      }
      public Builder AddRangeTags(scg::IEnumerable<global::rfiddata.XtiveTag> values) {
        PrepareBuilder();
        result.tags_.Add(values);
        return this;
      }
      public Builder ClearTags() {
        PrepareBuilder();
        result.tags_.Clear();
        return this;
      }
    }
    static XtiveTags() {
      object.ReferenceEquals(global::rfiddata.Proto.XtiveTag.Descriptor, null);
    }
  }
  
  #endregion
  
}

#endregion Designer generated code