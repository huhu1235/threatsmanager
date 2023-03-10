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
/// This field captures possible outcomes for this attack step.
/// </summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
[Serializable]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(AnonymousType=true, Namespace="http://capec.mitre.org/capec-2")]
public partial class Custom_Attack_StepTypeOutcome
{
    #region Private fields
    private string _outcome_Description;
    private Relevant_Attack_Surface_ElementsType _relevant_Attack_Surface_Elements;
    private ObservablesType2 _observables;
    private string _id;
    private Custom_Attack_StepTypeOutcomeType _type;
    #endregion
    
    /// <summary>
    /// Custom_Attack_StepTypeOutcome class constructor
    /// </summary>
    public Custom_Attack_StepTypeOutcome()
    {
        _observables = new ObservablesType2();
        _relevant_Attack_Surface_Elements = new Relevant_Attack_Surface_ElementsType();
    }
    
    public string Outcome_Description
    {
        get => _outcome_Description;
        set => _outcome_Description = value;
    }
    
    public Relevant_Attack_Surface_ElementsType Relevant_Attack_Surface_Elements
    {
        get => _relevant_Attack_Surface_Elements;
        set => _relevant_Attack_Surface_Elements = value;
    }
    
    public ObservablesType2 Observables
    {
        get => _observables;
        set => _observables = value;
    }
    
    /// <summary>
    /// This field contains a unique integer identifier for the outcome.
    /// </summary>
    [XmlAttribute(DataType="integer")]
    public string ID
    {
        get => _id;
        set => _id = value;
    }
    
    /// <summary>
    /// An outcome has a mandatory type attribute that can be one of the values ???success,??? ???failure,??? or ???inconclusive.??? It indicates what results of executing the attack step techniques should be considered successes, which should be considered failures, and which ones are inconclusive. Outcomes??? successes are determined relative to the attacker???s point of view. It is a success if the attack step got the attacker closer to his goal of attacking the application. It is a failure if the attacker got no closer to his goal.
    /// </summary>
    [XmlAttribute]
    public Custom_Attack_StepTypeOutcomeType type
    {
        get => _type;
        set => _type = value;
    }
}
}
#pragma warning restore
