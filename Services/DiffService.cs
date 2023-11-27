using DiffAPI.Helpers;
using DiffAPI.Models;

namespace DiffAPI.Services
{
    public class DiffService : IDiffService
    {
        /// <summary>
        /// The data store where the data sets are stored.
        /// </summary>
        public Dictionary<string, (string left, string right)> DataStore => _dataStore;
        private readonly Dictionary<string, (string left, string right)> _dataStore = new Dictionary<string, (string, string)>();

        /// <summary>
        /// Uploads left data to be diffed.
        /// </summary>
        /// <param name="id">The ID</param>
        /// <param name="data">The data</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void UploadLeftData(string id, string data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (!_dataStore.ContainsKey(id))
            {
                _dataStore[id] = (data, null);
            }
            else
            {
                _dataStore[id] = (data, _dataStore[id].right);
            }
        }

        /// <summary>
        /// Uploads right data to be diffed.
        /// </summary>
        /// <param name="id">The ID</param>
        /// <param name="data">The data</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void UploadRightData(string id, string data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (!_dataStore.ContainsKey(id))
            {
                _dataStore[id] = (null, data);
            }
            else
            {
                _dataStore[id] = (_dataStore[id].left, data);
            }
        }

        /// <summary>
        /// Gets the result of the diff operation for a data set.
        /// </summary>
        /// <param name="id">The ID of the data set. (pair)</param>
        /// <returns>A DiffResultModel representing the result of the diff operation.</returns>
        public DiffResultModel GetDiffResult(string id)
        {
            if (_dataStore.ContainsKey(id))
            {
                var (left, right) = _dataStore[id];
                return DiffHelper.CompareData(left, right);
            }

            return null;
        }
    }
}
