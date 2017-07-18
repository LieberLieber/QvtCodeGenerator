//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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

namespace LL.MDE.Components.Qvt.Metamodel.EMOF
{
    
    
    /// <summary>
    /// The default implementation of the Package class
    /// </summary>
    [XmlNamespaceAttribute("http://www.omg.org/spec/QVT/20140401/EMOF")]
    [XmlNamespacePrefixAttribute("emof")]
    [DebuggerDisplayAttribute("Package {Name}")]
    public class Package : NamedElement, IPackage, IModelElement
    {
        
        /// <summary>
        /// The backing field for the Uri property
        /// </summary>
        private string _uri;
        
        /// <summary>
        /// The backing field for the NestedPackage property
        /// </summary>
        private PackageNestedPackageCollection _nestedPackage;
        
        /// <summary>
        /// The backing field for the OwnedType property
        /// </summary>
        private PackageOwnedTypeCollection _ownedType;
        
        private static NMF.Models.Meta.IClass _classInstance;
        
        public Package()
        {
            this._nestedPackage = new PackageNestedPackageCollection(this);
            this._nestedPackage.CollectionChanging += this.NestedPackageCollectionChanging;
            this._nestedPackage.CollectionChanged += this.NestedPackageCollectionChanged;
            this._ownedType = new PackageOwnedTypeCollection(this);
            this._ownedType.CollectionChanging += this.OwnedTypeCollectionChanging;
            this._ownedType.CollectionChanged += this.OwnedTypeCollectionChanged;
        }
        
        /// <summary>
        /// The uri property
        /// </summary>
        [XmlElementNameAttribute("uri")]
        [XmlAttributeAttribute(true)]
        public virtual string Uri
        {
            get
            {
                return this._uri;
            }
            set
            {
                if ((this._uri != value))
                {
                    this.OnUriChanging(EventArgs.Empty);
                    this.OnPropertyChanging("Uri");
                    string old = this._uri;
                    this._uri = value;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnUriChanged(e);
                    this.OnPropertyChanged("Uri", e);
                }
            }
        }
        
        /// <summary>
        /// The nestedPackage property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [XmlElementNameAttribute("nestedPackage")]
        [XmlAttributeAttribute(false)]
        [ContainmentAttribute()]
        [XmlOppositeAttribute("nestingPackage")]
        [ConstantAttribute()]
        public virtual ISetExpression<IPackage> NestedPackage
        {
            get
            {
                return this._nestedPackage;
            }
        }
        
        /// <summary>
        /// The nestingPackage property
        /// </summary>
        [XmlElementNameAttribute("nestingPackage")]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        [XmlAttributeAttribute(true)]
        [XmlOppositeAttribute("nestedPackage")]
        public virtual IPackage NestingPackage
        {
            get
            {
                return ModelHelper.CastAs<IPackage>(this.Parent);
            }
            set
            {
                this.Parent = value;
            }
        }
        
        /// <summary>
        /// The ownedType property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [XmlElementNameAttribute("ownedType")]
        [XmlAttributeAttribute(false)]
        [ContainmentAttribute()]
        [XmlOppositeAttribute("package")]
        [ConstantAttribute()]
        public virtual ISetExpression<IType> OwnedType
        {
            get
            {
                return this._ownedType;
            }
        }
        
        /// <summary>
        /// Gets the child model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> Children
        {
            get
            {
                return base.Children.Concat(new PackageChildrenCollection(this));
            }
        }
        
        /// <summary>
        /// Gets the referenced model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> ReferencedElements
        {
            get
            {
                return base.ReferencedElements.Concat(new PackageReferencedElementsCollection(this));
            }
        }
        
        /// <summary>
        /// Gets fired before the Uri property changes its value
        /// </summary>
        public event EventHandler UriChanging;
        
        /// <summary>
        /// Gets fired when the Uri property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> UriChanged;
        
        /// <summary>
        /// Gets fired when the NestingPackage property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> NestingPackageChanged;
        
