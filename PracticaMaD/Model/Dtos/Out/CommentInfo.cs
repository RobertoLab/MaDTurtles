namespace Es.Udc.DotNet.Photogram.Model.Dtos
{
    public class CommentInfo
    {
        public string comment { get; set; }
        public string uploadDate { get; set; }
        public string userName { get; set; }

        public CommentInfo(string comment, System.DateTime uploadDate, string userName)
        {
            this.comment = comment;
            this.uploadDate = uploadDate.ToString();
            this.userName = userName;
        }
        
    }
}
