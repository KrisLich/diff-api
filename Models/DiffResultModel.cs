namespace DiffAPI.Models
{

    /// <summary>
    /// Static class for all the diff result types.
    /// </summary>
    public static class DiffResultType
    {
        public static string Equals => "Equals";
        public static string SizeDoNotMatch => "SizeDoNotMatch";
        public static string ContentDoNotMatch => "ContentDoNotMatch";
    }


    /// <summary>
    /// Model for the result of a diff operation, that will be sent back.
    /// </summary>
    public class DiffResultModel
    {
        public string DiffResultType { get; set; }
        public List<DiffDetailModel> Diffs { get; set; }
    }
}
