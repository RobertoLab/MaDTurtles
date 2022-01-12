using System;
using System.Collections.Generic;
using Ninject;
using Es.Udc.DotNet.Photogram.Model.CommentDao;
using Es.Udc.DotNet.Photogram.Model.Dtos;
using Es.Udc.DotNet.Photogram.Model.UserDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.Photogram.Model.ImageDao;
using static Es.Udc.DotNet.Photogram.Model.Dtos.CommentConversor;

namespace Es.Udc.DotNet.Photogram.Model.InteractionService
{
    public class InteractionService : IInteractionService
    {
        public InteractionService() { }

        [Inject]
        public ICommentDao CommentDao { private get; set; }
        [Inject]
        public IUserDao UserDao { private get; set; }
        [Inject]
        public IImageDao ImageDao { private get; set; }


        [Transactional]
        public Comment PostComment(string commentText, long userId, long imgId)
        {
            Comment comment = new Comment();
            comment.imgId = imgId;
            comment.uploadDate = DateTime.Now;
            comment.userId = userId;
            comment.comment = commentText;
            CommentDao.Create(comment);
            return comment;
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void DeleteComment(long commentId)
        {
            Comment comment = CommentDao.Find(commentId);

            CommentDao.Remove(commentId);
        }

        [Transactional]
        public Comment EditComment(long commentId, string newCommentText)
        {
            Comment comment = CommentDao.Find(commentId);
            comment.comment = newCommentText;
            comment.uploadDate = DateTime.Now;
            CommentDao.Update(comment);
            return comment;
        }

        [Transactional]
        public Block<CommentInfo> GetImageComments(long imgId, int startIndex, int count)
        {
            List<Comment> commentsFound = CommentDao.GetCommentsFromImage(imgId, startIndex, count + 1);
            bool existsMoreComments = (commentsFound.Count == count + 1);

            if (existsMoreComments) commentsFound.RemoveAt(count);

            return new Block<CommentInfo>(ToCommentInfos(commentsFound), existsMoreComments);
        }

        [Transactional]
        public void LikeImage(long userId, long imgId)
        {
            Image imageToLike = ImageDao.Find(imgId);
            User user = UserDao.Find(userId);

            ICollection<Image> imagesLiked = user.ImagesLiked;
            if (!imagesLiked.Contains(imageToLike))
            {
                imagesLiked.Add(imageToLike);
                user.ImagesLiked = imagesLiked;
                UserDao.UpdateImagesLiked(user);
            }
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void Unlike(long userId, long imgId)
        {
            Image imageToLike = ImageDao.Find(imgId);
            User user = UserDao.Find(userId);

            ICollection<Image> imagesLiked = user.ImagesLiked;
            if (imagesLiked.Contains(imageToLike))
            {
                imagesLiked.Remove(imageToLike);
                user.ImagesLiked = imagesLiked;
                UserDao.UpdateImagesLiked(user);
            }
        }

        [Transactional]
        public bool AlreadyLiked(long userId, long imgId)
        {
            Image imageToLike = ImageDao.Find(imgId);
            User user = UserDao.Find(userId);

            ICollection<Image> imagesLiked = user.ImagesLiked;
            if (imagesLiked.Contains(imageToLike))
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        [Transactional]
        public int GetImageLikes(long imgId)
        {
            return ImageDao.FindWithRelatedInfo(imgId).UsersLikes.Count;
        }
        [Transactional]
        public bool Commented(long imgId)
        {
            Image image = ImageDao.Find(imgId);
            if (image.Comments.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        [Transactional]
        public bool Follows(long followedId, long followerId)
        {
            User followed = UserDao.Find(followedId);
            User follower = UserDao.Find(followerId);
            ICollection<User> usersFollowed = follower.UserFollow;
            if (usersFollowed.Contains(followed))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [Transactional]
        public void Follow(long followedId, long followerId)
        {
            User followed = UserDao.Find(followedId);
            User follower = UserDao.Find(followerId);
            ICollection<User> usersFollowed = follower.UserFollow;
            if (!usersFollowed.Contains(followed))
            {
                usersFollowed.Add(followed);
                follower.UserFollow = usersFollowed;
                UserDao.UpdateUserFollow(follower);
            }
        }
    }
}
