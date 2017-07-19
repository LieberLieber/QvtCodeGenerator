namespace LL.MDE.Components.Qvt.Transformation.EA2FMEA
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using LL.MDE.Components.Qvt.Common;
	using LL.MDE.DataModels.EnAr;
	using LL.MDE.DataModels.XML;

	public class RelationEA2FMEA_Start
	{
		private readonly IMetaModelInterface editor;
		private readonly TransformationEA2FMEA transformation;

		private Dictionary<CheckOnlyDomains, EnforceDomains> traceabilityMap = new Dictionary<CheckOnlyDomains, EnforceDomains>();

		public RelationEA2FMEA_Start(IMetaModelInterface editor , TransformationEA2FMEA transformation )
		{
			this.editor = editor;this.transformation = transformation;
		}

		public void CheckAndEnforce(LL.MDE.DataModels.XML.XMLFile fmeaFile,LL.MDE.DataModels.EnAr.Package alP )
		{
			CheckOnlyDomains input = new CheckOnlyDomains(alP);
			EnforceDomains output = new EnforceDomains(fmeaFile);
			if (traceabilityMap.ContainsKey(input) && !traceabilityMap[input].Equals(output))
			{
				throw new Exception("This relation has already been used with different enforced parameters!");
			}
			if (!traceabilityMap.ContainsKey(input))
			{
				ISet<CheckResultEA2FMEA_Start> result = Check (alP);
				Enforce(result, fmeaFile);
				traceabilityMap[input] = output;
			}
		}

		public EnforceDomains FindPreviousResult(LL.MDE.DataModels.EnAr.Package alP)
		{
			CheckOnlyDomains input = new CheckOnlyDomains(alP);
			return traceabilityMap.ContainsKey(input) ? traceabilityMap[input] : null;
		}

		internal static ISet<CheckResultEA2FMEA_Start> Check( LL.MDE.DataModels.EnAr.Package alP )
		{
			ISet<CheckResultEA2FMEA_Start> result = new HashSet<CheckResultEA2FMEA_Start>();ISet<MatchDomainAlP> matchDomainAlPs = CheckDomainAlP(alP);foreach (MatchDomainAlP matchDomainAlP in matchDomainAlPs ) {LL.MDE.DataModels.EnAr.Element alPE = matchDomainAlP.alPE;
			LL.MDE.DataModels.EnAr.Package projectP = matchDomainAlP.projectP;
			string projectName = matchDomainAlP.projectName;
			LL.MDE.DataModels.EnAr.Element projectPE = matchDomainAlP.projectPE;
			CheckResultEA2FMEA_Start checkonlysMatch = new CheckResultEA2FMEA_Start () {matchDomainAlP = matchDomainAlP,};
			result.Add(checkonlysMatch);			} // End foreach
				return result;
		}

		internal static ISet<MatchDomainAlP> CheckDomainAlP(LL.MDE.DataModels.EnAr.Package alP)
		{
			ISet<MatchDomainAlP> result = new HashSet<MatchDomainAlP>();
			LL.MDE.DataModels.EnAr.Element alPE = alP.Element;
			LL.MDE.DataModels.EnAr.Package projectP = alP.ParentPackage();
			if (alPE.Stereotype == "abstraction level" && alPE.ElementPackage() == alP) {
			string projectName = projectP.Name;
			LL.MDE.DataModels.EnAr.Element projectPE = projectP.Element;
			if (projectPE.Stereotype == "fctsys" && projectPE.ElementPackage() == projectP) {
			MatchDomainAlP match = new MatchDomainAlP() {
			alP = alP,
			alPE = alPE,
			projectP = projectP,
			projectName = projectName,
			projectPE = projectPE,
			};
			result.Add(match);
			}}
			return result;
		}

		internal void Enforce(ISet<CheckResultEA2FMEA_Start> result, LL.MDE.DataModels.XML.XMLFile fmeaFile )
		{
			foreach (CheckResultEA2FMEA_Start match in result)
			{
			// Extracting variables binded in source domains
			LL.MDE.DataModels.EnAr.Package alP = match.matchDomainAlP.alP;
			LL.MDE.DataModels.EnAr.Element alPE = match.matchDomainAlP.alPE;
			LL.MDE.DataModels.EnAr.Package projectP = match.matchDomainAlP.projectP;
			string projectName = match.matchDomainAlP.projectName;
			LL.MDE.DataModels.EnAr.Element projectPE = match.matchDomainAlP.projectPE;

			// Assigning variables binded in the where clause

			// Enforcing each enforced domain
			MatchDomainFmeaFile targetMatchDomainFmeaFile = EnforceFmeaFile(projectName,  fmeaFile );

			// Retrieving variables binded in the enforced domains
			LL.MDE.DataModels.XML.Tag msrfmea = targetMatchDomainFmeaFile.msrfmea;
			LL.MDE.DataModels.XML.Tag fmProjectsTag = targetMatchDomainFmeaFile.fmProjectsTag;
			LL.MDE.DataModels.XML.Tag fmProjectTag = targetMatchDomainFmeaFile.fmProjectTag;
			LL.MDE.DataModels.XML.Tag fmStructureRefs = targetMatchDomainFmeaFile.fmStructureRefs;
			LL.MDE.DataModels.XML.Tag longName1 = targetMatchDomainFmeaFile.longName1;
			LL.MDE.DataModels.XML.Tag l41 = targetMatchDomainFmeaFile.l41;
			LL.MDE.DataModels.XML.Attribute lAttr1 = targetMatchDomainFmeaFile.lAttr1;
			LL.MDE.DataModels.XML.Tag fmStructureElementsTag = targetMatchDomainFmeaFile.fmStructureElementsTag;
			LL.MDE.DataModels.XML.Tag fmStructures = targetMatchDomainFmeaFile.fmStructures;

			// Calling other relations as defined in the where clause
			new RelationProduct2Structure(editor,transformation).CheckAndEnforce(alP,fmStructureRefs,fmStructureElementsTag,fmStructures);}
		}

		internal MatchDomainFmeaFile EnforceFmeaFile(string projectName, LL.MDE.DataModels.XML.XMLFile fmeaFile)
		{
			MatchDomainFmeaFile match = new MatchDomainFmeaFile();

			// Contructing fmeaFile
			editor.AddOrSetInField(fmeaFile, "encoding", "ISO-8859-1" );
			editor.AddOrSetInField(fmeaFile, "filename", "C:\vectortest\fmeatest3.xml" );
			LL.MDE.DataModels.XML.Tag msrfmea = null;
			msrfmea =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(fmeaFile, "content");

			// Contructing msrfmea
			editor.AddOrSetInField(msrfmea, "tagname", "MSRFMEA" );
			LL.MDE.DataModels.XML.Tag fmProjectsTag = null;
			fmProjectsTag =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(msrfmea, "childTags");

			LL.MDE.DataModels.XML.Tag fmStructureElementsTag = null;
			fmStructureElementsTag =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(msrfmea, "childTags");

			LL.MDE.DataModels.XML.Tag fmStructures = null;
			fmStructures =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(msrfmea, "childTags");

			// Contructing fmProjectsTag
			editor.AddOrSetInField(fmProjectsTag, "tagname", "FM-PROJECTS" );
			LL.MDE.DataModels.XML.Tag fmProjectTag = null;
			fmProjectTag =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(fmProjectsTag, "childTags");

			// Contructing fmProjectTag
			editor.AddOrSetInField(fmProjectTag, "tagname", "FM-PROJECT" );
			LL.MDE.DataModels.XML.Tag fmStructureRefs = null;
			fmStructureRefs =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(fmProjectTag, "childTags");

			LL.MDE.DataModels.XML.Tag longName1 = null;
			longName1 =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(fmProjectTag, "childTags");

			// Contructing fmStructureRefs
			editor.AddOrSetInField(fmStructureRefs, "tagname", "FM-STRUCTURE-REFS" );

			// Contructing longName1
			editor.AddOrSetInField(longName1, "tagname", "LONG-NAME" );
			LL.MDE.DataModels.XML.Tag l41 = null;
			l41 =  (LL.MDE.DataModels.XML.Tag) editor.CreateNewObjectInField(longName1, "childTags");

			// Contructing l41
			editor.AddOrSetInField(l41, "tagname", "L-4" );
			editor.AddOrSetInField(l41, "value", projectName );
			LL.MDE.DataModels.XML.Attribute lAttr1 = null;
			lAttr1 =  (LL.MDE.DataModels.XML.Attribute) editor.CreateNewObjectInField(l41, "attributes");

			// Contructing lAttr1
			editor.AddOrSetInField(lAttr1, "name", "L" );

			// Contructing fmStructureElementsTag
			editor.AddOrSetInField(fmStructureElementsTag, "tagname", "FM-STRUCTURE-ELEMENTS" );

			// Contructing fmStructures
			editor.AddOrSetInField(fmStructures, "tagname", "FM-STRUCTURES" );

				// Return newly binded variables
								match.fmeaFile  = fmeaFile;
								match.msrfmea  = msrfmea;
								match.fmProjectsTag  = fmProjectsTag;
								match.fmProjectTag  = fmProjectTag;
								match.fmStructureRefs  = fmStructureRefs;
								match.longName1  = longName1;
								match.l41  = l41;
								match.lAttr1  = lAttr1;
								match.fmStructureElementsTag  = fmStructureElementsTag;
								match.fmStructures  = fmStructures;
				return match;
		}

		public class CheckOnlyDomains : Tuple<Package>
		{
			public CheckOnlyDomains(Package alP)
				: base(alP)
			{
			}

			public Package alP
			{
				get { return Item1; }
			}
		}

		public class EnforceDomains : Tuple<XMLFile>
		{
			public EnforceDomains(XMLFile fmeaFile)
				: base(fmeaFile)
			{
			}

			public XMLFile fmeaFile
			{
				get { return Item1; }
			}
		}

		internal class CheckResultEA2FMEA_Start
		{
			public MatchDomainAlP matchDomainAlP;
		}

		internal class MatchDomainAlP
		{
			public LL.MDE.DataModels.EnAr.Package alP;
			public LL.MDE.DataModels.EnAr.Element alPE;
			public string projectName;
			public LL.MDE.DataModels.EnAr.Package projectP;
			public LL.MDE.DataModels.EnAr.Element projectPE;
		}

		internal class MatchDomainFmeaFile
		{
			public LL.MDE.DataModels.XML.XMLFile fmeaFile;
			public LL.MDE.DataModels.XML.Tag fmProjectsTag;
			public LL.MDE.DataModels.XML.Tag fmProjectTag;
			public LL.MDE.DataModels.XML.Tag fmStructureElementsTag;
			public LL.MDE.DataModels.XML.Tag fmStructureRefs;
			public LL.MDE.DataModels.XML.Tag fmStructures;
			public LL.MDE.DataModels.XML.Tag l41;
			public LL.MDE.DataModels.XML.Attribute lAttr1;
			public LL.MDE.DataModels.XML.Tag longName1;
			public LL.MDE.DataModels.XML.Tag msrfmea;
		}
	}
}