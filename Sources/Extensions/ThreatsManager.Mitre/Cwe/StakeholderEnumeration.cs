// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code++. Version 5.2.97.0. www.xsd2code.com
//  </auto-generated>
// ------------------------------------------------------------------------------
#pragma warning disable
namespace ThreatsManager.Mitre.Cwe
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
/// The StakeholderEnumeration simple type defines the different types of users within the CWE community.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
[Serializable]
[XmlTypeAttribute(Namespace="http://cwe.mitre.org/cwe-6")]
public enum StakeholderEnumeration
{
    [XmlEnumAttribute("Academic Researchers")]
    AcademicResearchers,
    [XmlEnumAttribute("Applied Researchers")]
    AppliedResearchers,
    [XmlEnumAttribute("Assessment Teams")]
    AssessmentTeams,
    [XmlEnumAttribute("Assessment Tool Vendors")]
    AssessmentToolVendors,
    [XmlEnumAttribute("CWE Team")]
    CWETeam,
    Educators,
    [XmlEnumAttribute("Hardware Designers")]
    HardwareDesigners,
    [XmlEnumAttribute("Information Providers")]
    InformationProviders,
    [XmlEnumAttribute("Product Customers")]
    ProductCustomers,
    [XmlEnumAttribute("Product Vendors")]
    ProductVendors,
    [XmlEnumAttribute("Software Developers")]
    SoftwareDevelopers,
    [XmlEnumAttribute("Vulnerability Analysts")]
    VulnerabilityAnalysts,
    Other,
}
}
#pragma warning restore