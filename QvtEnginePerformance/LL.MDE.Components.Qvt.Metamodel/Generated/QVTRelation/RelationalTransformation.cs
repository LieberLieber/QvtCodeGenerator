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
    /// The default implementation of the RelationalTransformation class
    /// </summary>
    [XmlNamespaceAttribute("http://www.omg.org/spec/QVT/20140401/QVTRelation")]
    [XmlNamespacePrefixAttribute("qvtrelation")]
    [ModelRepresentationClassAttribute("http://www.omg.org/spec/QVT/20140401/QVTRelation#//RelationalTransformation/")]
    [DebuggerDisplayAttribute("RelationalTransformation {Name}")]
    public class RelationalTransformation : Transformation, IRelationalTransformation, IModelElement
    {
        
        /// <summary>
        /// The backing field for the OwnedKey property
        /// </summary>
        private RelationalTransformationOwnedKeyCollection _ownedKey;
        
        private static NMF.Models.Meta.IClass _classInstance;
        
        public RelationalTransformation()
        {
            this._ownedKey = new RelationalTransformationOwnedKeyCollection(this);
            this._ownedKey.CollectionChanging += this.OwnedKeyCollectionChanging;
            this._ownedKey.CollectionChanged += this.OwnedKeyCollectionChanged;
        }
        
        /// <summary>
        /// The ownedKey property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [XmlElementNameAttribute("ownedKey")]
        [XmlAttributeAttribute(false)]
        [ContainmentAttribute()]
        [XmlOppositeAttribute("transformation")]
        [ConstantAttribute()]
        public virtual ISetExpression<IKey> OwnedKey
        {
            get
            {
                return this._ownedKey;
            }
        }
        
        /// <summary>
        /// Gets the child model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> Children
        {
            get
            {
                return base.Children.Concat(new RelationalTransformationChildrenCollection(this));
            }
        }
        
        /// <summary>
        /// Gets the referenced model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> ReferencedElements
        {
            get
            {
                return base.ReferencedElements.Concat(new RelationalTransformationReferencedElementsCollection(this));
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
                    _classInstance = ((NMF.Models.Meta.IClass)(MetaRepository.Instance.Resolve("http://www.omg.org/spec/QVT/20140401/QVTRelation#//RelationalTransformation/")));
                }
                return _classInstance;
            }
        }
        
        /// <summary>
        /// Forwards CollectionChanging notifications for the OwnedKey property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void OwnedKeyCollectionChanging(object sender, NMF.Collections.ObjectModel.NotifyCollectionChangingEventArgs e)
        {
            this.OnCollectionChanging("OwnedKey", e);
        }
        
        /// <summary>
        /// Forwards CollectionChanged notifications for the OwnedKey property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void OwnedKeyCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("OwnedKey", e);
        }
        
        /// <summary>
        /// Gets the Model element collection for the given feature
        /// </summary>
        /// <returns>A non-generic list of elements</returns>
        /// <param name="feature">The requested feature</param>
        protected override System.Collections.IList GetCollectionForFeature(string feature)
        {
            if ((feature == "OWNEDKEY"))
            {
                return this._ownedKey;
            }
            return base.GetCollectionForFeature(feature);
        }
        
        /// <summary>
        /// Gets the Class for this model element
        /// </summary>
        public override NMF.Models.Meta.IClass GetClass()
        {
            if ((_classInstance == null))
            {
                _classInstance = ((NMF.Models.Meta.IClass)(MetaRepository.Instance.Resolve("http://www.omg.org/spec/QVT/20140401/QVTRelation#//RelationalTransformation/")));
            }
            return _classInstance;
        }
        
        /// <summary>
        /// The collection class to to represent the children of the RelationalTransformation class
        /// </summary>
        public class RelationalTransformationChildrenCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private RelationalTransformation _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public RelationalTransformationChildrenCollection(RelationalTransformation parent)
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
                    count = (count + this._parent.OwnedKey.Count);
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.OwnedKey.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.OwnedKey.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                IKey ownedKeyCasted = item.As<IKey>();
                if ((ownedKeyCasted != null))
                {
                    this._parent.OwnedKey.Add(ownedKeyCasted);
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.OwnedKey.Clear();
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if (this._parent.OwnedKey.Contains(item))
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
                IEnumerator<IModelElement> ownedKeyEnumerator = this._parent.OwnedKey.GetEnumerator();
                try
                {
                    for (
                    ; ownedKeyEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = ownedKeyEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    ownedKeyEnumerator.Dispose();
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                IKey keyItem = item.As<IKey>();
                if (((keyItem != null) 
                            && this._parent.OwnedKey.Remove(keyItem)))
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.OwnedKey).GetEnumerator();
            }
        }
        
        /// <summary>
        /// The collection class to to represent the children of the RelationalTransformation class
        /// </summary>
        public class RelationalTransformationReferencedElementsCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private RelationalTransformation _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public RelationalTransformationReferencedElementsCollection(RelationalTransformation parent)
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
                    count = (count + this._parent.OwnedKey.Count);
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.OwnedKey.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.OwnedKey.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                IKey ownedKeyCasted = item.As<IKey>();
                if ((ownedKeyCasted != null))
                {
                    this._parent.OwnedKey.Add(ownedKeyCasted);
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.OwnedKey.Clear();
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if (this._parent.OwnedKey.Contains(item))
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
                IEnumerator<IModelElement> ownedKeyEnumerator = this._parent.OwnedKey.GetEnumerator();
                try
                {
                    for (
                    ; ownedKeyEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = ownedKeyEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    ownedKeyEnumerator.Dispose();
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                IKey keyItem = item.As<IKey>();
                if (((keyItem != null) 
                            && this._parent.OwnedKey.Remove(keyItem)))
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.OwnedKey).GetEnumerator();
            }
        }
    }
}

