using DiffAPI.Models;

namespace DiffAPI.Helpers
{
    /// <summary>
    /// Helper class for comparing data.
    /// </summary>
    public static class DiffHelper
    {
        /// <summary>
        /// Compares two strings and returns a model representing the differences between them.
        /// </summary>
        /// <param name="left">The 1st string to compare.</param>
        /// <param name="right">The 2nd string to compare.</param>
        /// <returns>A DiffResultModel representing the differences between the two strings. (offset and length)</returns>
        public static DiffResultModel CompareData(string left, string right)
        {
            if (left == null && right == null)
            {
                return new DiffResultModel { DiffResultType = DiffResultType.Equals };
            }

            if (left == null || right == null)
            {
                //_logger.LogError($"Data size does not match for ID {id}");
                return new DiffResultModel { DiffResultType = DiffResultType.SizeDoNotMatch };
            }

            if (left.Length != right.Length)
            {
                return new DiffResultModel { DiffResultType = DiffResultType.SizeDoNotMatch };
            }

            if (left.ToString().Equals(right.ToString()))
            {
                return new DiffResultModel { DiffResultType = DiffResultType.Equals };
            }

            // A simple diffing algorithm
            var diffs = new List<DiffDetailModel>();
            for (var i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                {
                    var diffLength = 1;
                    while (i + diffLength < left.Length && left[i + diffLength] != right[i + diffLength])
                    {
                        diffLength++;
                    }

                    diffs.Add(new DiffDetailModel { Offset = i, Length = diffLength });

                    i += diffLength - 1;
                }
            }

            return new DiffResultModel { DiffResultType = DiffResultType.ContentDoNotMatch, Diffs = diffs };
        }
    }
}
