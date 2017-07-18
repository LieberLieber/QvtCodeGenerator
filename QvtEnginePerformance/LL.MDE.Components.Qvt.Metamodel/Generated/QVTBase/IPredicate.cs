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
using LL.MDE.Components.Qvt.Metamodel.EssentialOCL;
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

namespace LL.MDE.Components.Qvt.Metamodel.QVTBase
{
    
    
    /// <summary>
    /// The public interface for Predicate
    /// </summary>
    [DefaultImplementationTypeAttribute(typeof(Predicate))]
    [XmlDefaultImplementationTypeAttribute(typeof(Predicate))]
    public interface IPredicate : IModelElement, IElement
    {
        
        /// <summary>
        /// The conditionExpression property
        /// </summary>
        IOclExpression ConditionExpression
        {
            get;
            set;
        }
        
        /// <summary>
        /// The pattern property
        /// </summary>
        IPattern Pattern
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets fired before the ConditionExpression property changes its value
        /// </summary>
        event EventHandler ConditionExpressionChanging;
        
        /// <summary>
        /// Gets fired when the ConditionExpression property changed its value
        /// </summary>
        event EventHandler<ValueChangedEventArgs> ConditionExpressionChanged;
        
        /// <summary>
        /// Gets fired when the Pattern property changed its value
        /// </summary>
        event EventHandler<ValueChangedEventArgs> PatternChanged;
    }
}

