namespace HuynhNha_2122110299.Model
{
    public class TinTuc
    {
        public int Id { get; set; }
        public string TieuDe { get; set; } // Title
        public string NoiDung { get; set; } // Content
        public string TacGia { get; set; } // Author
        public DateTime NgayTao { get; set; } // Created Date
        public DateTime? NgaySua { get; set; } // Modified Date (Nullable)
    }
}
