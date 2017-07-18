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

namespace LL.MDE.Components.Qvt.Metamodel.QVTTemplate
{
    
    
    /// <summary>
    /// The default implementation of the TemplateExp class
    /// </summary>
    [XmlNamespaceAttribute("http://www.omg.org/spec/QVT/20140401/QVTTemplate")]
    [XmlNamespacePrefixAttribute("qvttemplate")]
    [DebuggerDisplayAttribute("TemplateExp {Name}")]
    public abstract class TemplateExp : LiteralExp, ITemplateExp, IModelElement
    {
        
        /// <summary>
        /// The backing field for the BindsTo property
        /// </summary>
        private IVariable _bindsTo;
        
        /// <summary>
        /// The backing field for the Where property
        /// </summary>
        private IOclExpression _where;
        
        private static NMF.Models.Meta.IClass _classInstance;
        
        /// <summary>
        /// The bindsTo property
        /// </summary>
        [XmlElementNameAttribute("bindsTo")]
        [XmlAttributeAttribute(true)]
        public virtual IVariable BindsTo
        {
            get
            {
                return this._bindsTo;
            }
            set
            {
                if ((this._bindsTo != value))
                {
                    this.OnBindsToChanging(EventArgs.Empty);
                    this.OnPropertyChanging("BindsTo");
                    IVariable old = this._bindsTo;
                    this._bindsTo = value;
                    if ((old != null))
                    {
                        old.Deleted -= this.OnResetBindsTo;
                    }
                    if ((value != null))
                    {
                        value.Deleted += this.OnResetBindsTo;
                    }
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnBindsToChanged(e);
                    this.OnPropertyChanged("BindsTo", e);
                }
            }
        }
        
        /// <summary>
        /// The where property
        /// </summary>
        [XmlElementNameAttribute("where")]
        [XmlAttributeAttribute(false)]
        [ContainmentAttribute()]
        public virtual IOclExpression Where
        {
            get
            {
                return this._where;
            }
            set
            {
                if ((this._where != value))
                {
                    this.OnWhereChanging(EventArgs.Empty);
                    this.OnPropertyChanging("Where");
                    IOclExpression old = this._where;
                    this._where = value;
                    if ((old != null))
                    {
                        old.Parent = null;
                        old.Deleted -= this.OnResetWhere;
                    }
                    if ((value != null))
                    {
                        value.Parent = this;
                        value.Deleted += this.OnResetWhere;
                    }
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnWhereChanged(e);
                    this.OnPropertyChanged("Where", e);
                }
            }
        }
        
        /// <summary>
        /// Gets the child model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> Children
        {
            get
            {
                return base.Children.Concat(new TemplateExpChildrenCollection(this));
            }
        }
        
        /// <summary>
        /// Gets the referenced model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> ReferencedElements
        {
            get
            {
                return base.ReferencedElements.Concat(new TemplateExpReferencedElementsCollection(this));
            }
        }
        
        /// <summary>
        /// Gets fired before the BindsTo property changes its value
        /// </summary>
        public event EventHandler BindsToChanging;
        
        /// <summary>
        /// Gets fired when the BindsTo property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> BindsToChanged;
        
        /// <summary>
        /// Gets fired before the Where property changes its value
        /// </summary>
        public event EventHandler WhereChanging;
        
        /// <summary>
        /// Gets fired when the Where property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> WhereChanged;
        
