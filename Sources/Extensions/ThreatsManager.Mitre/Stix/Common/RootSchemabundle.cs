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
[Newtonsoft.Json.JsonObjectAttribute("RootSchemabundle", MemberSerialization=MemberSerialization.OptIn)]
public partial class RootSchemabundle
{
    #region Private fields
    private RootSchemabundleType _type;
    private object _id;
    private List<RootSchemabundleObjects> _objects;
    #endregion
    
    public RootSchemabundle()
    {
        _objects = new List<RootSchemabundleObjects>();
    }
    
    [Newtonsoft.Json.JsonPropertyAttribute()]
    [JsonConverter(typeof(StringEnumConverter))]
    public RootSchemabundleType type
    {
        get
        {
            return _type;
        }
        set
        {
            _type = value;
        }
    }
    
    [Newtonsoft.Json.JsonPropertyAttribute()]
    public object id
    {
        get
        {
            return _id;
        }
        set
        {
            _id = value;
        }
    }
    
    [Newtonsoft.Json.JsonPropertyAttribute()]
    public List<RootSchemabundleObjects> objects
    {
        get
        {
            return _objects;
        }
        set
        {
            _objects = value;
        }
    }
    
    #region Serialize/Deserialize
    /// <summary>
    /// Serializes current RootSchemabundle object into an json string
    /// </summary>
    public virtual string Serialize()
    {
        Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
        return JsonConvert.SerializeObject(this, settings);
    }
    
    /// <summary>
    /// Deserializes RootSchemabundle object
    /// </summary>
    /// <param name="input">string workflow markup to deserialize</param>
    /// <param name="obj">Output RootSchemabundle object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize(string input, out RootSchemabundle obj, out Exception exception)
    {
        exception = null;
        obj = default(RootSchemabundle);
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
    
    public static bool Deserialize(string input, out RootSchemabundle obj)
    {
        Exception exception = null;
        return Deserialize(input, out obj, out exception);
    }
    
    public static RootSchemabundle Deserialize(string input)
    {
        Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
        return JsonConvert.DeserializeObject<RootSchemabundle>(input, settings);
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
    
    public static RootSchemabundle LoadFromFile(string fileName)
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