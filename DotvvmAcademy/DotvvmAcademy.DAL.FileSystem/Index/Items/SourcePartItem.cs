using DotvvmAcademy.DAL.Base.Entities;

namespace DotvvmAcademy.DAL.FileSystem.Index.Items
{
    public class SourcePartItem : IndexItemBase<ISourcePart>
    {
        public int ArrayIndex { get; set; }
    }
}