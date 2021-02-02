// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code++. Version 4.2.0.44
//  </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

#pragma warning disable
namespace ThreatsManager.Extensions.Panels.ThreatSources.Capec
{
    /// <summary>
/// The DigitalSignatureInfoType type is used as a way to represent some of the basic information about a digital signature.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace="http://cybox.mitre.org/common-2")]
public partial class DigitalSignatureInfoType
{
    #region Private fields
    private StringObjectPropertyType _certificate_Issuer;
    private StringObjectPropertyType _certificate_Subject;
    private StringObjectPropertyType _signature_Description;
    private bool _signature_exists;
    private bool _signature_verified;
    #endregion
    
    /// <summary>
    /// DigitalSignatureInfoType class constructor
    /// </summary>
    public DigitalSignatureInfoType()
    {
        _signature_Description = new StringObjectPropertyType();
        _certificate_Subject = new StringObjectPropertyType();
        _certificate_Issuer = new StringObjectPropertyType();
    }
    
    /// <summary>
    /// The certificate issuer of the digital signature.
    /// </summary>
    public StringObjectPropertyType Certificate_Issuer
    {
        get => _certificate_Issuer;
        set => _certificate_Issuer = value;
    }
    
    /// <summary>
    /// The certificate subject of the digital signature.
    /// </summary>
    public StringObjectPropertyType Certificate_Subject
    {
        get => _certificate_Subject;
        set => _certificate_Subject = value;
    }
    
    /// <summary>
    /// A description of the digital signature.
    /// </summary>
    public StringObjectPropertyType Signature_Description
    {
        get => _signature_Description;
        set => _signature_Description = value;
    }
    
    /// <summary>
    /// Specifies whether the digital signature exists.
    /// </summary>
    [XmlAttribute]
    public bool signature_exists
    {
        get => _signature_exists;
        set => _signature_exists = value;
    }
    
    /// <summary>
    /// Specifies if the digital signature is verified.
    /// </summary>
    [XmlAttribute]
    public bool signature_verified
    {
        get => _signature_verified;
        set => _signature_verified = value;
    }
}
}
#pragma warning restore