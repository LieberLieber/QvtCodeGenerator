using LL.MDE.Components.Qvt.Metamodel.QVTRelations;

namespace LL.MDE.Components.Qvt.QvtCodeGenerator.Templates
{
    public partial class TransformationTemplate
    {
        public RelationalTransformation transformation { get; }

        public TransformationTemplate(RelationalTransformation transformation)
        {
            this.transformation = transformation;
        }
        
    }
}