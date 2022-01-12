using Ninject;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.Photogram.Model.UserDao;
using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.Photogram.Model.CommentDao;
using Es.Udc.DotNet.Photogram.Model.Dtos;

namespace Es.Udc.DotNet.Photogram.Model.InteractionService
{
    public interface IInteractionService
    {
        [Inject]
        ICommentDao CommentDao { set; }
        [Inject]
        IUserDao UserDao { set; }
        [Inject]
        IImageDao ImageDao { set; }

        /// <summary>
        /// Posts a comment with a given text.
        /// </summary>
        /// <param name="userId">The user that posted the comment.</param>
        /// <param name="imgId">The image to which the comment was posted.</param>
        /// <param name="commentText">The comment.</param>
        [Transactional]
        Comment PostComment(string commentText, long userId, long imgId);

        /// <summary>
        /// Deletes a comment .
        /// </summary>
        /// <param name="commentId">Id of the comment you want to delete.</param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void DeleteComment(long commentId);

        /// <summary>
        /// Edit a comment with a new text.
        /// </summary>
        /// <param name="commentId">Id of the comment you want to edit.</param>
        /// <param name="newCommentText">The new comment.</param>
        [Transactional]
        Comment EditComment(long commentId, string newCommentText);

        /// <summary>
        /// Gets the comments on a specific image, paginated.
        /// </summary>
        /// <param name="imgId">Id of the image from which you want to get the comments.</param>
        /// <returns>The list of the comments posted in that image.</returns>
        [Transactional]
        Block<CommentInfo> GetImageComments(long imgId, int startIndex, int count);

        /// <summary>
        /// Likes an image.
        /// </summary>
        /// <param name="userId">The user that likes the image.</param>
        /// <param name="imgId">The image liked.</param>
        [Transactional]
        void LikeImage(long userId, long imgId);

        /// <summary>
        /// Unlikes an image .
        /// </summary>
        /// <param name="likeId">Id of the like you want to delete.</param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void Unlike(long userId, long imgId);

        /// <summary>
        /// Finds if an image was given a like by the user .
        /// </summary>
        /// <param name="imgId">Id of the image.</param>
        /// <param name="userId">Id of the user.</param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        bool AlreadyLiked(long userId, long imgId);

        /// <summary>
        /// Gets the number of likes on a specific image.
        /// </summary>
        /// <param name="imgId">Id of the image from which you want to get the number of likes.</param>
        /// <returns>The number of likes in that image.</returns>
        [Transactional]
        int GetImageLikes(long imgId);

        /// <summary>
        /// Finds if an user was followed by the user .
        /// </summary>
        /// <param name="followedId">Id of the user that is being followed.</param>
        /// <param name="followerId">Id of the user that may be following.</param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        bool Follows(long followedId, long followerId);

        /// <summary>
        /// Makes a user follow another user .
        /// </summary>
        /// <param name="followedId">Id of the user that is being followed.</param>
        /// <param name="followerId">Id of the user that will follow.</param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void Follow(long followedId, long followerId);


    }
}
