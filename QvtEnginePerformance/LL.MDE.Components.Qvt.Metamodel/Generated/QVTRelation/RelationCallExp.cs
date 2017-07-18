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
    /// The default implementation of the RelationCallExp class
    /// </summary>
    [XmlNamespaceAttribute("http://www.omg.org/spec/QVT/20140401/QVTRelation")]
    [XmlNamespacePrefixAttribute("qvtrelation")]
    [ModelRepresentationClassAttribute("http://www.omg.org/spec/QVT/20140401/QVTRelation#//RelationCallExp/")]
    [DebuggerDisplayAttribute("RelationCallExp {Name}")]
    public class RelationCallExp : OclExpression, IRelationCallExp, IModelElement
    {
        
        /// <summary>
        /// The backing field for the Argument property
        /// </summary>
        private ObservableCompositionOrderedSet<IOclExpression> _argument;
        
        /// <summary>
        /// The backing field for the ReferredRelation property
        /// </summary>
        private IRelation _referredRelation;
        
        private static NMF.Models.Meta.IClass _classInstance;
        
        public RelationCallExp()
        {
            this._argument = new ObservableCompositionOrderedSet<IOclExpression>(this);
            this._argument.CollectionChanging += this.ArgumentCollectionChanging;
            this._argument.CollectionChanged += this.ArgumentCollectionChanged;
        }
        
        /// <summary>
        /// The argument property
        /// </summary>
        [LowerBoundAttribute(2)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [XmlElementNameAttribute("argument")]
        [XmlAttributeAttribute(false)]
        [ContainmentAttribute()]
        [ConstantAttribute()]
        public virtual IOrderedSetExpression<IOclExpression> Argument
        {
            get
            {
                return this._argument;
            }
        }
        
        /// <summary>
        /// The referredRelation property
        /// </summary>
        [XmlElementNameAttribute("referredRelation")]
        [XmlAttributeAttribute(true)]
        public virtual IRelation ReferredRelation
        {
            get
            {
                return this._referredRelation;
            }
            set
            {
                if ((this._referredRelation != value))
                {
                    this.OnReferredRelationChanging(EventArgs.Empty);
                    this.OnPropertyChanging("ReferredRelation");
                    IRelation old = this._referredRelation;
                    this._referredRelation = value;
                    if ((old != null))
                    {
                        old.Deleted -= this.OnResetReferredRelation;
                    }
                    if ((value != null))
                    {
                        value.Deleted += this.OnResetReferredRelation;
                    }
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnReferredRelationChanged(e);
                    this.OnPropertyChanged("ReferredRelation", e);
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
                return base.Children.Concat(new RelationCallExpChildrenCollection(this));
            }
        }
        
        /// <summary>
        /// Gets the referenced model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> ReferencedElements
        {
            get
            {
                return base.ReferencedElements.Concat(new RelationCallExpReferencedElementsCollection(this));
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
                    _classInstance = ((NMF.Models.Meta.IClass)(MetaRepository.Instance.Resolve("http://www.omg.org/spec/QVT/20140401/QVTRelation#//RelationCallExp/")));
                }
                return _classInstance;
            }
        }
        
        /// <summary>
        /// Gets fired before the ReferredRelation property changes its value
        /// </summary>
        public event EventHandler ReferredRelationChanging;
        
        /// <summary>
        /// Gets fired when the ReferredRelation property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> ReferredRelationChanged;
        
        /// <summary>
        /// Forwards CollectionChanging notifications for the Argument property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void ArgumentCollectionChanging(object sender, NMF.Collections.ObjectModel.NotifyCollectionChangingEventArgs e)
        {
            this.OnCollectionChanging("Argument", e);
        }
        
        /// <summary>
        /// Forwards CollectionChanged notifications for the Argument property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void ArgumentCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("Argument", e);
        }
        
        /// <summary>
        /// Raises the ReferredRelationChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnReferredRelationChanging(EventArgs eventArgs)
        {
            EventHandler handler = this.ReferredRelationChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the ReferredRelationChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnReferredRelationChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.ReferredRelationChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Handles the event that the ReferredRelation property must reset
        /// </summary>
        /// <param name="sender">The object that sent this reset request</param>
        /// <param name="eventArgs">The event data for the reset event</param>
        private void OnResetReferredRelation(object sender, System.EventArgs eventArgs)
        {
            this.ReferredRelation = null;
        }
        
        /// <summary>
        /// Gets the relative URI fragment for the given child model element
        /// </summary>
        /// <returns>A fragment of the relative URI</returns>
        /// <param name="element">The element that should be looked for</param>
        protected override string GetRelativePathForNonIdentifiedChild(IModelElement element)
        {
            int argumentIndex = ModelHelper.IndexOfReference(this.Argument, element);
            if ((argumentIndex != -1))
            {
                return ModelHelper.CreatePath("argument", argumentIndex);
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
            if ((reference == "ARGUMENT"))
            {
                if ((index < this.Argument.Count))
                {
                    return this.Argument[index];
                }
                else
                {
                    return null;
                }
            }
            return base.GetModelElementForReference(reference, index);
        }
        
        /// <summary>
        /// Gets the Model element collection for the given feature
        /// </summary>
        /// <returns>A non-generic list of elements</returns>
        /// <param name="feature">The requested feature</param>
        protected override System.Collections.IList GetCollectionForFeature(string feature)
        {
            if ((feature == "ARGUMENT"))
            {
                return this._argument;
            }
            return base.GetCollectionForFeature(feature);
        }
        
        /// <summary>
        /// Sets a value to the given feature
        /// </summary>
        /// <param name="feature">The requested feature</param>
        /// <param name="value">The value that should be set to that feature</param>
        protected override void SetFeature(string feature, object value)
        {
            if ((feature == "REFERREDRELATION"))
            {
                this.ReferredRelation = ((IRelation)(value));
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
            if ((attribute == "REFERREDRELATION"))
            {
                return new ReferredRelationProxy(this);
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
            if ((reference == "REFERREDRELATION"))
            {
                return new ReferredRelationProxy(this);
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
                _classInstance = ((NMF.Models.Meta.IClass)(MetaRepository.Instance.Resolve("http://www.omg.org/spec/QVT/20140401/QVTRelation#//RelationCallExp/")));
            }
            return _classInstance;
        }
        
        /// <summary>
        /// The collection class to to represent the children of the RelationCallExp class
        /// </summary>
        public class RelationCallExpChildrenCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private RelationCallExp _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public RelationCallExpChildrenCollection(RelationCallExp parent)
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
                    count = (count + this._parent.Argument.Count);
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.Argument.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.Argument.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                IOclExpression argumentCasted = item.As<IOclExpression>();
                if ((argumentCasted != null))
                {
                    this._parent.Argument.Add(argumentCasted);
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.Argument.Clear();
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if (this._parent.Argument.Contains(item))
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
                IEnumerator<IModelElement> argumentEnumerator = this._parent.Argument.GetEnumerator();
                try
                {
                    for (
                    ; argumentEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = argumentEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    argumentEnumerator.Dispose();
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                IOclExpression oclExpressionItem = item.As<IOclExpression>();
                if (((oclExpressionItem != null) 
                            && this._parent.Argument.Remove(oclExpressionItem)))
                {
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.Argument).GetEnumerator();
            }
        }
        
        /// <summary>
        /// The collection class to to represent the children of the RelationCallExp class
        /// </summary>
        public class RelationCallExpReferencedElementsCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private RelationCallExp _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public RelationCallExpReferencedElementsCollection(RelationCallExp parent)
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
                    count = (count + this._parent.Argument.Count);
                    if ((this._parent.ReferredRelation != null))
                    {
                        count = (count + 1);
                    }
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.Argument.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
                this._parent.ReferredRelationChanged += this.PropagateValueChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.Argument.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
                this._parent.ReferredRelationChanged -= this.PropagateValueChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                IOclExpression argumentCasted = item.As<IOclExpression>();
                if ((argumentCasted != null))
                {
                    this._parent.Argument.Add(argumentCasted);
                }
                if ((this._parent.ReferredRelation == null))
                {
                    IRelation referredRelationCasted = item.As<IRelation>();
                    if ((referredRelationCasted != null))
                    {
                        this._parent.ReferredRelation = referredRelationCasted;
                        return;
                    }
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.Argument.Clear();
                this._parent.ReferredRelation = null;
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if (this._parent.Argument.Contains(item))
                {
                    return true;
                }
                if ((item == this._parent.ReferredRelation))
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
                IEnumerator<IModelElement> argumentEnumerator = this._parent.Argument.GetEnumerator();
                try
                {
                    for (
                    ; argumentEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = argumentEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    argumentEnumerator.Dispose();
                }
                if ((this._parent.ReferredRelation != null))
                {
                    array[arrayIndex] = this._parent.ReferredRelation;
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
                IOclExpression oclExpressionItem = item.As<IOclExpression>();
                if (((oclExpressionItem != null) 
                            && this._parent.Argument.Remove(oclExpressionItem)))
                {
                    return true;
                }
                if ((this._parent.ReferredRelation == item))
                {
                    this._parent.ReferredRelation = null;
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.Argument).Concat(this._parent.ReferredRelation).GetEnumerator();
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the referredRelation property
        /// </summary>
        private sealed class ReferredRelationProxy : ModelPropertyChange<IRelationCallExp, IRelation>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public ReferredRelationProxy(IRelationCallExp modelElement) : 
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
                    return this.ModelElement.ReferredRelation;
                }
                set
                {
                    this.ModelElement.ReferredRelation = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.ReferredRelationChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.ReferredRelationChanged -= handler;
            }
        }
    }
}

