// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code++. Version 5.2.97.0. www.xsd2code.com
//  </auto-generated>
// ------------------------------------------------------------------------------
#pragma warning disable
namespace ThreatsManager.Mitre.Capec
{
using System;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;
using System.Xml;
using System.IO;
using System.Text;
using System.Collections.Generic;

/// <summary>
/// The TaxonomyNameEnumeration simple type lists the different known taxomomies that can be mapped to CAPEC.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
[Serializable]
[XmlTypeAttribute(Namespace="http://capec.mitre.org/capec-3")]
public enum TaxonomyNameEnumeration
{
    ATTACK,
    WASC,
    [XmlEnumAttribute("OWASP Attacks")]
    OWASPAttacks,
}
}
#pragma warning restore
