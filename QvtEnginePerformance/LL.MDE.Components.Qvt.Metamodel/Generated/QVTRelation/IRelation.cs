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
using LL.MDE.Components.Qvt.Metamodel.QVTBase;
using LL.MDE.Components.Qvt.Metamodel.QVTTemplate;
using NMF.Collections.Generic;
using NMF.Collections.ObjectModel;
using NMF.Expressions;
using NMF.Expressions.Linq;
using NMF.Models;
using NMF.Models.Collections;
using NMF.Models.Expressions;
using NMF.Models.Repository;
using NMF.Serialization;
using NMF.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace LL.MDE.Components.Qvt.Metamodel.QVTRelation
{
    
    
    /// <summary>
    /// The public interface for Relation
    /// </summary>
    [DefaultImplementationTypeAttribute(typeof(Relation))]
    [XmlDefaultImplementationTypeAttribute(typeof(Relation))]
    public interface IRelation : IModelElement, IRule
    {
        
        /// <summary>
        /// The isTopLevel property
        /// </summary>
        Nullable<bool> IsTopLevel
        {
            get;
            set;
        }
        
        /// <summary>
        /// The operationalImpl property
        /// </summary>
        ISetExpression<IRelationImplementation> OperationalImpl
        {
            get;
        }
        
        /// <summary>
        /// The variable property
        /// </summary>
        ISetExpression<IVariable> Variable
        {
            get;
        }
        
        /// <summary>
        /// The when property
        /// </summary>
        IPattern When
        {
            get;
            set;
        }
        
        /// <summary>
        /// The where property
        /// </summary>
        IPattern Where
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets fired before the IsTopLevel property changes its value
        /// </summary>
        event EventHandler IsTopLevelChanging;
        
        /// <summary>
        /// Gets fired when the IsTopLevel property changed its value
        /// </summary>
        event EventHandler<ValueChangedEventArgs> IsTopLevelChanged;
        
        /// <summary>
        /// Gets fired before the When property changes its value
        /// </summary>
        event EventHandler WhenChanging;
        
        /// <summary>
        /// Gets fired when the When property changed its value
        /// </summary>
        event EventHandler<ValueChangedEventArgs> WhenChanged;
        
        /// <summary>
        /// Gets fired before the Where property changes its value
        /// </summary>
        event EventHandler WhereChanging;
        
        /// <summary>
        /// Gets fired when the Where property changed its value
        /// </summary>
        event EventHandler<ValueChangedEventArgs> WhereChanged;
    }
}

