///////////////////////////////////////////////////////////
//  Table.cs
//  Implementation of the Class Table
//  Generated by Enterprise Architect
//  Created on:      13-Okt-2016 13:48:10
//  Original author: oalt
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



using LL.MDE.DataModels.SimpleRDBMS;
namespace LL.MDE.DataModels.SimpleRDBMS {
	public class Table : RModelElement
	{

		public Schema schema;
		public List<Column> column = new List<Column>();
		public List<ForeignKey> foreignKey;
		public List<Key> key = new List<Key>();

		public Table(){

		}

	

	}//end Table

}//end namespace SimpleRDBMS