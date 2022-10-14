using System;
using System.Threading.Tasks;

namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataProductImageUpdate;

public interface ICheckForImageDownloadsAvailableService
{
    event EventHandler<bool> ProductImageUpdatesAvailable;

    Task CheckIfUpdatedImagesAvailableAsync(long updateDateTimeProductImage, string currentLoginUserName);
}