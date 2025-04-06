using MedicalServices.Models;

namespace MedicalServices.ViewModel
{
    public class PaginationVm<T>
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; }
        public int TotalRecords { get; set; }
        public T Collections { get; set; }
        public PaginationVm()
        {
            
        }
        public PaginationVm(int _PageSize,int _CurrentPage,List<T> _Items)
        {
            PageSize = _PageSize;
            CurrentPage = _CurrentPage;
            Items = _Items;
            TotalRecords = _Items.Count();
            TotalPages = (int)Math.Ceiling((decimal)TotalRecords / (decimal)PageSize);
            Items = Items.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
        }

    }
}
