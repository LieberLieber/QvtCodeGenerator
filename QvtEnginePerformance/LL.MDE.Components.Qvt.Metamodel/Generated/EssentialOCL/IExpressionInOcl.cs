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
    /// The public interface for ExpressionInOcl
    /// </summary>
    [DefaultImplementationTypeAttribute(typeof(ExpressionInOcl))]
    [XmlDefaultImplementationTypeAttribute(typeof(ExpressionInOcl))]
    public interface IExpressionInOcl : IModelElement, ITypedElement
    {
        
        /// <summary>
        /// The bodyExpression property
        /// </summary>
        IOclExpression BodyExpression
        {
            get;
            set;
        }
        
        /// <summary>
        /// The contextVariable property
        /// </summary>
        IVariable ContextVariable
        {
            get;
            set;
        }
        
        /// <summary>
        /// The generatedType property
        /// </summary>
        ISetExpression<IType> GeneratedType
        {
            get;
        }
        
        /// <summary>
        /// The parameterVariable property
        /// </summary>
        ISetExpression<IVariable> ParameterVariable
        {
            get;
        }
        
        /// <summary>
        /// The resultVariable property
        /// </summary>
        IVariable ResultVariable
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets fired before the BodyExpression property changes its value
        /// </summary>
        event EventHandler BodyExpressionChanging;
        
        /// <summary>
        /// Gets fired when the BodyExpression property changed its value
        /// </summary>
        event EventHandler<ValueChangedEventArgs> BodyExpressionChanged;
        
        /// <summary>
        /// Gets fired before the ContextVariable property changes its value
        /// </summary>
        event EventHandler ContextVariableChanging;
        
        /// <summary>
        /// Gets fired when the ContextVariable property changed its value
        /// </summary>
        event EventHandler<ValueChangedEventArgs> ContextVariableChanged;
        
        /// <summary>
        /// Gets fired before the ResultVariable property changes its value
        /// </summary>
        event EventHandler ResultVariableChanging;
        
        /// <summary>
        /// Gets fired when the ResultVariable property changed its value
        /// </summary>
        event EventHandler<ValueChangedEventArgs> ResultVariableChanged;
    }
}

