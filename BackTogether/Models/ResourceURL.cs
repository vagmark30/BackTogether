namespace BackTogether.Models {
    public class ResourceURL {
        public string Id {
            get => Id;
            set {
                Id = Guid.NewGuid().ToString();
            }
        }
        public string Url { get; set; }
        public Enums.Resource type { get; set; }
    }
}