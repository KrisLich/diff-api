namespace DiffAPI.Models
{
    /// <summary>
    /// Model for details of a difference found between two data sets.
    /// </summary>
    public class DiffDetailModel
    {
        public int Offset { get; set; }
        public int Length { get; set; }
    }
}
