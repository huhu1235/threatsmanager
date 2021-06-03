// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code++. Version 5.1.94.0. www.xsd2code.com
//  </auto-generated>
// ------------------------------------------------------------------------------
#pragma warning disable
namespace ThreatsManager.Mitre
{
using System;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Collections.Generic;

[DebuggerStepThrough]
[Newtonsoft.Json.JsonObjectAttribute("rootSchemaObjectsValues", MemberSerialization=MemberSerialization.OptIn)]
public partial class rootSchemaObjectsValues
{
    #region Private fields
    private List<object> _items;
    private string _name;
    private string _data;
    private rootSchemaObjectsValuesData_type _data_type;
    #endregion
    
    public rootSchemaObjectsValues()
    {
        _items = new List<object>();
    }
    
    [Newtonsoft.Json.JsonPropertyAttribute()]
    public List<object> Items
    {
        get
        {
            return _items;
        }
        set
        {
            _items = value;
        }
    }
    
    [Newtonsoft.Json.JsonPropertyAttribute()]
    public string name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }
    
    [Newtonsoft.Json.JsonPropertyAttribute()]
    public string data
    {
        get
        {
            return _data;
        }
        set
        {
            _data = value;
        }
    }
    
    [Newtonsoft.Json.JsonPropertyAttribute()]
    public rootSchemaObjectsValuesData_type data_type
    {
        get
        {
            return _data_type;
        }
        set
        {
            _data_type = value;
        }
    }
    
    #region Serialize/Deserialize
    /// <summary>
    /// Serializes current rootSchemaObjectsValues object into an json string
    /// </summary>
    public virtual string Serialize()
    {
        Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
        return JsonConvert.SerializeObject(this, settings);
    }
    
    /// <summary>
    /// Deserializes rootSchemaObjectsValues object
    /// </summary>
    /// <param name="input">string workflow markup to deserialize</param>
    /// <param name="obj">Output rootSchemaObjectsValues object</param>
    /// <param name="exception">output Exception value if deserialize failed</param>
    /// <returns>true if this Serializer can deserialize the object; otherwise, false</returns>
    public static bool Deserialize(string input, out rootSchemaObjectsValues obj, out Exception exception)
    {
        exception = null;
        obj = default(rootSchemaObjectsValues);
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
    
    public static bool Deserialize(string input, out rootSchemaObjectsValues obj)
    {
        Exception exception = null;
        return Deserialize(input, out obj, out exception);
    }
    
    public static rootSchemaObjectsValues Deserialize(string input)
    {
        Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
        return JsonConvert.DeserializeObject<rootSchemaObjectsValues>(input, settings);
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
    
    public static rootSchemaObjectsValues LoadFromFile(string fileName)
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
