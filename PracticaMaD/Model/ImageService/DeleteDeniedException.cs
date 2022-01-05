using System;

namespace Es.Udc.DotNet.Photogram.Model.ImageService
{
    /// <summary>
    /// This exception is raised if the image category to search for
    /// does not exist.
    /// </summary>
    public class DeleteDeniedException : Exception
    {
        private readonly long userId;
        private readonly long imgId;

        #region Properties Region

        public long UserId
        {
            get { return userId; }
        }
        public long ImageId
        {
            get { return imgId; }
        }
        #endregion Properties Region

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="DeleteDeniedException"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="imgId">The image identifier.</param>
        public DeleteDeniedException(long userId,
            long imgId)
            : base("User cannot delete image => " +
            "user = " + userId + " | " +
            "image = " + imgId)
        {
            this.imgId = imgId;
            this.userId = userId;
        }
    }
}