        /// <summary>
        /// Raises the UriChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnUriChanging(EventArgs eventArgs)
        {
            EventHandler handler = this.UriChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the UriChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnUriChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.UriChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Forwards CollectionChanging notifications for the NestedPackage property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void NestedPackageCollectionChanging(object sender, NMF.Collections.ObjectModel.NotifyCollectionChangingEventArgs e)
        {
            this.OnCollectionChanging("NestedPackage", e);
        }
        
        /// <summary>
        /// Forwards CollectionChanged notifications for the NestedPackage property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void NestedPackageCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("NestedPackage", e);
        }
        
        /// <summary>
        /// Raises the NestingPackageChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnNestingPackageChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.NestingPackageChanged;
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
            IPackage oldNestingPackage = ModelHelper.CastAs<IPackage>(oldParent);
            IPackage newNestingPackage = ModelHelper.CastAs<IPackage>(newParent);
            if ((oldNestingPackage != null))
            {
                oldNestingPackage.NestedPackage.Remove(this);
            }
            if ((newNestingPackage != null))
            {
                newNestingPackage.NestedPackage.Add(this);
            }
            ValueChangedEventArgs e = new ValueChangedEventArgs(oldNestingPackage, newNestingPackage);
            this.OnNestingPackageChanged(e);
            this.OnPropertyChanged("NestingPackage", e);
        }
        
        /// <summary>
        /// Forwards CollectionChanging notifications for the OwnedType property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void OwnedTypeCollectionChanging(object sender, NMF.Collections.ObjectModel.NotifyCollectionChangingEventArgs e)
        {
            this.OnCollectionChanging("OwnedType", e);
        }
        