        /// <summary>
        /// Raises the BindsToChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnBindsToChanging(EventArgs eventArgs)
        {
            EventHandler handler = this.BindsToChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the BindsToChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnBindsToChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.BindsToChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Handles the event that the BindsTo property must reset
        /// </summary>
        /// <param name="sender">The object that sent this reset request</param>
        /// <param name="eventArgs">The event data for the reset event</param>
        private void OnResetBindsTo(object sender, System.EventArgs eventArgs)
        {
            this.BindsTo = null;
        }
        
        /// <summary>
        /// Raises the WhereChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnWhereChanging(EventArgs eventArgs)
        {
            EventHandler handler = this.WhereChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the WhereChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnWhereChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.WhereChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Handles the event that the Where property must reset
        /// </summary>
        /// <param name="sender">The object that sent this reset request</param>
        /// <param name="eventArgs">The event data for the reset event</param>
        private void OnResetWhere(object sender, System.EventArgs eventArgs)
        {
            this.Where = null;
        }
        
        /// <summary>
        /// Gets the relative URI fragment for the given child model element
        /// </summary>
        /// <returns>A fragment of the relative URI</returns>
        /// <param name="element">The element that should be looked for</param>
        protected override string GetRelativePathForNonIdentifiedChild(IModelElement element)
        {
            if ((element == this.Where))
            {
                return ModelHelper.CreatePath("Where");
            }
            return base.GetRelativePathForNonIdentifiedChild(element);
        }
        
        /// <summary>
        /// Resolves the given URI to a child model element
        /// </summary>
        /// <returns>The model element or null if it could not be found</returns>
        /// <param name="reference">The requested reference name</param>
        /// <param name="index">The index of this reference</param>
        protected override IModelElement GetModelElementForReference(string reference, int index)
        {
            if ((reference == "WHERE"))
            {
                return this.Where;
            }
            return base.GetModelElementForReference(reference, index);
        }
        
        /// <summary>
        /// Sets a value to the given feature
        /// </summary>
        /// <param name="feature">The requested feature</param>
        /// <param name="value">The value that should be set to that feature</param>
        protected override void SetFeature(string feature, object value)
        {
            if ((feature == "BINDSTO"))
            {
                this.BindsTo = ((IVariable)(value));
                return;
            }
            if ((feature == "WHERE"))
            {
                this.Where = ((IOclExpression)(value));
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
            if ((attribute == "BINDSTO"))
            {
                return new BindsToProxy(this);
            }
            if ((attribute == "WHERE"))
            {
                return new WhereProxy(this);
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
            if ((reference == "BINDSTO"))
            {
                return new BindsToProxy(this);
            }
            if ((reference == "WHERE"))
            {
                return new WhereProxy(this);
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
        /// The collection class to to represent the children of the TemplateExp class
        /// </summary>
        public class TemplateExpChildrenCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private TemplateExp _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public TemplateExpChildrenCollection(TemplateExp parent)
            {
                this._parent = parent;
            }
            
            /// <summary>
            /// Gets the amount of elements contained in this collection
            /// </summary>
            public override int Count
            {
                get
                {
                    int count = 0;
                    if ((this._parent.Where != null))
                    {
                        count = (count + 1);
                    }
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.WhereChanged += this.PropagateValueChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.WhereChanged -= this.PropagateValueChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                if ((this._parent.Where == null))
                {
                    IOclExpression whereCasted = item.As<IOclExpression>();
                    if ((whereCasted != null))
                    {
                        this._parent.Where = whereCasted;
                        return;
                    }
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.Where = null;
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if ((item == this._parent.Where))
                {
                    return true;
                }
                return false;
            }
            
            /// <summary>
            /// Copies the contents of the collection to the given array starting from the given array index
            /// </summary>
            /// <param name="array">The array in which the elements should be copied</param>
            /// <param name="arrayIndex">The starting index</param>
            public override void CopyTo(IModelElement[] array, int arrayIndex)
            {
                if ((this._parent.Where != null))
                {
                    array[arrayIndex] = this._parent.Where;
                    arrayIndex = (arrayIndex + 1);
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                if ((this._parent.Where == item))
                {
                    this._parent.Where = null;
                    return true;
                }
                return false;
            }
            
            /// <summary>
            /// Gets an enumerator that enumerates the collection
            /// </summary>
            /// <returns>A generic enumerator</returns>
            public override IEnumerator<IModelElement> GetEnumerator()
            {
                return Enumerable.Empty<IModelElement>().Concat(this._parent.Where).GetEnumerator();
            }
        }
        
        /// <summary>
        /// The collection class to to represent the children of the TemplateExp class
        /// </summary>
        public class TemplateExpReferencedElementsCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private TemplateExp _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public TemplateExpReferencedElementsCollection(TemplateExp parent)
            {
                this._parent = parent;
            }
            
            /// <summary>
            /// Gets the amount of elements contained in this collection
            /// </summary>
            public override int Count
            {
                get
                {
                    int count = 0;
                    if ((this._parent.BindsTo != null))
                    {
                        count = (count + 1);
                    }
                    if ((this._parent.Where != null))
                    {
                        count = (count + 1);
                    }
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.BindsToChanged += this.PropagateValueChanges;
                this._parent.WhereChanged += this.PropagateValueChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.BindsToChanged -= this.PropagateValueChanges;
                this._parent.WhereChanged -= this.PropagateValueChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                if ((this._parent.BindsTo == null))
                {
                    IVariable bindsToCasted = item.As<IVariable>();
                    if ((bindsToCasted != null))
                    {
                        this._parent.BindsTo = bindsToCasted;
                        return;
                    }
                }
                if ((this._parent.Where == null))
                {
                    IOclExpression whereCasted = item.As<IOclExpression>();
                    if ((whereCasted != null))
                    {
                        this._parent.Where = whereCasted;
                        return;
                    }
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.BindsTo = null;
                this._parent.Where = null;
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if ((item == this._parent.BindsTo))
                {
                    return true;
                }
                if ((item == this._parent.Where))
                {
                    return true;
                }
                return false;
            }
            
            /// <summary>
            /// Copies the contents of the collection to the given array starting from the given array index
            /// </summary>
            /// <param name="array">The array in which the elements should be copied</param>
            /// <param name="arrayIndex">The starting index</param>
            public override void CopyTo(IModelElement[] array, int arrayIndex)
            {
                if ((this._parent.BindsTo != null))
                {
                    array[arrayIndex] = this._parent.BindsTo;
                    arrayIndex = (arrayIndex + 1);
                }
                if ((this._parent.Where != null))
                {
                    array[arrayIndex] = this._parent.Where;
                    arrayIndex = (arrayIndex + 1);
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                if ((this._parent.BindsTo == item))
                {
                    this._parent.BindsTo = null;
                    return true;
                }
                if ((this._parent.Where == item))
                {
                    this._parent.Where = null;
                    return true;
                }
                return false;
            }
            
            /// <summary>
            /// Gets an enumerator that enumerates the collection
            /// </summary>
            /// <returns>A generic enumerator</returns>
            public override IEnumerator<IModelElement> GetEnumerator()
            {
                return Enumerable.Empty<IModelElement>().Concat(this._parent.BindsTo).Concat(this._parent.Where).GetEnumerator();
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the bindsTo property
        /// </summary>
        private sealed class BindsToProxy : ModelPropertyChange<ITemplateExp, IVariable>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public BindsToProxy(ITemplateExp modelElement) : 
                    base(modelElement)
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override IVariable Value
            {
                get
                {
                    return this.ModelElement.BindsTo;
                }
                set
                {
                    this.ModelElement.BindsTo = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.BindsToChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.BindsToChanged -= handler;
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the where property
        /// </summary>
        private sealed class WhereProxy : ModelPropertyChange<ITemplateExp, IOclExpression>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public WhereProxy(ITemplateExp modelElement) : 
                    base(modelElement)
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override IOclExpression Value
            {
                get
                {
                    return this.ModelElement.Where;
                }
                set
                {
                    this.ModelElement.Where = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.WhereChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.WhereChanged -= handler;
            }
        }
    }
}
