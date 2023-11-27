using DiffAPI.Models;

namespace DiffAPI.Services
{
    /// <summary>
    /// Interface for the diff service.
    /// </summary>
    public interface IDiffService
    {
        void UploadLeftData(string id, string data);

        void UploadRightData(string id, string data);

        DiffResultModel GetDiffResult(string id);
    }
}