        /// <summary>
        /// Forwards CollectionChanged notifications for the OwnedType property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void OwnedTypeCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("OwnedType", e);
        }
        
        /// <summary>
        /// Resolves the given attribute name
        /// </summary>
        /// <returns>The attribute value or null if it could not be found</returns>
        /// <param name="attribute">The requested attribute name</param>
        /// <param name="index">The index of this attribute</param>
        protected override object GetAttributeValue(string attribute, int index)
        {
            if ((attribute == "URI"))
            {
                return this.Uri;
            }
            return base.GetAttributeValue(attribute, index);
        }
        
        /// <summary>
        /// Gets the Model element collection for the given feature
        /// </summary>
        /// <returns>A non-generic list of elements</returns>
        /// <param name="feature">The requested feature</param>
        protected override System.Collections.IList GetCollectionForFeature(string feature)
        {
            if ((feature == "NESTEDPACKAGE"))
            {
                return this._nestedPackage;
            }
            if ((feature == "OWNEDTYPE"))
            {
                return this._ownedType;
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
            if ((feature == "NESTINGPACKAGE"))
            {
                this.NestingPackage = ((IPackage)(value));
                return;
            }
            if ((feature == "URI"))
            {
                this.Uri = ((string)(value));
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
            if ((attribute == "NESTINGPACKAGE"))
            {
                return new NestingPackageProxy(this);
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
            if ((reference == "NESTINGPACKAGE"))
            {
                return new NestingPackageProxy(this);
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
        /// The collection class to to represent the children of the Package class
        /// </summary>
        public class PackageChildrenCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private Package _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public PackageChildrenCollection(Package parent)
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
                    count = (count + this._parent.NestedPackage.Count);
                    count = (count + this._parent.OwnedType.Count);
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.NestedPackage.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
                this._parent.OwnedType.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.NestedPackage.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
                this._parent.OwnedType.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                IPackage nestedPackageCasted = item.As<IPackage>();
                if ((nestedPackageCasted != null))
                {
                    this._parent.NestedPackage.Add(nestedPackageCasted);
                }
                IType ownedTypeCasted = item.As<IType>();
                if ((ownedTypeCasted != null))
                {
                    this._parent.OwnedType.Add(ownedTypeCasted);
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.NestedPackage.Clear();
                this._parent.OwnedType.Clear();
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if (this._parent.NestedPackage.Contains(item))
                {
                    return true;
                }
                if (this._parent.OwnedType.Contains(item))
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
                IEnumerator<IModelElement> nestedPackageEnumerator = this._parent.NestedPackage.GetEnumerator();
                try
                {
                    for (
                    ; nestedPackageEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = nestedPackageEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    nestedPackageEnumerator.Dispose();
                }
                IEnumerator<IModelElement> ownedTypeEnumerator = this._parent.OwnedType.GetEnumerator();
                try
                {
                    for (
                    ; ownedTypeEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = ownedTypeEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    ownedTypeEnumerator.Dispose();
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                IPackage packageItem = item.As<IPackage>();
                if (((packageItem != null) 
                            && this._parent.NestedPackage.Remove(packageItem)))
                {
                    return true;
                }
                IType typeItem = item.As<IType>();
                if (((typeItem != null) 
                            && this._parent.OwnedType.Remove(typeItem)))
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.NestedPackage).Concat(this._parent.OwnedType).GetEnumerator();
            }
        }
        
        /// <summary>
        /// The collection class to to represent the children of the Package class
        /// </summary>
        public class PackageReferencedElementsCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private Package _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public PackageReferencedElementsCollection(Package parent)
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
                    count = (count + this._parent.NestedPackage.Count);
                    if ((this._parent.NestingPackage != null))
                    {
                        count = (count + 1);
                    }
                    count = (count + this._parent.OwnedType.Count);
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.NestedPackage.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
                this._parent.NestingPackageChanged += this.PropagateValueChanges;
                this._parent.OwnedType.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.NestedPackage.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
                this._parent.NestingPackageChanged -= this.PropagateValueChanges;
                this._parent.OwnedType.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                IPackage nestedPackageCasted = item.As<IPackage>();
                if ((nestedPackageCasted != null))
                {
                    this._parent.NestedPackage.Add(nestedPackageCasted);
                }
                if ((this._parent.NestingPackage == null))
                {
                    IPackage nestingPackageCasted = item.As<IPackage>();
                    if ((nestingPackageCasted != null))
                    {
                        this._parent.NestingPackage = nestingPackageCasted;
                        return;
                    }
                }
                IType ownedTypeCasted = item.As<IType>();
                if ((ownedTypeCasted != null))
                {
                    this._parent.OwnedType.Add(ownedTypeCasted);
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.NestedPackage.Clear();
                this._parent.NestingPackage = null;
                this._parent.OwnedType.Clear();
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if (this._parent.NestedPackage.Contains(item))
                {
                    return true;
                }
                if ((item == this._parent.NestingPackage))
                {
                    return true;
                }
                if (this._parent.OwnedType.Contains(item))
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
                IEnumerator<IModelElement> nestedPackageEnumerator = this._parent.NestedPackage.GetEnumerator();
                try
                {
                    for (
                    ; nestedPackageEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = nestedPackageEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    nestedPackageEnumerator.Dispose();
                }
                if ((this._parent.NestingPackage != null))
                {
                    array[arrayIndex] = this._parent.NestingPackage;
                    arrayIndex = (arrayIndex + 1);
                }
                IEnumerator<IModelElement> ownedTypeEnumerator = this._parent.OwnedType.GetEnumerator();
                try
                {
                    for (
                    ; ownedTypeEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = ownedTypeEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    ownedTypeEnumerator.Dispose();
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                IPackage packageItem = item.As<IPackage>();
                if (((packageItem != null) 
                            && this._parent.NestedPackage.Remove(packageItem)))
                {
                    return true;
                }
                if ((this._parent.NestingPackage == item))
                {
                    this._parent.NestingPackage = null;
                    return true;
                }
                IType typeItem = item.As<IType>();
                if (((typeItem != null) 
                            && this._parent.OwnedType.Remove(typeItem)))
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.NestedPackage).Concat(this._parent.NestingPackage).Concat(this._parent.OwnedType).GetEnumerator();
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the uri property
        /// </summary>
        private sealed class UriProxy : ModelPropertyChange<IPackage, string>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public UriProxy(IPackage modelElement) : 
                    base(modelElement)
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override string Value
            {
                get
                {
                    return this.ModelElement.Uri;
                }
                set
                {
                    this.ModelElement.Uri = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.UriChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.UriChanged -= handler;
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the nestingPackage property
        /// </summary>
        private sealed class NestingPackageProxy : ModelPropertyChange<IPackage, IPackage>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public NestingPackageProxy(IPackage modelElement) : 
                    base(modelElement)
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override IPackage Value
            {
                get
                {
                    return this.ModelElement.NestingPackage;
                }
                set
                {
                    this.ModelElement.NestingPackage = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.NestingPackageChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.NestingPackageChanged -= handler;
            }
        }
    }
}

