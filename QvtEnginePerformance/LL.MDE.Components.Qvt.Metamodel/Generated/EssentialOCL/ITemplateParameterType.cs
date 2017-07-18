//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using LL.MDE.Components.Qvt.Metamodel.EMOF;
using NMF.Collections.Generic;
using NMF.Collections.ObjectModel;
using NMF.Expressions;
using NMF.Expressions.Linq;
using NMF.Models;
using NMF.Models.Collections;
using NMF.Models.Expressions;
using NMF.Serialization;
using NMF.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace LL.MDE.Components.Qvt.Metamodel.EssentialOCL
{
    
    
    /// <summary>
    /// The public interface for TemplateParameterType
    /// </summary>
    [DefaultImplementationTypeAttribute(typeof(TemplateParameterType))]
    [XmlDefaultImplementationTypeAttribute(typeof(TemplateParameterType))]
    public interface ITemplateParameterType : IModelElement, IType
    {
        
        /// <summary>
        /// The specification property
        /// </summary>
        string Specification
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets fired before the Specification property changes its value
        /// </summary>
        event EventHandler SpecificationChanging;
        
        /// <summary>
        /// Gets fired when the Specification property changed its value
        /// </summary>
        event EventHandler<ValueChangedEventArgs> SpecificationChanged;
    }
}
