namespace Songify.Simple.Dtos
{
    public class ArtistResourceParameters
    {    
        private int _pageSize = 10;
        private const int MaxPageSize = 20;
        public string SearchQuery { get; set; }
        public int PageNumber { get; set; } = 1;
        public bool? IsActive { get; set; }

        public int PageSize
        {
            get => _pageSize; 
            // Todo: Add checking negative value
            set => _pageSize = (value < MaxPageSize) ? value : MaxPageSize;
        }
    }
}