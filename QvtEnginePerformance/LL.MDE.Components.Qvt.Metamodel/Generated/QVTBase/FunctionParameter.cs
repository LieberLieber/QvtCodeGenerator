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
    /// The default implementation of the FunctionParameter class
    /// </summary>
    [XmlNamespaceAttribute("http://www.omg.org/spec/QVT/20140401/QVTBase")]
    [XmlNamespacePrefixAttribute("qvtbase")]
    [DebuggerDisplayAttribute("FunctionParameter {Name}")]
    public class FunctionParameter : Variable, IFunctionParameter, IModelElement
    {
        
        /// <summary>
        /// The backing field for the IsOrdered property
        /// </summary>
        private Nullable<bool> _isOrdered = false;
        
        /// <summary>
        /// The backing field for the IsUnique property
        /// </summary>
        private Nullable<bool> _isUnique = true;
        
        /// <summary>
        /// The backing field for the Lower property
        /// </summary>
        private Nullable<int> _lower = 1;
        
        /// <summary>
        /// The backing field for the Upper property
        /// </summary>
        private Nullable<int> _upper = 1;
        
        private static NMF.Models.Meta.IClass _classInstance;
        
        /// <summary>
        /// The operation property
        /// </summary>
        [XmlElementNameAttribute("operation")]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        [XmlAttributeAttribute(true)]
        [XmlOppositeAttribute("ownedParameter")]
        public virtual IOperation Operation
        {
            get
            {
                return ModelHelper.CastAs<IOperation>(this.Parent);
            }
            set
            {
                this.Parent = value;
            }
        }
        
        /// <summary>
        /// The isOrdered property
        /// </summary>
        [DefaultValueAttribute(false)]
        [XmlElementNameAttribute("isOrdered")]
        [XmlAttributeAttribute(true)]
        public virtual Nullable<bool> IsOrdered
        {
            get
            {
                return this._isOrdered;
            }
            set
            {
                if ((this._isOrdered != value))
                {
                    this.OnIsOrderedChanging(EventArgs.Empty);
                    this.OnPropertyChanging("IsOrdered");
                    Nullable<bool> old = this._isOrdered;
                    this._isOrdered = value;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnIsOrderedChanged(e);
                    this.OnPropertyChanged("IsOrdered", e);
                }
            }
        }
        
        /// <summary>
        /// The isUnique property
        /// </summary>
        [DefaultValueAttribute(true)]
        [XmlElementNameAttribute("isUnique")]
        [XmlAttributeAttribute(true)]
        public virtual Nullable<bool> IsUnique
        {
            get
            {
                return this._isUnique;
            }
            set
            {
                if ((this._isUnique != value))
                {
                    this.OnIsUniqueChanging(EventArgs.Empty);
                    this.OnPropertyChanging("IsUnique");
                    Nullable<bool> old = this._isUnique;
                    this._isUnique = value;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnIsUniqueChanged(e);
                    this.OnPropertyChanged("IsUnique", e);
                }
            }
        }
        
        /// <summary>
        /// The lower property
        /// </summary>
        [DefaultValueAttribute(1)]
        [XmlElementNameAttribute("lower")]
        [XmlAttributeAttribute(true)]
        public virtual Nullable<int> Lower
        {
            get
            {
                return this._lower;
            }
            set
            {
                if ((this._lower != value))
                {
                    this.OnLowerChanging(EventArgs.Empty);
                    this.OnPropertyChanging("Lower");
                    Nullable<int> old = this._lower;
                    this._lower = value;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnLowerChanged(e);
                    this.OnPropertyChanged("Lower", e);
                }
            }
        }
        
        /// <summary>
        /// The upper property
        /// </summary>
        [DefaultValueAttribute(1)]
        [XmlElementNameAttribute("upper")]
        [XmlAttributeAttribute(true)]
        public virtual Nullable<int> Upper
        {
            get
            {
                return this._upper;
            }
            set
            {
                if ((this._upper != value))
                {
                    this.OnUpperChanging(EventArgs.Empty);
                    this.OnPropertyChanging("Upper");
                    Nullable<int> old = this._upper;
                    this._upper = value;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnUpperChanged(e);
                    this.OnPropertyChanged("Upper", e);
                }
            }
        }
        
        /// <summary>
        /// Gets fired when the Operation property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> OperationChanged;
        
        /// <summary>
        /// Gets fired before the IsOrdered property changes its value
        /// </summary>
        public event EventHandler IsOrderedChanging;
        
        /// <summary>
        /// Gets fired when the IsOrdered property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> IsOrderedChanged;
        
        /// <summary>
        /// Gets fired before the IsUnique property changes its value
        /// </summary>
        public event EventHandler IsUniqueChanging;
        
        /// <summary>
        /// Gets fired when the IsUnique property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> IsUniqueChanged;
        
        /// <summary>
        /// Gets fired before the Lower property changes its value
        /// </summary>
        public event EventHandler LowerChanging;
        
        /// <summary>
        /// Gets fired when the Lower property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> LowerChanged;
        
        /// <summary>
        /// Gets fired before the Upper property changes its value
        /// </summary>
        public event EventHandler UpperChanging;
        
        /// <summary>
        /// Gets fired when the Upper property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> UpperChanged;
        
        /// <summary>
        /// Raises the OperationChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnOperationChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.OperationChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Gets called when the parent model element of the current model element changes
        /// </summary>
        /// <param name="oldParent">The old parent model element</param>
        /// <param name="newParent">The new parent model element</param>
        protected override void OnParentChanged(IModelElement newParent, IModelElement oldParent)
        {
            IOperation oldOperation = ModelHelper.CastAs<IOperation>(oldParent);
            IOperation newOperation = ModelHelper.CastAs<IOperation>(newParent);
            if ((oldOperation != null))
            {
                oldOperation.OwnedParameter.Remove(this);
            }
            if ((newOperation != null))
            {
                newOperation.OwnedParameter.Add(this);
            }
            ValueChangedEventArgs e = new ValueChangedEventArgs(oldOperation, newOperation);
            this.OnOperationChanged(e);
            this.OnPropertyChanged("Operation", e);
        }
        
        /// <summary>
        /// Raises the IsOrderedChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnIsOrderedChanging(EventArgs eventArgs)
        {
            EventHandler handler = this.IsOrderedChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the IsOrderedChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnIsOrderedChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.IsOrderedChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the IsUniqueChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnIsUniqueChanging(EventArgs eventArgs)
        {
            EventHandler handler = this.IsUniqueChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the IsUniqueChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnIsUniqueChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.IsUniqueChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the LowerChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnLowerChanging(EventArgs eventArgs)
        {
            EventHandler handler = this.LowerChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the LowerChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnLowerChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.LowerChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the UpperChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnUpperChanging(EventArgs eventArgs)
        {
            EventHandler handler = this.UpperChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the UpperChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnUpperChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.UpperChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Resolves the given attribute name
        /// </summary>
        /// <returns>The attribute value or null if it could not be found</returns>
        /// <param name="attribute">The requested attribute name</param>
        /// <param name="index">The index of this attribute</param>
        protected override object GetAttributeValue(string attribute, int index)
        {
            if ((attribute == "ISORDERED"))
            {
                return this.IsOrdered;
            }
            if ((attribute == "ISUNIQUE"))
            {
                return this.IsUnique;
            }
            if ((attribute == "LOWER"))
            {
                return this.Lower;
            }
            if ((attribute == "UPPER"))
            {
                return this.Upper;
            }
            return base.GetAttributeValue(attribute, index);
        }
        
        /// <summary>
        /// Sets a value to the given feature
        /// </summary>
        /// <param name="feature">The requested feature</param>
        /// <param name="value">The value that should be set to that feature</param>
        protected override void SetFeature(string feature, object value)
        {
            if ((feature == "OPERATION"))
            {
                this.Operation = ((IOperation)(value));
                return;
            }
            if ((feature == "ISORDERED"))
            {
                this.IsOrdered = ((bool)(value));
                return;
            }
            if ((feature == "ISUNIQUE"))
            {
                this.IsUnique = ((bool)(value));
                return;
            }
            if ((feature == "LOWER"))
            {
                this.Lower = ((int)(value));
                return;
            }
            if ((feature == "UPPER"))
            {
                this.Upper = ((int)(value));
                return;
            }
            base.SetFeature(feature, value);
        }
        
        /// <summary>
        /// Gets the property expression for the given attribute
        /// </summary>
        /// <returns>An incremental property expression</returns>
        /// <param name="attribute">The requested attribute in upper case</param>
        protected override NMF.Expressions.INotifyExpression<object> GetExpressionForAttribute(string attribute)
        {
            if ((attribute == "OPERATION"))
            {
                return new OperationProxy(this);
            }
            return base.GetExpressionForAttribute(attribute);
        }
        
        /// <summary>
        /// Gets the property expression for the given reference
        /// </summary>
        /// <returns>An incremental property expression</returns>
        /// <param name="reference">The requested reference in upper case</param>
        protected override NMF.Expressions.INotifyExpression<NMF.Models.IModelElement> GetExpressionForReference(string reference)
        {
            if ((reference == "OPERATION"))
            {
                return new OperationProxy(this);
            }
            return base.GetExpressionForReference(reference);
        }
        
        /// <summary>
        /// Gets the Class for this model element
        /// </summary>
        public override NMF.Models.Meta.IClass GetClass()
        {
            throw new NotSupportedException();
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the operation property
        /// </summary>
        private sealed class OperationProxy : ModelPropertyChange<IParameter, IOperation>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public OperationProxy(IParameter modelElement) : 
                    base(modelElement)
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override IOperation Value
            {
                get
                {
                    return this.ModelElement.Operation;
                }
                set
                {
                    this.ModelElement.Operation = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.OperationChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.OperationChanged -= handler;
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the isOrdered property
        /// </summary>
        private sealed class IsOrderedProxy : ModelPropertyChange<IMultiplicityElement, Nullable<bool>>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public IsOrderedProxy(IMultiplicityElement modelElement) : 
                    base(modelElement)
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override Nullable<bool> Value
            {
                get
                {
                    return this.ModelElement.IsOrdered;
                }
                set
                {
                    this.ModelElement.IsOrdered = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.IsOrderedChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.IsOrderedChanged -= handler;
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the isUnique property
        /// </summary>
        private sealed class IsUniqueProxy : ModelPropertyChange<IMultiplicityElement, Nullable<bool>>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public IsUniqueProxy(IMultiplicityElement modelElement) : 
                    base(modelElement)
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override Nullable<bool> Value
            {
                get
                {
                    return this.ModelElement.IsUnique;
                }
                set
                {
                    this.ModelElement.IsUnique = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.IsUniqueChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.IsUniqueChanged -= handler;
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the lower property
        /// </summary>
        private sealed class LowerProxy : ModelPropertyChange<IMultiplicityElement, Nullable<int>>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public LowerProxy(IMultiplicityElement modelElement) : 
                    base(modelElement)
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override Nullable<int> Value
            {
                get
                {
                    return this.ModelElement.Lower;
                }
                set
                {
                    this.ModelElement.Lower = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.LowerChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.LowerChanged -= handler;
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the upper property
        /// </summary>
        private sealed class UpperProxy : ModelPropertyChange<IMultiplicityElement, Nullable<int>>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public UpperProxy(IMultiplicityElement modelElement) : 
                    base(modelElement)
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override Nullable<int> Value
            {
                get
                {
                    return this.ModelElement.Upper;
                }
                set
                {
                    this.ModelElement.Upper = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.UpperChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.UpperChanged -= handler;
            }
        }
    }
}

