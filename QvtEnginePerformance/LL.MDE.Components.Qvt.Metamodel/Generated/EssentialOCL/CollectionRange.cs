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
    /// The default implementation of the CollectionRange class
    /// </summary>
    [XmlNamespaceAttribute("http://www.omg.org/spec/QVT/20140401/EssentialOCL")]
    [XmlNamespacePrefixAttribute("essentialocl")]
    [DebuggerDisplayAttribute("CollectionRange {Name}")]
    public class CollectionRange : CollectionLiteralPart, ICollectionRange, IModelElement
    {
        
        /// <summary>
        /// The backing field for the First property
        /// </summary>
        private IOclExpression _first;
        
        /// <summary>
        /// The backing field for the Last property
        /// </summary>
        private IOclExpression _last;
        
        private static NMF.Models.Meta.IClass _classInstance;
        
        /// <summary>
        /// The first property
        /// </summary>
        [XmlElementNameAttribute("first")]
        [XmlAttributeAttribute(false)]
        [ContainmentAttribute()]
        public virtual IOclExpression First
        {
            get
            {
                return this._first;
            }
            set
            {
                if ((this._first != value))
                {
                    this.OnFirstChanging(EventArgs.Empty);
                    this.OnPropertyChanging("First");
                    IOclExpression old = this._first;
                    this._first = value;
                    if ((old != null))
                    {
                        old.Parent = null;
                        old.Deleted -= this.OnResetFirst;
                    }
                    if ((value != null))
                    {
                        value.Parent = this;
                        value.Deleted += this.OnResetFirst;
                    }
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnFirstChanged(e);
                    this.OnPropertyChanged("First", e);
                }
            }
        }
        
        /// <summary>
        /// The last property
        /// </summary>
        [XmlElementNameAttribute("last")]
        [XmlAttributeAttribute(false)]
        [ContainmentAttribute()]
        public virtual IOclExpression Last
        {
            get
            {
                return this._last;
            }
            set
            {
                if ((this._last != value))
                {
                    this.OnLastChanging(EventArgs.Empty);
                    this.OnPropertyChanging("Last");
                    IOclExpression old = this._last;
                    this._last = value;
                    if ((old != null))
                    {
                        old.Parent = null;
                        old.Deleted -= this.OnResetLast;
                    }
                    if ((value != null))
                    {
                        value.Parent = this;
                        value.Deleted += this.OnResetLast;
                    }
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnLastChanged(e);
                    this.OnPropertyChanged("Last", e);
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
                return base.Children.Concat(new CollectionRangeChildrenCollection(this));
            }
        }
        
        /// <summary>
        /// Gets the referenced model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> ReferencedElements
        {
            get
            {
                return base.ReferencedElements.Concat(new CollectionRangeReferencedElementsCollection(this));
            }
        }
        
        /// <summary>
        /// Gets fired before the First property changes its value
        /// </summary>
        public event EventHandler FirstChanging;
        
        /// <summary>
        /// Gets fired when the First property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> FirstChanged;
        
        /// <summary>
        /// Gets fired before the Last property changes its value
        /// </summary>
        public event EventHandler LastChanging;
        
        /// <summary>
        /// Gets fired when the Last property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> LastChanged;
        
        /// <summary>
        /// Raises the FirstChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnFirstChanging(EventArgs eventArgs)
        {
            EventHandler handler = this.FirstChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the FirstChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnFirstChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.FirstChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Handles the event that the First property must reset
        /// </summary>
        /// <param name="sender">The object that sent this reset request</param>
        /// <param name="eventArgs">The event data for the reset event</param>
        private void OnResetFirst(object sender, System.EventArgs eventArgs)
        {
            this.First = null;
        }
        
        /// <summary>
        /// Raises the LastChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnLastChanging(EventArgs eventArgs)
        {
            EventHandler handler = this.LastChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the LastChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnLastChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.LastChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Handles the event that the Last property must reset
        /// </summary>
        /// <param name="sender">The object that sent this reset request</param>
        /// <param name="eventArgs">The event data for the reset event</param>
        private void OnResetLast(object sender, System.EventArgs eventArgs)
        {
            this.Last = null;
        }
        
        /// <summary>
        /// Gets the relative URI fragment for the given child model element
        /// </summary>
        /// <returns>A fragment of the relative URI</returns>
        /// <param name="element">The element that should be looked for</param>
        protected override string GetRelativePathForNonIdentifiedChild(IModelElement element)
        {
            if ((element == this.First))
            {
                return ModelHelper.CreatePath("First");
            }
            if ((element == this.Last))
            {
                return ModelHelper.CreatePath("Last");
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
            if ((reference == "FIRST"))
            {
                return this.First;
            }
            if ((reference == "LAST"))
            {
                return this.Last;
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
            if ((feature == "FIRST"))
            {
                this.First = ((IOclExpression)(value));
                return;
            }
            if ((feature == "LAST"))
            {
                this.Last = ((IOclExpression)(value));
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
            if ((attribute == "FIRST"))
            {
                return new FirstProxy(this);
            }
            if ((attribute == "LAST"))
            {
                return new LastProxy(this);
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
            if ((reference == "FIRST"))
            {
                return new FirstProxy(this);
            }
            if ((reference == "LAST"))
            {
                return new LastProxy(this);
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
        /// The collection class to to represent the children of the CollectionRange class
        /// </summary>
        public class CollectionRangeChildrenCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private CollectionRange _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public CollectionRangeChildrenCollection(CollectionRange parent)
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
                    if ((this._parent.First != null))
                    {
                        count = (count + 1);
                    }
                    if ((this._parent.Last != null))
                    {
                        count = (count + 1);
                    }
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.FirstChanged += this.PropagateValueChanges;
                this._parent.LastChanged += this.PropagateValueChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.FirstChanged -= this.PropagateValueChanges;
                this._parent.LastChanged -= this.PropagateValueChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                if ((this._parent.First == null))
                {
                    IOclExpression firstCasted = item.As<IOclExpression>();
                    if ((firstCasted != null))
                    {
                        this._parent.First = firstCasted;
                        return;
                    }
                }
                if ((this._parent.Last == null))
                {
                    IOclExpression lastCasted = item.As<IOclExpression>();
                    if ((lastCasted != null))
                    {
                        this._parent.Last = lastCasted;
                        return;
                    }
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.First = null;
                this._parent.Last = null;
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if ((item == this._parent.First))
                {
                    return true;
                }
                if ((item == this._parent.Last))
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
                if ((this._parent.First != null))
                {
                    array[arrayIndex] = this._parent.First;
                    arrayIndex = (arrayIndex + 1);
                }
                if ((this._parent.Last != null))
                {
                    array[arrayIndex] = this._parent.Last;
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
                if ((this._parent.First == item))
                {
                    this._parent.First = null;
                    return true;
                }
                if ((this._parent.Last == item))
                {
                    this._parent.Last = null;
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.First).Concat(this._parent.Last).GetEnumerator();
            }
        }
        
        /// <summary>
        /// The collection class to to represent the children of the CollectionRange class
        /// </summary>
        public class CollectionRangeReferencedElementsCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private CollectionRange _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public CollectionRangeReferencedElementsCollection(CollectionRange parent)
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
                    if ((this._parent.First != null))
                    {
                        count = (count + 1);
                    }
                    if ((this._parent.Last != null))
                    {
                        count = (count + 1);
                    }
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.FirstChanged += this.PropagateValueChanges;
                this._parent.LastChanged += this.PropagateValueChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.FirstChanged -= this.PropagateValueChanges;
                this._parent.LastChanged -= this.PropagateValueChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                if ((this._parent.First == null))
                {
                    IOclExpression firstCasted = item.As<IOclExpression>();
                    if ((firstCasted != null))
                    {
                        this._parent.First = firstCasted;
                        return;
                    }
                }
                if ((this._parent.Last == null))
                {
                    IOclExpression lastCasted = item.As<IOclExpression>();
                    if ((lastCasted != null))
                    {
                        this._parent.Last = lastCasted;
                        return;
                    }
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.First = null;
                this._parent.Last = null;
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if ((item == this._parent.First))
                {
                    return true;
                }
                if ((item == this._parent.Last))
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
                if ((this._parent.First != null))
                {
                    array[arrayIndex] = this._parent.First;
                    arrayIndex = (arrayIndex + 1);
                }
                if ((this._parent.Last != null))
                {
                    array[arrayIndex] = this._parent.Last;
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
                if ((this._parent.First == item))
                {
                    this._parent.First = null;
                    return true;
                }
                if ((this._parent.Last == item))
                {
                    this._parent.Last = null;
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.First).Concat(this._parent.Last).GetEnumerator();
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the first property
        /// </summary>
        private sealed class FirstProxy : ModelPropertyChange<ICollectionRange, IOclExpression>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public FirstProxy(ICollectionRange modelElement) : 
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
                    return this.ModelElement.First;
                }
                set
                {
                    this.ModelElement.First = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.FirstChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.FirstChanged -= handler;
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the last property
        /// </summary>
        private sealed class LastProxy : ModelPropertyChange<ICollectionRange, IOclExpression>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public LastProxy(ICollectionRange modelElement) : 
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
                    return this.ModelElement.Last;
                }
                set
                {
                    this.ModelElement.Last = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.LastChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.LastChanged -= handler;
            }
        }
    }
}
