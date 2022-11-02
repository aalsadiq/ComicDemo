namespace ComicBookAPI.Models
{
    public class ComicBook
    {
        public int Id {get; set;}
        public long UPC { set; get; }
        public string? Title { set; get; }
        public int Issue { set; get; }
        public string? Author { set; get; }
        public string? Artist { set; get; }
        //public Image? Cover { get; set; }
    }
}