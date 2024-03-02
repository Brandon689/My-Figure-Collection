namespace SmartUI
{
    public class MFCItem
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Category { get; set; }
        public string Classification { get; set; }
        public string Origin { get; set; }
        public string Character { get; set; }
        public string Company { get; set; }
        public string Artists { get; set; }
        public string Materials { get; set; }
        public string ScaleDimensions { get; set; }
        public string ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public long JAN { get; set; }
        public string Version { get; set; }

        public string JCategory { get; set; }
        public string JClassification { get; set; }
        public string JOrigin { get; set; }
        public string JCharacter { get; set; }
        public string JCompany { get; set; }
        public string JArtists { get; set; }

        public int MFCId { get; set; }
        public int Hits { get; set; }
        public int Comments { get; set; }
        public int Likes { get; set; }
        public double AverageRating { get; set; }
        public int TimesRated { get; set; }
        public int OrderedBy { get; set; }
        public int OwnedBy { get; set; }
        public int WishedBy { get; set; }
        public int ListedBy { get; set; }
        public int SoldBy { get; set; }
        public int HuntedBy { get; set; }
        public int ReviewedBy { get; set; }
        public int MentionedIn { get; set; }
        public int Top100Owned { get; set; }
        public int Top100Ordered { get; set; }
        public int Top100Wished { get; set; }
    }
}
