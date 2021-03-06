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
    /// The default implementation of the Pattern class
    /// </summary>
    [XmlNamespaceAttribute("http://www.omg.org/spec/QVT/20140401/QVTBase")]
    [XmlNamespacePrefixAttribute("qvtbase")]
    public class Pattern : Element, IPattern, IModelElement
    {
        
        /// <summary>
        /// The backing field for the BindsTo property
        /// </summary>
        private ObservableAssociationSet<IVariable> _bindsTo;
        
        /// <summary>
        /// The backing field for the Predicate property
        /// </summary>
        private PatternPredicateCollection _predicate;
        
        private static NMF.Models.Meta.IClass _classInstance;
        
        public Pattern()
        {
            this._bindsTo = new ObservableAssociationSet<IVariable>();
            this._bindsTo.CollectionChanging += this.BindsToCollectionChanging;
            this._bindsTo.CollectionChanged += this.BindsToCollectionChanged;
            this._predicate = new PatternPredicateCollection(this);
            this._predicate.CollectionChanging += this.PredicateCollectionChanging;
            this._predicate.CollectionChanged += this.PredicateCollectionChanged;
        }
        
        /// <summary>
        /// The bindsTo property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [XmlElementNameAttribute("bindsTo")]
        [XmlAttributeAttribute(true)]
        [ConstantAttribute()]
        public virtual ISetExpression<IVariable> BindsTo
        {
            get
            {
                return this._bindsTo;
            }
        }
        
        /// <summary>
        /// The predicate property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [XmlElementNameAttribute("predicate")]
        [XmlAttributeAttribute(false)]
        [ContainmentAttribute()]
        [XmlOppositeAttribute("pattern")]
        [ConstantAttribute()]
        public virtual ISetExpression<IPredicate> Predicate
        {
            get
            {
                return this._predicate;
            }
        }
        
        /// <summary>
        /// Gets the child model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> Children
        {
            get
            {
                return base.Children.Concat(new PatternChildrenCollection(this));
            }
        }
        
        /// <summary>
        /// Gets the referenced model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> ReferencedElements
        {
            get
            {
                return base.ReferencedElements.Concat(new PatternReferencedElementsCollection(this));
            }
        }
        
        /// <summary>
        /// Forwards CollectionChanging notifications for the BindsTo property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void BindsToCollectionChanging(object sender, NMF.Collections.ObjectModel.NotifyCollectionChangingEventArgs e)
        {
            this.OnCollectionChanging("BindsTo", e);
        }
        
        /// <summary>
        /// Forwards CollectionChanged notifications for the BindsTo property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void BindsToCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("BindsTo", e);
        }
        
        /// <summary>
        /// Forwards CollectionChanging notifications for the Predicate property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void PredicateCollectionChanging(object sender, NMF.Collections.ObjectModel.NotifyCollectionChangingEventArgs e)
        {
            this.OnCollectionChanging("Predicate", e);
        }
        
        /// <summary>
        /// Forwards CollectionChanged notifications for the Predicate property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void PredicateCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("Predicate", e);
        }
        
        /// <summary>
        /// Gets the Model element collection for the given feature
        /// </summary>
        /// <returns>A non-generic list of elements</returns>
        /// <param name="feature">The requested feature</param>
        protected override System.Collections.IList GetCollectionForFeature(string feature)
        {
            if ((feature == "BINDSTO"))
            {
                return this._bindsTo;
            }
            if ((feature == "PREDICATE"))
            {
                return this._predicate;
            }
            return base.GetCollectionForFeature(feature);
        }
        
        /// <summary>
        /// Gets the Class for this model element
        /// </summary>
        public override NMF.Models.Meta.IClass GetClass()
        {
            throw new NotSupportedException();
        }
        
        /// <summary>
        /// The collection class to to represent the children of the Pattern class
        /// </summary>
        public class PatternChildrenCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private Pattern _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public PatternChildrenCollection(Pattern parent)
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
                    count = (count + this._parent.Predicate.Count);
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.Predicate.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.Predicate.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                IPredicate predicateCasted = item.As<IPredicate>();
                if ((predicateCasted != null))
                {
                    this._parent.Predicate.Add(predicateCasted);
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.Predicate.Clear();
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if (this._parent.Predicate.Contains(item))
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
                IEnumerator<IModelElement> predicateEnumerator = this._parent.Predicate.GetEnumerator();
                try
                {
                    for (
                    ; predicateEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = predicateEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    predicateEnumerator.Dispose();
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                IPredicate predicateItem = item.As<IPredicate>();
                if (((predicateItem != null) 
                            && this._parent.Predicate.Remove(predicateItem)))
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.Predicate).GetEnumerator();
            }
        }
        
        /// <summary>
        /// The collection class to to represent the children of the Pattern class
        /// </summary>
        public class PatternReferencedElementsCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private Pattern _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public PatternReferencedElementsCollection(Pattern parent)
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
                    count = (count + this._parent.BindsTo.Count);
                    count = (count + this._parent.Predicate.Count);
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.BindsTo.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
                this._parent.Predicate.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.BindsTo.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
                this._parent.Predicate.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                IVariable bindsToCasted = item.As<IVariable>();
                if ((bindsToCasted != null))
                {
                    this._parent.BindsTo.Add(bindsToCasted);
                }
                IPredicate predicateCasted = item.As<IPredicate>();
                if ((predicateCasted != null))
                {
                    this._parent.Predicate.Add(predicateCasted);
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.BindsTo.Clear();
                this._parent.Predicate.Clear();
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if (this._parent.BindsTo.Contains(item))
                {
                    return true;
                }
                if (this._parent.Predicate.Contains(item))
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
                IEnumerator<IModelElement> bindsToEnumerator = this._parent.BindsTo.GetEnumerator();
                try
                {
                    for (
                    ; bindsToEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = bindsToEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    bindsToEnumerator.Dispose();
                }
                IEnumerator<IModelElement> predicateEnumerator = this._parent.Predicate.GetEnumerator();
                try
                {
                    for (
                    ; predicateEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = predicateEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    predicateEnumerator.Dispose();
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                IVariable variableItem = item.As<IVariable>();
                if (((variableItem != null) 
                            && this._parent.BindsTo.Remove(variableItem)))
                {
                    return true;
                }
                IPredicate predicateItem = item.As<IPredicate>();
                if (((predicateItem != null) 
                            && this._parent.Predicate.Remove(predicateItem)))
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.BindsTo).Concat(this._parent.Predicate).GetEnumerator();
            }
        }
    }
}

