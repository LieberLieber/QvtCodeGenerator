using System.Collections.Generic;

namespace LL.MDE.Components.Qvt.Common
{
    public abstract class GeneratedTransformation
    {


        //public abstract bool CheckConformity();
       // public abstract List<Match> Check();
       // public abstract List<Match> EnforceTowards(string parameterName);
      //  public abstract void SetParam(string parameterName, ISet<object> model);
        
        /// <summary>
        /// API expected by Oliver
        /// </summary>
        /// <param name="topRelationName"></param>
        /// <param name="parameters"></param>
        public abstract void CallTopRelation(string topRelationName, List<object> parameters);

    }
}