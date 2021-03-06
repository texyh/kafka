// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: protos/address.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace ProtoBufSerialization {

  /// <summary>Holder for reflection information generated from protos/address.proto</summary>
  public static partial class AddressReflection {

    #region Descriptor
    /// <summary>File descriptor for protos/address.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static AddressReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChRwcm90b3MvYWRkcmVzcy5wcm90bxIVUHJvdG9CdWZTZXJpYWxpemF0aW9u",
            "IksKB0FkZHJlc3MSDgoGU3RyZWV0GAEgASgJEg0KBVN0YXRlGAIgASgJEg8K",
            "B1ppcENvZGUYAyABKAkSEAoIUGVyc29uSWQYBCABKAViBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::ProtoBufSerialization.Address), global::ProtoBufSerialization.Address.Parser, new[]{ "Street", "State", "ZipCode", "PersonId" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Address : pb::IMessage<Address> {
    private static readonly pb::MessageParser<Address> _parser = new pb::MessageParser<Address>(() => new Address());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Address> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ProtoBufSerialization.AddressReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Address() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Address(Address other) : this() {
      street_ = other.street_;
      state_ = other.state_;
      zipCode_ = other.zipCode_;
      personId_ = other.personId_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Address Clone() {
      return new Address(this);
    }

    /// <summary>Field number for the "Street" field.</summary>
    public const int StreetFieldNumber = 1;
    private string street_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Street {
      get { return street_; }
      set {
        street_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "State" field.</summary>
    public const int StateFieldNumber = 2;
    private string state_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string State {
      get { return state_; }
      set {
        state_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "ZipCode" field.</summary>
    public const int ZipCodeFieldNumber = 3;
    private string zipCode_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string ZipCode {
      get { return zipCode_; }
      set {
        zipCode_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "PersonId" field.</summary>
    public const int PersonIdFieldNumber = 4;
    private int personId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int PersonId {
      get { return personId_; }
      set {
        personId_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Address);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Address other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Street != other.Street) return false;
      if (State != other.State) return false;
      if (ZipCode != other.ZipCode) return false;
      if (PersonId != other.PersonId) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Street.Length != 0) hash ^= Street.GetHashCode();
      if (State.Length != 0) hash ^= State.GetHashCode();
      if (ZipCode.Length != 0) hash ^= ZipCode.GetHashCode();
      if (PersonId != 0) hash ^= PersonId.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Street.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Street);
      }
      if (State.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(State);
      }
      if (ZipCode.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(ZipCode);
      }
      if (PersonId != 0) {
        output.WriteRawTag(32);
        output.WriteInt32(PersonId);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Street.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Street);
      }
      if (State.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(State);
      }
      if (ZipCode.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(ZipCode);
      }
      if (PersonId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(PersonId);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Address other) {
      if (other == null) {
        return;
      }
      if (other.Street.Length != 0) {
        Street = other.Street;
      }
      if (other.State.Length != 0) {
        State = other.State;
      }
      if (other.ZipCode.Length != 0) {
        ZipCode = other.ZipCode;
      }
      if (other.PersonId != 0) {
        PersonId = other.PersonId;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Street = input.ReadString();
            break;
          }
          case 18: {
            State = input.ReadString();
            break;
          }
          case 26: {
            ZipCode = input.ReadString();
            break;
          }
          case 32: {
            PersonId = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
