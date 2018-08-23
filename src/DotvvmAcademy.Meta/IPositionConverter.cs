namespace DotvvmAcademy.Meta
{
    public interface IPositionConverter
    {
        (int lineNumber, int column) ToCoords(int index);

        int ToIndex(int lineNumber, int column);
    }
}