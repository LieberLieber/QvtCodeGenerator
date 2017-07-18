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
    /// The default implementation of the RelationImplementation class
    /// </summary>
    [XmlNamespaceAttribute("http://www.omg.org/spec/QVT/20140401/QVTRelation")]
    [XmlNamespacePrefixAttribute("qvtrelation")]
    [ModelRepresentationClassAttribute("http://www.omg.org/spec/QVT/20140401/QVTRelation#//RelationImplementation/")]
    public class RelationImplementation : Element, IRelationImplementation, IModelElement
    {
        
        /// <summary>
        /// The backing field for the Impl property
        /// </summary>
        private IOperation _impl;
        
        /// <summary>
        /// The backing field for the InDirectionOf property
        /// </summary>
        private ITypedModel _inDirectionOf;
        
        private static NMF.Models.Meta.IClass _classInstance;
        
        /// <summary>
        /// The impl property
        /// </summary>
        [XmlElementNameAttribute("impl")]
        [XmlAttributeAttribute(true)]
        public virtual IOperation Impl
        {
            get
            {
                return this._impl;
            }
            set
            {
                if ((this._impl != value))
                {
                    this.OnImplChanging(EventArgs.Empty);
                    this.OnPropertyChanging("Impl");
                    IOperation old = this._impl;
                    this._impl = value;
                    if ((old != null))
                    {
                        old.Deleted -= this.OnResetImpl;
                    }
                    if ((value != null))
                    {
                        value.Deleted += this.OnResetImpl;
                    }
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnImplChanged(e);
                    this.OnPropertyChanged("Impl", e);
                }
            }
        }
        
        /// <summary>
        /// The inDirectionOf property
        /// </summary>
        [XmlElementNameAttribute("inDirectionOf")]
        [XmlAttributeAttribute(true)]
        public virtual ITypedModel InDirectionOf
        {
            get
            {
                return this._inDirectionOf;
            }
            set
            {
                if ((this._inDirectionOf != value))
                {
                    this.OnInDirectionOfChanging(EventArgs.Empty);
                    this.OnPropertyChanging("InDirectionOf");
                    ITypedModel old = this._inDirectionOf;
                    this._inDirectionOf = value;
                    if ((old != null))
                    {
                        old.Deleted -= this.OnResetInDirectionOf;
                    }
                    if ((value != null))
                    {
                        value.Deleted += this.OnResetInDirectionOf;
                    }
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnInDirectionOfChanged(e);
                    this.OnPropertyChanged("InDirectionOf", e);
                }
            }
        }
        
        /// <summary>
        /// The relation property
        /// </summary>
        [XmlElementNameAttribute("relation")]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        [XmlAttributeAttribute(true)]
        [XmlOppositeAttribute("operationalImpl")]
        public virtual IRelation Relation
        {
            get
            {
                return ModelHelper.CastAs<IRelation>(this.Parent);
            }
            set
            {
                this.Parent = value;
            }
        }
        
        /// <summary>
        /// Gets the referenced model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> ReferencedElements
        {
            get
            {
                return base.ReferencedElements.Concat(new RelationImplementationReferencedElementsCollection(this));
            }
        }
        
        /// <summary>
        /// Gets the Class model for this type
        /// </summary>
        public new static NMF.Models.Meta.IClass ClassInstance
        {
            get
            {
                if ((_classInstance == null))
                {
                    _classInstance = ((NMF.Models.Meta.IClass)(MetaRepository.Instance.Resolve("http://www.omg.org/spec/QVT/20140401/QVTRelation#//RelationImplementation/")));
                }
                return _classInstance;
            }
        }
        
        /// <summary>
        /// Gets fired before the Impl property changes its value
        /// </summary>
        public event EventHandler ImplChanging;
        
        /// <summary>
        /// Gets fired when the Impl property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> ImplChanged;
        
        /// <summary>
        /// Gets fired before the InDirectionOf property changes its value
        /// </summary>
        public event EventHandler InDirectionOfChanging;
        
        /// <summary>
        /// Gets fired when the InDirectionOf property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> InDirectionOfChanged;
        
        /// <summary>
        /// Gets fired when the Relation property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> RelationChanged;
        
        /// <summary>
        /// Raises the ImplChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnImplChanging(EventArgs eventArgs)
        {
            EventHandler handler = this.ImplChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the ImplChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnImplChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.ImplChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Handles the event that the Impl property must reset
        /// </summary>
        /// <param name="sender">The object that sent this reset request</param>
        /// <param name="eventArgs">The event data for the reset event</param>
        private void OnResetImpl(object sender, System.EventArgs eventArgs)
        {
            this.Impl = null;
        }
        
        /// <summary>
        /// Raises the InDirectionOfChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnInDirectionOfChanging(EventArgs eventArgs)
        {
            EventHandler handler = this.InDirectionOfChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the InDirectionOfChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnInDirectionOfChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.InDirectionOfChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Handles the event that the InDirectionOf property must reset
        /// </summary>
        /// <param name="sender">The object that sent this reset request</param>
        /// <param name="eventArgs">The event data for the reset event</param>
        private void OnResetInDirectionOf(object sender, System.EventArgs eventArgs)
        {
            this.InDirectionOf = null;
        }
        
        /// <summary>
        /// Raises the RelationChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnRelationChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.RelationChanged;
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
            IRelation oldRelation = ModelHelper.CastAs<IRelation>(oldParent);
            IRelation newRelation = ModelHelper.CastAs<IRelation>(newParent);
            if ((oldRelation != null))
            {
                oldRelation.OperationalImpl.Remove(this);
            }
            if ((newRelation != null))
            {
                newRelation.OperationalImpl.Add(this);
            }
            ValueChangedEventArgs e = new ValueChangedEventArgs(oldRelation, newRelation);
            this.OnRelationChanged(e);
            this.OnPropertyChanged("Relation", e);
        }
        
        /// <summary>
        /// Sets a value to the given feature
        /// </summary>
        /// <param name="feature">The requested feature</param>
        /// <param name="value">The value that should be set to that feature</param>
        protected override void SetFeature(string feature, object value)
        {
            if ((feature == "IMPL"))
            {
                this.Impl = ((IOperation)(value));
                return;
            }
            if ((feature == "INDIRECTIONOF"))
            {
                this.InDirectionOf = ((ITypedModel)(value));
                return;
            }
            if ((feature == "RELATION"))
            {
                this.Relation = ((IRelation)(value));
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
            if ((attribute == "IMPL"))
            {
                return new ImplProxy(this);
            }
            if ((attribute == "INDIRECTIONOF"))
            {
                return new InDirectionOfProxy(this);
            }
            if ((attribute == "RELATION"))
            {
                return new RelationProxy(this);
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
            if ((reference == "IMPL"))
            {
                return new ImplProxy(this);
            }
            if ((reference == "INDIRECTIONOF"))
            {
                return new InDirectionOfProxy(this);
            }
            if ((reference == "RELATION"))
            {
                return new RelationProxy(this);
            }
            return base.GetExpressionForReference(reference);
        }
        
        /// <summary>
        /// Gets the Class for this model element
        /// </summary>
        public override NMF.Models.Meta.IClass GetClass()
        {
            if ((_classInstance == null))
            {
                _classInstance = ((NMF.Models.Meta.IClass)(MetaRepository.Instance.Resolve("http://www.omg.org/spec/QVT/20140401/QVTRelation#//RelationImplementation/")));
            }
            return _classInstance;
        }
        
        /// <summary>
        /// The collection class to to represent the children of the RelationImplementation class
        /// </summary>
        public class RelationImplementationReferencedElementsCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private RelationImplementation _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public RelationImplementationReferencedElementsCollection(RelationImplementation parent)
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
                    if ((this._parent.Impl != null))
                    {
                        count = (count + 1);
                    }
                    if ((this._parent.InDirectionOf != null))
                    {
                        count = (count + 1);
                    }
                    if ((this._parent.Relation != null))
                    {
                        count = (count + 1);
                    }
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.ImplChanged += this.PropagateValueChanges;
                this._parent.InDirectionOfChanged += this.PropagateValueChanges;
                this._parent.RelationChanged += this.PropagateValueChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.ImplChanged -= this.PropagateValueChanges;
                this._parent.InDirectionOfChanged -= this.PropagateValueChanges;
                this._parent.RelationChanged -= this.PropagateValueChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                if ((this._parent.Impl == null))
                {
                    IOperation implCasted = item.As<IOperation>();
                    if ((implCasted != null))
                    {
                        this._parent.Impl = implCasted;
                        return;
                    }
                }
                if ((this._parent.InDirectionOf == null))
                {
                    ITypedModel inDirectionOfCasted = item.As<ITypedModel>();
                    if ((inDirectionOfCasted != null))
                    {
                        this._parent.InDirectionOf = inDirectionOfCasted;
                        return;
                    }
                }
                if ((this._parent.Relation == null))
                {
                    IRelation relationCasted = item.As<IRelation>();
                    if ((relationCasted != null))
                    {
                        this._parent.Relation = relationCasted;
                        return;
                    }
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.Impl = null;
                this._parent.InDirectionOf = null;
                this._parent.Relation = null;
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if ((item == this._parent.Impl))
                {
                    return true;
                }
                if ((item == this._parent.InDirectionOf))
                {
                    return true;
                }
                if ((item == this._parent.Relation))
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
                if ((this._parent.Impl != null))
                {
                    array[arrayIndex] = this._parent.Impl;
                    arrayIndex = (arrayIndex + 1);
                }
                if ((this._parent.InDirectionOf != null))
                {
                    array[arrayIndex] = this._parent.InDirectionOf;
                    arrayIndex = (arrayIndex + 1);
                }
                if ((this._parent.Relation != null))
                {
                    array[arrayIndex] = this._parent.Relation;
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
                if ((this._parent.Impl == item))
                {
                    this._parent.Impl = null;
                    return true;
                }
                if ((this._parent.InDirectionOf == item))
                {
                    this._parent.InDirectionOf = null;
                    return true;
                }
                if ((this._parent.Relation == item))
                {
                    this._parent.Relation = null;
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.Impl).Concat(this._parent.InDirectionOf).Concat(this._parent.Relation).GetEnumerator();
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the impl property
        /// </summary>
        private sealed class ImplProxy : ModelPropertyChange<IRelationImplementation, IOperation>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public ImplProxy(IRelationImplementation modelElement) : 
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
                    return this.ModelElement.Impl;
                }
                set
                {
                    this.ModelElement.Impl = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.ImplChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.ImplChanged -= handler;
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the inDirectionOf property
        /// </summary>
        private sealed class InDirectionOfProxy : ModelPropertyChange<IRelationImplementation, ITypedModel>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public InDirectionOfProxy(IRelationImplementation modelElement) : 
                    base(modelElement)
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override ITypedModel Value
            {
                get
                {
                    return this.ModelElement.InDirectionOf;
                }
                set
                {
                    this.ModelElement.InDirectionOf = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.InDirectionOfChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.InDirectionOfChanged -= handler;
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the relation property
        /// </summary>
        private sealed class RelationProxy : ModelPropertyChange<IRelationImplementation, IRelation>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public RelationProxy(IRelationImplementation modelElement) : 
                    base(modelElement)
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override IRelation Value
            {
                get
                {
                    return this.ModelElement.Relation;
                }
                set
                {
                    this.ModelElement.Relation = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.RelationChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.RelationChanged -= handler;
            }
        }
    }
}

