using DiffAPI.Models;

namespace DiffAPI.Services
{
    /// <summary>
    /// Interface for the diff service.
    /// </summary>
    public interface IDiffService
    {
        void UploadLeftData(string id, byte[] data);

        void UploadRightData(string id, byte[] data);

        DiffResultModel GetDiffResult(string id);
    }
}
