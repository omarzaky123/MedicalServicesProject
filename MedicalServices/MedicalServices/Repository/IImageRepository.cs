namespace MedicalServices.Repository
{
    public interface IImageRepository
    {
        Task<bool> SaveImageInWroot(IHasImage hasImage, IFormFile ImageFile, string Location = "Images/Imagesadmin/");
        void DeleteImageInWroot(string Image);
        Task<bool> UpdateImageInWroot(IHasImage hasImage, string Location = "Images/Imagesadmin/");
    }
    public interface IHasImage
    {
        public string Image { get; set; }
    }

}
