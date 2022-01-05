using Ninject;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.Photogram.Model.CommentDao;
using Es.Udc.DotNet.Photogram.Model.Dtos;

namespace Es.Udc.DotNet.Photogram.Model.CommentService
{
    public interface ICommentService
    {
        [Inject]
        ICommentDao CommentDao { set; }

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

    }
}
