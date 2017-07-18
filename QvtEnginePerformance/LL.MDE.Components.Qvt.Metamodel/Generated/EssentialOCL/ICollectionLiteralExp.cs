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
    /// The public interface for CollectionLiteralExp
    /// </summary>
    [DefaultImplementationTypeAttribute(typeof(CollectionLiteralExp))]
    [XmlDefaultImplementationTypeAttribute(typeof(CollectionLiteralExp))]
    public interface ICollectionLiteralExp : IModelElement, ILiteralExp
    {
        
        /// <summary>
        /// The kind property
        /// </summary>
        Nullable<CollectionKind> Kind
        {
            get;
            set;
        }
        
        /// <summary>
        /// The part property
        /// </summary>
        ISetExpression<ICollectionLiteralPart> Part
        {
            get;
        }
        
        /// <summary>
        /// Gets fired before the Kind property changes its value
        /// </summary>
        event EventHandler KindChanging;
        
        /// <summary>
        /// Gets fired when the Kind property changed its value
        /// </summary>
        event EventHandler<ValueChangedEventArgs> KindChanged;
    }
}

