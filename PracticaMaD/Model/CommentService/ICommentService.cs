﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.Photogram.Model.CommentDao;

namespace Es.Udc.DotNet.Photogram.Model.CommentService
{
    interface ICommentService
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
        void PostComment(string commentText, long userId, long imgId);

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
        void EditComment(long commentId, string newCommentText);

        /// <summary>
        /// Gets the comments on a specific image.
        /// </summary>
        /// <param name="imgId">Id of the image from which you want to get the comments.</param>
        /// <returns>The list of the comments posted in that image.</returns>
        [Transactional]
        List<Comment> GetImageComments(long imgId);

    }
}