// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code++. Version 5.1.98.0. www.xsd2code.com
//  </auto-generated>
// ------------------------------------------------------------------------------
#pragma warning disable
namespace ThreatsManager.Mitre.Stix
{
using System;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using System.Text;
using System.Collections.Generic;

[DebuggerStepThrough]
[Newtonsoft.Json.JsonObjectAttribute("RootSchemabundleObjectsX509_v3_extensions", MemberSerialization=MemberSerialization.OptIn)]
public partial class RootSchemabundleObjectsX509_v3_extensions
{
    #region Private fields
    private string _basic_constraints;
    private string _name_constraints;
    private string _policy_constraints;
    private string _key_usage;
    private string _extended_key_usage;
    private string _subject_key_identifier;
    private string _authority_key_identifier;
    private string _subject_alternative_name;
    private string _issuer_alternative_name;
    private string _subject_directory_attributes;
    private string _crl_distribution_points;
    private string _inhibit_any_policy;
    private string _private_key_usage_period_not_before;
    private string _private_key_usage_period_not_after;
    private string _certificate_policies;
    private string _policy_mappings;
    #endregion
    
    [Newtonsoft.Json.JsonPropertyAttribute()]
    public string basic_constraints
    {
        get
        {
            return _basic_constraints;
        }
        set
        {
            _basic_constraints = value;
        }
    }
    
    [Newtonsoft.Json.JsonPropertyAttribute()]
    public string name_constraints
    {
        get
        {
            return _name_constraints;
        }
        set
        {
            _name_constraints = value;
        }
    }
    
    [Newtonsoft.Json.JsonPropertyAttribute()]
    public string policy_constraints
    {
        get
        {
            return _policy_constraints;
        }
        set
        {
            _policy_constraints = value;
        }
    }
    
    [Newtonsoft.Json.JsonPropertyAttribute()]
    public string key_usage
    {
        get
        {
            return _key_usage;
        }
        set
        {
            _key_usage = value;
        }
    }
    
    [Newtonsoft.Json.JsonPropertyAttribute()]
    public string extended_key_usage
    {
        get
        {
            return _extended_key_usage;
        }
        set
        {
            _extended_key_usage = value;
        }
    }
    
    [Newtonsoft.Json.JsonPropertyAttribute()]
    public string subject_key_identifier
    {
        get
        {
            return _subject_key_identifier;
        }
        set
        {
            _subject_key_identifier = value;
        }
    }
    
    [Newtonsoft.Json.JsonPropertyAttribute()]
    public string authority_key_identifier
    {
        get
        {
            return _authority_key_identifier;
        }
        set
        {
            _authority_key_identifier = value;
        }
    }
    
    [Newtonsoft.Json.JsonPropertyAttribute()]
    public string subject_alternative_name
    {
        get
        {
            return _subject_alternative_name;
        }
        set
        {
            _subject_alternative_name = value;
        }
    }
    
    [Newtonsoft.Json.JsonPropertyAttribute()]
    public string issuer_alternative_name
    {
        get
        {
            return _issuer_alternative_name;
        }
        set
        {
            _issuer_alternative_name = value;
        }
    }
    
    [Newtonsoft.Json.JsonPropertyAttribute()]
    public string subject_directory_attributes
    {
        get
        {
            return _subject_directory_attributes;
        }
        set
        {
            _subject_directory_attributes = value;
        }
    }
    
    [Newtonsoft.Json.JsonPropertyAttribute()]
    public string crl_distribution_points
    {
        get
        {
            return _crl_distribution_points;
        }
        set
        {
            _crl_distribution_points = value;
        }
    }
    
    [Newtonsoft.Json.JsonPropertyAttribute()]
    public string inhibit_any_policy
    {
        get
        {
            return _inhibit_any_policy;
        }
        set
        {
            _inhibit_any_policy = value;
        }
    }
    
    [Newtonsoft.Json.JsonPropertyAttribute()]
    public string private_key_usage_period_not_before
    {
        get
        {
            return _private_key_usage_period_not_before;
        }
        set
        {
            _private_key_usage_period_not_before = value;
        }
    }
    
    [Newtonsoft.Json.JsonPropertyAttribute()]
    public string private_key_usage_period_not_after
    {
        get
        {
            return _private_key_usage_period_not_after;
        }
        set
        {
            _private_key_usage_period_not_after = value;
        }
    }
    
    [Newtonsoft.Json.JsonPropertyAttribute()]
    public string certificate_policies
    {
        get
        {
            return _certificate_policies;
        }
        set
        {
            _certificate_policies = value;
        }
    }
    
    [Newtonsoft.Json.JsonPropertyAttribute()]
    public string policy_mappings
    {
        get
        {
            return _policy_mappings;
        }
        set
        {
            _policy_mappings = value;
        }
    }
    
    #region Serialize/Deserialize
    /// <summary>
    /// Serializes current RootSchemabundleObjectsX509_v3_extensions object into an json string
    /// </summary>
    public virtual string Serialize()
    {
        Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
        return JsonConvert.SerializeObject(this, settings);
    }
    
    /// <summary>
    /// Deserializes RootSchemabundleObjectsX509_v3_extensions object
    /// </summary>
    /// <param name="input">string workflow markup to deserialize</param>
    /// <param name="obj">Output RootSchemabundleObjectsX509_v3_extensions object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize(string input, out RootSchemabundleObjectsX509_v3_extensions obj, out Exception exception)
    {
        exception = null;
        obj = default(RootSchemabundleObjectsX509_v3_extensions);
        try
        {
            obj = Deserialize(input);
            return true;
        }
        catch (Exception ex)
        {
            exception = ex;
            return false;
        }
    }
    
    public static bool Deserialize(string input, out RootSchemabundleObjectsX509_v3_extensions obj)
    {
        Exception exception = null;
        return Deserialize(input, out obj, out exception);
    }
    
    public static RootSchemabundleObjectsX509_v3_extensions Deserialize(string input)
    {
        Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
        return JsonConvert.DeserializeObject<RootSchemabundleObjectsX509_v3_extensions>(input, settings);
    }
    #endregion
    
    public virtual void SaveToFile(string fileName)
    {
        StreamWriter streamWriter = null;
        try
        {
            string dataString = Serialize();
            FileInfo outputFile = new FileInfo(fileName);
            streamWriter = outputFile.CreateText();
            streamWriter.WriteLine(dataString);
            streamWriter.Close();
        }
        finally
        {
            if ((streamWriter != null))
            {
                streamWriter.Dispose();
            }
        }
    }
    
    public static RootSchemabundleObjectsX509_v3_extensions LoadFromFile(string fileName)
    {
        FileStream file = null;
        StreamReader sr = null;
        try
        {
            file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            sr = new StreamReader(file);
            string dataString = sr.ReadToEnd();
            sr.Close();
            file.Close();
            return Deserialize(dataString);
        }
        finally
        {
            if ((file != null))
            {
                file.Dispose();
            }
            if ((sr != null))
            {
                sr.Dispose();
            }
        }
    }
}
}
#pragma warning restore
